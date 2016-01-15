using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace web
{
    public partial class RegistryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResetErrorLabels();
        }

        /// <summary>
        /// Setzt alle Fehlermeldungen zurück.
        /// </summary>
        protected void ResetErrorLabels()
        {
            Label[] errorLabels = new Label[] {lblTitleError, lblErrorName, lblErrorPrename, lblErrorStreet, lblErrorStreetNr, lblErrorPLZ,
            lblErrorPlace, lblErrorPhone, lblErrorEmail, lblErrorPwdx1, lblErrorPwdx2};

            foreach (Label errorLabel in errorLabels)
            {
                errorLabel.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            InsertNewUserIfValidInputIsProvived();
        }

        /// <summary>
        /// Prüft alle Nutzereingaben auf Gültigkeit.
        /// Wenn alle Eingaben gültig sind und die angegeben E-Mail-Adresse noch nicht vorhanden ist, 
        /// wird der neue Benutzer in die Datenbank eingetragen.
        /// </summary>
        protected void InsertNewUserIfValidInputIsProvived()
        {
            clsUserFacade userFacade = new clsUserFacade();
            clsUser userToInsert = new bll.clsUser();
            bool userCanBeInsertedInDB = true;

            TextBox[] textBoxes = { txtBoxName, txtBoxVorname, txtBoxStraße, txtBoxHnr,
                txtBoxPLZ, txtBoxPlace, txtBoxPhone, txtBoxEmail, txtBoxPassword,
                txtBoxPasswordx2 };

            foreach (TextBox element in textBoxes)
            {
                bool error = CheckAndSetValidUserInput(element, userToInsert);
                userCanBeInsertedInDB = userCanBeInsertedInDB && !error;
            }

            if (userCanBeInsertedInDB)
            {
                if (!userFacade.UserInsert(userToInsert))
                {
                    lblErrorEmail.Text = "Fehler beim Anlegen des Benutzers. E-Mail-Adresse bereits im System vorhanden";
                }
                else
                {
                    Response.Redirect("login_page.aspx");
                }
            }
        }

        /// <summary>
        /// Überprüft die Eingaben des Benutzers auf Richtigkeit.
        /// Falls der Benutzer falsche Eingaben tätigt, wird eine passende Fehlermeldung angezeigt.
        /// </summary>
        /// <param name="textBoxToProve">Eingabefeld, das überprüft werden muss</param>
        /// <param name="userToInsert">Nutzer, dem Daten bei gültiger Eingabe zugeordnet werden</param>
        /// <returns>true, falls eine fehlerhafte Eingabe getätigt wurde</returns>
        protected bool CheckAndSetValidUserInput(TextBox textBoxToProve, clsUser userToInsert)
        {
            bool errorOccured = false;
            setIsActiveAndRole(userToInsert);

            errorOccured = HandleUserTitle(errorOccured, lblTitleError, "Bitte treffen Sie eine Auswahl!", userToInsert);

            switch (textBoxToProve.ID)
            {
                case "txtBoxName":
                    if (!(errorOccured = handleAlphaInput(txtBoxName, errorOccured, lblErrorName,
                        "Bitte geben Sie ihren Nachnamen ein!", "Nachnamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Name = txtBoxName.Text;
                    }
                    break;

                case "txtBoxVorname":
                    if (!(errorOccured = handleAlphaInput(txtBoxVorname, errorOccured, lblErrorPrename,
                        "Bitte geben Sie ihren Vornamen ein!", "Vornamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Prename = txtBoxVorname.Text;
                    }
                    break;

                case "txtBoxStraße":
                    if (!(errorOccured = handleAlphaInput(txtBoxStraße, errorOccured, lblErrorStreet,
                        "Bitte geben Sie eine Straße ein!", "Straßennamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Street = txtBoxStraße.Text;
                    }
                    break;

                case "txtBoxHnr":
                    if (!(errorOccured = HandleHnr(errorOccured, lblErrorStreetNr,
                        "Bitte geben Sie eine Hausnummer ein!", "Hausnummern haben das Format 26 bzw. 26b!")))
                    {
                        userToInsert.Nr = txtBoxHnr.Text;
                    }
                    break;

                case "txtBoxPLZ":
                    if (!(errorOccured = HandlePLZ(errorOccured, lblErrorPLZ,
                        "Bitte geben Sie eine Postleitzahl ein!", "Postleitzahlen sind fünfstellige Zahlen")))
                    {
                        userToInsert.Postcode = Convert.ToInt32(txtBoxPLZ.Text);
                    }
                    break;

                case "txtBoxPlace":
                    if (!(errorOccured = handleAlphaInput(txtBoxPlace, errorOccured, lblErrorPlace,
                        "Bitte geben sie einen Ort ein!", "Orte bestehen nur aus Buchstaben!")))
                    {
                        userToInsert.Place = txtBoxPlace.Text;
                    }
                    break;

                case "txtBoxPhone":
                    if (!(errorOccured = HandlePhoneNumber(errorOccured, lblErrorPhone,
                        "Bitte geben Sie eine Telefonnummer ein!", "Telefonnummer haben das Format 089/12345 oder 089 12345")))
                    {
                        userToInsert.Phone = txtBoxPhone.Text;
                    }
                    break;

                case "txtBoxEmail":
                    if (!(errorOccured = HandleEmail(errorOccured, lblErrorEmail,
                        "Bitte geben Sie eine E-Mail-Adresse ein!", "E-Mail-Adressen haben das folgende Format: user@domain.com")))
                    {
                        userToInsert.EMail = txtBoxEmail.Text;
                    }
                    break;

                case "txtBoxPassword":
                    errorOccured = HandleFirstPasswordInput(errorOccured, lblErrorPwdx1, "Bitte geben Sie ein Passwort ein!");
                    break;

                case "txtBoxPasswordx2":
                    if (!(errorOccured = HandleSecondPasswordInput(errorOccured, lblErrorPwdx2,
                        "Bitte geben Sie Ihr Passwort erneut ein!", "Bitte geben Sie zweimal das gleiche Passwort ein!")))
                        ;
                    {
                        MD5 md5Hash = MD5.Create();
                        userToInsert.Password = clsUser.CreateMD5Hash(md5Hash, txtBoxPasswordx2.Text);
                    }
                    break;
                default:
                    break;
            }
            return errorOccured;
        }

        /// <summary>
        /// Error-Handling für alle Nutzereingaben.
        /// </summary>
        /// <param name="errorOccured">gibt an, ob ein Fehler aufgetreten ist</param>
        /// <param name="errorLabel">Label, das im Falle eines Fehlers angezeigt wird</param>
        /// <param name="errorText">Meldung, die dem Label zugeordnet wird</param>
        protected void HandleError(out bool errorOccured, Label errorLabel, string errorText)
        {
            errorOccured = true;
            errorLabel.Text = errorText;
            errorLabel.Visible = true;
        }

        /// <summary>
        /// Aktiviert einen Benutzer und setzt die passende Rolle.
        /// </summary>
        protected void setIsActiveAndRole(clsUser userToInsert)
        {
            userToInsert.IsActive = true;
            userToInsert.Role = 3;
        }

        /// <summary>
        /// Error-Handling für ein Eingabefeld, in dem eine Eingabe erwartet wird, die nur Buchstaben enthält.
        /// </summary>
        /// <param name="textBox">das betroffene Eingabefeld</param>
        /// <param name="isErrorOccured">gibt an, ob ein Fehler aufgetreten ist</param>
        /// <param name="lblError">Error-Label, dass im Falle eines Fehlers angezeigt wird</param>
        /// <param name="emptyErrorText">Fehlertext für den Fall, dass der Nutzer nichts eingegeben hat</param>
        /// <param name="alphaErrorText">Fehlertext für den Fall, dass die Eingabe nicht nur aus Buchstaben besteht</param>
        /// <returns>true, falls bei der Zuordnung des Wertes ein Fehler aufgetreten ist</returns>
        protected bool handleAlphaInput(TextBox textBox, bool isErrorOccured, Label lblError, string emptyErrorText, string alphaErrorText)
        {
            if (textBox.Text.Equals(""))
            {
                HandleError(out isErrorOccured, lblError, emptyErrorText);
            }
            else if (!IsAlphaString(textBox.Text))
            {
                HandleError(out isErrorOccured, lblError, alphaErrorText);
            }
            return isErrorOccured;
        }

        protected bool HandleUserTitle(bool isErrorOccured, Label errorLabel, string errorText, clsUser userToInsert)
        {
            if (ddlTitle.SelectedItem.Text == " - " || ddlTitle.SelectedIndex == -1)
            {
                HandleError(out isErrorOccured, errorLabel, errorText);
            }
            else
            {
                userToInsert.Title = ddlTitle.SelectedItem.Text;
            }

            return isErrorOccured;
        }

        protected bool HandleHnr(bool isErrorOccured, Label errorLabel, string emptyErrorText, string hnrErrorText)
        {
            if (txtBoxHnr.Text.Equals(""))
            {
                HandleError(out isErrorOccured, errorLabel, emptyErrorText);
            }
            else if (!IsCorrectHouseNumber(txtBoxHnr.Text))
            {
                HandleError(out isErrorOccured, errorLabel, hnrErrorText);
            }
            return isErrorOccured;
        }

        protected bool HandlePLZ(bool isErrorOccured, Label errorLabel, string emptyErrorText, string plzErrorText)
        {
            if (txtBoxPLZ.Text.Equals(""))
            {
                HandleError(out isErrorOccured, errorLabel, emptyErrorText);
            }
            else if (!IsCorrectPostCode(txtBoxPLZ.Text))
            {
                HandleError(out isErrorOccured, errorLabel, plzErrorText);
            }
            return isErrorOccured;
        }

        protected bool HandlePhoneNumber(bool isErrorOccured, Label errorLabel, string emptyErrorText, string phoneErrorText)
        {
            if (txtBoxPhone.Text.Equals(""))
            {
                HandleError(out isErrorOccured, errorLabel, emptyErrorText);
            }
            else if (!IsCorrectPhoneNumber(txtBoxPhone.Text))
            {
                HandleError(out isErrorOccured, errorLabel, phoneErrorText);
            }
            return isErrorOccured;
        }

        protected bool HandleEmail(bool isErrorOccured, Label errorLabel, string emptyErrorText, string emailErrorText)
        {
            if (txtBoxEmail.Text.Equals(""))
            {
                HandleError(out isErrorOccured, errorLabel, emptyErrorText);
            }
            else if (!IsCorrectMailAdress(txtBoxEmail.Text))
            {
                HandleError(out isErrorOccured, errorLabel, emailErrorText);
            }
            return isErrorOccured;
        }

        protected bool HandleFirstPasswordInput(bool isErrorOccured, Label errorLabel, string emptyErrorText)
        {
            if (txtBoxPassword.Text.Equals(""))
            {
                HandleError(out isErrorOccured, errorLabel, emptyErrorText);
            }
            return isErrorOccured;
        }

        protected bool HandleSecondPasswordInput(bool isErrorOccured, Label errorLabel, string emptyErrorText, string notSameErrorText)
        {
            if (txtBoxPasswordx2.Text.Equals(""))
            {
                HandleError(out isErrorOccured, errorLabel, emptyErrorText);
            }
            else if (!txtBoxPassword.Text.Equals(txtBoxPasswordx2.Text))
            {
                HandleError(out isErrorOccured, errorLabel, notSameErrorText);
            }
            return isErrorOccured;
        }

        /// <summary>
        /// Prüft, ob die Eingabe eine gültige Postleitzahl ist.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>true, falls die Eingabe eine gültige Postleitzahl ist</returns>
        private bool IsCorrectPostCode(string input)
        {
            Regex template = new Regex(@"^\d{5}$");
            return template.IsMatch(input);
        }

        /// <summary>
        /// Prüft, ob die Eingabe eine gültige Telefonnummer ist.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>true, falls die Eingabe eine gültige Telefonnummer ist</returns>
        private bool IsCorrectPhoneNumber(string input)
        {
            Regex template = new Regex(@"^\d+(\s|\/|)\d+$");
            return template.IsMatch(input);
        }

        /// <summary>
        /// Prüft, ob die Eingabe eine gültige E-Mail-Adresse ist.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>true, falls die Eingabe eine gültige E-Mail-Adresse ist</returns>
        private bool IsCorrectMailAdress(string input)
        {
            Regex template = new Regex(@"^(\w|\d|\.|\-)+\@(\w|\d|\-)+\.\w{2,3}");
            return template.IsMatch(input);
        }

        /// <summary>
        /// Prüft, ob die Eingabe eine gültige Hausnummer ist.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>true, falls die Eingabe eine gültige Hausnummer ist</returns>
        private bool IsCorrectHouseNumber(string input)
        {
            Regex template = new Regex(@"^\d+(\w|)$");
            return template.IsMatch(input);
        }

        /// <summary>
        /// Prüft, ob die Eingabe nur aus Buchstaben besteht.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>true, falls die Eingabe nur aus Buchstaben besteht</returns>
        private bool IsAlphaString(string input)
        {
            Regex template = new Regex(@"^[A-Za-z_äÄöÖüÜß\s]+$");
            return template.IsMatch(input);
        }
    }
}
