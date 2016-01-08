using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal List<clsProduct> createListofProducts(params clsProduct[] _Products)
        {
            List<clsProduct> _myProductsList = new List<clsProduct>(_Products);

            return _myProductsList;
        }

        internal List<clsProduct> getAllProductsByCategory(int _category)
        {
            _myDAL.AddParam("PFKCategory", _category, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QPGetAllProductsByCategory");
            DataTable _myDataTable = _myDataSet.Tables[0];
            List<clsProduct> _myProductList = new List<clsProduct>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsProduct _myProduct = DatarowToClsProduct(_dr);
                _myProductList.Add(_myProduct);
            }

            return _myProductList;
        }

        internal OrderedDictionary getMostFanciedProduct()
        {

            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QPGetProductsOrderedByFanciness");
            DataTable _myDataTable = _myDataSet.Tables[0];

            OrderedDictionary amountOfProducts = new OrderedDictionary();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                amountOfProducts.Add(AddStringFieldValue(_dr, "PName"), AddIntFieldValue(_dr, "Anzahl"));
            }

            return amountOfProducts;
        }

        internal double getProductPricePerUnit(String _name)
        {
            _myDAL.AddParam("Name", _name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QPGetProductPricePerUnit");

            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                return AddDoubleFieldValue(_dr, "PPricePerUnit");
            }
            return 0;
        }

        internal String getCategory(int _category)
        {
            _myDAL.AddParam("Category", _category, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QPGetCategoryByCategoryID");

            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                return AddStringFieldValue(_dr, "CName");
            }
            return null;

        }

        /// <summary>
        /// Gibt Produkt mit gegebener ID zurück
        /// </summary>
        /// <param name="_id">ID des gesuchten Produkts</param>
        /// <returns>Produkt-Objekt (oder NULL) </returns>
        internal clsProduct GetProductById(int _id)
        {
            _myDAL.AddParam("ID", _id, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QPGetProductByID");

            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                return DatarowToClsProduct(_dr);
            }
            else
            {
                return null;
            }

        } // getProductById()

        internal bool UpdateProduct(clsProductExtended _product)
        {
            _myDAL.AddParam("PName", _product.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("PPricePerUnit", _product.PricePerUnit, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myDAL.AddParam("PSell", _product.ToSell, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myDAL.AddParam("CName", _product.Category, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("PID", _product.Id, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int affectedRow = _myDAL.MakeStoredProcedureAction("QPUpdateProductByID");
            return affectedRow == 1;
        }

        internal clsProduct DatarowToClsProduct(DataRow _dr)
        {
            clsProduct _myProduct = new clsProduct();
            _myProduct.Id = AddIntFieldValue(_dr, "PID");
            _myProduct.Name = AddStringFieldValue(_dr, "PName");
            _myProduct.PricePerUnit = AddDoubleFieldValue(_dr, "PPricePerUnit");
            _myProduct.CUnit = AddStringFieldValue(_dr, "CUnit");
            _myProduct.Category = AddStringFieldValue(_dr, "CName");
            _myProduct.ToSell = AddBoolFieldValue(_dr, "PSell");
            return _myProduct;
        }

        internal Dictionary<Int32, String> GetAllProductCategories()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QCGetAllCategories");

            DataTable _myDataTable = _myDataSet.Tables[0];

            Dictionary<Int32, String> _myCategories = new Dictionary<int, string>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _myCategories.Add(AddIntFieldValue(_dr, "CID"), AddStringFieldValue(_dr, "CName"));
            }
            return _myCategories;
        }

        internal Dictionary<string, double> GetProductsOrderedByTotalRevenue()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOPGetProductsOrderedByTotalRevenue");

            DataTable _myDataTable = _myDataSet.Tables[0];

            Dictionary<string, double> _myProducts = new Dictionary<string, double>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _myProducts.Add(AddStringFieldValue(_dr, "PName"), AddDoubleFieldValue(_dr, "Gesamtumsatz"));
            }
            return _myProducts;
        }
    }
}
