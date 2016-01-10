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

            Label[] errorLabels = { lblErrorName, lblErrorStreet, lblErrorPlace, lblErrorEmail, lblErrorPwd };

            foreach (TextBox element in textBoxes)
            {
                bool error = checkAndSetValidInput(element);
                userCanBeInsertedInDB = !error;
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
        /// Setzt den Wert einer Eigenschaft des Nutzers bei gültiger Eingabe.
        /// </summary>
        /// <param name="errorOccured">gibt an, ob ein Fehler aufgetreten ist</param>
        /// <param name="attribute">Eigenschaft des Nutzers</param>
        /// <param name="userInput">Wert, den der Nutzer eingegeben hat</param>
        protected void setValidUserAttribute(bool errorOccured, string attribute, string userInput)
        {
            if (!errorOccured)
            {
                attribute = userInput;
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
        /// <param name="attribute">Eigenschaft des Benutzers, der bei gültiger Eingabe der Wert zugeordnet wird</param>
        /// <param name="isErrorOccured">gibt an, ob bei der Zuordnung des Wertes ein Fehler aufgetreten ist</param>
        /// <param name="lblError">Error-Label, dass im Falle eines Fehlers angezeigt wird</param>
        /// <param name="emptyErrorText">Fehlertext für den Fall, dass der Nutzer nichts eingegeben hat</param>
        /// <param name="alphaErrorText">Fehlertext für den Fall, dass die Eingabe nicht nur aus Buchstaben besteht</param>
        /// <returns>true, falls bei der Zuordnung des Wertes ein Fehler aufgetreten ist</returns>
        protected bool handleNeededAlphaInput(TextBox textBox, string attribute, bool isErrorOccured, Label lblError, string emptyErrorText, string alphaErrorText)
        {
            if (textBox.Text.Equals(""))
            {
                handleError(out isErrorOccured, lblError, emptyErrorText);
            }
            else if (!IsAlphaString(textBox.Text))
            {
                handleError(out isErrorOccured, lblError, alphaErrorText);
            }
            setValidUserAttribute(isErrorOccured, attribute, textBox.Text);
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
                    if(!(errorOccured = handleNeededAlphaInput(txtBoxName, userToInsert.Name, errorOccured, lblErrorName,
                        "Bitte geben Sie ihren Nachnamen ein!", "Nachnamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Name = txtBoxName.Text;
                    }
                    
                    break;
                case "txtBoxVorname":
                    if(!(errorOccured = handleNeededAlphaInput(txtBoxVorname, userToInsert.Prename, errorOccured, lblErrorPrename,
                        "Bitte geben Sie ihren Vornamen ein!", "Vornamen können nur Buchstaben enthalten!")))
                    {
                        userToInsert.Prename = txtBoxVorname.Text;
                    }
                    break;

                case "txtBoxStraße":
                    if(!(errorOccured = handleNeededAlphaInput(txtBoxStraße, userToInsert.Street, errorOccured, lblErrorStreet,
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
                    else if (!IsAlphaNumericString(txtBoxHnr.Text))
                    {
                        lblErrorStreet.Text = "Hausnummern können nur Zahlen und Buchstaben enthalten!";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    { userToInsert.Nr = Convert.ToInt32(txtBoxHnr.Text); }
                    break;
                case "txtBoxPLZ":
                    if (txtBoxPLZ.Text.Equals(""))
                    {
                        lblErrorPlace.Text = "Bitte geben Sie eine Postleitzahl ein!";
                        errorOccured = true;
                    }
                    else if (!IsNumericString(txtBoxPLZ.Text))
                    {
                        lblErrorPlace.Text = "Die Postleitzahl kann nur Zahlen enthalten!";
                        errorOccured = true;
                    }
                    else if (GetLengthOfInput(txtBoxPLZ.Text) != 5)
                    {
                        lblErrorPlace.Text = "Postleitzahlen in Deutschland müssen fünfstellig sein!";
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
                    else if (!IsNumericString(txtBoxPhone.Text))
                    {
                        lblErrorPhone.Text = "Telefonnummern bestehen nur aus Zahlen!";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    {
                        userToInsert.Phone = "+49" + txtBoxPhone.Text;
                    }
                    break;

                case "txtBoxEmail":
                    if (txtBoxEmail.Text.Equals(""))
                    {
                        lblErrorEmail.Text = "Bitte geben Sie eine Email-Adresse ein!";
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
