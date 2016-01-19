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
            if (Session["userID"] == null)
            {
                Response.Redirect("login_page.aspx");
            }
            else
            {
                int _uId = (int)Session["userID"];
                WelcomeUser(_uId);
                if (!IsPostBack)
                {
                    FillLblSum(_uId);
                }
            }
        }

        /// <summary>
        /// Willkommenstext auf Benutzer anpassen.
        /// </summary>
        /// <param name="_uId">ID des Benutzers</param>
        private void WelcomeUser(int _uId)
        {
            clsUser _user = new clsUserFacade().UserGetById(_uId);
            lblWelcome.Text = "Herzlich willkommen " + _user.Title + " " + _user.Name + ",<br />";
            lblWelcome.Text += "Hier finden Sie Ihre offenen sowie abgeschlossenen Bestellungen:";
        }

        /// <summary>
        /// Gesamtsumme der Bestellungen (sortiert nach Bestellstatus) des Benutzers anzeigen.
        /// </summary>
        /// <param name="_uId">ID des Benutzers</param>
        private void FillLblSum(int _uId)
        {
            clsOrderFacade _orderFacade = new clsOrderFacade();
            Dictionary<String, Double> _statusAndSumDict = _orderFacade.GetOrderSumAndStatusByUserId(_uId);

            foreach (KeyValuePair<String, Double> _keyPair in _statusAndSumDict)
            {
                lblOrderSum.Text += _keyPair.Key + ": " + String.Format("{0:C}", _keyPair.Value) + "<br />";
            }
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
            ChangePasswordUI();
        }

        /// <summary>
        /// Ändern des Benutzer-Kennworts.
        /// Im Fehlerfall mit entsprechender Meldung.
        /// </summary>
        private void ChangePasswordUI()
        {
            if (txtBoxPassword.Text == txtBoxPasswordx2.Text && !String.IsNullOrEmpty(txtBoxPassword.Text))
            {
                if (ChangePassword(txtBoxPassword.Text))
                {
                    lblErrorPasswd.Text = "Passwort erfolgreich geändert.";
                }
                else
                {
                    lblErrorPasswd.Text = "Änderung fehlgeschlagen. <br />Bitte versuchen Sie es später erneut.";
                }

            }
            else
            {
                lblErrorPasswd.Text = "Bitte geben Sie 2x das gleiche Passwort ein!<br />Das Passwort darf nicht leer sein!";
            }
        }

        /// <summary>
        /// Durchführen der Passwortänderung in der DB
        /// </summary>
        /// <param name="_newPassword">neues Klartext-Passwort</param>
        /// <returns>true wenn erfolgreich</returns>
        private bool ChangePassword(string _newPassword)
        {
            MD5 md5Hash = MD5.Create();
            string hash = clsUser.CreateMD5Hash(md5Hash, _newPassword);
            return new clsUserFacade().ChangeUserPassword((int)Session["userID"], hash);
        }

    }
}