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

        internal List<clsExtra> getAllExtras()
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

        internal double getPriceOfExtra(int _eID)
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

        internal clsExtra DatarowToClsExtra(DataRow _dr)
        {
            clsExtra _myExtra = new clsExtra();
            _myExtra.ID = AddIntFieldValue(_dr, "EID");
            _myExtra.Name = AddStringFieldValue(_dr, "EName");
            _myExtra.Price = AddDoubleFieldValue(_dr, "EPrice");

            return _myExtra;
        }
    }
}
