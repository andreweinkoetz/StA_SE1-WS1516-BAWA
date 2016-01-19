using bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class AdmCoupon_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["roleID"] != null)
            {
                if((int)Session["roleID"] > 1)
                {
                    Response.Redirect("administration.aspx");
                }
            } else
            {
                Response.Redirect("login_page.aspx");
            }
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("administration.aspx");
        }

        protected void gvAdmCoupon_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeActivateCouponButtons(true);
        }

        protected void btCreateNew_Click(object sender, EventArgs e)
        {
            InsertNewCoupon();
        }

        /// <summary>
        /// Einfügen eines neuen Coupons in die DB.
        /// </summary>
        private void InsertNewCoupon()
        {
            bool readyForDB = true, insertSuccessful = false;

            clsCoupon _myCoupon = new clsCoupon();

            if (!String.IsNullOrEmpty(txtCode.Text))
            {
                _myCoupon.Code = txtCode.Text;

            }
            else
            {
                txtCode.BackColor = System.Drawing.Color.Red;
                readyForDB = false;
            }

            int _discount;
            if (Int32.TryParse(txtDiscount.Text, out _discount) && (_discount > 0 && _discount <= 100))
            {
                _myCoupon.Discount = _discount;
            }
            else
            {
                if (String.IsNullOrEmpty(txtDiscount.Text))
                {
                    txtDiscount.BackColor = System.Drawing.Color.Red;
                }
                txtDiscount.ForeColor = System.Drawing.Color.Red;
                readyForDB = false;
            }

            _myCoupon.IsValid = true;
            _myCoupon.Uid = Int32.Parse(ddlUsers.SelectedValue);

            if (readyForDB)
            {
                insertSuccessful = new clsCouponFacade().InsertCoupon(_myCoupon);

                if (!insertSuccessful)
                {
                    lblError.Text = "Fehler beim Einfügen in die Datenbank.<br />Hat dieser Benutzer evtl. schon diesen Gutschein? (siehe oben)";
                    lblError.Visible = true;
                } else
                {
                    lblError.Visible = false;
                    txtCode.BackColor = txtDiscount.BackColor = System.Drawing.Color.White;
                    txtCode.ForeColor = txtDiscount.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                lblError.Text = "Bitte beachten Sie die rot markierten Felder.";
            }

            gvAdmCoupon.DataBind();
        }

        protected void btToggleCoupon_Click(object sender, EventArgs e)
        {
            DeActivateSelectedCoupon();
        }

        /// <summary>
        /// De- bzw. Aktivieren des gewählten Coupons.
        /// </summary>
        private void DeActivateSelectedCoupon()
        {
            int _cuid = Int32.Parse(gvAdmCoupon.SelectedRow.Cells[1].Text);
            new clsCouponFacade().ToggleCoupon(_cuid);
            gvAdmCoupon.SelectedIndex = -1;
            DeActivateCouponButtons(false);
            gvAdmCoupon.DataBind();
        }

        /// <summary>
        /// Aktiviert bzw. Deaktiviert die Buttons zur Bearbeitung
        /// von Coupons.
        /// </summary>
        /// <param name="_active">true wenn Buttons aktiv gesetzt werden sollen.</param>
        private void DeActivateCouponButtons(bool _active)
        {
            if (_active)
            {
                btToggleCoupon.Enabled = true;
                btToggleCoupon.BackColor = System.Drawing.ColorTranslator.FromHtml("#CF323D");
            } else
            {
                btToggleCoupon.Enabled = false;
                btToggleCoupon.BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void btGenerateCode_Click(object sender, EventArgs e)
        {
            txtCode.Text = clsCoupon.GenerateCode();
        }
    }
}