using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// clsProductCollection: Verwalten von Product-Objekten und ProductExtended Objekten
    /// </summary>
    internal class clsProductCollection : clsBLLCollections
    {
        string _databaseFile; // String zur Access-Datei
        DAL.DALObjects.dDataProvider _myDAL; // DAL: Zugriff auf die Datenbank

        /// <summary>
        /// Standard-Konstruktor für neue Collection.
        /// </summary>
        internal clsProductCollection()
        {
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }


        /// <summary>
        /// Liefert Liste aller Produkte.
        /// </summary>
        /// <returns>Liste aller Produkte</returns>
        internal List<clsProduct> GetAllProducts()
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

        /// <summary>
        /// Liefert alle Produkte einer Kategorie
        /// </summary>
        /// <param name="_category">Kategorie-ID</param>
        /// <returns>Liste aller Produkte</returns>
        internal List<clsProduct> GetAllProductsByCategory(int _category)
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

        /// <summary>
        /// Gibt Produkt mit gegebener ID zurück
        /// </summary>
        /// <param name="_id">ID des gesuchten Produkts</param>
        /// <returns>Produkt-Objekt (oder NULL) </returns>
        internal clsProductExtended GetProductById(int _id)
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

        /// <summary>
        /// Aktualisiert ein Produkt.
        /// </summary>
        /// <param name="_product">Produkt das aktualisiert werden soll.</param>
        /// <returns>true wenn erfolgreich</returns>
        internal bool UpdateProduct(clsProductExtended _product)
        {
            _myDAL.AddParam("PName", _product.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("CID", _product.CID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("PPricePerUnit", _product.PricePerUnit, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myDAL.AddParam("PSell", _product.ToSell, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myDAL.AddParam("PID", _product.Id, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int affectedRow = _myDAL.MakeStoredProcedureAction("QPUpdateProductByID");
            return affectedRow == 1;
        }

        /// <summary>
        /// Gibt eine Liste aller Kategorien inkl. zug. ID zurück.
        /// </summary>
        /// <returns>Liste der Kategorienamen mit ID</returns>
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
        /// <summary>
        /// Einfügen eines neuen Produkts.
        /// </summary>
        /// <param name="_myProduct">einzufügendes Produkt</param>
        /// <returns>Anzahl der veränderten Zeilen (i.d.R. = 1)</returns>
        internal int InsertNewProduct(clsProductExtended _myProduct)
        {
            _myDAL.AddParam("PName", _myProduct.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("CategoryID", _myProduct.CID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("PPU", _myProduct.PricePerUnit, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myDAL.AddParam("toSell", _myProduct.ToSell, DAL.DataDefinition.enumerators.SQLDataType.BOOL);

            return _myDAL.MakeStoredProcedureAction("QPInsertProduct");

        }

        /// <summary>
        /// Erstellt eine Liste aller Bestellnummern, in denen das Produkt enthalten ist.
        /// </summary>
        /// <param name="_pid">Produkt-ID</param>
        /// <returns>Liste aller Bestellnummern.</returns>
        internal List<Int32> GetOrdersOfProductByPid(int _pid)
        {
            _myDAL.AddParam("PID", _pid, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QPGetOrdersOfProductbyPID");

            List<Int32> _orderNumbers = new List<Int32>();

            DataTable _myDataTable = _myDataSet.Tables[0];

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _orderNumbers.Add(AddIntFieldValue(_dr, "ONumber"));
            }

            return _orderNumbers;
        }

        /// <summary>
        /// Zeigt die Beliebtheit der verschiedenen Produkte an.
        /// </summary>
        /// <returns>OrderedDictionary (Name, Verkäufe)</returns>
        internal OrderedDictionary GetMostFanciestProduct()
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

        /// <summary>
        /// Liefert die Umsätze aller Produkte zurück.
        /// </summary>
        /// <returns>Dictionary (Name, Umsatz)</returns>
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

        /// <summary>
        /// Löscht Produkt aus Datenbank.
        /// </summary>
        /// <param name="_pid">Id des zu löschenden Produkts</param>
        /// <returns>true wenn Produkt gelöscht</returns>
        internal int DeleteProductByPid(int _pid)
        {
            _myDAL.AddParam("PID", _pid, DAL.DataDefinition.enumerators.SQLDataType.INT);

            return _myDAL.MakeStoredProcedureAction("QPDeleteProductById");
        }


        /// <summary>
        /// Hilfsmethode zur Erstellung eines Produktobjektes aus einer DataRow.
        /// </summary>
        /// <param name="_dr">DataRow die DB-Abfragewerte enthält</param>
        /// <returns>Produkt</returns>
        private clsProductExtended DatarowToClsProduct(DataRow _dr)
        {
            clsProductExtended _myProduct = new clsProductExtended();
            _myProduct.Id = AddIntFieldValue(_dr, "PID");
            _myProduct.Name = AddStringFieldValue(_dr, "PName");
            _myProduct.PricePerUnit = AddDoubleFieldValue(_dr, "PPricePerUnit");
            _myProduct.CUnit = AddStringFieldValue(_dr, "CUnit");
            _myProduct.Category = AddStringFieldValue(_dr, "CName");
            _myProduct.ToSell = AddBoolFieldValue(_dr, "PSell");
            _myProduct.CID = AddIntFieldValue(_dr, "PFKCategory");
            return _myProduct;
        }
    }
}
