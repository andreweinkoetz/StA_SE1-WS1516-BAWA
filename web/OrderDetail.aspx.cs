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

                double _sum = getTotalSum(_orderedProducts);

                lblTotalSum.Font.Bold = true;
                lblTotalSum.Font.Underline = true;
                lblTotalSum.Text = "Gesamtsumme: " + String.Format("{0:C}", _sum);

                FillCouponLabel(_orderNumber, _sum);

                btCancelOrder.Visible = _orderFacade.GetOrderStatusByOrderNumber(_orderNumber) == 1;

                initializeOrderDetailView(_orderedProducts);
            }
            else if (!IsPostBack)
            {
                redirectOverview();
            }
        }

        private void FillCouponLabel(int _orderNumber, double _sum)
        {
            clsOrderExtended _myOrder = new clsOrderFacade().GetOrderByOrderNumber(_orderNumber);
            if (_myOrder.MyCoupon != null)
            {
                clsCoupon _myCoupon = _myOrder.MyCoupon;
                double _saving = (_sum * ((double)_myCoupon.Discount / 100.0));
                double _newSum = _sum - _saving;
                lblCoupon.Text = "Eingelöster Gutschein: \"" + _myCoupon.Code + "\"<br />";
                lblCoupon.Text += "Wert des Gutscheins: " + _myCoupon.Discount + "%.<br />";
                lblCoupon.Text += "Ersparnis: " + String.Format("{0:C}", _saving) + "<br/>";
                lblNewSum.Text = "Neue Gesamtsumme: " + String.Format("{0:C}", _newSum);
                lblTotalSum.Font.Bold = false;
                lblTotalSum.Font.Underline = false;
                lblTotalSum.Font.Strikeout = true;
            }
        }

        private void initializeOrderDetailView(List<clsProductExtended> _orderedProducts)
        {

            DataTable dt = new DataTable();

            dt = new DataTable("MyOrder");

            dt.Columns.Add("Name");

            dt.Columns.Add("Größe");

            dt.Columns.Add("Extras");

            dt.Columns.Add("Preis Gesamt");

            foreach (clsProductExtended _product in _orderedProducts)
            {
                String _extraText = "";
                String _sizeText = "";
                if (_product.ProductExtras != null)
                {
                    foreach (clsExtra _extra in _product.ProductExtras)
                    {
                        _extraText += _extra.Name + "\n";
                    }
                }
                _sizeText = setSizeText(_product);
                dt.LoadDataRow(new object[] { _product.Name, _sizeText, _extraText, String.Format("{0:C}", (_product.PricePerUnit * _product.Size + GetPriceOfExtrasFromProduct(_product))) }, true);
            }

            gvOrderDetail.DataSource = dt;
            gvOrderDetail.DataBind();
        }

        private String setSizeText(clsProductExtended _product)
        {
            switch (_product.Category)
            {
                case "Pizza":
                    return _product.Size + " cm";
                case "Getränk":
                    return _product.Size + " Liter";
                case "Dessert":
                    return _product.Size + " Stück";
            }
            return "Fehler in der Verarbeitung";
        }

        private double GetPriceOfExtrasFromProduct(clsProductExtended _myProduct)
        {
            double price = 0.0;

            foreach (clsExtra _myExtra in _myProduct.ProductExtras)
            {
                price += _myExtra.Price;
            }

            return price;
        }

        private double getTotalSum(List<clsProductExtended> _orderedProducts)
        {
            double _sum = 0;

            foreach (clsProductExtended _product in _orderedProducts)
            {
                _sum += _product.PricePerUnit * _product.Size + GetPriceOfExtrasFromProduct(_product);
            }

            return _sum;
        }

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