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
                Response.Redirect("login_page.aspx");
            }

            if ((int)Session["roleID"] < 2)
            {
                if (Session["toEdit"] != null && !IsPostBack)
                {
                    InitializeToEdit();

                }
                else if (!IsPostBack)
                {
                    InitializeCreateNew();
                }
            }
            else
            {
                Response.Redirect("login_page.aspx");
            }
        }

        /// <summary>
        /// Initialisierung UI für Größen-Bearbeitung.
        /// </summary>
        private void InitializeToEdit()
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

        /// <summary>
        /// Initialisierung UI für Größen-Neuerstellung.
        /// </summary>
        private void InitializeCreateNew()
        {
            lblSizeEdit.Text = "Neue Größe anlegen";
            btEnter.Text = "Größe hinzufügen";
            btDelete.Visible = false;
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            InsertOrUpdateSize();

        }

        /// <summary>
        /// Prüft auf korrekte Eingaben und fügt
        /// die neue Größe in die DB ein bzw. updated die
        /// bestehende Größe.
        /// 
        /// Fall Einfügen erfolgreich wird der User zur Hauptansicht zurückgeleitet.
        /// Im Fehlerfall erscheint ein passender Hinweis.
        /// </summary>
        private void InsertOrUpdateSize()
        {
            bool readyForDB = true, insertSuccessful = false;

            TextBox[] _boxes = new TextBox[] { txtSname, txtSvalue };

            readyForDB = CheckEmptyTextBoxes(_boxes);

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

        /// <summary>
        /// Prüft von mehreren Textboxen, ob deren
        /// Text leer ist und gibt in diesem Fall false zurück.
        /// </summary>
        /// <param name="_boxes">TextBoxen, die geprüft werden sollen.</param>
        /// <returns>true wenn alle Textboxen gefüllt sind.</returns>
        private bool CheckEmptyTextBoxes(TextBox[] _boxes)
        {
            foreach (TextBox _box in _boxes)
            {
                if (String.IsNullOrEmpty(_box.Text))
                {
                    _box.BackColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            return true;
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            DeleteSize();
        }

        /// <summary>
        /// Löscht die gewählte Größe
        /// </summary>
        private void DeleteSize()
        {
            clsSizeFacade _sizeFacade = new clsSizeFacade();
            _sizeFacade.DeleteSizeById(Int32.Parse(txtSid.Text));
            RedirectAdmData();
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            RedirectAdmData();
        }

        /// <summary>
        /// Setzt Session-Variablen zurück und 
        /// leitet auf Hauptansicht um.
        /// </summary>
        private void RedirectAdmData()
        {
            Session["toEdit"] = null;
            Session["pCategory"] = null;
            Response.Redirect("adm_data.aspx");
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