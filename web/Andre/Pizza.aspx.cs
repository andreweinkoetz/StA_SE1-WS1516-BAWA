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

        internal void enableUI()
        {
            if (Session["roleID"] == null)
            {
                gvPizza.Columns[0].Visible = false;
                gvPizza.Columns[6].Visible = false;
                gvPizza.Columns[7].Visible = false;
                gvPizza.Columns[8].Visible = false;
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
                        _myExtraList.Add(new clsExtra((Int32.Parse(item.Value)), item.Text));
                        selectedRow.Cells[7].Text += item.Text + "\n";
                        item.Selected = false;
                    }
                }

                _myProduct.ProductExtras = _myExtraList;
                _myProduct.Id = Int32.Parse(selectedRow.Cells[1].Text);
                _myProduct.Name = selectedRow.Cells[2].Text;
                _myProduct.PricePerUnit = Double.Parse(selectedRow.Cells[3].Text);
                _myProduct.Size = Int32.Parse(selectedSize);

                lblChooseSize.Text = "";

                ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);

                //((ArrayList)base.Session["selProducts"]).Add(Andre.Master.cloneRow(selectedRow));

                Session["lastSelectedSize"] = "";
                selectedRow.Cells[8].Text = null;
                selectedRow.Cells[7].Text = null;
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
            double size = Double.Parse(sizeSelect.SelectedItem.Value);
            double prizePerUnit = Double.Parse(selRow.Cells[3].Text);
            double numberOfExtras = countExtrasOfPizza(selRow);

            selRow.Cells[8].Text = (size * prizePerUnit + numberOfExtras*0.5) + " EUR";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 2;
            Session["userID"] = 1;
    
        }

    }
}