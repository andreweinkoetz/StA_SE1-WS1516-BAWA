using bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class AdmData_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["selAdmData"] != null)
            {
                if (!IsPostBack)
                {
                    chooseSelection();
                }
            }
            else
            {
                Response.Redirect("administration.aspx");
            }
        }

        private void chooseSelection()
        {

            switch ((int)Session["selAdmData"])
            {
                case 1:
                    lblAdmData.Text = "Produktverwaltung";
                    btCreateNew.Text = "Neues Produkt anlegen";
                    clsProductFacade _productFacade = new clsProductFacade();
                    InitializeGvAdmData(_productFacade.ProductsGetAll(), 1);
                    break;
                case 2:
                    lblAdmData.Text = "Extraverwaltung";
                    btCreateNew.Text = "Neues Extra anlegen";
                    clsExtraFacade _extraFacade = new clsExtraFacade();
                    InitializeGvAdmData(_extraFacade.GetAllExtras(), 2);
                    break;
                case 3:
                    lblAdmData.Text = "Benutzerverwaltung";
                    btCreateNew.Text = "Neuen Benutzer anlegen";
                    clsUserFacade _userFacade = new clsUserFacade();
                    InitializeGvAdmData(_userFacade.GetAllUsers(), 3);
                    break;
                case 4:
                    lblAdmData.Text = "Größenverwaltung";
                    btCreateNew.Text = "Neue Größe anlegen";
                    clsSizeFacade _sizeFacade = new clsSizeFacade();
                    InitializeGvAdmData(_sizeFacade.GetAllSizes(), 4);
                    break;
            }
        }

        private void InitializeGvAdmData(object _list, int _selection)
        {

            CommandField _cmdField = new CommandField();
            _cmdField.ShowSelectButton = true;
            _cmdField.ButtonType = ButtonType.Image;
            _cmdField.SelectImageUrl = "~/img/edit_icon.png";
            _cmdField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            gvAdmData.Columns.Add(_cmdField);

            switch (_selection)
            {

                case 1:

                    DataTable dtProduct = new DataTable("Products");

                    dtProduct.Columns.Add("PID");

                    BoundField _pid = new BoundField();
                    _pid.DataField = "PID";
                    _pid.HeaderText = "PID";
                    _pid.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    dtProduct.Columns.Add("PName");

                    BoundField _pName = new BoundField();
                    _pName.DataField = "PName";
                    _pName.HeaderText = "Produktname";

                    dtProduct.Columns.Add("CName");

                    BoundField _pCategory = new BoundField();
                    _pCategory.DataField = "CName";
                    _pCategory.HeaderText = "Kategorie";
                    _pCategory.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    dtProduct.Columns.Add("PricePerUnit");

                    BoundField _pPU = new BoundField();
                    _pPU.DataField = "PricePerUnit";
                    _pPU.HeaderText = "Preis pro Einheit";
                    _pPU.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    dtProduct.Columns.Add("ToSell");

                    CheckBoxField _toSell = new CheckBoxField();
                    _toSell.DataField = "ToSell";
                    _toSell.HeaderText = "Zum Verkauf?";
                    _toSell.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    gvAdmData.Columns.Add(_pid);
                    gvAdmData.Columns.Add(_pName);
                    gvAdmData.Columns.Add(_pCategory);
                    gvAdmData.Columns.Add(_pPU);
                    gvAdmData.Columns.Add(_toSell);

                    foreach (clsProduct _product in ((List<clsProduct>)_list))
                    {
                        dtProduct.LoadDataRow(new object[] { _product.Id, _product.Name, _product.Category, _product.PricePerUnit, _product.ToSell }, true);
                    }
                    gvAdmData.DataSource = dtProduct;
                    gvAdmData.DataBind();

                    break;

                case 2:


                    DataTable dtExtra = new DataTable("Extras");

                    BoundField _eid = new BoundField();
                    _eid.DataField = "EID";
                    _eid.HeaderText = "EID";
                    _eid.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    BoundField _eName = new BoundField();
                    _eName.DataField = "EName";
                    _eName.HeaderText = "Name des Extras";

                    BoundField _ePrice = new BoundField();
                    _ePrice.DataField = "EPrice";
                    _ePrice.HeaderText = "Preis pro Extra";
                    _ePrice.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    CheckBoxField _etoSell = new CheckBoxField();
                    _etoSell.DataField = "ToSell";
                    _etoSell.HeaderText = "Zum Verkauf?";
                    _etoSell.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    gvAdmData.Columns.Add(_eid);
                    gvAdmData.Columns.Add(_eName);
                    gvAdmData.Columns.Add(_ePrice);
                    gvAdmData.Columns.Add(_etoSell);

                    dtExtra.Columns.Add("EID");

                    dtExtra.Columns.Add("EName");

                    dtExtra.Columns.Add("EPrice");

                    dtExtra.Columns.Add("ToSell");

                    foreach (clsExtra _extra in ((List<clsExtra>)_list))
                    {
                        dtExtra.LoadDataRow(new object[] { _extra.ID, _extra.Name, _extra.Price, _extra.ToSell }, true);
                    }
                    gvAdmData.DataSource = dtExtra;
                    gvAdmData.DataBind();

                    break;
                case 3:
                    DataTable dtUser = new DataTable("Users");

                    BoundField _uid = new BoundField();
                    _uid.HeaderText = "UID";
                    _uid.DataField = "UID";
                    _uid.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    BoundField _uName = new BoundField();
                    _uName.HeaderText = "Name";
                    _uName.DataField = "UName";

                    BoundField _uPrename = new BoundField();
                    _uPrename.HeaderText = "Vorname";
                    _uPrename.DataField = "UPrename";

                    BoundField _uTitle = new BoundField();
                    _uTitle.HeaderText = "Anrede";
                    _uTitle.DataField = "UTitle";

                    BoundField _uStreet = new BoundField();
                    _uStreet.HeaderText = "Straße";
                    _uStreet.DataField = "UStreet";

                    BoundField _uNr = new BoundField();
                    _uNr.HeaderText = "HNr.";
                    _uNr.DataField = "UNr";

                    BoundField _uPostcode = new BoundField();
                    _uPostcode.HeaderText = "PLZ";
                    _uPostcode.DataField = "UPostcode";

                    BoundField _uPlace = new BoundField();
                    _uPlace.HeaderText = "Ort";
                    _uPlace.DataField = "UPlace";

                    BoundField _uPhone = new BoundField();
                    _uPhone.HeaderText = "Telefon";
                    _uPhone.DataField = "UPhone";

                    CheckBoxField _uIsActive = new CheckBoxField();
                    _uIsActive.HeaderText = "Aktiv?";
                    _uIsActive.DataField = "UIsActive";
                    _uIsActive.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    BoundField _uRole = new BoundField();
                    _uRole.HeaderText = "Rollen-ID";
                    _uRole.DataField = "URole";

                    BoundField _uEmail = new BoundField();
                    _uEmail.HeaderText = "Email/Login";
                    _uEmail.DataField = "UEmail";

                    gvAdmData.Columns.Add(_uid);
                    gvAdmData.Columns.Add(_uTitle);
                    gvAdmData.Columns.Add(_uPrename);
                    gvAdmData.Columns.Add(_uName);
                    gvAdmData.Columns.Add(_uEmail);
                    gvAdmData.Columns.Add(_uPhone);
                    gvAdmData.Columns.Add(_uPlace);
                    gvAdmData.Columns.Add(_uPostcode);
                    gvAdmData.Columns.Add(_uStreet);
                    gvAdmData.Columns.Add(_uNr);
                    gvAdmData.Columns.Add(_uIsActive);

                    dtUser.Columns.Add("UID");
                    dtUser.Columns.Add("UTitle");
                    dtUser.Columns.Add("UName");
                    dtUser.Columns.Add("UPrename");
                    dtUser.Columns.Add("UStreet");
                    dtUser.Columns.Add("UNr");
                    dtUser.Columns.Add("UPostcode");
                    dtUser.Columns.Add("UPlace");
                    dtUser.Columns.Add("UPhone");
                    dtUser.Columns.Add("UIsActive");
                    dtUser.Columns.Add("URole");
                    dtUser.Columns.Add("UEmail");

                    foreach (clsUser _user in ((List<clsUser>)_list))
                    {
                        dtUser.LoadDataRow(new object[] { _user.ID, _user.Title, _user.Name, _user.Prename, _user.Street, _user.Nr, _user.Postcode, _user.Place, _user.Phone, _user.IsActive, _user.Role, _user.EMail }, true);
                    }
                    gvAdmData.DataSource = dtUser;
                    gvAdmData.DataBind();

                    break;
                case 4:
                    DataTable dtSizes = new DataTable("Sizes");

                    BoundField _sid = new BoundField();
                    _sid.DataField = "SID";
                    _sid.HeaderText = "SID";
                    _sid.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    BoundField _sName = new BoundField();
                    _sName.DataField = "SName";
                    _sName.HeaderText = "Name der Größe";

                    BoundField _sValue = new BoundField();
                    _sValue.DataField = "SValue";
                    _sValue.HeaderText = "Wert einer Größe";

                    BoundField _sCategory = new BoundField();
                    _sCategory.DataField = "SCategory";
                    _sCategory.HeaderText = "Kategorie";

                    gvAdmData.Columns.Add(_sid);
                    gvAdmData.Columns.Add(_sName);
                    gvAdmData.Columns.Add(_sValue);
                    gvAdmData.Columns.Add(_sCategory);

                    dtSizes.Columns.Add("SID");
                    dtSizes.Columns.Add("SName");
                    dtSizes.Columns.Add("SValue");
                    dtSizes.Columns.Add("SCategory");

                    foreach (clsSize _size in ((List<clsSize>)_list))
                    {
                        dtSizes.LoadDataRow(new object[] { _size.Id, _size.Name, _size.Value, _size.Category }, true);
                    }
                    gvAdmData.DataSource = dtSizes;
                    gvAdmData.DataBind();

                    break;
            }


        }

        protected void gvAdmData_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableCellCollection _rowContent = gvAdmData.SelectedRow.Cells;
            Session["toEdit"] = Int32.Parse(_rowContent[1].Text);
            TransferToEditPage();
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            Session["toEdit"] = Session["selAdmData"] = null;
            Response.Redirect("administration.aspx");
        }

        protected void btCreateNew_Click(object sender, EventArgs e)
        {
            Session["toEdit"] = null;
            TransferToEditPage();
        }

        private void TransferToEditPage()
        {
            switch ((int)Session["selAdmData"])
            {
                case 1:
                    Server.Transfer("edit_product.aspx");
                    break;
                case 2:
                    Server.Transfer("edit_extra.aspx");
                    break;
                case 3:
                    Server.Transfer("edit_user.aspx");
                    break;
                case 4:
                    Server.Transfer("edit_size.aspx");
                    break;
            }
        }
    }
}