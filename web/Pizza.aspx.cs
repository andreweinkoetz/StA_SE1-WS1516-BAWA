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
                    + "Sie können nun am Fuße der Seite die Größe sowie Anzahl gleicher Pizzen festlegen und "
                    + "die gewünschte Pizza über das Einkaufswagen-Symbol in Ihren Warenkorb legen.";
            }
        }

        protected void gvPizza_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
        }

        private void EnableSelection()
        {
            gvPizza.Columns[0].Visible = gvPizza.Columns[6].Visible = Session["userID"] != null;
        }

        protected void gvPizza_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPizza();
        }

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

            //Weitergabe an Größen/Anzahlauswahl.
            PizzaSelected(_myProduct);

        }

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

        private void PizzaToCart(int _amount, double _size)
        {

            clsProductExtended _myProduct = (clsProductExtended)Session["selectedPizza"];
            _myProduct.Size = _size;

            for (int i = 0; i < _amount; i++)
            {
                ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);
            }

            Session["selectedPizza"] = null;
            gvPizzaToOrder.Visible = false;
            lblGvToOrder.Visible = false;
            gvPizza.SelectedIndex = -1;
        }
    }
}