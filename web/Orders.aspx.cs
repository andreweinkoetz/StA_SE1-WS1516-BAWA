using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bll;

namespace web
{
    public partial class Orders : System.Web.UI.Page
    {
        private List<clsProductExtended> selectedProducts;

        protected void Page_Load(object sender, EventArgs e)
        {
            selectedProducts = (List<clsProductExtended>)Session["selProducts"];

            //Bei Aktualisierung des Warenkorbs kann ein neuer Gutschein eingegeben werden - ein nicht eingelöster bleibt aktiv.
            if (!IsPostBack)
            {
                Session["coupon"] = null;
            }

            if (!IsPostBack && (Session["selProducts"] != null) && Session["coupon"] == null)
            {
                if (selectedProducts.Count != 0)
                {
                    //Warenkorb auflisten.
                    InitializeOrderView(selectedProducts);

                    //Gesamtsumme berechnen und anzeigen.
                    double _sum = clsOrderFacade.GetOrderSum(selectedProducts);
                    lblSum.Text = "Gesamtsumme: " + String.Format("{0:C}", _sum);

                    //Prüfen ob Lieferung gewählt und Mindestbestellwert erfüllt.
                    CheckDelivery();
                    CheckMinimumOrder(_sum);
                }
            }

            if (IsPostBack && Session["selProducts"] == null)
            {
                Response.Redirect("orders.aspx", true);
            }
        }

        protected void chkDelivery_CheckedChanged(object sender, EventArgs e)
        {
            CheckMinimumOrder(clsOrderFacade.GetOrderSum(selectedProducts, (clsCoupon)Session["coupon"]));
        }

        /// <summary>
        /// Prüft ob eine Lieferung möglich ist (20km)
        /// </summary>
        private void CheckDelivery()
        {
            if (GetDistance() > 20.0)
            {
                chkDelivery.Enabled = false;
                chkDelivery.Checked = false;
            }
        }

        /// <summary>
        /// Prüft ob die angegebene Gesamtsumme den Mindestbestellwert erreicht.
        /// </summary>
        /// <param name="_sum">Gesamtsumme der Bestellung</param>
        private void CheckMinimumOrder(double _sum)
        {
            String _msg;
            btOrder.Enabled = clsOrderFacade.CheckMinimumOrder(_sum, chkDelivery.Checked, out _msg);
            lblStatus.Text = _msg;
        }

        /// <summary>
        /// Liefert die Distanz zum Endkunden in km.
        /// </summary>
        /// <returns>Distanz in km</returns>
        private double GetDistance()
        {
            return new clsUserFacade().GetDistanceByUser(Convert.ToInt32(Session["userID"]));
        }

        private void InitializeOrderView(List<clsProductExtended> _selectedProducts)
        {
            DataTable dt = new clsOrderExtended().CreateDataTableOfOrder(_selectedProducts);

            gvOrder.DataSource = dt;
            gvOrder.DataBind();
        }

        protected void clearCart_Click(object sender, EventArgs e)
        {
            Session["selProducts"] = null;
        }

        protected void btOrder_Click(object sender, EventArgs e)
        {
            InsertOrder();
        }

