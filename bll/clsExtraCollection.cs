using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Bietet Zugriff zum DAL, ist nur in der BLL sichtbar und stellt Methoden für clsExtra-Objekte bereit.
    /// </summary>
    internal class clsExtraCollection : clsBLLCollections
    {
        string _databaseFile; // String zur Access-Datei, wird im Konstruktor initialisiert
        DAL.DALObjects.dDataProvider _myDAL; // DAL-Objekt für den Zugriff auf die Datenbank

        /// <summary>
        /// Stellt eine Verbindung zur Datenbank her.
        /// </summary>
        internal clsExtraCollection()
        {
            // Hier wird der Pfad zur Access-Datei aus web.config gelesen und eine DAL-Instanz erzeugt, die den Zugriff auf die Datenbank ermöglicht
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        /// <summary>
        /// Liefert alle vorhandenen (d.h. auch inaktive) Extras zurück.
        /// </summary>
        /// <returns> alle vorhandenen Extras</returns>
        internal List<clsExtra> GetAllExtras()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QEGetAllExtras");
            DataTable _myDataTable = _myDataSet.Tables[0];
            List<clsExtra> _myExtrasList = new List<clsExtra>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsExtra _myExtra = DatarowToClsExtra(_dr);
                _myExtrasList.Add(_myExtra);
            }
            return _myExtrasList;
        }

        /// <summary>
        /// Liefert alle aktiven Extras zurück.
        /// </summary>
        /// <returns>alle aktiven Extras</returns>
        internal List<clsExtra> GetAllActiveExtras()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QEGetAllActiveExtras");
            DataTable _myDataTable = _myDataSet.Tables[0];
            List<clsExtra> _myExtrasList = new List<clsExtra>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsExtra _myExtra = DatarowToClsExtra(_dr);
                _myExtrasList.Add(_myExtra);
            }

            return _myExtrasList;
        }

        /// <summary>
        /// Liefert den Preis des Extras mit der gewählten ID zurück.
        /// </summary>
        /// <param name="extraID">ID des Extras</param>
        /// <returns>Preis des Extras</returns>
        internal double GetPriceOfExtra(int extraID)
        {
            _myDAL.AddParam("EID", extraID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QEGetPriceOfExtra");
            DataTable _myDataTable = _myDataSet.Tables[0];

            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                return AddDoubleFieldValue(_dr, "EPrice");
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Liefert das Extra mit der gewählten ID zurück.
        /// </summary>
        /// <param name="extraID">ID des Extras</param>
        /// <returns>Extra mit der gewählten ID</returns>
        internal clsExtra GetExtraById(int extraID)
        {
            _myDAL.AddParam("EID", extraID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QEGetExtraByEid");
            DataTable _myDataTable = _myDataSet.Tables[0];

            DataRow _dr = _myDataTable.Rows[0];
            return DatarowToClsExtra(_dr);
        }

        /// <summary>
        /// Liefert alle Bestellungen zurück, in denen das Extra verwendet wird.
        /// </summary>
        /// <param name="_extraID">ID des Extras</param>
        /// <returns>Anzahl der betroffenen Bestellungen</returns>
        internal List<Int32> GetOrdersOfExtrasByEID(int _extraID)
        {
            List<Int32> _orderNumbers = new List<Int32>();
            _myDAL.AddParam("EID", _extraID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QEGetOrdersOfExtrasByEID");
            DataTable _myDataTable = _myDataSet.Tables[0];

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _orderNumbers.Add(AddIntFieldValue(_dr, "ONumber"));
            }
            return _orderNumbers;
        }

        /// <summary>
        /// Fügt ein neues Extra in die Datenbank ein.
        /// </summary>
        /// <param name="_myExtra">das einzufügende Extra</param>
        /// <returns>Anzahl der eingefügten Datensätze</returns>
        internal int InsertExtra(clsExtra _myExtra)
        {
            _myDAL.AddParam("EName", _myExtra.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("EPrice", _myExtra.Price, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myDAL.AddParam("ESell", _myExtra.ToSell, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            return _myDAL.MakeStoredProcedureAction("QEInsertExtra");
        }

        /// <summary>
        /// Ändert ein Extra in der Datenbank.
        /// </summary>
        /// <param name="_myExtra">das zu ändernde Extra</param>
        /// <returns>Anzahl der zu ändernden Datensätze</returns>
        internal int UpdateExtra(clsExtra _myExtra)
        {
            _myDAL.AddParam("EName", _myExtra.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("EPrice", _myExtra.Price, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myDAL.AddParam("ESell", _myExtra.ToSell, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myDAL.AddParam("EID", _myExtra.ID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            return _myDAL.MakeStoredProcedureAction("QEUpdateExtraByID");
        }

        /// <summary>
        /// Löscht ein Extra in der Datenbank.
        /// </summary>
        /// <param name="_extraID">ID des zu löschenden Extras</param>
        /// <returns>Anzahl der gelöschten Datensätze</returns>
        internal int DeleteExtraByID(int _extraID)
        {
            _myDAL.AddParam("EID", _extraID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            return _myDAL.MakeStoredProcedureAction("QEDeleteExtraById");
        }

        /// <summary>
        /// Erstellt aus einem Datensatz ein Extra.
        /// </summary>
        /// <param name="_dr">Datensatz in der Datenbank</param>
        /// <returns>das erstellte Extra</returns>
        private clsExtra DatarowToClsExtra(DataRow _dr)
        {
            clsExtra _myExtra = new clsExtra();
            _myExtra.ID = AddIntFieldValue(_dr, "EID");
            _myExtra.Name = AddStringFieldValue(_dr, "EName");
            _myExtra.Price = AddDoubleFieldValue(_dr, "EPrice");
            _myExtra.ToSell = AddBoolFieldValue(_dr, "ESell");
            return _myExtra;
        }
    }
}
