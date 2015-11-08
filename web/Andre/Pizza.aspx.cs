using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Andre
{
    public partial class PizzaCode : System.Web.UI.Page
    {

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
                CheckBoxList extraCheckList =  (CheckBoxList)gvPizza.SelectedRow.FindControl("ExtrasCheckBoxList");
                foreach (ListItem item in extraCheckList.Items)
                {
                    if (item.Selected)
                    {
                        gvPizza.SelectedRow.Cells[7].Text += item.Text + "\n";
                        item.Selected = false;
                    }
                }
                lblChooseSize.Text = "";

                ((ArrayList)Session["selProducts"]).Add(Andre.Master.cloneRow(gvPizza.SelectedRow));

                ((DropDownList)gvPizza.SelectedRow.FindControl("sizeSelect")).SelectedValue = "";
                Session["lastSelectedSize"] = "";
                gvPizza.SelectedRow.Cells[8].Text = "";
                gvPizza.SelectedRow.Cells[7].Text = "";
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


        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                lblChooseSize.ForeColor = System.Drawing.Color.Red;
            }

            Session["category"] = 1;
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
    
        }

    }
}