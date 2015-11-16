using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bll;

namespace web.Andre
{
    public partial class PizzaCode : System.Web.UI.Page
    {

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                lblChooseSize.ForeColor = System.Drawing.Color.Red;
            }

            Session["category"] = 1;
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
                gvPizza.Columns[0].Visible = false;
                gvPizza.Columns[6].Visible = false;
                gvPizza.Columns[7].Visible = false;
            }
            else
            {
                gvPizza.Columns[0].Visible = true;
                gvPizza.Columns[6].Visible = true;
                gvPizza.Columns[7].Visible = true;
            }
        }

        protected void gvPizza_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSize = (string)Session["lastSelectedSize"];

            if (!String.IsNullOrEmpty(selectedSize))
            {
                clsProductExtended _myProduct = new clsProductExtended();
                List<clsExtra> _myExtraList = new List<clsExtra>();
                GridViewRow selectedRow = gvPizza.SelectedRow;
                CheckBoxList extraCheckList =  (CheckBoxList)selectedRow.FindControl("ExtrasCheckBoxList");
                foreach (ListItem item in extraCheckList.Items)
                {
                    if (item.Selected)
                    {
                        clsExtraFacade _myExtraFacade = new clsExtraFacade();
                        int _eID = Int32.Parse(item.Value);
                        double _priceOfExtra = _myExtraFacade.getPriceOfExtra(_eID);
                        _myExtraList.Add(new clsExtra(_eID, item.Text,_priceOfExtra));
                        item.Selected = false;
                    }
                }

                _myProduct.ProductExtras = _myExtraList;
                _myProduct.Id = Int32.Parse(selectedRow.Cells[1].Text);
                _myProduct.Name = selectedRow.Cells[2].Text;
                _myProduct.PricePerUnit = Double.Parse(selectedRow.Cells[3].Text.Substring(0, selectedRow.Cells[3].Text.IndexOf('€')));
                _myProduct.Size = Double.Parse(selectedSize);
                _myProduct.Category = Session["category"].ToString();

                lblChooseSize.Text = "";

                ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);

                Session["lastSelectedSize"] = "";

            }
            else
            {
                lblChooseSize.Text = "Bitte wählen Sie eine Größe!";
            }
        }

        

        private double countExtrasOfPizza(GridViewRow selRow)
        {
            CheckBoxList extraCheckList = (CheckBoxList)selRow.FindControl("ExtrasCheckBoxList");
            double extrasOfPizza = 0.0;
            foreach (ListItem item in extraCheckList.Items)
            {
                if (item.Selected)
                    extrasOfPizza++;
            }
            return extrasOfPizza;
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