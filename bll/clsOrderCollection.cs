using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace bll
{
    /// <summary> 
    /// Bietet Zugriff zum DAL, ist nur in der BLL sichtbar und stellt Methoden für clsOrder- und clsOrderExtended-Objekte bereit.
    /// </summary>
    internal class clsOrderCollection : clsBLLCollections
    {
        string _databaseFile; // String zur Access-Datei, wird im Konstruktor initialisiert
        DAL.DALObjects.dDataProvider _myDAL; // DAL-Objekt für den Zugriff auf die Datenbank

        /// <summary>
        /// Stellt eine Verbindung zur Datenbank her.
        /// </summary>
        public clsOrderCollection()
        {
            // Hier wird der Pfad zur Access-Datei aus web.config gelesen und eine DAL-Instanz erzeugt, die den Zugriff auf die Datenbank ermöglicht
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        /// <summary>
        /// Liefert alle vorhandenen Bestellungen zurück.
        /// </summary>
        /// <returns>alle vorhandenen Bestellungen</returns>
        public List<clsOrderExtended> GetAllOrders()
        {
            //Hier wird das Dataset aus der Datenbank befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetAllOrders");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Hinzufügen einer Bestellung zur Liste
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }
            return _myOrderList;
        }

        /// <summary>
        /// Fügt eine neue Bestellung in die Datenbank ein.
        /// </summary>
        /// <param name="_Order">die einzufügende Bestellung</param>
        /// <returns>die Anzahl der eingefügten Datensätze (Anzahl = 1, falls das Einfügen der Bestellung erfolgreich ist)</returns>
        internal int InsertOrder(clsOrderExtended _Order)
        {
            //DB-Provider wird instanziiert und eine Verbindung zur Datenbank aufgebaut
            DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

            // Hinzufügen der Übergabeparameter (Parameter in der selben Reihenfolge wie in der Access-Query) 
            _myProvider.AddParam("Number", _Order.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("UserId", _Order.UserId, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("Date", _Order.OrderDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myProvider.AddParam("ODeliveryDate", _Order.OrderDeliveryDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myProvider.AddParam("Delivery", _Order.OrderDelivery, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myProvider.AddParam("Status", _Order.OrderStatus, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("Sum", _Order.OrderSum, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
            _myProvider.AddParam("FKCouponId", _Order.CouponId, DAL.DataDefinition.enumerators.SQLDataType.INT);

            return _myProvider.MakeStoredProcedureAction("QOInsertOrder");
        }

        /// <summary>
        /// Fügt das Produkt einer Bestellung in die Datenbank ein.
        /// </summary>
        /// <param name="_Order">die zugehörige Bestellung zum Produkt</param>
        /// <param name="_Product">das einzufügende Produkt</param>
        /// <returns>die Anzahl der eingefügten Datensätze</returns>
        internal int InsertOrderedProduct(clsOrderExtended _Order, clsProductExtended _Product)
        {
            //DB-Provider wird instanziiert und eine Verbindung zur Datenbank aufgebaut
            DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

            // Hinzufügen der Übergabeparameter (Parameter in der selben Reihenfolge wie in der Access-Query) 
            _myProvider.AddParam("OPID", _Product.OpID, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OPFKProductID", _Product.Id, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OPOrderNumber", _Order.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OPSize", _Product.Size, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);

            return _myProvider.MakeStoredProcedureAction("QOInsertOrderedProduct");
        }

        /// <summary>
        /// Fügt die Extras des Produkts einer Bestellung in die Datenbank ein.
        /// </summary>
        /// <param name="_Product">Produkt, zu dem die Extras gewählt wurden</param>
        /// <param name="_Extras">die einzufügenden Extras</param>
        /// <returns>die Anzahl der eingefügten Datensätze</returns>
        internal int InsertOrderedExtras(clsProductExtended _Product, List<clsExtra> _Extras)
        {
            int _changedSets = 0;
            foreach (clsExtra _Extra in _Extras)
            {
                //DB-Provider wird instanziiert und eine Verbindung zur Datenbank aufgebaut
                DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

                // Hinzufügen der Übergabeparameter (Parameter in der selben Reihenfolge wie in der Access-Query) 
                _myProvider.AddParam("OEFKOPID", _Product.OpID, DAL.DataDefinition.enumerators.SQLDataType.INT);
                _myProvider.AddParam("OEFKExtraID", _Extra.ID, DAL.DataDefinition.enumerators.SQLDataType.INT);

                _changedSets += _myProvider.MakeStoredProcedureAction("QOInsertOrderedExtras");
            }
            return _changedSets;
        }

        /// <summary>
        /// Liefert alle Bestellungen des Benutzers zurück.
        /// </summary>
        /// <param name="_userID">ID des Benutzers</param>
        /// <returns>alle Bestellungen des Benutzers</returns>
        internal List<clsOrderExtended> GetOrdersByUserID(int _userID)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("UserID", _userID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Befüllen des Datasets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersByUserID");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Hinzufügen einer Bestellung zur Liste
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }
            return _myOrderList;
        }

        /// <summary>
        /// Gibt den Status der Bestellung zurück.
        /// </summary>
        /// <param name="_orderNumber">Bestellnummer der Bestellung</param>
        /// <returns>Status der Bestellung</returns>
        internal int GetOrderStatusByOrderNumber(int _orderNumber)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("ONumber", _orderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Befüllen des Datasets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrderStatusByONumber");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Die DataTable enthält nur eine DataRow
            int _orderStatus = AddIntFieldValue(_myDataTable.Rows[0], "OStatus");
            return _orderStatus;
        }

        /// <summary>
        /// Liefert alle Produkte inkl. Extras einer Bestellung zurück.
        /// </summary>
        /// <param name="_orderNumber">Bestellnummer der Bestellung</param>
        /// <returns>alle Produkte inkl. Extras einer Bestellung</returns>
        internal List<clsProductExtended> GetOrderedProductsByOrderNumber(int _orderNumber)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("ONumber", _orderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOPGetOrderedProductsByOrderNumber");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Product-Objekten
            List<clsProductExtended> _myProductList = new List<clsProductExtended>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsProductExtended _product = new clsProductExtended();
                _product.Id = AddIntFieldValue(_dr, "PID");
                _product.OpID = AddIntFieldValue(_dr, "OPID");
                _product.Name = AddStringFieldValue(_dr, "PName");
                _product.PricePerUnit = AddDoubleFieldValue(_dr, "PPricePerUnit");
                _product.Size = AddDoubleFieldValue(_dr, "OPSize");
                _product.CID = AddIntFieldValue(_dr, "PFKCategory");
                _product.Category = AddStringFieldValue(_dr, "CName");
                _product.ProductExtras = GetExtrasByOPID(_product.OpID);
                _myProductList.Add(_product);
            }
            return _myProductList;
        }

        /// <summary>
        /// Liefert alle Bestellungen sortiert nach Datum zurück.
        /// </summary>
        /// <returns>alle Bestellungen sortiert nach Datum</returns>
        internal List<clsOrderExtended> GetOrdersOrderedByDate()
        {
            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersOrderedByDate");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

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
        /// Liefert zurück, wieviele Bestellungen in den verschiedenen Bestellstatus sind.
        /// </summary>
        /// <returns>Anzahl der Bestellungen in den verschiedenen Bestellstatus</returns>
        internal List<Tuple<String, Int32, Double>> GetOrdersOrderedByStatus()
        {
            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersOrderedByStatus");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Ergebnistupeln
            List<Tuple<String, Int32, Double>> _resultTuples = new List<Tuple<String, Int32, Double>>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                Tuple<String, Int32, Double> _order = new Tuple<String, Int32, Double>(AddStringFieldValue(_dr, "Bestellstatus"), AddIntFieldValue(_dr, "AnzahlBestellungen"), AddDoubleFieldValue(_dr, "Gesamtsumme"));
                _resultTuples.Add(_order);
            }
            return _resultTuples;
        }

        /// <summary>
        /// Liefert alle abgeschlossenen Bestellungen mit ihrer benötigten Lieferzeit zurück.
        /// </summary>
        /// <returns>alle abgeschlossenen Bestellungen mit ihrer benötigten Lieferzeit</returns>
        internal Dictionary<Int32, Int32> GetTimeToDeliverOfOrders()
        {
            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetTimeToDeliverOfOrders");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren des Ergebnisdictionarys
            Dictionary<Int32, Int32> _results = new Dictionary<Int32, Int32>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _results.Add(AddIntFieldValue(_dr, "ONumber"), AddIntFieldValue(_dr, "Differenz"));
            }
            return _results;
        }

        /// <summary>
        /// Liefert alle Produkte aus Bestellungen zurück, die zur gewählten Kategorie gehören.
        /// </summary>
        /// <param name="_category">gewählte Kategorie der Produkte</param>
        /// <returns>alle Produkte, die zur gewählten Kategorie gehören</returns>
        internal List<Tuple<int, String, double>> GetOrderedProductsSortByCategory(String _category)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("CName", _category, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);

            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOPGetOrderedProductsSortByCategory");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Ergebnistupeln
            List<Tuple<int, string, double>> _productList = new List<Tuple<int, string, double>>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                int orderNumber = AddIntFieldValue(_dr, "OPOrderNumber");
                string name = AddStringFieldValue(_dr, "PName");
                double price = AddDoubleFieldValue(_dr, "Preis");

                //Instantiieren eines Ergebnistupels
                Tuple<int, string, double> _productByCategory = new Tuple<int, string, double>(orderNumber, name, price);
                _productList.Add(_productByCategory);
            }
            return _productList;
        }

        /// <summary>
        /// Liefert alle Bestellungen eines Benutzers zurück.
        /// </summary>
        /// <param name="_email">Email-Adresse des Benutzers</param>
        /// <returns>alle Bestellungen des Benutzers</returns>
        internal List<clsOrderExtended> GetOrdersByEmail(String _email)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("Email", _email, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);

            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersByUserEmail");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

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
        /// Liefert alle Extras zurück, die zu einem bestimmten Produkt bestellt wurden. (interne Hilfsmethode)
        /// </summary>
        /// <param name="_opID">ID des bestellten Produkts</param>
        /// <returns>alle Extras, die zu diesem Produkt bestellt wurden</returns>
        private List<clsExtra> GetExtrasByOPID(int _opID)
        {
            // Neuer Provider muss angelegt werden, da die Query sonst keinen Wert liefert wegen falschen Parametern
            DAL.DALObjects.dDataProvider _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);

            //Hinzufügen eines Übergabeparameters
            _myProvider.AddParam("OPID", _opID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myProvider.GetStoredProcedureDSResult("QOEGetExtrasByOPID");

            //Instantiieren einer Liste von Extra-Objekten
            List<clsExtra> _myExtrasList = new List<clsExtra>();

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                clsExtra _extra = new clsExtra();
                _extra.ID = AddIntFieldValue(_dr, "EID");
                _extra.Name = AddStringFieldValue(_dr, "EName");
                _extra.Price = AddDoubleFieldValue(_dr, "EPrice");
                _myExtrasList.Add(_extra);
            }
            return _myExtrasList;
        }

        /// <summary>
        /// Liefert alle noch nicht gelieferten Bestellungen zurück.
        /// </summary>
        /// <returns>alle noch nicht gelieferten Bestellungen</returns>
        internal List<clsOrderExtended> GetOrdersNotDelivered()
        {
            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersNotDelivered");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Hinzufügen einer Bestellung zur Liste
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }
            return _myOrderList;
        }

        /// <summary>
        /// Liefert alle gelieferten Bestellungen zurück.
        /// </summary>
        /// <returns>alle gelieferten Bestellungen </returns>
        internal List<clsOrderExtended> GetFinishedOrders()
        {
            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetAllFinishedOrders");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren einer Liste von Order-Objekten
            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Hinzufügen einer Bestellung zur Liste
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }
            return _myOrderList;
        }

        /// <summary>
        /// Ändert den Status einer Bestellung.
        /// </summary>
        /// <param name="_myOrder">Bestellung, deren Status geändert wird</param>
        /// <returns>Anzahl der geänderten Datensätze</returns>
        internal int UpdateOrderStatusByONumber(clsOrderExtended _myOrder)
        {
            // Hinzufügen der Übergabeparameter (Parameter in der selben Reihenfolge wie in der Access-Query) 
            _myDAL.AddParam("Status", _myOrder.OrderStatus, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("DeliveryDate", _myOrder.OrderDeliveryDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myDAL.AddParam("ONumber", _myOrder.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            return _myDAL.MakeStoredProcedureAction("QOUpdateOrderStatusByONumber");
        }

        /// <summary>
        /// Storniert eine Bestellung.
        /// </summary>
        /// <param name="_oNumber">Bestellnummer der Bestellung</param>
        /// <returns>Anzahl der geänderten Datensätze</returns>
        internal int CancelOrderByONumber(int _oNumber)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            return _myDAL.MakeStoredProcedureAction("QOCancelOrderByONumber");
        }

        /// <summary>
        /// Liefert die Bestellung mit der angegebenen Bestellnummer zurück.
        /// </summary>
        /// <param name="_oNumber">Bestellnummer der Bestellung</param>
        /// <returns>die Bestellung mit der angegebenen Bestellnummer</returns>
        internal clsOrderExtended GetOrderByOrderNumber(int _oNumber)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Befüllen des DataSets mit Daten aus der Datenbank
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrderByOrderNumber");

            //Das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            if (_myDataTable.Rows.Count > 0)
            {
                clsOrderExtended _myOrder = DatarowToclsOrderExtended(_myDataTable.Rows[0]);
                if (_myOrder.CouponId != 0)
                {
                    clsCoupon _myCoupon = new clsCouponFacade().GetCouponById(_myOrder.CouponId);
                    _myOrder.MyCoupon = _myCoupon;
                }
                return _myOrder;

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Löscht eine Bestellung. 
        /// Dabei werden auch alle Produkte sowie Extras der Bestellung gelöscht.
        /// </summary>
        /// <param name="_oNumber">Bestellnummer der Bestellung</param>
        /// <returns>Anzahl der gelöschten Datensätze</returns>
        internal int DeleteOrderByOrderNumber(int _oNumber)
        {
            //Hinzufügen eines Übergabeparameters
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            //Löschen der Extras der Bestellung
            int changedSets = _myDAL.MakeStoredProcedureAction("QOEDeleteOrderedExtrasByONumber");

            //Neues DAL-Objekt nötig, da die Verbindung sonst gesperrt ist
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            //Löschen der Produkte der Bestellung
            changedSets += _myDAL.MakeStoredProcedureAction("QOPDeleteOrderedProductsByONumber");

            //Neues DAL-Objekt nötig, da die Verbindung sonst gesperrt ist
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            //Löschen der Bestellung
            changedSets += _myDAL.MakeStoredProcedureAction("QODeleteOrderByONumber");

            return changedSets;
        }

        /// <summary>
        /// Erstellt aus einem Datensatz eine Bestellung.
        /// </summary>
        /// <param name="_dr">Datensatz in der Datenbank</param>
        /// <returns>die erstellte Bestellung</returns>
        private clsOrderExtended DatarowToclsOrderExtended(DataRow _dr)
        {
            clsOrderExtended _myOrder = new clsOrderExtended();
            _myOrder.ID = (int)_dr["OID"];
            _myOrder.OrderNumber = AddIntFieldValue(_dr, "ONumber");
            _myOrder.UserId = AddIntFieldValue(_dr, "OFKUserId");
            _myOrder.UserName = AddStringFieldValue(_dr, "UName");
            _myOrder.OrderDate = AddDateTimeFieldValue(_dr, "ODate");
            _myOrder.OrderDeliveryDate = AddDateTimeFieldValue(_dr, "ODeliveryDate");
            _myOrder.OrderDelivery = AddBoolFieldValue(_dr, "ODelivery");
            _myOrder.OrderStatus = AddIntFieldValue(_dr, "OStatus");
            _myOrder.OrderSum = AddDoubleFieldValue(_dr, "OSum");
            _myOrder.OrderStatusDescription = AddStringFieldValue(_dr, "STDescription");
            _myOrder.CouponId = AddIntFieldValue(_dr, "OFKCouponId");
            return _myOrder;
        }
    }
}
