using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die nach außen sichtbaren Methoden bzgl. der Gutschein-Verwaltung bereit.
    /// Als Grundlage werden die clsCouponCollection-Methoden verwendet.
    /// </summary>
    public class clsCouponFacade
    {
        private clsCouponCollection _couponCol; // Objektvariable für die Coupon-Collection, wird im Konstruktor instantiiert

        /// <summary>
        /// Erstellt ein neues Objekt der clsCouponCollection.
        /// </summary>
        public clsCouponFacade()
        {
            _couponCol = new clsCouponCollection();
        }

        /// <summary>
        /// Liefert alle aktiven Gutscheincodes eines Kunden zurück.
        /// </summary>
        /// <param name="_uid">ID des Kunden</param>
        /// <returns>alle aktiven Gutscheincodes</returns>
        public List<clsCoupon> GetAllActiveCouponsByUser(int _uid)
        {
            return _couponCol.GetAllActiveCouponsByUser(_uid);
        }

        /// <summary>
        /// Fügt einen neuen Gutschein in die Datenbank ein und weist ihn einem Benutzer zu.
        /// </summary>
        /// <param name="_myCoupon">der einzufügende Gutschein</param>
        /// <returns>true, falls das Einfügen erfolgreich ist</returns>
        public bool InsertCoupon(clsCoupon _myCoupon)
        {
            return _couponCol.InsertCoupon(_myCoupon) == 1;
        }

        /// <summary>
        /// Liefert alle vorhandenen Gutscheine zurück.
        /// </summary>
        /// <returns>alle vorhandenen Gutscheine</returns>
        public List<clsCoupon> GetAllCoupons()
        {
            return _couponCol.GetAllCoupons();
        }

        /// <summary>
        /// Liefert den Gutschein mit der angegebenen ID zurück.
        /// </summary>
        /// <param name="_cuid">ID des Gutscheins</param>
        /// <returns>Gutschein mit der angegebenen ID</returns>
        public clsCoupon GetCouponById(int _cuid)
        {
            return _couponCol.GetCouponById(_cuid);
        }

        /// <summary>
        /// Aktiviert bzw. deaktiviert einen Gutschein.
        /// </summary>
        /// <param name="_cuid">ID des Gutscheins</param>
        /// <returns>true, falls die Aktivierung bzw. Deaktivierung erfolgreich ist</returns>
        public bool ToggleCoupon(int _cuid)
        {
            return _couponCol.ToggleCoupon(_cuid) == 1;
        }

        /// <summary>
        /// Prüft, ob der eingegebene Gutschein-Code vom angemeldeten Benutzer eingelöst werden kann.
        /// </summary>
        /// <param name="_couponCode">Gutschein-Code, den der Benutzer eingegeben hat</param>
        /// <param name="_uid">User-ID des angemeldeten Benutzers</param>
        /// <param name="_coupon">Gutschein, falls die Validierung erfolgreich ist</param>
        /// <returns>true, falls der Gutschein-Code vom angemeldeten Benutzer eingelöst werden kann</returns>
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
        /// Liefert alle aktiven Benutzer zurück, die einen Gutschein erhalten können.
        /// </summary>
        /// <returns>alle aktiven Benutzer, die einen Gutschein erhalten können</returns>
        public Dictionary<int, String> GetAllUsersForCoupons()
        {
            return _couponCol.GetAllUsersForCoupons();
        }
    }
}
