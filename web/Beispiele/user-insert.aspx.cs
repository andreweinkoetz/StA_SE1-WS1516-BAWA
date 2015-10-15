using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.beispiele
{
    public partial class user_insert : System.Web.UI.Page
    {
        /// <summary>
        /// Page_Load(): wird zuerst aufgerufen (vor btnInsert_Click())
        ///   Falls Erstaufruf der Seite (GET), soll Checkbox chkIsActive mit true vorbelegt werden.
        ///   Falls Aufruf der Seite wg. Insert-Button, dann Checkbox chkIsActive nicht ändern, sondern was immer der User ausgewählt hat weitergeben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                chkIsActive.Checked = true;
            // else: Postback: User hat auf btnInsert geklickt, Page_Load() soll dann nichts tun
        }

        /// <summary>
        /// wird bei Klick des Einfügen-Knopfs aufgerufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            bll.clsUser newUser = new bll.clsUser();
            // bll.clsUserFacade _User = new bll.clsUserFacade();

            newUser.Name = txtName.Text;
            newUser.Address = txtAddress.Text;
            newUser.Role = Convert.ToInt32(rblRolle.SelectedValue);
            newUser.IsActive = chkIsActive.Checked;
            newUser.Password = txtPassword.Text;

            if (newUser.Insert())
                lblMsg.Text = "Insert von " + newUser.Name + " erfolgreich!";
            else
                lblMsg.Text = "Insert von " + newUser.Name + " NICHT erfolgreich!";
            
        } //btnInsert_Click()
    }
}