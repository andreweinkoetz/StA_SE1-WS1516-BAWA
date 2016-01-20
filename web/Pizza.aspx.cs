using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using bll;
using System.Data;

namespace web
{
    public partial class PizzaCode : System.Web.UI.Page
    {

        protected override void OnInit(EventArgs e)
        {
            Session["category"] = 1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["selectedPizza"] = null;
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
                lblInfoPizza.Text += "<br />Melden Sie sich gleich an und bestellen Sie.";
            }
            else
            {
                lblInfoPizza.Text += "<br />Wählen Sie zunächst den Extrabelag der Pizza (Zusatzkosten!). "
                    + " Anschließend wählen Sie die Pizza über das Hand-Symbol. <br />"
                    + "Sie können nun am Fuße der Seite die Größe sowie Anzahl festlegen und danach "
                    + "die Pizza über das Einkaufswagen-Symbol in Ihren Warenkorb legen.";
            }
        }

        protected void gvPizza_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
        }

        /// <summary>
        /// Erstellt die Datenquelle für die Extra CheckBoxen.
        /// </summary>
        /// <returns>DataTable als DataSource für CheckBoxListe</returns>
        private DataTable GetExtraDataTable()
        {
            List<clsExtra> _extras = GetAllActiveExtrasForCheckBox();

            DataTable dt = new DataTable();
            dt.Columns.Add("EID");
            dt.Columns.Add("EName");

            foreach (clsExtra _extra in _extras)
            {
                String _nameAndPrice = _extra.Name + " (" + String.Format("{0:C}", _extra.Price) + ")";
                dt.LoadDataRow(new object[] { _extra.ID, _nameAndPrice }, true);
            }

            return dt;
        }

        /// <summary>
        /// Liefert eine Liste aller aktiven Extras aus der DB.
        /// </summary>
        /// <returns>Liste aller aktiven Extras</returns>
        private List<clsExtra> GetAllActiveExtrasForCheckBox()
        {
            return new clsExtraFacade().GetAllActiveExtras();
        }

        /// <summary>
        /// Deaktivieren des Auswahl-Buttons und der Extra-Auswahl für Gäste.
        /// </summary>
        private void EnableSelection()
        {
            gvPizza.Columns[0].Visible = gvPizza.Columns[6].Visible = Session["userID"] != null;
        }

        protected void gvPizza_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Falls keine Pizza in den Warenkorb gelegt wurde, aber eine andere ausgewählt wurde.
            EnableExtrasCheckBoxList();

            //Ausgewählte Pizza weiter bearbeiten.
            GetSelectedPizza();
        }

        /// <summary>
        /// Reaktiviert alle CheckBoxen.
        /// </summary>
        private void EnableExtrasCheckBoxList()
        {
            foreach (GridViewRow _row in gvPizza.Rows)
            {
                CheckBoxList extraCheckList = (CheckBoxList)_row.FindControl("ExtrasCheckBoxList");
                extraCheckList.Enabled = true;
            }
        }

        /// <summary>
        /// Pizza, welche gewählt wurde aus DB lesen und als Objekt weitergeben.
        /// </summary>
        private void GetSelectedPizza()
        {
            //Gewählte Pizzazeile.
            GridViewRow selectedRow = gvPizza.SelectedRow;

            //ID der Pizza ermitteln.
            int _id = Int32.Parse(selectedRow.Cells[1].Text);

            //Gewählte Extras ermitteln.
            List<int> _extraIds = new List<int>();
            CheckBoxList extraCheckList = (CheckBoxList)selectedRow.FindControl("ExtrasCheckBoxList");

            //Ausgewählte Extras zur Liste hinzufügen.
            foreach (ListItem item in extraCheckList.Items)
            {
                if (item.Selected)
                {
                    _extraIds.Add(Int32.Parse(item.Value));
                    item.Selected = false;
                }
            }

            //Liste aller Extras der gewählten Pizza erstellen und Pizza erstellen.
            List<clsExtra> _myExtraList = clsExtra.ExtraListFactory(_extraIds.ToArray());
            clsProductExtended _myProduct = clsProductExtended.PizzaFactory(_id, _myExtraList);

            //Deaktivieren der nachträglichen Extra-Wahl
            extraCheckList.Enabled = false;

            //Weitergabe an Größen/Anzahlauswahl.
            PizzaSelected(_myProduct);

        }

        /// <summary>
        /// Pizza-Objekt in Auswahl-GridView darstellen.
        /// </summary>
        /// <param name="_myProduct">Pizza-Objekt</param>
        private void PizzaSelected(clsProductExtended _myProduct)
        {
            Session["selectedPizza"] = _myProduct;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Größe");
            dt.Columns.Add("Extras");
            dt.Columns.Add("Anzahl");

            String selExtras = "";

            foreach (clsExtra _myExtra in _myProduct.ProductExtras)
            {
                selExtras += _myExtra.Name + " ";
            }

            dt.LoadDataRow(new object[] { _myProduct.Id, _myProduct.Name, null, selExtras, null }, true);

            gvPizzaToOrder.DataSource = dt;
            gvPizzaToOrder.DataBind();
            gvPizzaToOrder.SelectedIndex = -1;
            gvPizzaToOrder.Visible = true;
            lblGvToOrder.Visible = true;
        }

        protected void gvPizzaToOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow _selectedRow = gvPizzaToOrder.SelectedRow;
            TextBox txtAmount = (TextBox)_selectedRow.FindControl("txtAmount");
            DropDownList sizeSelect = (DropDownList)_selectedRow.FindControl("sizeSelect");
            if (sizeSelect.SelectedIndex > 0)
            {
                PizzaToCart(Int32.Parse(txtAmount.Text), Double.Parse(sizeSelect.SelectedValue));
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
        private void PizzaToCart(int _amount, double _size)
        {

            clsProductExtended _myProduct = (clsProductExtended)Session["selectedPizza"];
            _myProduct.Size = _size;

            for (int i = 0; i < _amount; i++)
            {
                ((List<clsProductExtended>)Session["selProducts"]).Add(new clsProductExtended(_myProduct));
            }

            Session["selectedPizza"] = null;
            gvPizzaToOrder.Visible = false;
            lblGvToOrder.Visible = false;
            gvPizza.SelectedIndex = -1;

            EnableExtrasCheckBoxList();
        }

        protected void ExtrasCheckBoxList_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((CheckBoxList)sender).DataSource = GetExtraDataTable();
                ((CheckBoxList)sender).DataBind();
            }
        }
    }
}