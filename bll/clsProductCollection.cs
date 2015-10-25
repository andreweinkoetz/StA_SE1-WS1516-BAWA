using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bll;

namespace bll
{
   internal class clsProductCollection : clsBLLCollections
    {
        string _databaseFile; // String zur Access-Datei
        DAL.DALObjects.dDataProvider _myDAL; // DAL: Zugriff auf die Datenbank

        internal clsProductCollection()
        {
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        internal List<clsProduct> getAllProducts()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QPGetAllProducts");
            DataTable _myDataTable = _myDataSet.Tables[0];
            List<clsProduct> _myProductList = new List<clsProduct>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsProduct _myProduct = DatarowToClsProduct(_dr);
                _myProductList.Add(_myProduct);
            }

            return _myProductList;
        }

        internal clsProduct DatarowToClsProduct(DataRow _dr)
        {
            clsProduct _myProduct = new clsProduct();
            _myProduct.Id = AddIntFieldValue(_dr, "PID");
            _myProduct.Name = AddStringFieldValue(_dr, "PName");
            _myProduct.PricePerUnit = AddDoubleFieldValue(_dr, "PPricePerUnit");
            return _myProduct;
        }
    }
}
