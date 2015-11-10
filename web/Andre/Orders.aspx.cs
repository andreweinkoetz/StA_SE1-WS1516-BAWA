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
            if (Session["selProducts"] != null)
            {
                selectedProducts = (List<clsProductExtended>)Session["selProducts"];
            }

            if (!IsPostBack)
            {

                initializeOrderView(selectedProducts);
                lblSum.Text = "Gesamtsumme: " + getTotalSum() + " EUR";
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

            dt.Columns.Add("Preis");



            foreach (clsProductExtended _product in _selectedProducts)
            {
                String extraText = "";

                foreach (clsExtra _extra in _product.ProductExtras)
                {
                    extraText += _extra.Name + "\n";
                }

                dt.LoadDataRow(new object[] { _product.Id, _product.Name, _product.Size, extraText, (_product.PricePerUnit * _product.Size + getNumberOfExtras(_product) * 0.5) }, true);
            }

            gvOrder.DataSource = dt;
            gvOrder.DataBind();
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
            _myOrder.OrderStatus = 0;
            _myOrder.OrderSum = getTotalSum();

            foreach (clsProductExtended _product in selectedProducts)
            {
                _product.OpID = _product.GetHashCode() + _myOrder.OrderNumber;
                orderIsCorrect = _orderFacade.InsertOrderedProduct(_myOrder, _product) && orderIsCorrect;
                if (_product.ProductExtras.Count > 0)
                {
                    orderIsCorrect = _orderFacade.InsertOrderedExtras(_product, _product.ProductExtras) && orderIsCorrect;
                }
            }
            orderIsCorrect = _orderFacade.InsertOrder(_myOrder) && orderIsCorrect;

            if (orderIsCorrect)
            {
                lblOrder.ForeColor = System.Drawing.Color.Red;
                lblOrder.Text = "Ihre Bestellung war erfolgreich. Bestellnummer: #" + _myOrder.OrderNumber;
            }

        }

        private int getNumberOfExtras(clsProductExtended _product)
        {

            int numberOfExtras = 0;

            foreach (clsExtra _extra in _product.ProductExtras)
            {
                numberOfExtras++;
            }

            return numberOfExtras;

        }

        private double getTotalSum()
        {
            double _sum = 0;

            foreach (clsProductExtended _product in selectedProducts)
            {

                _sum += _product.PricePerUnit * _product.Size + getNumberOfExtras(_product) * 0.5;
            }

            return _sum;
        }
    }
}