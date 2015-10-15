using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Beispiele
{
    public partial class order_insert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            bll.clsOrder newOrder = new bll.clsOrder();
            bll.clsOrderFacade myOrders = new bll.clsOrderFacade();
            newOrder.UserId = Convert.ToInt32(ddlUsers.SelectedValue) ;
            newOrder.ProductId = Convert.ToInt32(txtProduct.Text); // besser als DropDownList realisieren!
            newOrder.OrderDate = System.DateTime.Now;
            newOrder.OrderSize = Convert.ToInt32(txtSize.Text);
            newOrder.OrderExtras = 0; // implementieren!
            newOrder.OrderDelivery = chkDelivery.Checked;
            newOrder.OrderCount = Convert.ToInt32(txtCount.Text);
            newOrder.OrderStatus = 0;
            newOrder.OrderSum = myOrders.CalculateOrderPrice(newOrder);
            if (myOrders.OrderInsert(newOrder))
                lblMsg.Text = "Neue Bestellung erfolgreich aufgenommen!";
            else
                lblMsg.Text = "Fehler bei Bestellannahme, wenden Sie sich vertrauensvoll an Ihren Programmierer!";
        } // btnInsert_Click()
    }
}