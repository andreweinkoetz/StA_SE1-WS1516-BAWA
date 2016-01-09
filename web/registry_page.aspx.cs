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
                bool result = checkAndSetValidInput(element);
                if (result)
                {
                    userCanBeInsertedInDB = false;
                }
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

        private bool checkAndSetValidInput(TextBox stringToProve)
        {
            bool errorOccured = false;

            if (ddlTitle.SelectedItem.Text == " - " || ddlTitle.SelectedIndex == -1)
            {
                lblTitleError.Text = "Bitte treffen Sie eine Auswahl.";
                return !errorOccured;
            }

            userToInsert.Title = ddlTitle.SelectedItem.Text;
            userToInsert.IsActive = true;
            userToInsert.Role = 3;

            switch (stringToProve.ID)
            {
                case "txtBoxName":
                case "txtBoxVorname":
                    if (txtBoxName.Text.Equals("") | txtBoxVorname.Text.Equals(""))
                    {
                        lblErrorName.Text = "Bitte geben Sie ihren Vor- und Nachnamen ein!";
                        errorOccured = true;
                    }
                    else if (!isAlphaString(txtBoxName.Text) | !isAlphaString(txtBoxVorname.Text))
                    {
                        lblErrorName.Text = "Namen können nur Buchstaben enthalten!";
                        errorOccured = true;
                    }
                    if (!errorOccured)
                    {
                        userToInsert.Name = txtBoxName.Text;
                        userToInsert.Prename = txtBoxVorname.Text;
                    }
                    break;

                case "txtBoxStraße":
                    if (txtBoxStraße.Text.Equals(""))
                    {
                        lblErrorStreet.Text = "Bitte geben Sie eine Straße ein!";
                        errorOccured = true;
                    }
                    else if (!isAlphaString(txtBoxStraße.Text))
                    {
                        lblErrorStreet.Text = "Straßennamen können nur Buchstaben enthalten!";
                        errorOccured = true;
                    }
                    if (!errorOccured) { userToInsert.Street = txtBoxStraße.Text; }
                    break;
                case "txtBoxHnr":
                    if (txtBoxHnr.Text.Equals(""))
                    {
                        lblErrorStreet.Text = "Bitte geben Sie eine Hausnummer ein!";
                        errorOccured = true;
                    }
                    else if (!isAlphaNumericString(txtBoxHnr.Text))
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
                    else if (!isNumericString(txtBoxPLZ.Text))
                    {
                        lblErrorPlace.Text = "Die Postleitzahl kann nur Zahlen enthalten!";
                        errorOccured = true;
                    }
                    else if (getLengthOfWord(txtBoxPLZ.Text) != 5)
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
                    else if (!isAlphaString(txtBoxPlace.Text))
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
                    else if (!isTelephoneNumber(txtBoxPhone.Text))
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
        /// Checks if it is a valid alphanumeric String.
        /// </summary>
        /// <param name="wordToTest"></param>
        /// <returns></returns>
        private bool isAlphaNumericString(string wordToTest)
        {
            System.Text.RegularExpressions.Regex template = new Regex(@"^[A-Za-z0-9]+$");
            return template.IsMatch(wordToTest);
        }

        /// <summary>
        /// Checks if only letters are used.
        /// </summary>
        /// <param name="wordToTest"></param>
        /// <returns></returns>
        private static bool isAlphaString(string wordToTest)
        {

            System.Text.RegularExpressions.Regex template = new Regex(@"^[A-Za-z_äÄöÖüÜß\s]+$");
            return template.IsMatch(wordToTest);

        }

        /// <summary>
        /// Checks if only digits are used.
        /// </summary>
        /// <param name="wordToTest"></param>
        /// <returns></returns>
        private bool isNumericString(string wordToTest)
        {
            System.Text.RegularExpressions.Regex template = new Regex(@"^[0-9]+$");
            return template.IsMatch(wordToTest);
        }

        private bool isTelephoneNumber(string wordToTest)
        {
            System.Text.RegularExpressions.Regex template = new Regex(@"^[0-9]+$");
            return template.IsMatch(wordToTest);
        }

        /// <summary>
        /// Ermittelt die Laenge eines Zeichensatzes.
        /// </summary>
        /// <param name="wordToTest"></param>
        /// <returns></returns>
        private int getLengthOfWord(string wordToTest)
        {
            return wordToTest.Length;
        }
    }
}
