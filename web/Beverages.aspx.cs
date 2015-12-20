using bll;
using System;
using System.Collections.Generic;
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
            if (!IsPostBack)
            {
                lblChooseSize.ForeColor = System.Drawing.Color.Red;
            }

            Session["category"] = 2;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            enableUI();
            base.OnLoadComplete(e);
        }

        private void enableUI()
        {
            if (Session["roleID"] == null)
            {
                gvBeverages.Columns[0].Visible = false;
                gvBeverages.Columns[6].Visible = false;
            }
            else
            {
                gvBeverages.Columns[0].Visible = true;
                gvBeverages.Columns[6].Visible = true;
            }
        }

        protected void gvBeverage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSize = (string)Session["lastSelectedSize"];

            if (!String.IsNullOrEmpty(selectedSize))
            {
                clsProductExtended _myProduct = new clsProductExtended();
                GridViewRow selectedRow = gvBeverages.SelectedRow;

                _myProduct.Id = Int32.Parse(selectedRow.Cells[1].Text);
                _myProduct.Name = selectedRow.Cells[2].Text;
                _myProduct.PricePerUnit = Double.Parse(selectedRow.Cells[3].Text.Substring(0, selectedRow.Cells[3].Text.IndexOf('€')));
                _myProduct.Size = Double.Parse(selectedSize);
                _myProduct.CID = (int)Session["category"];

                lblChooseSize.Text = "";

                ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);

                Session["lastSelectedSize"] = "";

            }
            else
            {
                lblChooseSize.Text = "Bitte wählen Sie eine Größe!";

            }

        }

        protected void sizeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList sizeSelect = (DropDownList)sender;
            GridViewRow selRow = (GridViewRow)sizeSelect.Parent.Parent;
            selRow.Cells[6].Text = sizeSelect.SelectedItem.Text;
            Session["lastSelectedSize"] = sizeSelect.SelectedItem.Value; 
        }

        protected void lnkBtCart_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 2;
            Session["userID"] = 1;
        }
    }
}