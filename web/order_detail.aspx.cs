using bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class OrderDetail_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["oNumber"] != null)
            {
                InitializeOrderDetails();
            }
            else if (!IsPostBack)
            {
                redirectOverview();
            }
        }

        /// <summary>
        /// Initialisieren der Grundinformationen zur Darstellung
        /// der detaillierten Bestellung.
        /// </summary>
        private void InitializeOrderDetails()
        {
            int _orderNumber = (int)Session["oNumber"];
            lblOrderNumber.Text = "Bestellung #" + _orderNumber;
            clsOrderFacade _orderFacade = new clsOrderFacade();
            List<clsProductExtended> _orderedProducts = _orderFacade.GetOrderedProductsByOrderNumber(_orderNumber);
            clsOrderExtended _myOrder = _orderFacade.GetOrderByOrderNumber(_orderNumber);
            double _sum = clsOrderFacade.GetOrderSum(_orderedProducts);

            lblTotalSum.Text = "Gesamtsumme: " + String.Format("{0:C}", _sum);

            if (_myOrder.MyCoupon != null)
            {
                FillCouponLabel(_myOrder, _sum);
            }
            btCancelOrder.Visible = _orderFacade.GetOrderStatusByOrderNumber(_orderNumber) == 1;

            InitializeOrderDetailView(_orderedProducts);
        }

        /// <summary>
        /// Falls ein Gutschein eingelöst wurde, so ist er
        /// in der Detailansicht ebenfalls einsehbar.
        /// </summary>
        /// <param name="_myOrder">Bestellung die dargestellt wird</param>
        /// <param name="_sum">Gesamtsumme der Bestellung</param>
        private void FillCouponLabel(clsOrderExtended _myOrder, double _sum)
        {
            lblCoupon.Text = "Eingelöster Gutschein: \"" + _myOrder.MyCoupon.Code + "\"<br />";
            lblCoupon.Text += clsOrderFacade.GetMsgCoupon(_sum, out _sum, _myOrder.MyCoupon);
            lblNewSum.Text = "Neue Gesamtsumme: " + String.Format("{0:C}", _sum);
            lblTotalSum.Font.Bold = false;
            lblTotalSum.Font.Underline = false;
            lblTotalSum.Font.Strikeout = true;
        }

        /// <summary>
        /// Erstellt ein neues DataTable-Objekt und füllt damit das GridView-Element.
        /// </summary>
        /// <param name="_orderedProducts">Liste der bestellten Produkte</param>
        private void InitializeOrderDetailView(List<clsProductExtended> _orderedProducts)
        {
            DataTable dt = new clsOrderExtended().CreateDataTableOfOrder(_orderedProducts);

            gvOrderDetail.DataSource = dt;
            gvOrderDetail.DataBind();
        }

        protected void btOrderOverview_Click(object sender, EventArgs e)
        {
            redirectOverview();
        }

        /// <summary>
        /// Setzt Session-Variablen zurück und 
        /// leitet auf Hauptansicht um.
        /// </summary>
        private void redirectOverview()
        {
            Session["oNumber"] = null;
            Response.Redirect("my_account.aspx");
        }

        protected void btCancelOrder_Click(object sender, EventArgs e)
        {
            CancelOrder();
        }

        /// <summary>
        /// Storniert die ausgewählte Bestellung.
        /// </summary>
        private void CancelOrder()
        {
            clsOrderFacade _myOrderFacade = new clsOrderFacade();
            _myOrderFacade.CancelOrderByONumber((int)Session["oNumber"]);
            redirectOverview();
        }
    }
}