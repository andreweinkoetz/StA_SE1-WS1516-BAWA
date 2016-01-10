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
    public partial class Beverage_Code : System.Web.UI.Page
    {

        protected override void OnInit(EventArgs e)
        {
            Session["category"] = 2;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["selectedBeverage"] = null;
                FillInfoText();
            }
        }

        /// <summary>
        /// Prüft ob Gast oder Benutzer und füllt 
        /// dementsprechend den Informationstext.
        /// </summary>
        private void FillInfoText()
        {
            if (Session["userID"] == null)
            {
                lblInfoBeverages.Text += "<br />Melden Sie sich gleich an und bestellen Sie.";
            }
            else
            {
                lblInfoBeverages.Text += "<br />Zunächst wählen Sie das Getränk über das Hand-Symbol. <br />"
                                        + "Sie können nun am Fuße der Seite die Größe sowie Anzahl gleicher Getränke festlegen und "
                                        + "das gewünschte Getränk über das Einkaufswagen-Symbol in Ihren Warenkorb legen.";
            }
        }

        protected void gvBeverages_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
        }

        /// <summary>
        /// Deaktivieren des Auswahl-Buttons für Gäste.
        /// </summary>
        private void EnableSelection()
        {
            gvBeverages.Columns[0].Visible = Session["userID"] != null;
        }

        protected void gvBeverage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedBeverage();
        }

        /// <summary>
        /// Getränk, welches gewählt wurde aus DB lesen und als Objekt weitergeben.
        /// </summary>
        private void GetSelectedBeverage()
        {
            //Gewählte Getränkzeile ermitteln.
            GridViewRow selectedRow = gvBeverages.SelectedRow;

            //ID des Getränks ermitteln.
            int _id = Int32.Parse(selectedRow.Cells[1].Text);

            //Gewähltes Produkt in Auswahlmenü legen.
            BeverageSelected(clsProductExtended.ProductFactory(_id));

        }

        /// <summary>
        /// Getränk-Objekt in Auswahl-GridView darstellen.
        /// </summary>
        /// <param name="_myProduct">Getränk-Objekt</param>
        private void BeverageSelected(clsProductExtended _myProduct)
        {
            Session["selectedBeverage"] = _myProduct;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Größe");
            dt.Columns.Add("Anzahl");

            dt.LoadDataRow(new object[] { _myProduct.Id, _myProduct.Name, null, null }, true);

            gvBeverageToOrder.DataSource = dt;
            gvBeverageToOrder.DataBind();
            gvBeverageToOrder.SelectedIndex = -1;
            gvBeverageToOrder.Visible = true;
            lblGvToOrder.Visible = true;
        }

        protected void gvBeverageToOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow _selectedRow = gvBeverageToOrder.SelectedRow;
            TextBox txtAmount = (TextBox)_selectedRow.FindControl("txtAmount");
            DropDownList sizeSelect = (DropDownList)_selectedRow.FindControl("sizeSelect");
            if (sizeSelect.SelectedIndex > 0)
            {
                BeverageToCart(Int32.Parse(txtAmount.Text), Double.Parse(sizeSelect.SelectedValue));
                lblChooseSize.Visible = false;
            }
            else
            {
                lblChooseSize.Visible = true;
            }
        }

        /// <summary>
        /// Ausgewähltes Produkt in bestimmter Größe und 
        /// Anzahl in den Warenkorb legen.
        /// </summary>
        /// <param name="_amount">Anzahl der Produkte</param>
        /// <param name="_size">Größe des Produkts</param>
        private void BeverageToCart(int _amount, double _size)
        {
            clsProductExtended _myProduct = (clsProductExtended)Session["selectedBeverage"];
            _myProduct.Size = _size;

            for (int i = 0; i < _amount; i++)
            {
                ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);
            }

            Session["selectedBeverage"] = null;
            gvBeverageToOrder.Visible = false;
            lblGvToOrder.Visible = false;
            gvBeverages.SelectedIndex = -1;
        }
    }
}