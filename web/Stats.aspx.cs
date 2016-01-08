using bll;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class Stats : System.Web.UI.Page
    {
        public static int previousIndex;
        clsOrderFacade orderFacade = new clsOrderFacade();
        clsUserFacade userFacade = new clsUserFacade();
        clsProductFacade productFacade = new clsProductFacade();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillUserAndCategoryDropDownList();
                btnCreateStats.Enabled = false;
                btnCreateStats.BackColor = System.Drawing.Color.Gray;
            }

            manageExtendedStats();
            manageUser();
            manageCategories();
            if (ddlStats.SelectedIndex != previousIndex)
            {
                fillExtendedStatsDropDownList(sender, e);
                ddlUser.Visible = false;
                ddlCategory.Visible = false;
            }

            if (ddlStatsExtended.SelectedIndex == 0)
            {
                btnCreateStats.Enabled = false;
                btnCreateStats.BackColor = System.Drawing.Color.Gray;
            }
            else if (IsPostBack)
            {
                btnCreateStats.Enabled = true;
                btnCreateStats.BackColor = System.Drawing.Color.Red;
            }

            gvStats.Visible = false;
            stats.Visible = false;
            lblAnnotation.Visible = false;
        }

        protected void ddlStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            previousIndex = ddlStats.SelectedIndex;
        }

        protected void fillUserAndCategoryDropDownList()
        {
            clsUserFacade userFacade = new clsUserFacade();

            List<clsUser> users = userFacade.getAllUsers();
            ddlUser.DataSource = users;
            ddlUser.DataTextField = "Email";
            ddlUser.DataValueField = "Email";
            ddlUser.DataBind();

            Dictionary<Int32, String> categories = productFacade.GetAllProductCategories();
            ddlCategory.DataSource = categories;
            ddlCategory.DataTextField = "Value";
            ddlCategory.DataValueField = "Value";
            ddlCategory.DataBind();

            ddlStatsExtended.Visible = false;
            ddlCategory.Visible = false;
            ddlUser.Visible = false;
        }

        protected void fillExtendedStatsDropDownList(object sender, EventArgs e)
        {
            if (ddlStats.SelectedIndex == 1)
            {
                ddlStatsExtended.Items.Clear();
                ddlStatsExtended.Items.Add("- Daten wählen");
                ddlStatsExtended.Items.Add("sortiert nach Datum");
                ddlStatsExtended.Items.Add("pro Kategorie");
                ddlStatsExtended.Items.Add("des Kunden:");
            }
            if (ddlStats.SelectedIndex == 2)
            {
                ddlStatsExtended.Items.Clear();
                ddlStatsExtended.Items.Add("- Daten wählen");
                ddlStatsExtended.Items.Add("sortiert nach Beliebtheit");
                ddlStatsExtended.Items.Add("sortiert nach Umsatz");
            }
            if (ddlStats.SelectedIndex == 3)
            {
                ddlStatsExtended.Items.Clear();
                ddlStatsExtended.Items.Add("- Daten wählen");
                ddlStatsExtended.Items.Add("sortiert nach Umsatz");
            }
        }

        protected void manageExtendedStats()
        {
            if (ddlStats.SelectedIndex == 0)
            {
                ddlStatsExtended.Visible = false;
            }
            else
            {
                ddlStatsExtended.Visible = true;
            }
        }

        protected void manageCategories()
        {
            if (ddlStats.SelectedIndex == 1 && ddlStatsExtended.SelectedIndex == 2)
            {
                ddlCategory.Visible = true;
            }
            else
            {
                ddlCategory.Visible = false;
            }
        }

        protected void manageUser()
        {
            if (ddlStatsExtended.SelectedIndex != 3)
            {
                ddlUser.Visible = false;
            }
            else
            {
                ddlUser.Visible = true;
            }
        }

        protected void GetAllOrdersOrderedByDate()
        {
            DataTable dt = new DataTable("OrderStats");
            CreateDataTable(dt, new List<String> { "Bestelldatum", "Kunde", "Bestellnummer", "Bestellstatus", "Lieferdatum", "Summe" });

            List<clsOrderExtended> orderList = orderFacade.getOrdersOrderedByDate();
            double revenueResult = 0;
            foreach (clsOrderExtended _order in orderList)
            {
                dt.LoadDataRow(new object[] { _order.OrderDate, _order.UserName, _order.OrderNumber,
              _order.OrderStatusDescription, _order.OrderDeliveryDate, String.Format("{0:C}",_order.OrderSum) }, true);

                revenueResult += _order.OrderSum;
            }
            MalculateTotalAndAverageRevenue(revenueResult, orderList.Count);
            ManageVisibliityAndDataBinding(dt);
        }

        protected void MalculateTotalAndAverageRevenue(double totalRevenue, int amount)
        {
            lblRevenueResult.Text = String.Format("{0:C}", totalRevenue);
            if (amount > 0)
            {
                lblAverageResult.Text = String.Format("{0:C}", (totalRevenue / amount));
            }
            else
            {
                lblAverageResult.Text = String.Format("{0:C}", 0);
            }
        }

        protected void CreateDataTable(DataTable dt, List<String> args)
        {
            foreach (String name in args)
            {
                dt.Columns.Add(name);
            }
        }

        protected void ManageVisibliityAndDataBinding(DataTable dt)
        {
            gvStats.DataSource = dt;
            gvStats.DataBind();
            gvStats.Visible = true;
            stats.Visible = true;
        }

        protected void GetOrdersFromASpecificUser()
        {
            DataTable dt = new DataTable("OrderStats");
            CreateDataTable(dt, new List<String> { "Bestelldatum", "Kunde", "Bestellnummer", "Bestellstatus", "Lieferdatum", "Summe" });

            List<clsOrderExtended> orderList = orderFacade.getOrdersByEmail(ddlUser.SelectedValue);
            double revenueResult = 0;
            foreach (clsOrderExtended _order in orderList)
            {
                dt.LoadDataRow(new object[] { _order.OrderDate, _order.UserName, _order.OrderNumber,
              _order.OrderStatusDescription, _order.OrderDeliveryDate, String.Format("{0:C}",_order.OrderSum) }, true);

                revenueResult += _order.OrderSum;
            }

            MalculateTotalAndAverageRevenue(revenueResult, orderList.Count);
            ManageVisibliityAndDataBinding(dt);
        }

        protected void GetOrdersOrderedByCategory()
        {
            DataTable dt = new DataTable("OrderStats");
            CreateDataTable(dt, new List<String> { "Bestellnummer", "Produktname", "Preis" });

            List<Tuple<int, string, double>> productList = orderFacade.GetOrderedProductsSortByCategory(ddlCategory.SelectedValue);
            double revenueResult = 0;
            foreach (Tuple<int, string, double> _product in productList)
            {
                dt.LoadDataRow(new object[] { _product.Item1, _product.Item2, String.Format("{0:C}", _product.Item3) }, true);
                revenueResult += _product.Item3;
            }

            MalculateTotalAndAverageRevenue(revenueResult, productList.Count);
            ManageVisibliityAndDataBinding(dt);
            lblAnnotation.Visible = true;
        }

        protected void GetFanciestProduct()
        {
            DataTable dt = new DataTable("OrderStats");
            CreateDataTable(dt, new List<String> { "Name des Produkts", "Anzahl der Käufe" });


            OrderedDictionary favoriteProduct = productFacade.getMostFanciedProduct();
            foreach (DictionaryEntry entry in favoriteProduct)
            {
                dt.LoadDataRow(new object[] { entry.Key, entry.Value }, true);
            }
            ManageVisibliityAndDataBinding(dt);
        }

        protected void GetProductsOrderedByRevenue()
        {
            DataTable dt = new DataTable("ProductSales");
            CreateDataTable(dt, new List<String> { "Name des Produkts", "Höhe des Umsatzes" });

            Dictionary<string, double> products = productFacade.GetProductsOrderedByTotalRevenue();
            foreach (KeyValuePair<string, double> _entry in products)
            {
                dt.LoadDataRow(new object[] { _entry.Key, String.Format("{0:C}", _entry.Value) }, true);
            }
            ManageVisibliityAndDataBinding(dt);
        }

        protected void GetCustomersOrderedByRevenue()
        {
            DataTable dt = new DataTable("CustomerStats");
            CreateDataTable(dt, new List<String> { "Kunde", "Gesamtumsatz", "Anzahl der Bestellungen" });

            List<Tuple<string, double, int>> userList = userFacade.GetUsersOrderedByTotalRevenue();
            foreach (Tuple<string, double, int> _user in userList)
            {
                dt.LoadDataRow(new object[] { _user.Item1, String.Format("{0:C}", _user.Item2), _user.Item3 }, true);
            }
            ManageVisibliityAndDataBinding(dt);
        }

        protected void btnCreateStats_Click(object sender, EventArgs e)
        {
            string firstIndex = ddlStats.SelectedItem.Text;
            string secondIndex = "-";
            if (ddlStatsExtended.Visible)
            {
                secondIndex = ddlStatsExtended.SelectedItem.Text;
            }

            switch (firstIndex)
            {
                case "- Daten wählen":
                    btnCreateStats.Enabled = false;
                    break;

                //Auswertungen für Bestellungen
                case "Bestellungen":
                    switch (secondIndex)
                    {
                        case "sortiert nach Datum":
                            GetAllOrdersOrderedByDate();
                            break;
                        case "pro Kategorie":
                            GetOrdersOrderedByCategory();
                            break;
                        case "des Kunden:":
                            GetOrdersFromASpecificUser();
                            break;
                    }
                    break;

                // Auswertungen für Produkte
                case "Produkte":
                    switch (secondIndex)
                    {
                        case "sortiert nach Beliebtheit":
                            GetFanciestProduct();
                            break;
                        case "sortiert nach Umsatz":
                            GetProductsOrderedByRevenue();
                            break;
                    }
                    break;

                //Auswertungen für Kunden
                case "Kunden":
                    switch (secondIndex)
                    {
                        case "sortiert nach Umsatz":
                            GetCustomersOrderedByRevenue();
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}