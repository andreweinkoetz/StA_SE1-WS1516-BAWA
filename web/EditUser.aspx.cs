using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class EditUser_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["roleID"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }

            if ((int)Session["roleID"] < 2)
            {
                if (Session["toEdit"] != null && !IsPostBack)
                {
                    btEnter.Text = "Benutzer ändern";
                    clsUserFacade _userFacade = new clsUserFacade();
                    clsUser _myUser = _userFacade.UserGetById((int)Session["toEdit"]);
                    _userFacade = new clsUserFacade();
                    List<Int32> _userOrders = _userFacade.GetOrdersFromUserById(_myUser.ID);
                    if (_userOrders.Count == 0)
                    {
                        btDelete.Enabled = true;
                    }
                    else
                    {
                        lblOpenOrders.Text = "Benutzer kann nicht gelöscht werden, da er folgende Bestellungen aufgegeben hat: <br />{<br />";

                        foreach (int _oNumber in _userOrders)
                        {
                            lblOpenOrders.Text += _oNumber + "<br />";
                        }

                        lblOpenOrders.Text += "}";
                        btDelete.BackColor = System.Drawing.Color.Gray;

                    }
                    txtBoxId.Text = _myUser.ID.ToString();
                    txtBoxEmail.Text = _myUser.EMail;
                    ddlTitle.SelectedValue = _myUser.Title;
                    txtBoxName.Text = _myUser.Name;
                    txtBoxVorname.Text = _myUser.Prename;
                    txtBoxPlace.Text = _myUser.Place;
                    txtBoxPLZ.Text = _myUser.Postcode.ToString();
                    txtBoxStraße.Text = _myUser.Street;
                    txtBoxHnr.Text = _myUser.Nr.ToString();
                    txtBoxPhone.Text = _myUser.Phone;
                    chkActive.Checked = _myUser.IsActive;
                    ddlUserRole.SelectedValue = _myUser.Role.ToString();
                    lblUserEdit.Text = "Benutzer #" + _myUser.ID + " ändern:";

                }
                else if (!IsPostBack)
                {
                    lblUserEdit.Text = "Benutzer anlegen";
                    btEnter.Text = "Benutzer anlegen";
                    btDelete.Visible = false;
                    pwRow.Visible = pwRow2.Visible = true;
                    changePwChkRow.Visible = false;

                }
            }
            else
            {
                Response.Redirect("Administration.aspx");
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            clsUserFacade _userFacade = new clsUserFacade();
            _userFacade.UserDelete(Int32.Parse(txtBoxId.Text));
            RedirectAdmData();
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            bool successful = false;
            bool readyForDB = true;
            bool changePassword = txtBoxPassword.Visible;

            clsUser _userToInsert = new clsUser();
            if (!String.IsNullOrEmpty(txtBoxId.Text))
            {
                _userToInsert.ID = Int32.Parse(txtBoxId.Text);
            }
            _userToInsert.Name = txtBoxName.Text;
            _userToInsert.Prename = txtBoxVorname.Text;
            _userToInsert.Place = txtBoxPlace.Text;

            _userToInsert.Street = txtBoxStraße.Text;

            _userToInsert.Phone = txtBoxPhone.Text;
            _userToInsert.EMail = txtBoxEmail.Text;
            _userToInsert.IsActive = chkActive.Checked;
            _userToInsert.Title = ddlTitle.SelectedValue;
            _userToInsert.Role = Int32.Parse(ddlUserRole.SelectedValue);

            try
            {
                _userToInsert.Postcode = Int32.Parse(txtBoxPLZ.Text);
            }
            catch (Exception)
            {
                lblErrorPlace.Text = "Bitte überprüfen Sie Ihre Angaben.";
                txtBoxPLZ.ForeColor = System.Drawing.Color.Red;
                readyForDB = false;
            }

            try
            {
                _userToInsert.Nr = Int32.Parse(txtBoxHnr.Text);
            }
            catch (Exception)
            {
                lblErrorStreet.Text = "Bitte überprüfen Sie Ihre Angaben.";
                txtBoxHnr.ForeColor = System.Drawing.Color.Red;
                readyForDB = false;
            }


            if (changePassword)
            {
                if (!String.IsNullOrEmpty(txtBoxPassword.Text) && txtBoxPassword.Text == txtBoxPasswordx2.Text)
                {
                    MD5 hash = MD5.Create();
                    _userToInsert.Password = createMD5Hash(hash, txtBoxPasswordx2.Text);
                }
                else
                {
                    readyForDB = false;
                    lblErrorPwd.Text = "Passwörter müssen übereinstimmen.";
                }
            }

            if (readyForDB)
            {
                clsUserFacade _userFacade = new clsUserFacade();
                if (_userToInsert.ID == 0)
                {
                    successful = _userFacade.UserInsert(_userToInsert);
                }
                else
                {
                    successful = _userFacade.UserUpdate(_userToInsert);
                }

                if (successful)
                {
                    RedirectAdmData();
                }
                else
                {
                    lblError.Text = "Fehler bei der DB-INSERT.";
                }
            }

        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            RedirectAdmData();
        }


        /// <summary>
        /// Creates and returns the MD5 Hash of a String
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string createMD5Hash(MD5 md5Hash, string password)
        {

            // Converts the password to a byte array and computes the MD5 hash
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sBuilder = new StringBuilder();

            // Formats each byte into a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private void RedirectAdmData()
        {
            Session["toEdit"] = null;
            Session["pCategory"] = null;
            Response.Redirect("AdmData.aspx");
        }

        protected void chkChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            pwRow.Visible = pwRow2.Visible = chkChangePassword.Checked;
        }
    }
}