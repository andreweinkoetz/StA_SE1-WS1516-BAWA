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
    public partial class Dessert_Code : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            Session["category"] = 3;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["selectedDessert"] = null;
                FillInfoText();
            }
        }

        private void FillInfoText()
        {
            if (Session["userID"] == null)
            {
                lblInfoDessert.Text += "<br />Melden Sie sich gleich an und bestellen Sie.";
            }
            else
            {
                lblInfoDessert.Text += "<br />Wählen Sie das Dessert und legen Sie es über das Einkaufswagen-Symbol in Ihren Warenkorb.";
            }
        }

        protected void gvDesserts_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
        }

        private void EnableSelection()
        {
            gvDesserts.Columns[0].Visible = Session["userID"] != null;
        }

        protected void gvDesserts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedDessert();
        }

        private void GetSelectedDessert()
        {
            GridViewRow selectedRow = gvDesserts.SelectedRow;
            int _id = Int32.Parse(selectedRow.Cells[1].Text);

            //Desserts werden ausschließlich in Stück verkauft. Daher ist die Größe = 1!
            clsProductExtended _myProduct = clsProductExtended.ProductFactory(_id);
            _myProduct.Size = 1;

            DessertSelected(_myProduct);
        }

        private void DessertSelected(clsProductExtended _myProduct)
        {
            Session["selectedDessert"] = _myProduct;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Anzahl");

            dt.LoadDataRow(new object[] { _myProduct.Id, _myProduct.Name, null }, true);

            gvDessertToOrder.DataSource = dt;
            gvDessertToOrder.DataBind();
            gvDessertToOrder.SelectedIndex = -1;
            gvDessertToOrder.Visible = true;
            lblGvToOrder.Visible = true;
        }

        protected void gvDessertToOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow _selectedRow = gvDessertToOrder.SelectedRow;
            TextBox txtAmount = (TextBox)_selectedRow.FindControl("txtAmount");
            DessertToCart(Int32.Parse(txtAmount.Text));
        }

        private void DessertToCart(int _amount)
        {
            clsProductExtended _myProduct = (clsProductExtended)Session["selectedDessert"];

            for (int i = 0; i < _amount; i++)
            {
                ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);
            }

            Session["selectedDessert"] = null;
            gvDessertToOrder.Visible = false;
            lblGvToOrder.Visible = false;
            gvDesserts.SelectedIndex = -1;
        }
    }
}