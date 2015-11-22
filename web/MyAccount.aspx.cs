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
                lblMyOrders.ForeColor = System.Drawing.Color.Red;
                lblMyOrders.Font.Size = 16;
                lblMyOrders.Text = "Sie sind nicht authorisiert diese Seite zu nutzen. \nBitte melden Sie sich an.";
                btLogout.Visible = false;
            }
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Session["userID"] = null;
            Session["roleID"] = null;
            Session["oNumber"] = null;
            Session["selProducts"] = null;
            Server.Transfer("LoginPage.aspx");
        }

        protected void gvMyOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["oNumber"] = Int32.Parse(gvMyOrders.SelectedRow.Cells[1].Text);
            Server.Transfer("OrderDetail.aspx",false);
        }
    }
}