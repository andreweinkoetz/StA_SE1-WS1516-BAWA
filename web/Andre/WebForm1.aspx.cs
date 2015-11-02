using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Andre
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 1;
            //Server.Transfer("WebForm2.aspx");
            
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((List<GridViewRow>)Session["selPizza"]).Add(GridView2.SelectedRow);
           
            
            TextBox1.Text += GridView2.SelectedRow.Cells[2].Text; 
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["selPizza"] = new List<GridViewRow>();
                
            }

            Session["category"] = 1;
        }

    }
}