        /// <summary>
        /// Prüft auf korrekte Eingaben und fügt
        /// die Bestellung in die DB ein.
        /// 
        /// Falls Einfügen erfolgreich ist, wird dem User ein entsprechender Hinweis
        /// angezeigt sowie die vorraussichtliche Lieferdauer (falls Lieferung gewählt)
        /// Im Fehlerfall erscheint ein passender Hinweis.
        /// </summary>
        private void InsertOrder()
        {
            bool orderIsCorrect = true;
            clsOrderFacade _orderFacade = new clsOrderFacade();
            clsOrderExtended _myOrder = new clsOrderExtended();

            _myOrder.OrderNumber = _myOrder.GetHashCode(); //eindeutige Nummer
            _myOrder.UserId = (int)Session["userID"];

            _myOrder.OrderDate = DateTime.Now;  //Bestellungseingang
            _myOrder.OrderStatus = 1; // Bestellung eingangen!
            _myOrder.OrderDelivery = chkDelivery.Checked;

            if (Session["coupon"] != null)
            {
                _myOrder.MyCoupon = (clsCoupon)Session["coupon"];
                _myOrder.CouponId = _myOrder.MyCoupon.Id;
            }
            else
            {
                _myOrder.CouponId = 0;
            }

            //Berechnung der Bestellsumme ausgelagert in OrderFacade.
            _myOrder.OrderSum = clsOrderFacade.GetOrderSum(selectedProducts, _myOrder.MyCoupon);

            //Jedes Produkt inkl. seiner Extras einfügen.
            foreach (clsProductExtended _product in selectedProducts)
            {
                orderIsCorrect = new clsOrderFacade().InsertOrderedProductWithExtras(_myOrder, _product) && orderIsCorrect;
            }

            //Bestellung gesamt einfügen.
            orderIsCorrect = _orderFacade.InsertOrder(_myOrder) && orderIsCorrect;

            if (orderIsCorrect)
            {
                //Wenn Gutschein eingelöst, dann deaktivieren.
                if (_myOrder.CouponId != 0)
                {
                    new clsCouponFacade().ToggleCoupon(_myOrder.CouponId);
                }

                OrderPlacedSuccessfully(_myOrder);
            }
        }

        /// <summary>
        /// Meldet die erfolgreiche Bestellung an den Benutzer.
        /// </summary>
        /// <param name="_myOrder">Bestellung</param>
        private void OrderPlacedSuccessfully(clsOrderExtended _myOrder)
        {
            lblOrder.ForeColor = System.Drawing.Color.Red;
            lblOrder.Text = "Ihre Bestellung war erfolgreich. Bestellnummer: #" + _myOrder.OrderNumber;
            lblEmptyCart.Text = "";
            SetEstimatedTime();
            Session["selProducts"] = null;
        }

        /// <summary>
        /// Anzeigen der vorraussichtlichen Lieferzeit.
        /// </summary>
        private void SetEstimatedTime()
        {
            lblEmptyCart.Text = clsOrderFacade.GetEstimatedTime(selectedProducts, GetDistance(), chkDelivery.Checked);
            lblEmptyCart.Visible = true;
        }

        protected void btCoupon_Click(object sender, EventArgs e)
        {
            ValidateCouponAndUpdateUI();
        }

        /// <summary>
        /// Prüft den eingegeben Gutscheincode und passt die Bestellung an.
        /// Im Fehlerfall wird ein entsprechender Hinweis angezeigt.
        /// </summary>
        private void ValidateCouponAndUpdateUI()
        {
            clsCoupon _myCoupon;
            if (!String.IsNullOrEmpty(txtCouponCode.Text))
            {
                if (ValidateCoupon(txtCouponCode.Text, (int)Session["userID"], out _myCoupon))
                {
                    string _msg;
                    double _newSum = clsOrderFacade.GetOrderSum(selectedProducts, _myCoupon, out _msg);
                    txtCouponCode.Enabled = false;
                    Session["coupon"] = _myCoupon;
                    CheckMinimumOrder(_newSum);
                    lblCouponValid.Text = _msg;
                    lblNewSum.Text = "Neue Gesamtsumme: " + String.Format("{0:C}", _newSum);
                }
                else
                {
                    lblErrorCoupon.Text = "Gutschein ist fehlerhaft bzw. passt nicht zu angemeldetem Benutzer.";
                }
            }
        }

        /// <summary>
        /// Prüft ob Gutschein gültig und zu entsprechendem User gehört.
        /// </summary>
        /// <param name="_couponCode">Gutscheincode</param>
        /// <param name="_uid">User-ID</param>
        /// <param name="_myCoupon">eingegebener Gutschein</param>
        /// <returns></returns>
        private bool ValidateCoupon(String _couponCode, int _uid, out clsCoupon _myCoupon)
        {
            clsCouponFacade _couponFacade = new clsCouponFacade();
            return _couponFacade.CheckCouponValid(_couponCode, _uid, out _myCoupon);
        }
    }
}