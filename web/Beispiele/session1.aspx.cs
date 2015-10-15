using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Beispiele
{
    public partial class session1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] != null)
            {   // Session["CurrentUser"] ist mit dem zuletzt angemeldeten Benutzer belegt
                bll.clsUser _currentUser = (bll.clsUser) Session["CurrentUser"];
                lblUser.Text = "Hallo " + _currentUser.Name + " aus " + _currentUser.Address + "!";
            }
            else
            {   // bisher niemend angemeldet
                lblUser.Text = "Hello, Who are you? ";
             }  
        }
    
        /// <summary>
        /// Aus DropDown-Liste den ausgewählten User (bzw. dessen Uid) lesen, User-Objekt aus DB holen und in Session-Variable speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bll.clsUserFacade _myUsers = new bll.clsUserFacade();
            bll.clsUser _currentUser = _myUsers.UserGetById(Convert.ToInt32(ddlUsers.SelectedValue));
            Session["CurrentUser"] = _currentUser;
            lblUser.Text = "Hallo " + _currentUser.Name + " aus " + _currentUser.Address + "!";
        }
    }
}