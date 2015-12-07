using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class OrderManagement_Code : System.Web.UI.Page
    {
        private clsOrderFacade _myOrderFacade = new clsOrderFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            disableUI();

            if (Session["roleID"] != null)
            {
                if ((int)Session["roleID"] < 3)
                {
                    if ((int)Session["roleID"] == 1)
                    {
                        gvOrderMgmt.DataSource = getAllOrders;
                    }
                    else
                    {
                        gvOrderMgmt.DataSource = getOrdersNotDelivered;
                    }

                    gvOrderMgmt.DataBind();
                    enableUI();
                }
            }

        }

        private void disableUI()
        {
            gvOrderMgmt.Visible = false;
            btLogout.Visible = false;
            lblOrderMgmt.ForeColor = System.Drawing.Color.Red;
            lblOrderMgmt.Font.Size = 16;
            lblOrderMgmt.Text = "Sie sind nicht authorisiert diese Seite zu nutzen. \nBitte melden Sie sich an.";
        }

        private void enableUI()
        {
            gvOrderMgmt.Visible = true;
            btLogout.Visible = true;
            btDelivered.Visible = true;
            btInProgress.Visible = true;
            lblOrderMgmt.ForeColor = System.Drawing.Color.Black;
            lblOrderMgmt.Font.Size = 20;
            lblOrderMgmt.Text = "Bestellverwaltung";
        }


        protected void btLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }

        protected void gvOrderMgmt_SelectedIndexChanged(object sender, EventArgs e)
        {
            btDelivered.BackColor = System.Drawing.ColorTranslator.FromHtml("#CF323D");
            btInProgress.BackColor = System.Drawing.ColorTranslator.FromHtml("#CF323D");
            btDelivered.Enabled = true;
            btInProgress.Enabled = true;

        }

        protected void btDelivered_Click(object sender, EventArgs e)
        {
            clsOrderExtended _updateOrder = new clsOrderExtended();
            _updateOrder.OrderNumber = Int32.Parse(gvOrderMgmt.SelectedRow.Cells[1].Text);
            _updateOrder.OrderStatus = 3;
            _updateOrder.OrderDeliveryDate = DateTime.Now;
            _myOrderFacade.UpdateOrderStatusByONumber(_updateOrder);

            refreshAfterUpdate();
        }

        protected void btInProgress_Click(object sender, EventArgs e)
        {
            clsOrderExtended _updateOrder = new clsOrderExtended();
            _updateOrder.OrderNumber = Int32.Parse(gvOrderMgmt.SelectedRow.Cells[1].Text);
            _updateOrder.OrderStatus = 2;
            _myOrderFacade.UpdateOrderStatusByONumber(_updateOrder);

            refreshAfterUpdate();
        }

        private void refreshAfterUpdate()
        {
            getOrdersNotDelivered.EnableCaching = false;
            gvOrderMgmt.DataBind();
            getOrdersNotDelivered.EnableCaching = true;
            gvOrderMgmt.SelectedIndex = -1;
            btDelivered.Enabled = false;
            btInProgress.Enabled = false;
            btDelivered.BackColor = System.Drawing.Color.Gray;
            btInProgress.BackColor = System.Drawing.Color.Gray;
        }

    }
}