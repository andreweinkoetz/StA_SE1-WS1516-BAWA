using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bll;

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
            //Nichts zu beachten.
        }

        protected void gvPizza_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
        }

        private void EnableSelection()
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
            SelectedPizzaToCart();
        }

        private void SelectedPizzaToCart()
        {
            string selectedSize = (string)Session["lastSelectedSize"];

            if (!String.IsNullOrEmpty(selectedSize))
            {
                GridViewRow selectedRow = gvPizza.SelectedRow;

                int _id = Int32.Parse(selectedRow.Cells[1].Text);
                double _size = Double.Parse(selectedSize);

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
                clsProductExtended _myProduct = clsProductExtended.PizzaFactory(_id, _size, _myExtraList);

                lblChooseSize.Text = "";

                //Ablage in Warenkorb.
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


    }
}