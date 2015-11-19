using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bll;

namespace web.Andre
{
    public partial class Orders : System.Web.UI.Page
    {
        private List<clsProductExtended> selectedProducts;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && (Session["selProducts"] != null))
            {
                selectedProducts = (List<clsProductExtended>)Session["selProducts"];
                initializeOrderView(selectedProducts);
                lblSum.Text = "Gesamtsumme: " + String.Format("{0:C}",getTotalSum());
            }
            else if (Session["selProducts"] != null)
            {
                selectedProducts = (List<clsProductExtended>)Session["selProducts"];
            }

        }

        private void initializeOrderView(List<clsProductExtended> _selectedProducts)
        {

            DataTable dt = new DataTable();

            dt = new DataTable("MyOrder");

            dt.Columns.Add("ID");

            dt.Columns.Add("Name");

            dt.Columns.Add("Größe");

            dt.Columns.Add("Extras");

            dt.Columns.Add("Preis Gesamt");

            foreach (clsProductExtended _product in _selectedProducts)
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
                dt.LoadDataRow(new object[] { _product.Id, _product.Name, _sizeText, _extraText, String.Format("{0:C}", (_product.PricePerUnit * _product.Size + getCostsOfExtras(_product))) }, true);
            }

            gvOrder.DataSource = dt;
            gvOrder.DataBind();
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

        protected void clearCart_Click(object sender, EventArgs e)
        {
            Session["selProducts"] = null;
        }

        protected void btOrder_Click(object sender, EventArgs e)
        {
            bool orderIsCorrect = true;
            clsOrderFacade _orderFacade = new clsOrderFacade();
            clsOrderExtended _myOrder = new clsOrderExtended();
            _myOrder.OrderNumber = _myOrder.GetHashCode();
            _myOrder.UserId = (int)Session["userID"];
            _myOrder.OrderDate = DateTime.Now;
            _myOrder.OrderStatus = 1; // Bestellung eingangen!
            _myOrder.OrderDelivery = chkDelivery.Checked;
            _myOrder.OrderSum = getTotalSum();

            foreach (clsProductExtended _product in selectedProducts)
            {
                _product.OpID = _product.GetHashCode() + _myOrder.OrderNumber;
                orderIsCorrect = _orderFacade.InsertOrderedProduct(_myOrder, _product) && orderIsCorrect;
                if (_product.ProductExtras != null)
                {
                    if (_product.ProductExtras.Count > 0)
                    {
                        orderIsCorrect = _orderFacade.InsertOrderedExtras(_product, _product.ProductExtras) && orderIsCorrect;
                    }
                }
            }
            orderIsCorrect = _orderFacade.InsertOrder(_myOrder) && orderIsCorrect;

            if (orderIsCorrect)
            {
                lblOrder.ForeColor = System.Drawing.Color.Red;
                lblOrder.Text = "Ihre Bestellung war erfolgreich. Bestellnummer: #" + _myOrder.OrderNumber;
                Session["selProducts"] = null;
            }

        }

        private double getCostsOfExtras(clsProductExtended _product)
        {

            double _costsOfExtras = 0;

            if (_product.ProductExtras == null)
            {
                return _costsOfExtras;
            }

            foreach (clsExtra _extra in _product.ProductExtras)
            {
                _costsOfExtras += _extra.Price;
            }

            return _costsOfExtras;

        }

        private double getTotalSum()
        {
            double _sum = 0;

            foreach (clsProductExtended _product in selectedProducts)
            {
                _sum += _product.PricePerUnit * _product.Size + getCostsOfExtras(_product);
            }

            return _sum;
        }
    }
}