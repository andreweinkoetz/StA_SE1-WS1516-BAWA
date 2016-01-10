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
        public List<clsOrderExtended> GetAllOrders()
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
        internal int InsertOrder(clsOrderExtended _Order)
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
            _myProvider.AddParam("FKCouponId", _Order.CouponId, DAL.DataDefinition.enumerators.SQLDataType.INT);
            //Ausführen und veränderte Zeilen zurückgeben
            int _changedSets = _myProvider.MakeStoredProcedureAction("QOInsertOrder");

            return _changedSets;
        } //insertOrder()

        /// <summary>
        /// Einfügen der Produkte einer Bestellung.
        /// </summary>
        /// <param name="_Order">Bestellung des Produkts</param>
        /// <param name="_Product">einzufügendes Produkt</param>
        /// <returns>Anzahl der veränderten Zeilen</returns>
        internal int InsertOrderedProduct(clsOrderExtended _Order, clsProductExtended _Product)
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

        /// <summary>
        /// Einfügen der Extras der Produkte einer Bestellung.
        /// </summary>
        /// <param name="_Product">Produkt das die Extras enthält</param>
        /// <param name="_Extras">Extras die einzufügen sind.</param>
        /// <returns>Anzahl der veränderten Zeilen</returns>
        internal int InsertOrderedExtras(clsProductExtended _Product, List<clsExtra> _Extras)
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
        /// Liest alle Bestellungen eines Users aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns>Liste aller Bestellungen eines Users</returns>
        internal List<clsOrderExtended> GetOrdersByUserID(int _userID)
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
        /// Gibt den Status einer Bestellung zurück.
        /// </summary>
        /// <param name="_orderNumber">Bestellnummer</param>
        /// <returns>Status der Bestellung</returns>
        internal int GetOrderStatusByOrderNumber(int _orderNumber)
        {
            _myDAL.AddParam("ONumber", _orderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrderStatusByONumber");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //die DataTable enthält nur eine DataRow
            int _orderStatus = AddIntFieldValue(_myDataTable.Rows[0], "OStatus");

            return _orderStatus;
        }


        /// <summary>
        /// Liest alle Produkte inkl. Extras einer Bestellung aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns>alle Produkte inkl. Extras einer Bestellung als Liste</returns>
        internal List<clsProductExtended> GetOrderedProductsByOrderNumber(int _orderNumber)
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
        /// Gibt alle Bestellungen geordnet nach Datum zurück.
        /// </summary>
        /// <returns>Liste aller Bestellungen geordnet nach Datum</returns>
        internal List<clsOrderExtended> GetOrdersOrderedByDate()
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

        /// <summary>
        /// Gibt eine Liste von Ergebnistupeln geordnet nach Status zurück.
        /// </summary>
        /// <returns>Liste von Ergebnistupeln</returns>
        internal List<Tuple<String,Int32,Double>> GetOrdersOrderedByStatus()
        {
            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrdersOrderedByStatus");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Erzeugen einer Liste von Ergebnistupeln
            List<Tuple<String, Int32, Double>> _resultTuples = new List<Tuple<String, Int32, Double>>();


            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                Tuple<String, Int32, Double> _order = new Tuple<String, Int32, Double>(AddStringFieldValue(_dr, "Bestellstatus"), AddIntFieldValue(_dr, "AnzahlBestellungen"), AddDoubleFieldValue(_dr, "Gesamtsumme"));
                _resultTuples.Add(_order);
            }
            return _resultTuples;
        }

        /// <summary>
        /// Gibt ein Dictionary mit den Ergebniswerten zurück.
        /// </summary>
        /// <returns>Dictionary mit Ergebniswerten</returns>
        internal Dictionary<Int32, Int32> GetTimeToDeliverOfOrders()
        {
            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetTimeToDeliverOfOrders");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Erzeugen des Ergebnisdictionaries.
            Dictionary<Int32, Int32> _results = new Dictionary<Int32, Int32>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _results.Add(AddIntFieldValue(_dr, "ONumber"), AddIntFieldValue(_dr, "Differenz"));
            }

            return _results;
        }


        /// <summary>
        /// Gibt eine Liste von Ergebnistupeln geordnet nach Kategorie zurück.
        /// </summary>
        /// <param name="_category">Kategorie der Produkte</param>
        /// <returns>Liste von Ergebnistupeln</returns>
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


        /// <summary>
        /// Erstellt eine Liste aller Bestellungen eines Users.
        /// </summary>
        /// <param name="_email">Username/Email des Users.</param>
        /// <returns>Liste aller Bestellungen des Users</returns>
        internal List<clsOrderExtended> GetOrdersByEmail(String _email)
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
        /// Interne Hilfsmethode - Erstellt eine Liste aller Extras, die ein best. Produkt in einer Bestellung hat.
        /// </summary>
        /// <param name="_opID">DB-ID des bestellten Produkts</param>
        /// <returns>Liste aller Extras</returns>
        private List<clsExtra> GetExtrasByOPID(int _opID)
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
                _extra.Price = AddDoubleFieldValue(_dr, "EPrice");

                _myExtrasList.Add(_extra);
            }

            return _myExtrasList;
        }

        /// <summary>
        /// Liest alle nicht gelieferten Orders aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns>Liste alle nicht gelieferten Orders</returns>
        internal List<clsOrderExtended> GetOrdersNotDelivered()
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
        }

        /// <summary>
        /// Liest alle gelieferten Orders aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns>Liste aller gelieferten Orders</returns>
        internal List<clsOrderExtended> GetFinishedOrders()
        {
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetAllFinishedOrders");
            DataTable _myDataTable = _myDataSet.Tables[0];

            List<clsOrderExtended> _myOrderList = new List<clsOrderExtended>();

            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _myOrderList.Add(DatarowToclsOrderExtended(_dr));
            }

            return _myOrderList;
        }


        /// <summary>
        /// Legt den neuen Status einer Bestellung fest.
        /// </summary>
        /// <param name="_myOrder">Bestellung deren Status verändert wird</param>
        /// <returns>Anzahl der veränderten Zeilen (i.d.R. = 1)</returns>
        internal int UpdateOrderStatusByONumber(clsOrderExtended _myOrder)
        {
            _myDAL.AddParam("Status", _myOrder.OrderStatus, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("DeliveryDate", _myOrder.OrderDeliveryDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myDAL.AddParam("ONumber", _myOrder.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);


            int changedSets = _myDAL.MakeStoredProcedureAction("QOUpdateOrderStatusByONumber");
            return changedSets;
        }

        /// <summary>
        /// Storniert eine Bestellung.
        /// </summary>
        /// <param name="_oNumber">Nummer der Bestellung</param>
        /// <returns>Anzahl der veränderten Zeilen (i.d.R. = 1)</returns>
        internal int CancelOrderByONumber(int _oNumber)
        {
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int changedSets = _myDAL.MakeStoredProcedureAction("QOCancelOrderByONumber");
            return changedSets;
        }


        /// <summary>
        /// Liefert eine bestimmte Bestellung zurück.
        /// </summary>
        /// <param name="_oNumber">Nummer der Bestellung</param>
        /// <returns>Bestellung</returns>
        internal clsOrderExtended GetOrderByOrderNumber(int _oNumber)
        {
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QOGetOrderByOrderNumber");
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
        /// Löscht eine Bestellung. Dabei werden auch alle Produkte sowie Extras der zug. Bestellung entfernt.
        /// </summary>
        /// <param name="_oNumber">Nummer der Bestellung</param>
        /// <returns>Anzahl der veränderten Zeilen.</returns>
        internal int DeleteOrderByOrderNumber(int _oNumber)
        {
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            int changedSets = _myDAL.MakeStoredProcedureAction("QOEDeleteOrderedExtrasByONumber");

            //Neues DAL-Objekt nötig, da Verbindung sonst gesperrt ist.
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            changedSets += _myDAL.MakeStoredProcedureAction("QOPDeleteOrderedProductsByONumber");

            //Neues DAL-Objekt nötig, da Verbindung sonst gesperrt ist.
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
            _myDAL.AddParam("ONumber", _oNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            changedSets += _myDAL.MakeStoredProcedureAction("QODeleteOrderByONumber");

            return changedSets;
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
        } //DatarowToclsOrder()

    } //clsOrderCollection
}
