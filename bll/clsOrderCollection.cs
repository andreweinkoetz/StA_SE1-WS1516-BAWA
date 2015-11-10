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
        DAL.DALObjects.dDataProvider _myProvider;   // DAL-Objekt, wird in Konstruktor instantiiert

        /// <summary>
        /// Order-Collection Konstruktor 
        /// </summary>
        public clsOrderCollection()
        {
            // hier wird der Pfad zur Access-Datei aus web.config gelesen
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            // DAL-Objekt instantiieren, wird von den Methoden unten genutzt
            _myProvider = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        /// <summary>
        /// Liest alle Order aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns></returns>
        public List<clsOrderExtended> getAllOrders()
        {
            //Hier wird unser Dataset aus der DB befüllt
            DataSet _myDataSet = _myProvider.GetStoredProcedureDSResult("QOGetAllOrders");

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
            _myProvider.AddParam("ONumber", _Order.OrderNumber, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OFKUserId", _Order.UserId, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("ODate", _Order.OrderDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myProvider.AddParam("ODeliveryDate", _Order.OrderDate, DAL.DataDefinition.enumerators.SQLDataType.DATETIME);
            _myProvider.AddParam("ODelivery", _Order.OrderDelivery, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myProvider.AddParam("OStatus", _Order.OrderStatus, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myProvider.AddParam("OSum", _Order.OrderSum, DAL.DataDefinition.enumerators.SQLDataType.DOUBLE);
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
            _myProvider.AddParam("OPSize", _Product.Size, DAL.DataDefinition.enumerators.SQLDataType.INT);

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
            _myOrder.ProductName = AddStringFieldValue(_dr, "PName");
            _myOrder.UserId = AddIntFieldValue(_dr, "OFKUserId");
            _myOrder.UserName = AddStringFieldValue(_dr, "UName");
            _myOrder.OrderDate = AddDateTimeFieldValue(_dr, "ODate");
            _myOrder.OrderSum = AddDoubleFieldValue(_dr, "OSum");
            _myOrder.OrderDelivery = AddBoolFieldValue(_dr, "ODelivery");
            _myOrder.OrderStatus = AddIntFieldValue(_dr, "OStatus");
            return _myOrder;
        } //DatarowToclsOrder()


    } //clsOrderCollection
}
