using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    internal class clsSizeCollection : clsBLLCollections
    {
        string _databaseFile;   // String zur Access-Datei, wird im Konstruktor initialisiert
        DAL.DALObjects.dDataProvider _myDAL;   // DAL-Objekt, wird in Konstruktor instantiiert

        public clsSizeCollection()
        {
            // hier wird der Pfad zur Access-Datei aus web.config gelesen
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            // DAL-Objekt instantiieren, wird von den Methoden unten genutzt
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

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

        internal clsSize GetSizeById(int _sid)
        {
            _myDAL.AddParam("SID", _sid, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QSGetSizeById");
            DataTable _myDataTable = _myDataSet.Tables[0];
            clsSize _mySize = DatarowToClsSize(_myDataTable.Rows[0]);

            return _mySize;
        }

        internal int DeleteSizeById(int _sid)
        {
            _myDAL.AddParam("SID", _sid, DAL.DataDefinition.enumerators.SQLDataType.INT);
            int _affectedRows = _myDAL.MakeStoredProcedureAction("QSDeleteSizeById");

            return _affectedRows;
        }

        internal int UpdateSize(clsSize _size)
        {
            _myDAL.AddParam("SValue", _size.Value, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("SName", _size.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("SFKCategory", _size.CID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("SID", _size.Id, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int _affectedRows = _myDAL.MakeStoredProcedureAction("QSUpdateSizeById");
            return _affectedRows;
        }

        internal int InsertSize(clsSize _size)
        {
            _myDAL.AddParam("Value", _size.Value, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Name", _size.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("FKCategory", _size.CID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int _affectedRows = _myDAL.MakeStoredProcedureAction("QSInsertSize");
            return _affectedRows;
        }

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
