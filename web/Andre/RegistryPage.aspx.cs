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
        private clsUser userToInsert = new clsUser();
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

            checkForValidDigits(txtBoxName.Text, lblErrorName, sender, e);
            userToInsert.Name = txtBoxName.Text;

            checkForValidDigits(txtBoxVorname.Text, lblErrorName, sender, e);
            userToInsert.Prename = txtBoxVorname.Text;

            userToInsert.Street = txtBoxStraße.Text;
            if (!txtBoxHnr.Text.Equals(""))
            {
                int Hnr = Convert.ToInt32(txtBoxHnr.Text);
                userToInsert.Nr = Hnr;
            }
            userToInsert.Place = txtBoxPlace.Text;
            userToInsert.IsActive = true;
            userToInsert.Role = 3;

            if (txtBoxPassword.Text.Equals("") | txtBoxPaswordx2.Text.Equals(""))
            {
                lblErrorPwd.Text = "Das Feld darf nicht leer sein!";
            }

            if (!txtBoxPassword.Text.Equals(txtBoxPaswordx2.Text))
            {
                lblErrorPwd.Text = "Sie müssen 2x das selbe Passwort eingeben!";
                userCanBeInsertedInDB = false;
            }

            MD5 md5Hash = MD5.Create();
            string hash = createMD5Hash(md5Hash, txtBoxPaswordx2.Text);
            userToInsert.Password = hash;

            if (userCanBeInsertedInDB)
            {
                if (userFacade.UserInsert(userToInsert))
                {
                    Server.Transfer("LoginPage.aspx");
                }
                else
                {
                    lblErrorName.Text = "Registrierung fehlgeschlagen. Fehler in der DB.";
                }
            }
        }

        /// <summary>
        /// Checks if valid informations are entered.
        /// </summary>
        /// <param name="textToProve"></param>
        /// <param name="errorLabel"></param>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        void checkForValidDigits(string textToProve, Label errorLabel, object sender, EventArgs e)
        {

            if (textToProve.Equals(""))
            {
                errorLabel.Text = "Bitte geben sie einen (Vor-)Namen ein!";
                userCanBeInsertedInDB = false;
            }
            else if (!isAlphaString(textToProve))
            {
                errorLabel.Text = "Namen können nur Buchstaben enthalten!";
                userCanBeInsertedInDB = false;
            }
        }

        /// <summary>
        /// Creates and returns the MD5 Hash of a String
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string createMD5Hash(MD5 md5Hash, string password)
        {

            // Converts the password to a byte array and computes the MD5 hash
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sBuilder = new StringBuilder();

            // Formats each byte into a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// Checks if it is a valid alphanumeric String.
        /// </summary>
        /// <param name="wordToTest"></param>
        /// <returns></returns>
        bool isAlphaNumericString(string wordToTest)
        {
            System.Text.RegularExpressions.Regex template = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return template.IsMatch(wordToTest);
        }

        /// <summary>
        /// Checks if only letters are used.
        /// </summary>
        /// <param name="wordToTest"></param>
        /// <returns></returns>
        static bool isAlphaString(string wordToTest)
        {

            System.Text.RegularExpressions.Regex template = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
            return template.IsMatch(wordToTest);

        }

        /// <summary>
        /// Checks if only digits are used.
        /// </summary>
        /// <param name="wordToTest"></param>
        /// <returns></returns>
        bool isNumericString(string wordToTest)
        {
            System.Text.RegularExpressions.Regex template = new System.Text.RegularExpressions.Regex("^[0-9]$");
            return template.IsMatch(wordToTest);
        }

    }
}
