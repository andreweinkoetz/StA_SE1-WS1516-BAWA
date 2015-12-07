using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class OrderArchive_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["roleID"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            else if ((int)Session["roleID"] >= 2)
            {
                Response.Redirect("Administration.aspx");
            }

        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administration.aspx");
        }

        protected void gvOrderArchive_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _oNumber = Int32.Parse(gvOrderArchive.SelectedRow.Cells[2].Text);
            clsOrderFacade _orderFacade = new clsOrderFacade();
            clsOrderExtended _myOrder = _orderFacade.GetOrderByOrderNumber(_oNumber);
            _orderFacade = new clsOrderFacade();
            List<clsProductExtended> _myProductList = _orderFacade.GetOrderedProductsByOrderNumber(_oNumber);
            _orderFacade = new clsOrderFacade();
            int _deletedRecords = _orderFacade.DeleteOrderByOrderNumber(_oNumber);
            if (_deletedRecords > 0)
            {
                lblError.ForeColor = System.Drawing.Color.Black;
                lblError.Text = "Es wurden " + _deletedRecords + " Datensätze erfolgreich gelöscht.";
                gvOrderArchive.DataBind();
                CreateCSV(_myOrder, _myProductList);
                btDownloadCSV.Visible = true;
            }
            else
            {
                lblError.Text = "Es konnten keine Datensätze gelöscht werden. Fehler bei DB-DELETE.";
            }
            
        }

        private void CreateCSV(clsOrderExtended _myOrder, List<clsProductExtended> _myProductList)
        {
            StringBuilder _toCSV = new StringBuilder();

            _toCSV.Append("Bestellung: #" + _myOrder.OrderNumber);
            _toCSV.AppendLine();
            _toCSV.Append("Bestellnummer;Kunde;Lieferzeitpunkt;Lieferung;Endstatus;Gesamtsumme");
            _toCSV.AppendLine();
            _toCSV.Append(_myOrder.OrderNumber);
            _toCSV.Append(';');
            _toCSV.Append(_myOrder.UserName);
            _toCSV.Append(';');
            _toCSV.Append(_myOrder.OrderDeliveryDate);
            _toCSV.Append(';');
            _toCSV.Append(_myOrder.OrderDelivery);
            _toCSV.Append(';');
            _toCSV.Append(_myOrder.OrderStatusDescription);
            _toCSV.Append(';');
            _toCSV.Append(String.Format("{0:N}", _myOrder.OrderSum));
            _toCSV.Append(" EUR");
            _toCSV.AppendLine();
            _toCSV.Append("Enthaltene Produkte:");
            _toCSV.AppendLine();
            _toCSV.Append("Produktname;Preis pro Einheit;Größe;Produktkategorie;Enthaltene Extras;Preis des Produkts");
            _toCSV.AppendLine();

            foreach (clsProductExtended _myProduct in _myProductList)
            {
                double _productPrice = _myProduct.Size * _myProduct.PricePerUnit;
                _toCSV.Append(_myProduct.Name);
                _toCSV.Append(';');
                _toCSV.Append(String.Format("{0:N}", _myProduct.PricePerUnit));
                _toCSV.Append(" EUR");
                _toCSV.Append(';');
                _toCSV.Append(_myProduct.Size);
                _toCSV.Append(';');
                _toCSV.Append(_myProduct.Category);
                _toCSV.Append(';');

                double _extrasPrice = 0.0;
                foreach (clsExtra _myExtra in _myProduct.ProductExtras)
                {
                    _extrasPrice += _myExtra.Price;
                    _toCSV.Append(_myExtra.Name);
                    _toCSV.Append(" ");
                }
                _toCSV.Append(';');
                _productPrice += _extrasPrice;
                _toCSV.Append(String.Format("{0:N}", _productPrice));
                _toCSV.Append(" EUR");
                _toCSV.AppendLine();
            }
            _toCSV.AppendLine();
            _toCSV.Append("Exportiert am " + DateTime.Now + " Uhr");

            Session["Report"] = _toCSV;
            Session["ReportFileName"] = "export - Order" + _myOrder.OrderNumber + " " + DateTime.Now + ".csv";
        }

        protected void btDownloadCSV_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Session["ReportFileName"].ToString());
            Response.ContentEncoding = Encoding.GetEncoding("iso-8859-15");
            Response.ContentType = "Text/vnd.ms-excel";
            Response.Write(Session["Report"]);
            Response.End();
            Session["Report"] = Session["ReportFileName"] = null;
        }
    }
}

