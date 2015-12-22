using bll;
using System;
using System.Collections.Generic;
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

        }

        private void EnableSelection()
        {
            if (Session["roleID"] == null)
            {
                gvDesserts.Columns[0].Visible = false;
            }
            else
            {
                gvDesserts.Columns[0].Visible = true;
            }
        }

        protected void gvDesserts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = gvDesserts.SelectedRow;
            int _id = Int32.Parse(selectedRow.Cells[1].Text);

            //Desserts werden ausschließlich in Stück verkauft. Daher ist die Größe = 1!
            clsProductExtended _myProduct = clsProductExtended.ProductFactory(_id, 1);


            //_myProduct.Id = Int32.Parse(selectedRow.Cells[1].Text);
            //_myProduct.Name = selectedRow.Cells[2].Text;
            //_myProduct.PricePerUnit = Double.Parse(selectedRow.Cells[3].Text.Substring(0, selectedRow.Cells[3].Text.IndexOf('€')));

            //_myProduct.Size = 1.0;
            //_myProduct.CID = (int)Session["category"];

            ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);
        }

        protected void gvDesserts_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
        }
    }
}