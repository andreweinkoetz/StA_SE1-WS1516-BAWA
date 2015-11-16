using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Andre
{
    public partial class Administration_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            disableUI();

            if (!(Session["roleID"] == null))
            {
                if ((int)Session["roleID"] < 2)
                {
                    enableUI();
                }
            }
        }

        private void disableUI()
        {
            btAdmData.Visible = false;
            btLogout.Visible = false;
            btAdmOrders.Visible = false;
            btAdmStat.Visible = false;
            tblAdm.Visible = false;
            lblAdminOverview.ForeColor = System.Drawing.Color.Red;
            lblAdminOverview.Font.Size = 16;
            lblAdminOverview.Text = "Sie sind nicht authorisiert diese Seite zu nutzen. \nBitte melden Sie sich an.";
        }

        private void enableUI()
        {
            btAdmData.Visible = true;
            btLogout.Visible = true;
            btAdmOrders.Visible = true;
            btAdmStat.Visible = true;
            tblAdm.Visible = true;
            lblAdminOverview.ForeColor = System.Drawing.Color.Black;
            lblAdminOverview.Font.Size = 20;
            lblAdminOverview.Text = "Administration";
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            Session["userID"] = null;
            Session["roleID"] = null;
            Server.Transfer("LoginPage.aspx");
        }

        protected void btAdmData_Click(object sender, EventArgs e)
        {
            if(ddlistData.SelectedIndex > 0)
            {
                Session["selAdmData"] = Int32.Parse(ddlistData.SelectedValue);
                Server.Transfer("AdmData.aspx");
            } else
            {
                lblErrorMsg.Visible = true;
            }
        }

        protected void btAdmOrders_Click(object sender, EventArgs e)
        {
            Server.Transfer("Orders.aspx");
        }

        protected void btAdmStat_Click(object sender, EventArgs e)
        {

        }
    }
}