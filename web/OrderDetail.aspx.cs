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
                List<clsProductExtended> _orderedProducts = _orderFacade.getOrderedProductsByOrderNumber((int)Session["oNumber"]);

                lblTotalSum.Font.Bold = true;
                lblTotalSum.Font.Underline = true;
                lblTotalSum.Text = "Gesamtsumme: " + String.Format("{0:C}", getTotalSum(_orderedProducts));

                initializeOrderDetailView(_orderedProducts);
            }
            else if (!IsPostBack)
            {
                lblOrderNumber.ForeColor = System.Drawing.Color.Red;
                lblOrderNumber.Font.Size = 12;
                lblOrderNumber.Text = "Fehler in der Übermittlung. Bitte wählen Sie eine Bestellung aus.";
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
                dt.LoadDataRow(new object[] { _product.Name, _sizeText, _extraText, String.Format("{0:C}", (_product.PricePerUnit * _product.Size + getNumberOfExtras(_product) * 0.5)) }, true);
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

        private int getNumberOfExtras(clsProductExtended _product)
        {

            int numberOfExtras = 0;

            if (_product.ProductExtras == null)
            {
                return numberOfExtras;
            }

            foreach (clsExtra _extra in _product.ProductExtras)
            {
                numberOfExtras++;
            }

            return numberOfExtras;

        }

        private double getTotalSum(List<clsProductExtended> _orderedProducts)
        {
            double _sum = 0;

            foreach (clsProductExtended _product in _orderedProducts)
            {
                _sum += _product.PricePerUnit * _product.Size + getNumberOfExtras(_product) * 0.5;
            }

            return _sum;
        }

        protected void btOrderOverview_Click(object sender, EventArgs e)
        {
            Session["oNumber"] = null;
            Server.Transfer("MyAccount.aspx");
        }
    }
}