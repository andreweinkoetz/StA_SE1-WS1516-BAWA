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
    public partial class WebForm2 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList andre = new ArrayList(((List<GridViewRow>)Session["selPizza"]));


            DataTable dt = new DataTable();

            if (dt != null)
            {

                dt = new DataTable("MyTable");

                dt.Columns.Add("Col1");

                dt.Columns.Add("Col2");

                dt.Columns.Add("Col3");

                dt.Columns.Add("Col4");

            }

            foreach (GridViewRow item in andre)
            {
                DropDownList drop1 = new DropDownList();

               

               dt.LoadDataRow(new object[] { item.Cells[1].Text,item.Cells[2].Text,item.Cells[6].Text,item.Cells[7].Text }, true);
            }
            

            
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, EventArgs e)
        {

          
        }

    }
}