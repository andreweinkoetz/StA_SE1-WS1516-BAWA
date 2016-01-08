using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace bll
{
    /// <summary>
    /// clsOrderCollection: Verwalten von Order-Objekten und OrderExtended Objekten
    /// </summary>
    internal class clsOrderCollection : clsBLLCollections
    {
        string _databaseFile;   // String zur Access-Datei, wird im Konstruktor initialisiert
        DAL.DALObjects.dDataProvider _myDAL;   // DAL-Objekt, wird in Konstruktor instantiiert

        /// <summary>
        /// Order-Collection Konstruktor 
        /// </summary>
        public clsOrderCollection()
        {
            // hier wird der Pfad zur Access-Datei aus web.config gelesen
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            // DAL-Objekt instantiieren, wird von den Methoden unten genutzt
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        /// <summary>
        /// Liest alle Order aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns></returns>
        public List<clsOrderExtended> getAllOrders()
        {
            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetAllOrders");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren eine Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Wir füllen unsere Liste nach und nach mit neuen Ordern
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }
            return _myOrderList;
        } //getAllOrders() 

        /// <summary>
        /// Insert eines Orderobjekts
        /// </summary>
        /// <param name="_Order">Order-Objekt</param>
        /// <returns>1 falls Insert erfolgreich </returns>
        public int InsertOrder(clsOrderExtended _Order)
        {

            //DB-Provider instanziiert und eine Verbindung zur access-Datenbank aufgebaut
            DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

            // Jetzt müssen wir erstmal die Übergabeparameter hinzufügen 
            // (Parameter in derselben Reihenfolge wie in der Access-Query)
            _myProvider.AddParam("Number", _Order.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("UserId", _Order.UserId, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("Date", _Order.OrderDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myProvider.AddParam("ODeliveryDate", _Order.OrderDeliveryDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myProvider.AddParam("Delivery", _Order.OrderDelivery, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myProvider.AddParam("Status", _Order.OrderStatus, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("Sum", _Order.OrderSum, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            //Ausführen und veränderte Zeilen zurückgeben
            int _changedSets = _myProvider.MakeStoredProcedureAction("QOInsertOrder");

            return _changedSets;
        } //insertOrder()

        public int InsertOrderedProduct(clsOrderExtended _Order, clsProductExtended _Product)
        {
            int _changedSets = 0;


            //DB-Provider instanziiert und eine Verbindung zur access-Datenbank aufgebaut
            DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

            _myProvider.AddParam("OPID", _Product.OpID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OPFKProductID", _Product.Id, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OPOrderNumber", _Order.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OPSize", _Product.Size, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);

            //Ausführen und veränderte Zeilen zurückgeben
            _changedSets += _myProvider.MakeStoredProcedureAction("QOInsertOrderedProduct");

            return _changedSets;
        }

        public int InsertOrderedExtras(clsProductExtended _Product, List<clsExtra> _Extras)
        {
            int _changedSets = 0;

            foreach (clsExtra _Extra in _Extras)
            {
                //DB-Provider instanziiert und eine Verbindung zur access-Datenbank aufgebaut
                DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

                // Jetzt müssen wir erstmal die Übergabeparameter hinzufügen 
                // (Parameter in derselben Reihenfolge wie in der Access-Query)
                _myProvider.AddParam("OEFKOPID", _Product.OpID, DAL.DataDefinition.enumerators.SQLDataType.INT);
                _myProvider.AddParam("OEFKExtraID", _Extra.ID, DAL.DataDefinition.enumerators.SQLDataType.INT);

                //Ausführen und veränderte Zeilen zurückgeben
                _changedSets += _myProvider.MakeStoredProcedureAction("QOInsertOrderedExtras");
            }

            return _changedSets;

        }

        /// <summary>
        /// DatarowToclsOrder(): Transforms a DataRow into a OrderExtended Object
        /// </summary>
        /// <param name="_dr">DataRow</param>
        /// <returns>OrderExtended-Objekt</returns>
        private clsOrderExtended DatarowToclsOrderExtended(DataRow _dr)
        {
            clsOrderExtended _myOrder = new clsOrderExtended();
            //und hier die Daten nach Index
            _myOrder.ID = (int)_dr["OID"];
            //_myOrder.ProductName = AddStringFieldValue(_dr, "PName");
            _myOrder.OrderNumber = AddIntFieldValue(_dr, "ONumber");
            _myOrder.UserId = AddIntFieldValue(_dr, "OFKUserId");
            _myOrder.UserName = AddStringFieldValue(_dr, "UName");
            _myOrder.OrderDate = AddDateTimeFieldValue(_dr, "ODate");
            _myOrder.OrderDeliveryDate = AddDateTimeFieldValue(_dr, "ODeliveryDate");
            _myOrder.OrderDelivery = AddBoolFieldValue(_dr, "ODelivery");
            _myOrder.OrderStatus = AddIntFieldValue(_dr, "OStatus");
            _myOrder.OrderSum = AddDoubleFieldValue(_dr, "OSum");
            _myOrder.OrderStatusDescription = AddStringFieldValue(_dr, "STDescription");

            return _myOrder;
        } //DatarowToclsOrder()


        /// <summary>
        /// Liest alle Bestellungen eines Users aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns></returns>
        internal List<clsOrderExtended> getOrdersByUserID(int _userID)
        {
            _myDAL.AddParam("UserID", _userID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersByUserID");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren eine Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Wir füllen unsere Liste nach und nach mit neuen Ordern
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }
            return _myOrderList;
        }

        /// <summary>
        /// Gibt alle Bestellungen geordnet nach Datum zurück.
        /// </summary>
        /// <returns></returns>
        internal List<clsOrderExtended> getOrdersOrderedByDate()
        {
            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersOrderedByDate");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren eine Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsOrderExtended _order = new clsOrderExtended();

                _order.OrderDate = AddDateTimeFieldValue(_dr, "ODate");
                _order.UserName = AddStringFieldValue(_dr, "UEmail");
                _order.OrderNumber = AddIntFieldValue(_dr, "ONumber");
                _order.OrderStatusDescription = AddStringFieldValue(_dr, "STDescription");
                _order.OrderDeliveryDate = AddDateTimeFieldValue(_dr, "ODeliveryDate");
                _order.OrderSum = AddDoubleFieldValue(_dr, "OSum");
                _myOrderList.Add(_order);
            }
            return _myOrderList;
        }

        internal List<Tuple<int, String, double>> GetOrderedProductsSortByCategory(String _category)
        {

            //Parameter definieren
            _myDAL.AddParam("CName", _category, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);

            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOPGetOrderedProductsSortByCategory");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            List<Tuple<int, string, double>> _productList = new List<Tuple<int, string, double>>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                int orderNumber = AddIntFieldValue(_dr, "OPOrderNumber");
                string name = AddStringFieldValue(_dr, "PName");
                double price = AddDoubleFieldValue(_dr, "Preis");

                //Instantiieren eine Liste von Order-Objekten
                Tuple<int, string, double> _productByCategory = new Tuple<int, string, double>(orderNumber, name, price);
                _productList.Add(_productByCategory);
            }

            return _productList;
        }


        internal List<clsOrderExtended> getOrdersByEmail(String _email)
        {
            _myDAL.AddParam("Email", _email, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);

            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersByUserEmail");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren eine Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsOrderExtended _order = new clsOrderExtended();

                _order.OrderDate = AddDateTimeFieldValue(_dr, "ODate");
                _order.UserName = AddStringFieldValue(_dr, "UEmail");
                _order.OrderNumber = AddIntFieldValue(_dr, "ONumber");
                _order.OrderStatusDescription = AddStringFieldValue(_dr, "STDescription");
                _order.OrderDeliveryDate = AddDateTimeFieldValue(_dr, "ODeliveryDate");
                _order.OrderSum = AddDoubleFieldValue(_dr, "OSum");
                _myOrderList.Add(_order);
            }
            return _myOrderList;
        }


        /// <summary>
        /// Liest alle Produkte inkl. Extras einer Bestellung aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns></returns>
        internal List<clsProductExtended> getOrderedProductsByOrderNumber(int _orderNumber)
        {
            _myDAL.AddParam("ONumber", _orderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOPGetOrderedProductsByOrderNumber");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren eine Liste von Product-Objekten
            List<clsProductExtended> _myProductList = new List<clsProductExtended>();

            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsProductExtended _product = new clsProductExtended();

                _product.OpID = AddIntFieldValue(_dr, "OPID");
                _product.Name = AddStringFieldValue(_dr, "PName");
                _product.PricePerUnit = AddDoubleFieldValue(_dr, "PPricePerUnit");
                _product.Size = AddDoubleFieldValue(_dr, "OPSize");
                _product.Category = AddIntFieldValue(_dr, "PFKCategory").ToString();
                _product.ProductExtras = getExtrasByOPID(_product.OpID);
                _myProductList.Add(_product);
            }
            return _myProductList;
        }

        private List<clsExtra> getExtrasByOPID(int _opID)
        {
            // Neuer Provider muss angelegt werden da die Abfrage sonst keinen Wert liefert wegen falschen Parametern!
            DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

            _myProvider.AddParam("OPID", _opID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myProvider.GetStoredProcedureDSResult("QOEGetExtrasByOPID");

            List<clsExtra> _myExtrasList = new List<clsExtra>();

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsExtra _extra = new clsExtra();
                _extra.ID = AddIntFieldValue(_dr, "EID");
                _extra.Name = AddStringFieldValue(_dr, "EName");

                _myExtrasList.Add(_extra);
            }

            return _myExtrasList;
        }

        /// <summary>
        /// Liest alle Order aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns></returns>
        public List<clsOrderExtended> getOrdersNotDelivered()
        {
            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersNotDelivered");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren eine Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Wir füllen unsere Liste nach und nach mit neuen Ordern
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }
            return _myOrderList;
        } //getAllOrders() 

        internal int updateOrderStatusByONumber(clsOrderExtended _myOrder)
        {
            _myDAL.AddParam("Status", _myOrder.OrderStatus, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("DeliveryDate", _myOrder.OrderDeliveryDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myDAL.AddParam("ONumber", _myOrder.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);


            int changedSets = _myDAL.MakeStoredProcedureAction("QOUpdateOrderStatusByONumber");
            return changedSets;
        }

    } //clsOrderCollection
}
