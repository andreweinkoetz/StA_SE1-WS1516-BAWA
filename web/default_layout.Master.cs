using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bll;

namespace web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Warenkorb (Session) Handling
            if (Session["selProducts"] == null)
            {
                Session["selProducts"] = new List<clsProductExtended>();
            }  
        }
    }
}