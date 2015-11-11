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

        internal List<clsSize> getSizesByCategory(int _category)
        {
            _myDAL.AddParam("SFKCategory", _category, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QSGetSizesByCategory");
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
            _mySize.Value = AddIntFieldValue(_dr, "SValue");
            _mySize.Name = AddStringFieldValue(_dr, "SName");
            return _mySize;
        }
    }
}
