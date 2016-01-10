using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class MyAccount_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userID"] == null)
            {
                Response.Redirect("login_page.aspx");
            } else
            {
                WelcomeUser((int)Session["userID"]);
            }
        }

        private void WelcomeUser(int _uId)
        {
            clsUser _user = new clsUserFacade().UserGetById(_uId);
            lblWelcome.Text = "Herzlich willkommen " + _user.Title + " " + _user.Name + ",<br />";
            lblWelcome.Text += "Hier finden Sie Ihre offenen sowie abgeschlossenen Bestellungen:";
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login_page.aspx");
        }

        protected void gvMyOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["oNumber"] = Int32.Parse(gvMyOrders.SelectedRow.Cells[1].Text);
            Response.Redirect("order_detail.aspx", false);
        }

        protected void btChangePasswd_Click(object sender, EventArgs e)
        {
            if(txtBoxPassword.Text == txtBoxPasswordx2.Text && !String.IsNullOrEmpty(txtBoxPassword.Text))
            {
                if (ChangePassword(txtBoxPassword.Text))
                {
                    lblErrorPasswd.Text = "Passwort erfolgreich geändert.";
                } else
                {
                    lblErrorPasswd.Text = "Änderung fehlgeschlagen. <br />Bitte versuchen Sie es später erneut.";
                }
                
            } else
            {
                lblErrorPasswd.Text = "Bitte geben Sie 2x das gleiche Passwort ein!<br />Das Passwort darf nicht leer sein!";
            }
        }

        private bool ChangePassword(string _newPassword)
        {
            MD5 md5Hash = MD5.Create();
            string hash = clsUser.CreateMD5Hash(md5Hash, _newPassword);
            return new clsUserFacade().ChangeUserPassword((int)Session["userID"], hash);
        }

    }
}