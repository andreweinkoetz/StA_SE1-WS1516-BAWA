using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Login for the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            clsUserFacade userFacade = new clsUserFacade();
            bll.clsUser userToLogin = new bll.clsUser();
            userToLogin.EMail = txtBoxUsername.Text;
            userToLogin.Password = userFacade.getPassword(userToLogin.EMail);

            MD5 md5Hash = MD5.Create();
            bool isValid = VerifyPassword(md5Hash, txtBoxPassword.Text, userToLogin.Password);
            if (isValid)
            {
                Session["userID"] = userFacade.getIDOfUser(userToLogin.EMail);
                Session["roleID"] = userFacade.getRoleOfUser(userToLogin.EMail);
                Server.Transfer("Pizza.aspx");
            }
            else
            {
                lblTest.Text = "Die Validierung war nicht erfolgreich!";
            }
        }

        /// <summary>
        /// Creates and returns the MD5 Hash of a String
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string createMD5Hash(MD5 md5Hash, string password)
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
        /// Verify the password
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="passwordInput"></param>
        /// <param name="originalPassword"></param>
        /// <returns></returns>
        bool VerifyPassword(MD5 md5Hash, string passwordInput, string originalPassword)
        {
            string passwordInputHash = createMD5Hash(md5Hash, passwordInput);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            clsUserFacade userFacade = new clsUserFacade();
            bll.clsUser userToLogin = new bll.clsUser();
            userToLogin.Name = txtBoxUsername.Text;
            userToLogin.Password = userFacade.getPassword(userToLogin.Name);

            return (comparer.Compare(passwordInputHash, userFacade.getPassword(userToLogin.Name)) == 0);
        }

        /// <summary>
        /// Show the password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkBoxClear_Click(object sender, EventArgs e)
        {
            String safe = txtBoxPassword.Text;
            if (chkClear.Checked)
            {
                txtBoxPassword.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                txtBoxPassword.TextMode = TextBoxMode.Password;
                txtBoxPassword.Text = safe;
            }
        }
    }
}