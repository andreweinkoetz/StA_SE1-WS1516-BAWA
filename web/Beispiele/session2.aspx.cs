using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Beispiele
{
    public partial class session2 : System.Web.UI.Page
    {
        private int _uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] != null)
            {   // jemand hat sich auf einer anderen Seite angemeldet
                bll.clsUser _currentUser = (bll.clsUser) Session["CurrentUser"];
                lblUser.Text = "Hallo, Sie sind " + _currentUser.Name + " aus " + _currentUser.Address + "!";
                _uid = _currentUser.ID;

            }
            else
            {   // Session["CurrentUser"] nicht gesetzt
                lblUser.Text = "Hello, Who are you? ";
                _uid = 0;
            }
            Session["UID"] = _uid;   
        } // Page_Load()

    }
}