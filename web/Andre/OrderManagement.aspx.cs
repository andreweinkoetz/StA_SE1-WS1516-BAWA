using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Andre
{
    public partial class OrderManagement_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["roleID"] == null))
            {
                if (Session["roleID"].ToString() == "2")
                {
                   
                }
            } else
            {
                gvOrderMgmt.Visible = false;
                btLogout.Visible = false;
                statusTable.Visible = false;
                lblOrderMgmt.ForeColor = System.Drawing.Color.Red;
                lblOrderMgmt.Font.Size = 16;
                lblOrderMgmt.Font.Bold = true;
                lblOrderMgmt.Text = "Sie sind nicht authorisiert diese Seite zu nutzen. \nBitte melden Sie sich an.";
            }
            
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Session["userID"] = null;
            Session["roleID"] = null;
            Server.Transfer("LoginPage.aspx");
        }
    }
}