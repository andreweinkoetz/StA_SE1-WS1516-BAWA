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

            if (!IsPostBack)
            {
                Session["coupon"] = null;
            }

            if (!IsPostBack && (Session["selProducts"] != null) && Session["coupon"] == null)
            {
                if (selectedProducts.Count != 0)
                {
                    InitializeOrderView(selectedProducts);
                    double _sum = clsOrderFacade.GetOrderSum(selectedProducts);
                    lblSum.Text = "Gesamtsumme: " + String.Format("{0:C}", _sum);
                    CheckDelivery();
                    CheckMinimumOrder(_sum);
                }
            }
            else if (Session["selProducts"] != null)
            {
                selectedProducts = (List<clsProductExtended>)Session["selProducts"];
            }

        }

        private void CheckDelivery()
        {
            if (GetDistance() > 20.0)
            {
                chkDelivery.Enabled = false;
                chkDelivery.Checked = false;
            }
        }

        private void CheckMinimumOrder(double _sum)
        {
            String _msg;
            btOrder.Enabled = clsOrderFacade.CheckMinimumOrder(_sum, chkDelivery.Checked, out _msg);
            lblStatus.Text = _msg;
        }

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
            bool orderIsCorrect = true;
            clsOrderFacade _orderFacade = new clsOrderFacade();
            clsOrderExtended _myOrder = new clsOrderExtended();
            _myOrder.OrderNumber = _myOrder.GetHashCode();
            _myOrder.UserId = (int)Session["userID"];
            _myOrder.OrderDate = DateTime.Now;
            _myOrder.OrderStatus = 1; // Bestellung eingangen!
            _myOrder.OrderDelivery = chkDelivery.Checked;
            
            if(Session["coupon"] != null)
            {
                _myOrder.MyCoupon = (clsCoupon)Session["coupon"];
                _myOrder.CouponId = _myOrder.MyCoupon.Id;
            } else
            {
                _myOrder.CouponId = 0;
            }

            _myOrder.OrderSum = clsOrderFacade.GetOrderSum(selectedProducts,_myOrder.MyCoupon);

            foreach (clsProductExtended _product in selectedProducts)
            {
                _product.OpID = _product.GetHashCode() + _myOrder.OrderNumber;
                orderIsCorrect = _orderFacade.InsertOrderedProduct(_myOrder, _product) && orderIsCorrect;
                if (_product.ProductExtras != null)
                {
                    if (_product.ProductExtras.Count > 0)
                    {
                        orderIsCorrect = _orderFacade.InsertOrderedExtras(_product, _product.ProductExtras) && orderIsCorrect;
                    }
                }
            }
            orderIsCorrect = _orderFacade.InsertOrder(_myOrder) && orderIsCorrect;

            if (orderIsCorrect)
            {
                if(_myOrder.CouponId != 0)
                {
                    new clsCouponFacade().ToggleCoupon(_myOrder.CouponId);
                }
                lblOrder.ForeColor = System.Drawing.Color.Red;
                lblOrder.Text = "Ihre Bestellung war erfolgreich. Bestellnummer: #" + _myOrder.OrderNumber;
                lblEmptyCart.Text = "";
                setEstimatedTime();
                Session["selProducts"] = null;
            }

        }

        private void setEstimatedTime()
        {
            lblEmptyCart.Text = clsOrderFacade.GetEstimatedTime(selectedProducts,GetDistance(),chkDelivery.Checked);
            lblEmptyCart.Visible = true;
        }

        private bool ValidateCoupon(String _couponCode, int _uid, out clsCoupon _myCoupon)
        {
            clsCouponFacade _couponFacade = new clsCouponFacade();
            return _couponFacade.CheckCouponValid(_couponCode, _uid, out _myCoupon);
        }

        protected void chkDelivery_CheckedChanged(object sender, EventArgs e)
        {
            CheckMinimumOrder(clsOrderFacade.GetOrderSum(selectedProducts,(clsCoupon)Session["coupon"]));
        }

        protected void btCoupon_Click(object sender, EventArgs e)
        {
            clsCoupon _myCoupon;
            if (!String.IsNullOrEmpty(txtCouponCode.Text))
            {
                if(ValidateCoupon(txtCouponCode.Text, (int)Session["userID"], out _myCoupon))
                {
                    string _msg;
                    double _newSum = clsOrderFacade.GetOrderSum(selectedProducts, _myCoupon, out _msg);
                    txtCouponCode.Enabled = false;
                    Session["coupon"] = _myCoupon;
                    CheckMinimumOrder(_newSum);
                    lblCouponValid.Text = _msg;
                    lblNewSum.Text = "Neue Gesamtsumme: " + String.Format("{0:C}", _newSum);
                } else
                {
                    lblErrorCoupon.Text = "Gutschein ist fehlerhaft bzw. passt nicht zu angemeldetem Benutzer.";
                }
            }
        }
    }
}