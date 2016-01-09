using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
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
            lblAdminOverview.Text = "Sie sind nicht authorisiert diese Seite zu nutzen. <br />Bitte melden Sie sich an.";
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
            Session.Abandon();
            Response.Redirect("login_page.aspx");
        }

        protected void btAdmData_Click(object sender, EventArgs e)
        {
            if (ddlistData.SelectedIndex > 0)
            {
                Session["selAdmData"] = Int32.Parse(ddlistData.SelectedValue);
                Response.Redirect("adm_data.aspx");
            }
            else
            {
                lblErrorMsg.Visible = true;
            }
        }

        protected void btAdmOrders_Click(object sender, EventArgs e)
        {
            switch (ddlistOrders.SelectedIndex)
            {
                case 1:
                    Response.Redirect("order_management.aspx");
                    break;
                case 2:
                    Response.Redirect("order_archive.aspx");
                    break;
                default:
                    lblErrorMsg.Visible = true;
                    break;
            }
        }

        protected void btAdmStat_Click(object sender, EventArgs e)
        {
            Response.Redirect("stats.aspx");
        }

        protected void btAdmCoupons_Click(object sender, EventArgs e)
        {
            Response.Redirect("adm_coupon.aspx");
        }
    }
}