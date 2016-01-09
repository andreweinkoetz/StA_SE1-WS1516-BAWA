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
            
            if(Session["roleID"] == null)
            {
                Response.Redirect("login_page.aspx", true);
            }

            switch ((int)Session["roleID"])
            {
                case 1:
                    gvOrderMgmt.DataSource = getAllOrders;
                    btLogout.Text = "Zurück";
                    break;
                case 2:
                    gvOrderMgmt.DataSource = getOrdersNotDelivered;
                    break;
                case 3:
                    Response.Redirect("administration.aspx", true);
                    break;
            }

            gvOrderMgmt.DataBind();

        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            if ((int)Session["roleID"] != 1)
            {
                Session.Abandon();
                Response.Redirect("login_page.aspx");
            } else
            {
                Response.Redirect("administration.aspx");
            }
        }

        protected void gvOrderMgmt_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrderManagementButtons();

        }

        private void EnableOrderManagementButtons()
        {
            btDelivered.BackColor = System.Drawing.ColorTranslator.FromHtml("#CF323D");
            btInProgress.BackColor = System.Drawing.ColorTranslator.FromHtml("#CF323D");
            btDelivered.Enabled = true;
            btInProgress.Enabled = true;
        }

        protected void btDelivered_Click(object sender, EventArgs e)
        {
            OrderDelivered();
            refreshAfterUpdate();
        }

        private void OrderDelivered()
        {
            clsOrderExtended _updateOrder = new clsOrderExtended();
            _updateOrder.OrderNumber = Int32.Parse(gvOrderMgmt.SelectedRow.Cells[1].Text);
            _updateOrder.OrderStatus = 3;
            _updateOrder.OrderDeliveryDate = DateTime.Now;
            _myOrderFacade.UpdateOrderStatusByONumber(_updateOrder);
        }

        protected void btInProgress_Click(object sender, EventArgs e)
        {
            OrderInProgress();
            refreshAfterUpdate();
        }

        private void OrderInProgress()
        {
            clsOrderExtended _updateOrder = new clsOrderExtended();
            _updateOrder.OrderNumber = Int32.Parse(gvOrderMgmt.SelectedRow.Cells[1].Text);
            _updateOrder.OrderStatus = 2;
            _myOrderFacade.UpdateOrderStatusByONumber(_updateOrder);
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