using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bll;

namespace web
{
    public partial class Orders : System.Web.UI.Page
    {
        private List<clsProductExtended> selectedProducts;

        protected void Page_Load(object sender, EventArgs e)
        {
            selectedProducts = (List<clsProductExtended>)Session["selProducts"];

            if (!IsPostBack && (Session["selProducts"] != null))
            {
                if (selectedProducts.Count != 0)
                {
                    initializeOrderView(selectedProducts);
                    double _sum = getTotalSum();
                    lblSum.Text = "Gesamtsumme: " + String.Format("{0:C}", _sum);
                    if (GetDistance() > 20.0)
                    {
                        chkDelivery.Enabled = false;
                        chkDelivery.Checked = false;
                    }

                    checkMinimumOrder(_sum);
                }
            }
            else if (Session["selProducts"] != null)
            {
                selectedProducts = (List<clsProductExtended>)Session["selProducts"];
            }

        }

        private void checkMinimumOrder(double _sum)
        {
            double _diff = 20 - _sum;
            //_diff *= 100;
            //_diff = (int)_diff;
            //_diff /= 100;

            if (_sum <= 20 && chkDelivery.Checked)
            {
                lblStatus.Text = "Mindestbestellwert nicht erreicht. <br />"
                    + "Bitte bestellen Sie für mindestens 20,00 EUR wenn Sie eine Lieferung wünschen.<br />"
                    + "\n Es fehlen noch " + String.Format("{0:F}",_diff) + " EUR.";
                btOrder.Enabled = false;
            } else
            {
                lblStatus.Text = "";
                btOrder.Enabled = true;
            }
        }

        private double GetDistance()
        {
            return new clsUserFacade().GetDistanceByUser(Convert.ToInt32(Session["userID"]));
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
            switch (_product.CID)
            {
                case 1:
                    return _product.Size + " cm";
                case 2:
                    return _product.Size + " Liter";
                case 3:
                    return _product.Size + " Stück";
            }
            return "Fehler in der Verarbeitung";
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
                lblEmptyCart.Text = "";
                setEstimatedTime();
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

        private void setEstimatedTime()
        {
            double _minutes = 0;

            if (chkDelivery.Checked)
            {
                _minutes += GetDistance() * 2;
            }

            foreach (clsProductExtended _myProduct in selectedProducts)
            {
                if (_myProduct.CID == 1)
                {
                    _minutes += 10.0;
                }
            }
            lblStatus.Text = "Die Wartezeit beträgt vorraussichtlich " + Math.Round(_minutes) + " Minuten.";
        }

        protected void chkDelivery_CheckedChanged(object sender, EventArgs e)
        {
            checkMinimumOrder(getTotalSum());
        }
    }
}