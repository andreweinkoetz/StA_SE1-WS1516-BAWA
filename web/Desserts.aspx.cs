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
            //Nichts zu beachten.
        }

        protected void gvDesserts_DataBound(object sender, EventArgs e)
        {
            EnableSelection();
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
            SelectedDessertToCart();
        }

        private void SelectedDessertToCart()
        {
            GridViewRow selectedRow = gvDesserts.SelectedRow;
            int _id = Int32.Parse(selectedRow.Cells[1].Text);

            //Desserts werden ausschließlich in Stück verkauft. Daher ist die Größe = 1!
            clsProductExtended _myProduct = clsProductExtended.ProductFactory(_id, 1);

            ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);
        }
    }
}