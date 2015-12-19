using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    internal class clsExtraCollection : clsBLLCollections
    {
        string _databaseFile; // String zur Access-Datei
        DAL.DALObjects.dDataProvider _myDAL; // DAL: Zugriff auf die Datenbank

        internal clsExtraCollection()
        {
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

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

        internal double GetPriceOfExtra(int _eID)
        {
            _myDAL.AddParam("EID", _eID, DAL.DataDefinition.enumerators.SQLDataType.INT);

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

        internal clsExtra GetExtraById(int _eID)
        {
            _myDAL.AddParam("EID", _eID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QEGetExtraByEid");
            DataTable _myDataTable = _myDataSet.Tables[0];

            DataRow _dr = _myDataTable.Rows[0];
            return DatarowToClsExtra(_dr);
        }

        internal List<Int32> GetOrdersOfExtrasByEID(int _eID)
        {
            List<Int32> _orderNumbers = new List<Int32>();
            _myDAL.AddParam("EID", _eID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QEGetOrdersOfExtrasByEID");
            DataTable _myDataTable = _myDataSet.Tables[0];

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _orderNumbers.Add(AddIntFieldValue(_dr, "ONumber"));
            }

            return _orderNumbers;
        }

        internal int InsertExtra(clsExtra _myExtra)
        {
            _myDAL.AddParam("EName", _myExtra.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("EPrice", _myExtra.Price, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myDAL.AddParam("ESell", _myExtra.ToSell, DAL.DataDefinition.enumerators.SQLDataType.BOOL);

            return _myDAL.MakeStoredProcedureAction("QEInsertExtra");
        }

        internal int UpdateExtra(clsExtra _myExtra)
        {
            _myDAL.AddParam("EName", _myExtra.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("EPrice", _myExtra.Price, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myDAL.AddParam("ESell", _myExtra.ToSell, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myDAL.AddParam("EID", _myExtra.ID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            return _myDAL.MakeStoredProcedureAction("QEUpdateExtraByID");
        }

        internal int DeleteExtraByID(int _eID)
        {
            _myDAL.AddParam("EID", _eID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            return _myDAL.MakeStoredProcedureAction("QEDeleteExtraById");
        }

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
