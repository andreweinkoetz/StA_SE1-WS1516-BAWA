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
        protected void Page_Load(object sender, EventArgs e)
        {
            //Sicherheitsmechanismus für den Zugriff auf die Seite
            if (Session["roleID"] == null || (int)Session["roleID"] != 1)
            {
                Response.Redirect("login_page.aspx");
            }

            if (Session["initialized"] == null)
            {
                //Einmaliges, anfängliches Initialisieren der Komponenten
                InitializeUserAndCategoryDropDownList();
                btnCreateStats.Enabled = false;
                btnCreateStats.BackColor = System.Drawing.Color.Gray;
                Session["initialized"] = true;

                //Benötigt, damit beim erstmaligen Benutzen der Auswertungsseite die verfeinernde Dropdown-Liste gefüllt wird
                Session["stats"] = -1;
            }

            //Management der verfeinernden Dropdown-Liste bzgl. der allgemeinen Dropdown-Liste
            if (Session["stats"] != null && ddlStats.SelectedIndex != (int)Session["stats"])
            {
                SetFurtherPossibilitiesDependingOnSelectedStatistic();
                ddlUser.Visible = false;
                ddlCategory.Visible = false;
            }

            //Management der verfeinernden und zusätzlichen Dropdown-Liste bei Postbacks
            ShowFurtherPossibilitiesForSelectedStatistic();
            ShowUsersForOrdersByUser();
            ShowCategoriesForOrdersByCategory();

            //Aktivieren bzw. Deaktivieren des Buttons bei Postbacks
            if (IsPostBack && ddlStats.SelectedIndex > 0 && ddlStatsExtended.SelectedIndex > 0)
            {
                btnCreateStats.Enabled = true;
                btnCreateStats.BackColor = System.Drawing.ColorTranslator.FromHtml("#CF323D");
            }
            else
            {
                //Benötigt für eine erneute Graufärbung des Buttons, falls er bereits rot war
                btnCreateStats.Enabled = false;
                btnCreateStats.BackColor = System.Drawing.Color.Gray;
            }

            //"Zurücksetzen" der Tabellen bei Postbacks
            gvStats.Visible = false;
            stats.Visible = false;
            lblAnnotation.Visible = false;
        }

        protected void ddlStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStats.SelectedIndex >= 0 && ddlStats.SelectedIndex <= 3)
            {
                Session["stats"] = ddlStats.SelectedIndex;
                SetFurtherPossibilitiesDependingOnSelectedStatistic();
            }
            else
            {
                Session["stats"] = null; //Ungültigen Index erhalten
            }
        }

        protected void InitializeUserAndCategoryDropDownList()
        {
            List<clsUser> users = new clsUserFacade().GetAllUsers();
            ddlUser.DataSource = users;
            InitializeDropDownList(ddlUser, "Email", "Email");

            Dictionary<Int32, String> categories = new clsProductFacade().GetAllProductCategories();
            ddlCategory.DataSource = categories;
            InitializeDropDownList(ddlCategory, "Value", "Value");
        }

        protected void InitializeDropDownList(DropDownList ddl, string textField, string valueField)
        {
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
            ddl.Visible = false;
        }

        protected void SetFurtherPossibilitiesDependingOnSelectedStatistic()
        {
            switch (ddlStats.SelectedIndex)
            {
                case 1:
                    BuildDropDownList(ddlStatsExtended, new List<string> { "- Daten wählen", "sortiert nach Datum", "pro Kategorie", "des Kunden", "nach Status", "nach Lieferdauer" }, true);
                    break;
                case 2:
                    BuildDropDownList(ddlStatsExtended, new List<string> { "- Daten wählen", "sortiert nach Beliebtheit", "sortiert nach Umsatz" }, true);
                    break;
                case 3:
                    BuildDropDownList(ddlStatsExtended, new List<string> { "- Daten wählen", "sortiert nach Umsatz" }, true);
                    break;
                default:
                    //Unbekannter Index
                    break;
            }
        }

        protected void BuildDropDownList(DropDownList ddl, List<string> possibilities, bool isCleared)
        {
            if (isCleared)
            {
                ddl.Items.Clear();
            }
            foreach (string possibility in possibilities)
            {
                ddl.Items.Add(possibility);
            }
        }

        protected void ShowFurtherPossibilitiesForSelectedStatistic()
        {
            ddlStatsExtended.Visible = !(ddlStats.SelectedIndex == 0);
        }

        protected void ShowCategoriesForOrdersByCategory()
        {
            ddlCategory.Visible = ddlStats.SelectedIndex == 1 && ddlStatsExtended.SelectedIndex == 2;
        }

        protected void ShowUsersForOrdersByUser()
        {
            ddlUser.Visible = ddlStats.SelectedIndex == 1 && ddlStatsExtended.SelectedIndex == 3;
        }

        protected void BuildDataTable(DataTable dt, List<String> names)
        {
            foreach (String name in names)
            {
                dt.Columns.Add(name);
            }
        }

        protected void ManageVisibilityAndDataBinding(DataTable dt, bool isOrder)
        {
            gvStats.DataSource = dt;
            gvStats.DataBind();
            gvStats.Visible = true;

            if (isOrder)
            {
                stats.Visible = true;
            }
        }

        protected void CalculateTotalAndAverageRevenue(double totalRevenue, int amount)
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

        protected void GetOrdersOrderedByDate()
        {
            DataTable dt = new DataTable("OrderStats");
            BuildDataTable(dt, new List<String> { "Bestelldatum", "Kunde", "Bestellnummer", "Bestellstatus", "Lieferdatum", "Summe" });

            List<clsOrderExtended> orderList = new clsOrderFacade().GetOrdersOrderedByDate();
            double revenueResult = 0;
            foreach (clsOrderExtended _order in orderList)
            {
                dt.LoadDataRow(new object[] { _order.OrderDate, _order.UserName, _order.OrderNumber,
              _order.OrderStatusDescription, _order.OrderDeliveryDate, String.Format("{0:C}",_order.OrderSum) }, true);

                revenueResult += _order.OrderSum;
            }
            CalculateTotalAndAverageRevenue(revenueResult, orderList.Count);
            ManageVisibilityAndDataBinding(dt, true);
        }

        protected void GetOrdersOrderedByCategory()
        {
            DataTable dt = new DataTable("OrderStats");
            BuildDataTable(dt, new List<String> { "Bestellnummer", "Produktname", "Preis" });

            List<Tuple<int, string, double>> productList = new clsOrderFacade().GetOrderedProductsSortByCategory(ddlCategory.SelectedValue);
            double revenueResult = 0;
            foreach (Tuple<int, string, double> _product in productList)
            {
                dt.LoadDataRow(new object[] { _product.Item1, _product.Item2, String.Format("{0:C}", _product.Item3) }, true);
                revenueResult += _product.Item3;
            }

            CalculateTotalAndAverageRevenue(revenueResult, productList.Count);
            ManageVisibilityAndDataBinding(dt, true);

            string labelText = "Beachte: Gutscheine";
            if (ddlCategory.SelectedValue.Equals("Pizza"))
            {
                labelText += " und Extras";
            }
            labelText += " werden nicht berücksichtigt.";
            lblAnnotation.Text = labelText;
            lblAnnotation.Visible = true;
        }

        protected void GetOrdersOfUser()
        {
            DataTable dt = new DataTable("OrderStats");
            BuildDataTable(dt, new List<String> { "Bestelldatum", "Kunde", "Bestellnummer", "Bestellstatus", "Lieferdatum", "Summe" });

            List<clsOrderExtended> orderList = new clsOrderFacade().GetOrdersByEmail(ddlUser.SelectedValue);
            double revenueResult = 0;
            foreach (clsOrderExtended _order in orderList)
            {
                dt.LoadDataRow(new object[] { _order.OrderDate, _order.UserName, _order.OrderNumber,
              _order.OrderStatusDescription, _order.OrderDeliveryDate, String.Format("{0:C}",_order.OrderSum) }, true);

                revenueResult += _order.OrderSum;
            }

            CalculateTotalAndAverageRevenue(revenueResult, orderList.Count);
            ManageVisibilityAndDataBinding(dt, true);
        }

        protected void GetFanciestProducts()
        {
            DataTable dt = new DataTable("OrderStats");
            BuildDataTable(dt, new List<String> { "Name des Produkts", "Anzahl der Käufe" });


            OrderedDictionary favoriteProduct = new clsProductFacade().GetMostFanciestProduct();
            foreach (DictionaryEntry entry in favoriteProduct)
            {
                dt.LoadDataRow(new object[] { entry.Key, entry.Value }, true);
            }
            ManageVisibilityAndDataBinding(dt, false);
        }

        protected void GetOrdersOrderedByStatus()
        {
            DataTable dt = new DataTable("OrderStats");
            BuildDataTable(dt, new List<String> { "Bestellstatus", "Anzahl der Bestellungen", "Gesamtsumme" });


            List<Tuple<String, Int32, Double>> _resultTuples = new clsOrderFacade().GetOrdersOrderedByStatus();
            foreach (Tuple<String, Int32, Double> _tuple in _resultTuples)
            {
                dt.LoadDataRow(new object[] { _tuple.Item1, _tuple.Item2, _tuple.Item3 }, true);
            }
            ManageVisibilityAndDataBinding(dt, false);
        }

        protected void GetOrdersOrderedByDeliveryTime()
        {
            DataTable dt = new DataTable("OrderStats");
            BuildDataTable(dt, new List<String> { "Bestellnummer", "Dauer der Lieferung in Minuten" });


            Dictionary<Int32, Int32> _resultTuples = new clsOrderFacade().GetTimeToDeliverOfOrders();

            for (int i = 0; i < _resultTuples.Count; i++)
            {
                dt.LoadDataRow(new object[] { _resultTuples.Keys.ElementAt(i), _resultTuples.Values.ElementAt(i) + " min" }, true);
            }

            ManageVisibilityAndDataBinding(dt, false);
        }

        protected void GetProductsOrderedByRevenue()
        {
            DataTable dt = new DataTable("ProductSales");
            BuildDataTable(dt, new List<String> { "Name des Produkts", "Höhe des Umsatzes" });

            Dictionary<string, double> products = new clsProductFacade().GetProductsOrderedByTotalRevenue();
            foreach (KeyValuePair<string, double> _entry in products)
            {
                dt.LoadDataRow(new object[] { _entry.Key, String.Format("{0:C}", _entry.Value) }, true);
            }
            ManageVisibilityAndDataBinding(dt, false);
        }

        protected void GetCustomersOrderedByRevenue()
        {
            DataTable dt = new DataTable("CustomerStats");
            BuildDataTable(dt, new List<String> { "Kunde", "Gesamtumsatz", "Anzahl der Bestellungen" });

            List<Tuple<string, double, int>> userList = new clsUserFacade().GetUsersOrderedByTotalRevenue();
            foreach (Tuple<string, double, int> _user in userList)
            {
                dt.LoadDataRow(new object[] { _user.Item1, String.Format("{0:C}", _user.Item2), _user.Item3 }, true);
            }
            ManageVisibilityAndDataBinding(dt, false);
        }

        protected void CreateStatisticDependingOnSelection()
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
                            GetOrdersOrderedByDate();
                            break;
                        case "pro Kategorie":
                            GetOrdersOrderedByCategory();
                            break;
                        case "des Kunden":
                            GetOrdersOfUser();
                            break;
                        case "nach Status":
                            GetOrdersOrderedByStatus();
                            break;
                        case "nach Lieferdauer":
                            GetOrdersOrderedByDeliveryTime();
                            break;
                    }
                    break;

                // Auswertungen für Produkte
                case "Produkte":
                    switch (secondIndex)
                    {
                        case "sortiert nach Beliebtheit":
                            GetFanciestProducts();
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
                    //Nicht bekannte Auswertungsart
                    break;
            }
        }

        protected void btnCreateStats_Click(object sender, EventArgs e)
        {
            CreateStatisticDependingOnSelection();
        }

        protected void ddlStatsExtended_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedItem = ddlStatsExtended.SelectedItem.Text;

            if (selectedItem == "pro Kategorie" || selectedItem == "des Kunden")
            {
                InitializeUserAndCategoryDropDownList();
                ShowCategoriesForOrdersByCategory();
                ShowUsersForOrdersByUser();
            }
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            RedirectAdministration();
        }

        private void RedirectAdministration()
        {
            Session["stats"] = null;
            Session["initialized"] = null;
            Response.Redirect("administration.aspx");
        }
    }
}