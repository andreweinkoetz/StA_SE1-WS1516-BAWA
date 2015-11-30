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
            chooseSelection();
        }

        private void chooseSelection()
        {
            switch ((int)Session["selAdmData"])
            {
                case 1:
                    lblAdmData.Text = "Produktverwaltung";
                    clsProductFacade _productFacade = new clsProductFacade();
                    initializeGvAdmData(_productFacade.ProductsGetAll(),1);
                    break;
                case 2:
                    lblAdmData.Text = "Extraverwaltung";
                    clsExtraFacade _extraFacade = new clsExtraFacade();
                    initializeGvAdmData(_extraFacade.ExtrasGetAll(), 2);
                    break;
                case 3:
                    lblAdmData.Text = "Kundenverwaltung";

                    break;
            }
        }

        private void initializeGvAdmData(object _list, int _selection)
        {
            

            switch (_selection)
            {
                
                case 1:

                    //ObjectDataSource1.SelectMethod = "ProductsGetAll";
                    //ObjectDataSource1.UpdateMethod = "UpdateProduct";
                    //ObjectDataSource1.TypeName = "bll.clsProductFacade";

                    

                    //DataTable dtProduct = new DataTable("Products");

                    //dtProduct.Columns.Add("PID");

                    //BoundField _pid = new BoundField();
                    //_pid.DataField = "PID";
                    //_pid.HeaderText = "PID";

                    //dtProduct.Columns.Add("PName");

                    //BoundField _pName = new BoundField();
                    //_pName.DataField = "PName";
                    //_pName.HeaderText = "Produktname";

                    //dtProduct.Columns.Add("CName");

                    //BoundField _pCategory = new BoundField();
                    //_pCategory.DataField = "CName";
                    //_pCategory.HeaderText = "Kategorie";

                    //dtProduct.Columns.Add("PricePerUnit");

                    //BoundField _pPU = new BoundField();
                    //_pPU.DataField = "PricePerUnit";
                    //_pPU.HeaderText = "Preis pro Einheit";

                    //dtProduct.Columns.Add("ToSell");

                    //CheckBoxField _toSell = new CheckBoxField();
                    //_toSell.DataField = "ToSell";
                    //_toSell.HeaderText = "Zum Verkauf?";

                    //gvAdmData.Columns.Add(_pid);
                    //gvAdmData.Columns.Add(_pName);
                    //gvAdmData.Columns.Add(_pCategory);
                    //gvAdmData.Columns.Add(_pPU);
                    //gvAdmData.Columns.Add(_toSell);

                    //foreach (clsProduct _product in ((List<clsProduct>)_list))
                    //{
                    //    dtProduct.LoadDataRow(new object[] { _product.Id, _product.Name, _product.Category, _product.PricePerUnit, _product.ToSell }, true);
                    //}
                    //gvAdmData.AutoGenerateColumns = false;
                    //gvAdmData.DataSource = dtProduct;
                    //gvAdmData.DataBind();

                    break;

                case 2:

                    DataTable dtExtra = new DataTable("Extras");

                    dtExtra.Columns.Add("EID");

                    dtExtra.Columns.Add("EName");

                    dtExtra.Columns.Add("EPrice");

                    foreach (clsExtra _extra in ((List<clsExtra>)_list))
                    {
                        dtExtra.LoadDataRow(new object[] { _extra.ID, _extra.Name, _extra.Price }, true);
                    }

                    gvAdmData.DataSource = ObjectDataSource1;
                    gvAdmData.DataBind();

                    break;
            }
        }


        protected void btLogout_Click(object sender, EventArgs e)
        {
            Session["userID"] = null;
            Session["roleID"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        protected void gvAdmData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
        }
    }
}