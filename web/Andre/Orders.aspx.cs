using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Andre
{
    public partial class Orders : System.Web.UI.Page
    {
        private ArrayList selectedProducts;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["selProducts"] != null)
            {
                selectedProducts = (ArrayList)Session["selProducts"];
                initializeOrderView(selectedProducts);
            }

        }

        private void initializeOrderView(ArrayList showInGridView)
        {

            DataTable dt = new DataTable();

            dt = new DataTable("MyOrder");

            dt.Columns.Add("ID");

            dt.Columns.Add("Name");

            dt.Columns.Add("Größe");

            dt.Columns.Add("Extras");

            dt.Columns.Add("Preis");

            foreach (GridViewRow row in showInGridView)
            {
                dt.LoadDataRow(new object[] { row.Cells[1].Text, row.Cells[2].Text, row.Cells[6].Text, row.Cells[7].Text, row.Cells[8].Text }, true);
            }

            gvOrder.DataSource = dt;
            gvOrder.DataBind();
        }

        protected void clearCart_Click(object sender, EventArgs e)
        {
            Session["selProducts"] = null;
        }
    }
}