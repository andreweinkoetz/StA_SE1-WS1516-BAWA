using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class EditSize_Code : System.Web.UI.Page
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
                    clsSizeFacade _sizeFacade = new clsSizeFacade();
                    clsSize _mySize = _sizeFacade.GetSizeById((int)Session["toEdit"]);
                    txtSid.Text = _mySize.Id.ToString();
                    txtSname.Text = _mySize.Name;
                    txtSvalue.Text = _mySize.Value.ToString();
                    Session["pCategory"] = _mySize.CID;
                    lblSizeEdit.Text = "Größe " + _mySize.Name + " bearbeiten.";
                    btEnter.Text = "Größe ändern";


                }
                else if (!IsPostBack)
                {
                    lblSizeEdit.Text = "Neue Größe anlegen";
                    btEnter.Text = "Größe hinzufügen";
                    btDelete.Visible = false;
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            clsSizeFacade _sizeFacade = new clsSizeFacade();
            _sizeFacade.DeleteSizeById(Int32.Parse(txtSid.Text));
            RedirectAdmData();
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            bool readyForDB = true, insertSuccessful = false;

            clsSize _mySize = new clsSize();

            if (!String.IsNullOrEmpty(txtSid.Text))
            {
                _mySize.Id = Int32.Parse(txtSid.Text);
            }
            _mySize.Name = txtSname.Text;
            _mySize.CID = Int32.Parse(ddlCategory.SelectedValue);
            _mySize.Category = ddlCategory.SelectedItem.Text;

            double _value;
            if (Double.TryParse(txtSvalue.Text, out _value))
            {
                _mySize.Value = _value;
            }
            else
            {
                txtSvalue.ForeColor = System.Drawing.Color.Red;
                readyForDB = false;
            }

            clsSizeFacade _sizeFacade = new clsSizeFacade();
            if (readyForDB)
            {
                if (_mySize.Id == 0)
                {
                    insertSuccessful = _sizeFacade.InsertSize(_mySize);
                }
                else
                {
                    insertSuccessful = _sizeFacade.UpdateSize(_mySize);
                }

                if (insertSuccessful)
                {
                    RedirectAdmData();
                }
                else
                {
                    lblError.Text = "Einfügen/Update fehlgeschlagen. Bitte beachten Sie die rot markierten Felder.";
                }
            }
            else
            {
                lblError.Text = "Fehlerhafte Eingabe. Bitte überprüfen Sie die roten Felder.";
            }

        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            RedirectAdmData();
        }


        private void RedirectAdmData()
        {
            Session["toEdit"] = null;
            Session["pCategory"] = null;
            Response.Redirect("AdmData.aspx");
        }

        protected void ddlCategory_DataBound(object sender, EventArgs e)
        {
            if (Session["pCategory"] != null)
            {
                ddlCategory.SelectedValue = Session["pCategory"].ToString();
            }
        }
    }
}