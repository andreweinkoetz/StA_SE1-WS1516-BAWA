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
                fillUserDropDownList();
                btnCreateStats.Enabled = false;
                btnCreateStats.BackColor = System.Drawing.Color.Gray;
            }

            manageExtendedStats();
            manageUser();
            if (ddlStats.SelectedIndex != previousIndex)
            {
                fillExtendedStatsDropDownList(sender, e);
                ddlUser.Visible = false;
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
        }

        protected void ddlStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            previousIndex = ddlStats.SelectedIndex;
        }

        protected void fillUserDropDownList()
        {
            clsUserFacade userFacade = new clsUserFacade();

            List<clsUser> users = userFacade.getAllUsers();
            ddlUser.DataSource = users;
            ddlUser.DataTextField = "Email";
            ddlUser.DataValueField = "Email";
            ddlUser.DataBind();
            ddlStatsExtended.Visible = false;
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

        protected void getAllOrdersOrderedByDate()
        {
            DataTable dt = new DataTable("OrderStats");

            dt.Columns.Add("Bestelldatum");
            dt.Columns.Add("Kunde");
            dt.Columns.Add("Bestellnummer");
            dt.Columns.Add("Bestellstatus");
            dt.Columns.Add("Lieferdatum");
            dt.Columns.Add("Summe");

            List<clsOrderExtended> orderList = orderFacade.getOrdersOrderedByDate();

            foreach (clsOrderExtended _order in orderList)
            {
                dt.LoadDataRow(new object[] { _order.OrderDate, _order.UserName, _order.OrderNumber,
              _order.OrderStatusDescription, _order.OrderDeliveryDate, _order.OrderSum }, true);
            }

            gvStats.DataSource = dt;
            gvStats.DataBind();

        }

        protected void getOrdersFromASpecificUser()
        {

            DataTable dt = new DataTable("OrderStats");

            dt.Columns.Add("Bestelldatum");
            dt.Columns.Add("Kunde");
            dt.Columns.Add("Bestellnummer");
            dt.Columns.Add("Bestellstatus");
            dt.Columns.Add("Lieferdatum");
            dt.Columns.Add("Summe");

            List<clsOrderExtended> orderList = orderFacade.getOrdersByEmail(ddlUser.SelectedValue);

            foreach (clsOrderExtended _order in orderList)
            {
                dt.LoadDataRow(new object[] { _order.OrderDate, _order.UserName, _order.OrderNumber,
              _order.OrderStatusDescription, _order.OrderDeliveryDate, _order.OrderSum }, true);
            }
            gvStats.DataSource = dt;
            gvStats.DataBind();
        }

        protected void getOrdersOrderedByCategory()
        {

        }

        protected void getFanciestProduct()
        {
            DataTable dt = new DataTable("OrderStats");

            dt.Columns.Add("Name des Produkts");
            dt.Columns.Add("Anzahl der Käufe");

            OrderedDictionary favoriteProduct = productFacade.getMostFanciedProduct();

            foreach (DictionaryEntry entry in favoriteProduct)
            {
                dt.LoadDataRow(new object[] { entry.Key, entry.Value }, true);
            }

            gvStats.DataSource = dt;
            gvStats.DataBind();
        }

        protected void getProductsOrderedBySales()
        {
            DataTable dt = new DataTable("ProductSales");

            dt.Columns.Add("Name des Produkts");
            dt.Columns.Add("Höhe des Umsatzes");

            OrderedDictionary amountOfProducts = productFacade.getMostFanciedProduct();

            //Namen der Produkte
            List<string> dictionaryKeysToString = new List<string>(amountOfProducts.Count);
            foreach (string key in amountOfProducts.Keys)
            {
                dictionaryKeysToString.Add("" + key);
            }

            //Anzahl der Käufe der Produkte
            List<int> dictionaryValuesToString = new List<int>(amountOfProducts.Count);
            foreach (int value in amountOfProducts.Values)
            {
                dictionaryValuesToString.Add(value);
            }

            // Befüllen der Tabelle mit Produktnamen + zugehörigen Umsatz
            for (int i = 0; i < amountOfProducts.Count; i++)
            {
                dt.LoadDataRow(new object[] { dictionaryKeysToString[i], dictionaryValuesToString[i] * productFacade.getProductPricePerUnit(dictionaryKeysToString[i]) }, true);
            }

            gvStats.DataSource = dt;
            gvStats.DataBind();
        }

        protected void getCustomersOrderedBySales()
        {

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
                            getAllOrdersOrderedByDate();
                            break;
                        case "pro Kategorie":
                            getOrdersOrderedByCategory();
                            break;
                        case "des Kunden:":
                            getOrdersFromASpecificUser();
                            break;
                    }
                    break;

                // Auswertungen für Produkte
                case "Produkte":
                    switch (secondIndex)
                    {
                        case "sortiert nach Beliebtheit":
                            getFanciestProduct();
                            break;
                        case "sortiert nach Umsatz":
                            getProductsOrderedBySales();
                            break;
                    }
                    break;

                //Auswertungen für Kunden
                case "Kunden":
                    switch (secondIndex)
                    {
                        case "sortiert nach Umsatz":
                            getCustomersOrderedBySales();
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}