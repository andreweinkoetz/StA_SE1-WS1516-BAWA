using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class EditExtra_Code : System.Web.UI.Page
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
                Response.Redirect("administration.aspx");
            }
        }

        /// <summary>
        /// Initialisierung UI für Extra-Bearbeitung.
        /// </summary>
        private void InitializeToEdit()
        {
            clsExtraFacade _extraFacade = new clsExtraFacade();
            clsExtra _myExtra = _extraFacade.GetExtraById((int)Session["toEdit"]);
            _extraFacade = new clsExtraFacade();
            List<Int32> _orderNumbers = _extraFacade.GetOrdersOfExtrasByEID(_myExtra.ID);
            if (_orderNumbers.Count == 0)
            {
                btDelete.Enabled = true;
            }
            else
            {
                lblOpenOrders.Text = clsOrderFacade.CreateStringOfOpenOrders(_orderNumbers, (int)Session["selAdmData"]);
                btDelete.BackColor = System.Drawing.Color.Gray;
            }
            txtEid.Text = _myExtra.ID.ToString();
            txtEname.Text = _myExtra.Name;
            txtPpE.Text = _myExtra.Price.ToString();
            chkSell.Checked = _myExtra.ToSell;
            lblExtraEdit.Text = "Extra \"" + _myExtra.Name + "\" bearbeiten";
            btEnter.Text = "Extra ändern";
        }

        /// <summary>
        /// Initialisierung UI für Extra-Neuerstellung.
        /// </summary>
        private void InitializeCreateNew()
        {
            lblExtraEdit.Text = "Neues Extra anlegen";
            btEnter.Text = "Extra hinzufügen";
            btDelete.Visible = false;
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            InsertOrUpdateExtra();
        }

        /// <summary>
        /// Prüft auf korrekte Eingaben und fügt
        /// das neue Extra in die DB ein bzw. updated das
        /// bestehende Extra.
        /// 
        /// Fall Einfügen erfolgreich wird der User zur Hauptansicht zurückgeleitet.
        /// Im Fehlerfall erscheint ein passender Hinweis.
        /// </summary>
        private void InsertOrUpdateExtra()
        {
            bool readyForDB = true, insertSuccessful = false;

            clsExtra _myExtra = new clsExtra();
            if (!String.IsNullOrEmpty(txtEid.Text))
            {
                _myExtra.ID = Int32.Parse(txtEid.Text);
            }
            _myExtra.Name = txtEname.Text;
            _myExtra.ToSell = chkSell.Checked;
            double price;
            if (!Double.TryParse(txtPpE.Text, out price))
            {
                readyForDB = false;
                txtPpE.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                _myExtra.Price = price;
            }

            clsExtraFacade _extraFacade = new clsExtraFacade();
            if (readyForDB)
            {
                if (_myExtra.ID == 0)
                {
                    insertSuccessful = _extraFacade.InsertExtra(_myExtra);
                }
                else
                {
                    insertSuccessful = _extraFacade.UpdateExtra(_myExtra);
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

        protected void btDelete_Click(object sender, EventArgs e)
        {
            DeleteExtra();
        }

        /// <summary>
        /// Löscht das gewählte Extra.
        /// </summary>
        private void DeleteExtra()
        {
            clsExtraFacade _extraFacade = new clsExtraFacade();
            _extraFacade.DeleteExtraByID(Int32.Parse(txtEid.Text));
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

    }
}