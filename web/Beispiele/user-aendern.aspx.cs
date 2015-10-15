using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.beispiele
{
    public partial class user_aendern : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Ereignis beim Klicken auf den Auswahlknopf: 
        /// Redirect auf aendern1-Seite mit uid als Parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _uid = grdUsers.SelectedRow.Cells[0].Text;       // 1. Spalte in gridView enthält UID
            Response.Redirect("user-aendern1.aspx?uid=" + _uid);    // user-aendern1.aspx aufrufen, dabei _uid als Parameter übergeben
        }

    }
}