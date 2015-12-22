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
            string selectedSize = (string)Session["lastSelectedSize"];

            if (!String.IsNullOrEmpty(selectedSize))
            {
                GridViewRow selectedRow = gvPizza.SelectedRow;

                int _id = Int32.Parse(selectedRow.Cells[1].Text);
                double _size = Double.Parse(selectedSize);

                clsProductExtended _myProduct;
                List<clsExtra> _myExtraList;

                List<int> _extraIds = new List<int>();
                CheckBoxList extraCheckList =  (CheckBoxList)selectedRow.FindControl("ExtrasCheckBoxList");

                foreach (ListItem item in extraCheckList.Items)
                {
                    if (item.Selected)
                    {
                        //clsExtraFacade _myExtraFacade = new clsExtraFacade();
                        //int _eID = Int32.Parse(item.Value);
                        //double _priceOfExtra = _myExtraFacade.GetPriceOfExtra(_eID);
                        //_myExtraList.Add(new clsExtra(_eID, item.Text,_priceOfExtra));
                        _extraIds.Add(Int32.Parse(item.Value));
                        item.Selected = false;
                    }
                }

                _myExtraList = clsExtra.ExtraListFactory(_extraIds.ToArray());
                _myProduct = clsProductExtended.PizzaFactory(_id, _size, _myExtraList);

                //_myProduct.ProductExtras = _myExtraList;
                //_myProduct.Id = Int32.Parse(selectedRow.Cells[1].Text);
                //_myProduct.Name = selectedRow.Cells[2].Text;
                //_myProduct.PricePerUnit = Double.Parse(selectedRow.Cells[3].Text.Substring(0, selectedRow.Cells[3].Text.IndexOf('€')));
                //_myProduct.Size = Double.Parse(selectedSize);
                //_myProduct.CID = (int)Session["category"];

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

        protected void gvPizza_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
        }
    }
}