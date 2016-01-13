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
        private clsUserFacade userFacade = new clsUserFacade();
        private bll.clsUser userToInsert = new bll.clsUser();
        private bool userCanBeInsertedInDB = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitleError.Visible = false;
            lblErrorName.Visible = false;
            lblErrorPrename.Visible = false;
        }

        /// <summary>
        /// User data is written in the database
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { txtBoxName, txtBoxVorname, txtBoxStraße, txtBoxHnr,
                txtBoxPLZ, txtBoxPlace, txtBoxPhone, txtBoxEmail, txtBoxPassword,
                txtBoxPasswordx2 };

            foreach (TextBox element in textBoxes)
            {
                bool error = checkAndSetValidInput(element);
                userCanBeInsertedInDB = userCanBeInsertedInDB && !error;
            }

            if (userCanBeInsertedInDB)
            {

                if (!userFacade.UserInsert(userToInsert))
                {
                    lblErrorEmail.Text = "Fehler beim Anlegen. E-Mail Adresse bereits vorhanden";
                }
                else
                {
                    Response.Redirect("login_page.aspx");
                }
            }
        }

        /// <summary>
        /// Error-Handling für Nutzereingaben.
        /// </summary>
        /// <param name="errorOccured">gibt an, ob ein Fehler aufgetreten ist</param>
        /// <param name="errorLabel">Label, das im Falle eines Fehlers angezeigt wird</param>
        /// <param name="errorText">Text, der dem Label zugeordnet wird</param>
        protected void handleError(out bool errorOccured, Label errorLabel, string errorText)
        {
            errorOccured = true;
            errorLabel.Text = errorText;
            errorLabel.Visible = true;
        }

        /// <summary>
        /// Error-Handling für ein Textfeld, in dem eine Eingabe erwartet wird, die nur Buchstaben enthält.
        /// </summary>
        /// <param name="textBox">die betroffene Textbox</param>
        /// <param name="isErrorOccured">gibt an, ob bei der Zuordnung des Wertes ein Fehler aufgetreten ist</param>
        /// <param name="lblError">Error-Label, dass im Falle eines Fehlers angezeigt wird</param>
        /// <param name="emptyErrorText">Fehlertext für den Fall, dass der Nutzer nichts eingegeben hat</param>
        /// <param name="alphaErrorText">Fehlertext für den Fall, dass die Eingabe nicht nur aus Buchstaben besteht</param>
        /// <returns>true, falls bei der Zuordnung des Wertes ein Fehler aufgetreten ist</returns>
        protected bool handleNeededAlphaInput(TextBox textBox, bool isErrorOccured, Label lblError, string emptyErrorText, string alphaErrorText)
        {
            if (textBox.Text.Equals(""))
            {
                handleError(out isErrorOccured, lblError, emptyErrorText);
            }
            else if (!IsAlphaString(textBox.Text))
            {
                handleError(out isErrorOccured, lblError, alphaErrorText);
            }
            
            return isErrorOccured;
        }

        /// <summary>
        /// Error-Handling für den Titel des Benutzers.
        /// </summary>
        /// <param name="isErrorOccured">gibt an, ob bei der Zuordnung des Wertes ein Fehler aufgetreten ist</param>
        /// <param name="errorLabel">Error-Label, das im Falle eines Fehlers angezeigt wird</param>
        /// <param name="errorText">Fehlertext für den Fall, dass kein Titel ausgewählt wird</param>
        /// <returns></returns>
        protected bool HandleUserTitle(bool isErrorOccured, Label errorLabel, string errorText)
        {
            if (ddlTitle.SelectedItem.Text == " - " || ddlTitle.SelectedIndex == -1)
            {
                handleError(out isErrorOccured, errorLabel, errorText);
            } else
            {
                userToInsert.Title = ddlTitle.SelectedItem.Text;
            }
          
            return isErrorOccured;
        }

        /// <summary>
        /// Aktiviert einen User und setzt die passende Rolle.
        /// </summary>
        protected void setIsActiveAndRole()
        {
            userToInsert.IsActive = true;
            userToInsert.Role = 3;
        }

        protected bool checkAndSetValidInput(TextBox stringToProve)
        {
            bool errorOccured = false;

            setIsActiveAndRole();
            errorOccured = HandleUserTitle(errorOccured, lblTitleError, "Bitte treffen Sie eine Auswahl!");

            switch (stringToProve.ID)
            {
                case "txtBoxName":
                    if(!(errorOccured = handleNeededAlphaInput(txtBoxName, errorOccured, lblErrorName,
                        "Bitte geben Sie ihren Nachnamen ein!", "Nachnamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Name = txtBoxName.Text;
                    }
                    
                    break;
                case "txtBoxVorname":
                    if(!(errorOccured = handleNeededAlphaInput(txtBoxVorname, errorOccured, lblErrorPrename,
                        "Bitte geben Sie ihren Vornamen ein!", "Vornamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Prename = txtBoxVorname.Text;
                    }
                    break;

                case "txtBoxStraße":
                    if(!(errorOccured = handleNeededAlphaInput(txtBoxStraße, errorOccured, lblErrorStreet,
                        "Bitte geben Sie eine Straße ein!", "Straßennamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Street = txtBoxStraße.Text;
                    }
                    break;

                case "txtBoxHnr":
                    if (txtBoxHnr.Text.Equals(""))
                    {
                        lblErrorStreet.Text = "Bitte geben Sie eine Hausnummer ein!";
                        errorOccured = true;
                    }
                    else if (!IsCorrectHouseNumber(txtBoxHnr.Text))
                    {
                        lblErrorStreet.Text = "Hausnummern haben das Format 26 (auch 26b)";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    { userToInsert.Nr = txtBoxHnr.Text; }
                    break;
                case "txtBoxPLZ":
                    if (txtBoxPLZ.Text.Equals(""))
                    {
                        lblErrorPlace.Text = "Bitte geben Sie eine Postleitzahl ein!";
                        errorOccured = true;
                    }
                    else if (!IsCorrectPostCode(txtBoxPLZ.Text))
                    {
                        lblErrorPlace.Text = "Postleitzahlen sind fünfstellige Zahlen!";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    { userToInsert.Postcode = Convert.ToInt32(txtBoxPLZ.Text); }
                    break;

                case "txtBoxPlace":
                    if (txtBoxPlace.Text.Equals(""))
                    {
                        lblErrorPlace.Text = "Bitte geben Sie einen Ort ein!";
                        errorOccured = true;
                    }
                    else if (!IsAlphaString(txtBoxPlace.Text))
                    {
                        lblErrorPlace.Text = "Orte können nur Buchstaben enthalten!";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    {
                        userToInsert.Place = txtBoxPlace.Text;
                    }
                    break;

                case "txtBoxPhone":
                    if (txtBoxPhone.Text.Equals(""))
                    {
                        lblErrorPhone.Text = "Bitte geben Sie eine Telefonnummer ein!";
                        errorOccured = true;
                    }
                    else if (!IsCorrectPhoneNumber(txtBoxPhone.Text))
                    {
                        lblErrorPhone.Text = "Telefonnummern haben das Format 089/12345678 oder 089 12345678.";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    {
                        userToInsert.Phone = txtBoxPhone.Text;
                    }
                    break;

                case "txtBoxEmail":
                    if (txtBoxEmail.Text.Equals(""))
                    {
                        lblErrorEmail.Text = "Bitte geben Sie eine E-Mail-Adresse ein!";
                        errorOccured = true;
                    } else if (!IsCorrectMailAdress(txtBoxEmail.Text))
                    {
                        lblErrorEmail.Text = "Eine E-Mail-Adresse hat folgendes Format: user@domain.com";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    { userToInsert.EMail = txtBoxEmail.Text; }
                    break;

                case "txtBoxPassword":
                case "txtBoxPasswordx2":
                    if (txtBoxPassword.Text.Equals("") | txtBoxPasswordx2.Text.Equals(""))
                    {
                        lblErrorPwd.Text = "Bitte geben Sie ein Passwort ein!";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    {
                        MD5 md5Hash = MD5.Create();
                        string hash = clsUser.CreateMD5Hash(md5Hash, txtBoxPasswordx2.Text);
                        userToInsert.Password = hash;
                    }
                    break;
                default:
                    Console.WriteLine("Komponente nicht gefunden!");
                    break;
            }

            if (!txtBoxPassword.Text.Equals(txtBoxPasswordx2.Text))
            {
                lblErrorPwd.Text = "Bitte geben Sie zweimal das gleiche Passwort ein!";
                errorOccured = true;
            }

            return errorOccured;
        }

        /// <summary>
        /// Prüft, ob die Eingabe nur aus Buchsaben und Zahlen besteht.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>true, wenn die Eingabe nur aus Buchstaben und Zahlen besteht</returns>
        private bool IsAlphaNumericString(string input)
        {
            Regex template = new Regex(@"^[A-Za-z0-9]+$");
            return template.IsMatch(input);
        }

        private bool IsCorrectPostCode(string input)
        {
            Regex template = new Regex(@"^\d{5}$");
            return template.IsMatch(input);
        }

        private bool IsCorrectPhoneNumber(string input)
        {
            Regex template = new Regex(@"^\d+(\s|\/|)\d+$");
            return template.IsMatch(input);
        }

        private bool IsCorrectMailAdress(string input)
        {
            Regex template = new Regex(@"^(\w|\d|\.|\-)+\@(\w|\d|\-)+\.\w{2,3}");
            return template.IsMatch(input);
        }

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

        /// <summary>
        /// Prüft, ob die Eingabe nur aus Zahlen besteht.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>true, falls die Eingabe nur aus Zahlen besteht</returns>
        private bool IsNumericString(string input)
        {
            Regex template = new Regex(@"^[0-9]+$");
            return template.IsMatch(input);
        }

        /// <summary>
        /// Ermittelt die Länge der Eingabe.
        /// </summary>
        /// <param name="input">die eingegebene Zeichenfolge</param>
        /// <returns>die Länge der Eingabe</returns>
        private int GetLengthOfInput(string input)
        {
            return input.Length;
        }
    }
}
