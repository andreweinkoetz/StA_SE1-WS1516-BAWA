using bll;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}