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
                lblOrderNumber.Text = "Bestellung #" + Session["oNumber"].ToString();
                clsOrderFacade _orderFacade = new clsOrderFacade();
                List<clsProductExtended> _orderedProducts = _orderFacade.GetOrderedProductsByOrderNumber((int)Session["oNumber"]);

                lblTotalSum.Font.Bold = true;
                lblTotalSum.Font.Underline = true;
                lblTotalSum.Text = "Gesamtsumme: " + String.Format("{0:C}", getTotalSum(_orderedProducts));

                btCancelOrder.Visible = _orderFacade.GetOrderStatusByOrderNumber((int)Session["oNumber"]) == 1;

                initializeOrderDetailView(_orderedProducts);
            }
            else if (!IsPostBack)
            {
                redirectOverview();
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
                case "1":
                    return _product.Size + " cm";
                case "2":
                    return _product.Size + " Liter";
                case "3":
                    return _product.Size + " Stück";
            }
            return null;
        }

        private double GetPriceOfExtrasFromProduct(clsProductExtended _myProduct)
        {
            double price = 0.0;

            foreach(clsExtra _myExtra in _myProduct.ProductExtras)
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