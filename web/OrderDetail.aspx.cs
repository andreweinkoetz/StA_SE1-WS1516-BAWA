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
                int _orderNumber = (int)Session["oNumber"];
                lblOrderNumber.Text = "Bestellung #" + _orderNumber;
                clsOrderFacade _orderFacade = new clsOrderFacade();
                List<clsProductExtended> _orderedProducts = _orderFacade.GetOrderedProductsByOrderNumber(_orderNumber);

                double _sum = clsOrderFacade.GetOrderSum(_orderedProducts);

                lblTotalSum.Text = "Gesamtsumme: " + String.Format("{0:C}", _sum);

                FillCouponLabel(_orderNumber, _sum);

                btCancelOrder.Visible = _orderFacade.GetOrderStatusByOrderNumber(_orderNumber) == 1;

                InitializeOrderDetailView(_orderedProducts);
            }
            else if (!IsPostBack)
            {
                redirectOverview();
            }
        }

        private void FillCouponLabel(int _orderNumber, double _sum)
        {
            clsOrderExtended _myOrder = new clsOrderFacade().GetOrderByOrderNumber(_orderNumber);
            lblCoupon.Text = "Eingelöster Gutschein: \"" + _myOrder.MyCoupon.Code + "\"<br />";
            lblCoupon.Text += clsOrderFacade.GetMsgCoupon(_sum, out _sum, _myOrder.MyCoupon);
            lblNewSum.Text = "Neue Gesamtsumme: " + String.Format("{0:C}", _sum);
            lblTotalSum.Font.Bold = false;
            lblTotalSum.Font.Underline = false;
            lblTotalSum.Font.Strikeout = true;

        }

        private void InitializeOrderDetailView(List<clsProductExtended> _orderedProducts)
        {

            DataTable dt = new clsOrderExtended().CreateDataTableOfOrder(_orderedProducts);

            gvOrderDetail.DataSource = dt;
            gvOrderDetail.DataBind();
        }

        //private double GetPriceOfExtrasFromProduct(clsProductExtended _myProduct)
        //{
        //    double price = 0.0;

        //    foreach (clsExtra _myExtra in _myProduct.ProductExtras)
        //    {
        //        price += _myExtra.Price;
        //    }

        //    return price;
        //}

        //private double getTotalSum(List<clsProductExtended> _orderedProducts)
        //{
        //    double _sum = 0;

        //    foreach (clsProductExtended _product in _orderedProducts)
        //    {
        //        _sum += _product.PricePerUnit * _product.Size + GetPriceOfExtrasFromProduct(_product);
        //    }

        //    return _sum;
        //}

        protected void btOrderOverview_Click(object sender, EventArgs e)
        {
            redirectOverview();
        }

        private void redirectOverview()
        {
            Session["oNumber"] = null;
            Response.Redirect("MyAccount.aspx");
        }

        protected void btCancelOrder_Click(object sender, EventArgs e)
        {
            clsOrderFacade _myOrderFacade = new clsOrderFacade();
            _myOrderFacade.CancelOrderByONumber((int)Session["oNumber"]);
            redirectOverview();
        }
    }
}