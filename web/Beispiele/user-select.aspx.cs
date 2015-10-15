using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bll;

namespace web.beispiele
{
    public partial class user_select : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Ereignis beim Klick auf den Auswahlknopf zur DropDownList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDropDownSelect_Click(object sender, EventArgs e)
        {
            clsUser _myUser = new clsUser();
            clsUserFacade _Users = new clsUserFacade();

            // ddlUser.SelectedValue enthält die UID des ausgewälten Users
            int _id = Convert.ToInt32(ddlUser.SelectedValue);

            _myUser = _Users.UserGetById(_id);

            if (_myUser != null)
                lblMsg.Text = "Auswahl in DropDownList1: " + _myUser.Name + " " + _myUser.Address;
            else
                lblMsg.Text = "Auswahl in DropDownList1 nicht gefunden: " + _id.ToString();
        }  // btnDropDownSelect_Click()

        /// <summary>
        /// Ereignis bei DropDownList mit Autopostback, Code derselbe wie oben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUsersWithAutoPostback_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsUser _myUser = new clsUser();
            clsUserFacade _Users = new clsUserFacade();

            // ddlUser.SelectedValue enthält die UID des ausgewälten Users
            int _id = Convert.ToInt32(ddlUsersWithAutoPostback.SelectedValue);

            _myUser = _Users.UserGetById(_id);

            if (_myUser != null)
                lblMsg.Text = "Auswahl in DropDownList2: " + _myUser.Name + " " + _myUser.Address;
            else
                lblMsg.Text = "Auswahl in DropDownList2 nicht gefunden: " + _id.ToString();

        }  // ddlUsersWithAutoPostback_SelectedIndexChanged()
    }
}