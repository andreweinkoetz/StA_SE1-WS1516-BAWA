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

        protected override void OnLoadComplete(EventArgs e)
        {
            enableUI();
            base.OnLoadComplete(e);
        }

        internal void enableUI()
        {
            if (Session["roleID"] == null)
            {
                GridView2.Columns[0].Visible = false;
                GridView2.Columns[6].Visible = false;
            }
            else
            {
                GridView2.Columns[0].Visible = true;
                GridView2.Columns[6].Visible = true;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GridView2.SelectedRow.Cells[6].Text != "")
            {
                lblChooseSize.Text = "";
                ((List<GridViewRow>)Session["selPizza"]).Add(GridView2.SelectedRow);
                GridView2.SelectedRow.Cells[7].Text = "";
            } else
            {
                lblChooseSize.Text = "Bitte wählen Sie eine Größe!";
                
            }
            
           
            
            //TextBox1.Text += GridView2.SelectedRow.Cells[2].Text; 
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["selPizza"] = new List<GridViewRow>();
                lblChooseSize.ForeColor = System.Drawing.Color.Red;
            } 

            Session["category"] = 1;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drop1 = (DropDownList)sender;
            GridViewRow test = (GridViewRow)drop1.Parent.Parent;
            test.Cells[6].Text = drop1.SelectedItem.Text;
            double size = Double.Parse(drop1.SelectedItem.Value);
            double prizePerUnit = Double.Parse(test.Cells[3].Text);

            test.Cells[7].Text = size * prizePerUnit + " EUR";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["roleID"] = 1;
        }

        
    }
}