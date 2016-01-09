using bll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class EditProduct_Code : System.Web.UI.Page
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

        private void InitializeToEdit()
        {
            clsProductFacade _myFacade = new clsProductFacade();
            clsProductExtended _myProduct = _myFacade.GetProductByID((int)Session["toEdit"]);
            lblProductEdit.Text = "Produkt \"" + _myProduct.Name + "\" bearbeiten:";
            txtPid.Text = _myProduct.Id.ToString();
            Session["pCategory"] = _myProduct.CID;
            txtPname.Text = _myProduct.Name;
            txtPpU.Text = _myProduct.PricePerUnit.ToString();
            chkSell.Checked = _myProduct.ToSell;
            btEnter.Text = "Produkt ändern";
            _myFacade = new clsProductFacade();
            List<Int32> _ordersOfProduct = _myFacade.GetOrdersOfProductByPid(_myProduct.Id);
            if (_ordersOfProduct.Count == 0)
            {
                btDelete.Enabled = true;
            }
            else
            {
                ddlCategory.Enabled = false;
                btDelete.BackColor = System.Drawing.Color.Gray;
                lblOpenOrders.Text = clsOrderFacade.CreateStringOfOpenOrders(_ordersOfProduct, (int)Session["selAdmData"]);
            }
        }

        private void InitializeCreateNew()
        {
            lblProductEdit.Text = "Neues Produkt anlegen";
            btEnter.Text = "Produkt hinzufügen";
            btDelete.Visible = false;
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            InsertOrUpdateProduct();
        }

        private void InsertOrUpdateProduct()
        {
            bool insertSuccessful = false, isValidPrice = true;

            clsProductFacade _myProductFacade = new clsProductFacade();
            clsProductExtended _myProduct = new clsProductExtended();

            if (!String.IsNullOrEmpty(txtPid.Text))
            {
                _myProduct.Id = Int32.Parse(txtPid.Text);
            }

            _myProduct.Name = txtPname.Text;

            double d;
            if (Double.TryParse(txtPpU.Text, out d))
            {
                _myProduct.PricePerUnit = d;
            }
            else
            {
                txtPpU.ForeColor = System.Drawing.Color.Red;
                isValidPrice = false;
            }

            _myProduct.ToSell = chkSell.Checked;
            _myProduct.CID = Int32.Parse(ddlCategory.SelectedValue);
            _myProduct.Category = ddlCategory.SelectedItem.Text;

            if (_myProduct.Id == 0 && isValidPrice)
            {
                insertSuccessful = _myProductFacade.InsertNewProduct(_myProduct);
            }
            else if (isValidPrice)
            {
                insertSuccessful = _myProductFacade.UpdateProduct(_myProduct);
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

        protected void btDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct();

        }

        private void DeleteProduct()
        {
            clsProductFacade _productFacade = new clsProductFacade();
            if (_productFacade.DeleteProductByPid(Int32.Parse(txtPid.Text)))
            {
                RedirectAdmData();
            }
            else
            {
                lblError.Text = "Produkt konnte nicht gelöscht werden. Fehler in DB";
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