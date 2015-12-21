using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// clsCouponFacade: Nach aussen sichtbare Fassade für Gutscheine.
    /// </summary>
    public class clsCouponFacade
    {
        private clsCouponCollection _couponCol;

        /// <summary>
        /// Standardkonstruktor für eine neue Coupon-Fassade.
        /// </summary>
        public clsCouponFacade()
        {
            _couponCol = new clsCouponCollection();
        }

        /// <summary>
        /// Liefert eine Liste aller Gutscheine eines bestimmten Benutzers.
        /// </summary>
        /// <param name="_uid">Id des Benutzers.</param>
        /// <returns>Liste seiner Gutscheine.</returns>
        public List<clsCoupon> GetAllActiveCouponsByUser(int _uid)
        {
            return _couponCol.GetAllActiveCouponsByUser(_uid);
        }

        /// <summary>
        /// Fügt einen neuen Gutschein hinzu und weist ihn einem Benutzer zu.
        /// </summary>
        /// <param name="_myCoupon">Hinzuzufügender Gutschein.</param>
        /// <returns>true wenn Einfügen erfolgreich.</returns>
        public bool InsertCoupon(clsCoupon _myCoupon)
        {
            return _couponCol.InsertCoupon(_myCoupon) == 1;
        }

        /// <summary>
        /// Liefert eine Liste aller Gutscheine.
        /// </summary>
        /// <returns>Liste aller Gutscheine.</returns>
        public List<clsCoupon> GetAllCoupons()
        {
            return _couponCol.GetAllCoupons();
        }

        /// <summary>
        /// Liefert einen bestimmten Gutschein.
        /// </summary>
        /// <param name="_cuid">Id des zu liefernden Gutscheins.</param>
        /// <returns>Gutschein-Objekt</returns>
        public clsCoupon GetCouponById(int _cuid)
        {
            return _couponCol.GetCouponById(_cuid);
        }

        /// <summary>
        /// (De-)Aktiviert einen Gutschein. 
        /// </summary>
        /// <param name="_cuid">Id des Gutscheins, der (de-)aktiviert werden soll.</param>
        /// <returns>true wenn (De-)Aktivierung erfolgreich.</returns>
        public bool ToggleCoupon(int _cuid)
        {
            return _couponCol.ToggleCoupon(_cuid) == 1;
        }


        /// <summary>
        /// Prüft ob ein eingegebener Gutschein-Code zum angemeldeten Benutzer passt.
        /// </summary>
        /// <param name="_couponCode">Gutschein-Code, welchen der Benutzer eingegeben hat.</param>
        /// <param name="_uid">User-Id des angemeldeten Benutzers.</param>
        /// <param name="_coupon">Coupon, falls Validierung erfolgreich</param>
        /// <returns>true, wenn Gutscheincode zu angemeldetem Benutzer passt.</returns>
        public bool CheckCouponValid(String _couponCode, int _uid, out clsCoupon _coupon)
        {
            List<clsCoupon> _myCouponList = _couponCol.GetAllActiveCouponsByUser(_uid);

            foreach (clsCoupon _myCoupon in _myCouponList)
            {
                if (_myCoupon.Code == _couponCode)
                {
                    _coupon = _myCoupon;
                    return true;
                }
            }
            _coupon = null;
            return false;
        }

        /// <summary>
        /// Liefert ein Dictionary aller aktiven Benutzer, zur Auswahl der Gutscheinzuweisung.
        /// </summary>
        /// <returns>Dictionary aller aktiven Benutzer, key=User-ID; value=Voller Name des Benutzers.</returns>
        public Dictionary<int, String> GetAllUsersForCoupons()
        {
            return _couponCol.GetAllUsersForCoupons();
        }
    }
}
