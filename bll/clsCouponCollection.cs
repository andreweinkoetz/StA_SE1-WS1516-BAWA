using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Bietet Zugriff zum DAL, ist nur in der BLL sichtbar und stellt Methoden für clsCoupon-Objekte bereit.
    /// </summary>
    internal class clsCouponCollection : clsBLLCollections
    {
        string _databaseFile; // String zur Access-Datei, wird im Konstruktor initialisiert
        DAL.DALObjects.dDataProvider _myDAL; // DAL-Objekt für den Zugriff auf die Datenbank

        /// <summary>
        /// Stellt eine Verbindung zur Datenbank her.
        /// </summary>
        internal clsCouponCollection()
        {
            // Hier wird der Pfad zur Access-Datei aus web.config gelesen und eine DAL-Instanz erzeugt, die den Zugriff auf die Datenbank ermöglicht
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        /// <summary>
        /// Liefert alle aktiven Gutscheincodes eines Kunden zurück.
        /// </summary>
        /// <param name="_uid">ID des Kunden</param>
        /// <returns>alle aktiven Gutscheincodes</returns>
        internal List<clsCoupon> GetAllActiveCouponsByUser(int _uid)
        {
            _myDAL.AddParam("UID", _uid, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QCUGetAllActiveCouponsByUser");
            DataTable _myDataTable = _myDataSet.Tables[0];

            List<clsCoupon> _myCouponList = new List<clsCoupon>();
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsCoupon _myCoupon = DataRowToClsCoupon(_dr);
                _myCouponList.Add(_myCoupon);
            }
            return _myCouponList;
        }

        /// <summary>
        /// Fügt einen neuen Gutschein in die Datenbank ein und weist ihn einem Benutzer zu.
        /// </summary>
        /// <param name="_myCoupon">der einzufügende Coupon</param>
        /// <returns>Anzahl der eingefügten Datensätze</returns>
        internal int InsertCoupon(clsCoupon _myCoupon)
        {
            _myDAL.AddParam("Code", _myCoupon.Code, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Discount", _myCoupon.Discount, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("IsActive", _myCoupon.IsValid, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myDAL.AddParam("FKUserId", _myCoupon.Uid, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int _affectedRows = -1;
            try
            {
                //Try-Catch-Block hier nötig, um einen möglichen DB-Insert-Fehler aufgrund des Primary Keys des Coupons zu behandeln
                _affectedRows = _myDAL.MakeStoredProcedureAction("QCInsertCoupon");
            }
            catch { /*Fehlerbehandlung hier nicht nötig, da -1 zurückgegeben wird, was zu einer passenden Fehlermeldung für den Kunden führt.*/ }
            return _affectedRows;
        }

        /// <summary>
        /// Liefert alle vorhandenen Coupons zurück.
        /// </summary>
        /// <returns>alle vorhandenen Coupons</returns>
        internal List<clsCoupon> GetAllCoupons()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QCUGetAllCoupons");
            DataTable _myDataTable = _myDataSet.Tables[0];
            List<clsCoupon> _myCouponList = new List<clsCoupon>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsCoupon _myCoupon = DataRowToClsCoupon(_dr);
                _myCouponList.Add(_myCoupon);
            }
            return _myCouponList;
        }

        /// <summary>
        /// Liefert den Coupon mit der angegebenen ID zurück.
        /// </summary>
        /// <param name="_cuid">ID des Coupons</param>
        /// <returns>Coupon mit der angegebenen ID</returns>
        internal clsCoupon GetCouponById(int _cuid)
        {
            _myDAL.AddParam("Id", _cuid, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QCUGetCouponById");
            DataTable _myDataTable = _myDataSet.Tables[0];
            clsCoupon _myCoupon = DataRowToClsCoupon(_myDataTable.Rows[0]);
            return _myCoupon;
        }

        /// <summary>
        /// Aktiviert bzw. deaktiviert einen Coupon.
        /// </summary>
        /// <param name="_cuid">ID des Coupons</param>
        /// <returns>Anzahl der betroffenen Datensätze</returns>
        internal int ToggleCoupon(int _cuid)
        {
            _myDAL.AddParam("ID", _cuid, DAL.DataDefinition.enumerators.SQLDataType.INT);
            int _affectedRows = _myDAL.MakeStoredProcedureAction("QCUToggleCoupon");
            return _affectedRows;
        }

        /// <summary>
        /// Liefert alle aktiven Benutzer zurück, die einen Gutschein erhalten können.
        /// </summary>
        /// <returns>alle aktiven Benutzer, die einen Gutschein erhalten können</returns>
        internal Dictionary<int, String> GetAllUsersForCoupons()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QCUGetAllUsers");
            DataTable _myDataTable = _myDataSet.Tables[0];

            Dictionary<int, String> _myUserDictionary = new Dictionary<int, string>();
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                int _key = AddIntFieldValue(_dr, "UID");
                String _value = AddStringFieldValue(_dr, "Fullname");
                _myUserDictionary.Add(_key, _value);
            }
            return _myUserDictionary;
        }

        /// <summary>
        /// Erstellt aus einem Datensatz einen Coupon.
        /// </summary>
        /// <param name="_dr">Datensatz in der Datenbank</param>
        /// <returns>den erstellten Coupon</returns>
        internal clsCoupon DataRowToClsCoupon(DataRow _dr)
        {
            clsCoupon _myCoupon = new clsCoupon();
            _myCoupon.Id = AddIntFieldValue(_dr, "CUID");
            _myCoupon.Code = AddStringFieldValue(_dr, "CUCode");
            _myCoupon.Discount = AddIntFieldValue(_dr, "CUDiscount");
            _myCoupon.IsValid = AddBoolFieldValue(_dr, "CUIsActive");
            _myCoupon.Uid = AddIntFieldValue(_dr, "UID");
            _myCoupon.UserName = AddStringFieldValue(_dr, "UEmail");
            return _myCoupon;
        }
    }
}
