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
            lblInactiveUser.ForeColor = System.Drawing.Color.Red; 
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
                int _userId = userFacade.GetIDOfUser(userToLogin.EMail);
                if (userFacade.GetUserActive(_userId))
                {
                    Session["userID"] = _userId;
                    Session["roleID"] = userFacade.GetRoleOfUser(userToLogin.EMail);
                    Response.Redirect("pizza.aspx");
                } else
                {
                    lblInactiveUser.Text = "Ihr Konto wurde gesperrt. Bitte wenden Sie sich an support@pizzapizza.de";
                }
            }
            else
            {
                lblInactiveUser.Text = "Die Validierung war nicht erfolgreich!";
            }
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
            string passwordInputHash = clsUser.CreateMD5Hash(md5Hash, passwordInput);

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

        protected void btadmin_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 1;
            Session["userID"] = 13;
            Response.Redirect("administration.aspx", true);
        }

        protected void btservice_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 2;
            Session["userID"] = 14;
            Response.Redirect("order_management.aspx", true);
        }

        protected void btkunde_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 3;
            Session["userID"] = 15;
            Response.Redirect("default.aspx", true);
        }
    }
}