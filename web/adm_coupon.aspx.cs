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
            btToggleCoupon.Enabled = true;
            btToggleCoupon.BackColor = System.Drawing.ColorTranslator.FromHtml("#CF323D");
        }

        protected void btCreateNew_Click(object sender, EventArgs e)
        {
            bool readyForDB = true, insertSuccessful = false;

            clsCoupon _myCoupon = new clsCoupon();

            if (!String.IsNullOrEmpty(txtCode.Text))
            {
                _myCoupon.Code = txtCode.Text;
                
            } else
            {
                txtCode.BackColor = System.Drawing.Color.Red;
                readyForDB = false;
            }

            int _discount;
            if (Int32.TryParse(txtDiscount.Text, out _discount) && (_discount > 0 && _discount <= 100))
            {
                    _myCoupon.Discount = _discount;
            } else
            {
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
                }
            }  else
            {
                lblError.Text = "Bitte beachten Sie die rot markierten Felder.";
            }

            gvAdmCoupon.DataBind();

        }

        protected void btToggleCoupon_Click(object sender, EventArgs e)
        {
            int _cuid = Int32.Parse(gvAdmCoupon.SelectedRow.Cells[1].Text);
            new clsCouponFacade().ToggleCoupon(_cuid);
            gvAdmCoupon.DataBind();
        }

        protected void btGenerateCode_Click(object sender, EventArgs e)
        {
            txtCode.Text = clsCoupon.GenerateCode();
        }
    }
}