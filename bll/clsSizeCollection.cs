using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Bietet Zugriff zum DAL, ist nur in der BLL sichtbar und stellt Methoden für clsSize-Objekte bereit.
    /// </summary>
    internal class clsSizeCollection : clsBLLCollections
    {
        string _databaseFile; // String zur Access-Datei, wird im Konstruktor initialisiert
        DAL.DALObjects.dDataProvider _myDAL; // DAL-Objekt für den Zugriff auf die Datenbank

        /// <summary>
        /// Stellt eine Verbindung zur Datenbank her.
        /// </summary>
        public clsSizeCollection()
        {
            // Hier wird der Pfad zur Access-Datei aus web.config gelesen und eine DAL-Instanz erzeugt, die den Zugriff auf die Datenbank ermöglicht
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        /// <summary>
        /// Liefert alle Größen einer Kategorie zurück.
        /// </summary>
        /// <param name="_category">ID der Kategorie</param>
        /// <returns>alle zu dieser Kategorie gehörenden Größen</returns>
        internal List<clsSize> GetSizesByCategory(int _category)
        {
            _myDAL.AddParam("SFKCategory", _category, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QSGetSizesByCategory");
            DataTable _myDataTable = _myDataSet.Tables[0];
            List<clsSize> _mySizesList = new List<clsSize>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsSize _mySize = new clsSize();
                _mySize.Value = AddDoubleFieldValue(_dr, "SValue");
                _mySize.Name = AddStringFieldValue(_dr, "SName");
                _mySizesList.Add(_mySize);
            }
            return _mySizesList;
        }

        /// <summary>
        /// Liefert eine Größe anhand ihrer ID zurück.
        /// </summary>
        /// <param name="_sid">ID der Größe</param>
        /// <returns>die Größe mit der angegebenen ID</returns>
        internal clsSize GetSizeById(int _sid)
        {
            _myDAL.AddParam("SID", _sid, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QSGetSizeById");
            DataTable _myDataTable = _myDataSet.Tables[0];
            clsSize _mySize = DatarowToClsSize(_myDataTable.Rows[0]);
            return _mySize;
        }

        /// <summary>
        /// Löscht die Größe mit der angegebenen ID.
        /// </summary>
        /// <param name="_sid">ID der Größe</param>
        /// <returns>Anzahl der gelöschten Datensätze in der Datenbank</returns>
        internal int DeleteSizeById(int _sid)
        {
            _myDAL.AddParam("SID", _sid, DAL.DataDefinition.enumerators.SQLDataType.INT);
            int _affectedRows = _myDAL.MakeStoredProcedureAction("QSDeleteSizeById");
            return _affectedRows;
        }

        /// <summary>
        /// Ändert die angegebene Größe.
        /// </summary>
        /// <param name="_size">die zu ändernde Größe</param>
        /// <returns>Anzahl der geänderten Datensätze in der Datenbank</returns>
        internal int UpdateSize(clsSize _size)
        {
            _myDAL.AddParam("SValue", _size.Value, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("SName", _size.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("SFKCategory", _size.CID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("SID", _size.Id, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int _affectedRows = _myDAL.MakeStoredProcedureAction("QSUpdateSizeById");
            return _affectedRows;
        }

        /// <summary>
        /// Fügt eine neue Größe in die Datenbank ein.
        /// </summary>
        /// <param name="_size">die einzufügende Größe</param>
        /// <returns>Anzahl der eingefügten Datensätze in der Datenbank</returns>
        internal int InsertSize(clsSize _size)
        {
            _myDAL.AddParam("Value", _size.Value, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Name", _size.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("FKCategory", _size.CID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int _affectedRows = _myDAL.MakeStoredProcedureAction("QSInsertSize");
            return _affectedRows;
        }

        /// <summary>
        /// Liefert alle vorhandenen Größen zurück.
        /// </summary>
        /// <returns>alle vorhandenen Größen</returns>
        internal List<clsSize> GetAllSizes()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QSGetAllSizes");
            DataTable _myDataTable = _myDataSet.Tables[0];
            List<clsSize> _mySizesList = new List<clsSize>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsSize _mySize = DatarowToClsSize(_dr);
                _mySizesList.Add(_mySize);
            }
            return _mySizesList;
        }

        /// <summary>
        /// Erstellt aus einem Datensatz eine Größe.
        /// </summary>
        /// <param name="_dr">Datensatz in der Datenbank</param>
        /// <returns>die erstellte Größe</returns>
        internal clsSize DatarowToClsSize(DataRow _dr)
        {
            clsSize _mySize = new clsSize();
            _mySize.Id = AddIntFieldValue(_dr, "SID");
            _mySize.Value = AddDoubleFieldValue(_dr, "SValue");
            _mySize.Name = AddStringFieldValue(_dr, "SName");
            _mySize.CID = AddIntFieldValue(_dr, "SFKCategory");
            _mySize.Category = AddStringFieldValue(_dr, "CName");
            return _mySize;
        }
    }
}
