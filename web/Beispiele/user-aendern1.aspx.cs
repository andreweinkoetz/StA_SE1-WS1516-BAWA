using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.Beispiele
{
    public partial class user_aendern1 : System.Web.UI.Page
    {
        /// <summary>
        /// PageLoad(): beim ersten Aufruf (GET): uid aus Parameter auslesen, zugehörigen User lesen, Felder in Form befüllen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.Params.GetKey(0) == "uid")
            {   // wir erwarten als Parameter "uid" beim Seitenaufruf  eine UserId
                int _uid = Convert.ToInt32(Page.Request.Params.GetValues(0)[0]);                
                if (!IsPostBack)
                {   
                        lblMsg.Text = "GET: " + Page.Request.Params.GetKey(0) + " " + _uid.ToString();  // nur zur Ablaufverfolgung
                        bll.clsUserFacade _Users = new bll.clsUserFacade();
                        bll.clsUser _myUser = _Users.UserGetById(_uid);     // User lesen
                        txtName.Text = _myUser.Name;
                        txtAddress.Text = _myUser.Address;
                        txtPassword.Text = _myUser.Password;
                        txtDistanz.Text = _myUser.Distance.ToString(); ;
                        chkIsActive.Checked = _myUser.IsActive;
                        rblRole.SelectedValue = _myUser.Role.ToString();
                }
                else
                {   // Postback: Benutzer hat die Änderungen gemacht und will sie speichern
                    // Speicherung wird in btnUpdate_Click() gemacht
                        lblMsg.Text = "POST: " + Page.Request.Params.GetKey(0) + " " + _uid.ToString();  // nur zur Ablaufverfolgung
                }
            }
            else { 
                lblMsg.Text = "Error Parameter: " + Page.Request.Params.GetKey(0);
            }
        }   // Page_load()

        /// <summary>
        /// btnUpdate_Click(): Änderungen des Users in DB speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bll.clsUserFacade _Users = new bll.clsUserFacade();
            bll.clsUser _updUser = new bll.clsUser();
            _updUser.ID = Convert.ToInt32(Page.Request.Params.GetValues(0)[0]);
            _updUser.Name = txtName.Text;
            _updUser.Address = txtAddress.Text;
            _updUser.Password = txtPassword.Text;
            _updUser.Distance = Convert.ToInt32(txtDistanz.Text);
            _updUser.IsActive = chkIsActive.Checked;
            _updUser.Role = Convert.ToInt32(rblRole.SelectedValue);
            bool result = _Users.UserUpdate(_updUser);       // Update in DB
            if (result)
                lblMsg.Text += " Update erfolgreich";
            else
                lblMsg.Text += " Update nicht erfolgreich";
        }   // btnUpdate_Click()
    }   // class user_aendern1
}