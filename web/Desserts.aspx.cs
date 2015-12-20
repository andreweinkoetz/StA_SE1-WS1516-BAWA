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

        protected override void OnLoadComplete(EventArgs e)
        {
            enableUI();
            base.OnLoadComplete(e);
        }

        private void enableUI()
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
            clsProductExtended _myProduct = new clsProductExtended();
            GridViewRow selectedRow = gvDesserts.SelectedRow;

            _myProduct.Id = Int32.Parse(selectedRow.Cells[1].Text);
            _myProduct.Name = selectedRow.Cells[2].Text;
            _myProduct.PricePerUnit = Double.Parse(selectedRow.Cells[3].Text.Substring(0, selectedRow.Cells[3].Text.IndexOf('€')));
            //Desserts werden ausschließlich in Stück verkauft. Daher ist die Größe = 1!
            _myProduct.Size = 1.0;
            _myProduct.CID = (int)Session["category"];

            ((List<clsProductExtended>)Session["selProducts"]).Add(_myProduct);
        }


        protected void lnkBtCart_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 2;
            Session["userID"] = 1;
        }
    }
}