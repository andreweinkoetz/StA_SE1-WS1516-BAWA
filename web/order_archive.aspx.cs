using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class OrderArchive_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["roleID"] == null)
            {
                Response.Redirect("login_page.aspx");
            }
            else if ((int)Session["roleID"] >= 2)
            {
                Response.Redirect("administration.aspx");
            }

        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("administration.aspx");
        }

        protected void gvOrderArchive_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArchiveOrderAndGenerateReport(Int32.Parse(gvOrderArchive.SelectedRow.Cells[2].Text));

        }

        private void ArchiveOrderAndGenerateReport(int _oNumber)
        {
            clsOrderFacade _orderFacade = new clsOrderFacade();
            clsOrderExtended _myOrder = _orderFacade.GetOrderByOrderNumber(_oNumber);
            _orderFacade = new clsOrderFacade();
            Session["ReportFileName"] = "export - Order" + _oNumber + " " + DateTime.Now + ".csv";
            Session["Report"] = _myOrder.CreateCSVString();
            int _deletedRecords = _orderFacade.DeleteOrderByOrderNumber(_oNumber);
            if (_deletedRecords > 0)
            {
                lblError.ForeColor = System.Drawing.Color.Black;
                lblError.Text = "Es wurden " + _deletedRecords + " Datensätze erfolgreich gelöscht.";
                gvOrderArchive.DataBind();
                btDownloadCSV.Visible = true;
            }
            else
            {
                lblError.Text = "Es konnten keine Datensätze gelöscht werden. Fehler bei DB-DELETE.";
            }
        }

        protected void btDownloadCSV_Click(object sender, EventArgs e)
        {
            DownloadCSV();
        }

        private void DownloadCSV()
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Session["ReportFileName"].ToString());
            Response.ContentEncoding = Encoding.GetEncoding("iso-8859-15");
            Response.ContentType = "Text/vnd.ms-excel";
            Response.Write(Session["Report"]);
            Response.End();
            Session["Report"] = Session["ReportFileName"] = null;
        }
    }
}

