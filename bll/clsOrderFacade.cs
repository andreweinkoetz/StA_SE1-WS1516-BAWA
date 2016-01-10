using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// clsOrderFacade: nach außen hin sichtbare Methoden bzgl. Order-Verwaltung
    /// das meiste wird direkt an clsOrderCollection-Methoden durchgereicht
    /// </summary>
    public class clsOrderFacade
    {
        clsOrderCollection _orderCol;  // Objektvariable für Order-Collection, wird im Konstruktor instantiiert 
        /// <summary>
        /// Konstruktor, instantiiert _orderCol
        /// </summary>
        public clsOrderFacade()
        {
            _orderCol = new clsOrderCollection();
        }

        /// <summary>
        /// Alle Orders lesen
        /// </summary>
        /// <returns>Liste der Order</returns>
        public List<clsOrderExtended> OrdersGetAll()
        {
            return _orderCol.GetAllOrders();
        } // OrdersGetAll()

        /// <summary>
        /// Alle Bestellungen die noch nicht geliefert wurden anzeigen.
        /// </summary>
        /// <returns></returns>
        public List<clsOrderExtended> GetOrdersNotDelivered()
        {
            return _orderCol.GetOrdersNotDelivered();
        }

        /// <summary>
        /// OrderInsert
        /// </summary>
        /// <param name="_newOrder">neue Bestellung</param>
        /// <returns>true falls insert erfolgreich</returns>
        public bool InsertOrder(clsOrderExtended _newOrder)
        {
            if (_orderCol.InsertOrder(_newOrder) == 1)
                return true;
            else
                return false;
        } // OrderInsert()


        /// <summary>
        /// Alle Bestellungen eines Users.
        /// </summary>
        /// <param name="_userID">ID des Benutzers</param>
        /// <returns>Liste aller Bestellungen</returns>
        public List<clsOrderExtended> GetOrdersByUserID(int _userID)
        {
            return _orderCol.GetOrdersByUserID(_userID);
        }

        /// <summary>
        /// Alle Produkte inkl. Extras einer Bestellung.
        /// </summary>
        /// <param name="_orderNumber">Nummer der Bestellung</param>
        /// <returns>Liste aller Produkte inkl. Extras</returns>
        public List<clsProductExtended> GetOrderedProductsByOrderNumber(int _orderNumber)
        {
            return _orderCol.GetOrderedProductsByOrderNumber(_orderNumber);
        }

        /// <summary>
        /// Erstellt eine Liste aller abgeschlossenen Bestellungen.
        /// </summary>
        /// <returns>Liste aller abgeschlossenen Bestellungen.</returns>
        public List<clsOrderExtended> GetFinishedOrders()
        {
            return _orderCol.GetFinishedOrders();
        }


        /// <summary>
        /// Errechnet auf Basis eines Gutscheins die neue Gesamtsumme und liefert einen passenden Text zurück.
        /// </summary>
        /// <param name="_sum">Summe der Bestellung.</param>
        /// <param name="_newSum">Summe der Bestellung.</param>
        /// <param name="_myCoupon">Coupon der Bestellung.</param>
        /// <returns>Gutscheinbasierter Text.</returns>
        public static String GetMsgCoupon(double _sum, out double _newSum, clsCoupon _myCoupon)
        {
            String _msgCoupon = "";
            _newSum = _sum;
            if (_myCoupon != null)
            {
                _newSum = _sum - (_sum * (_myCoupon.Discount / 100.0));
                double _saving = _sum - _newSum;
                _msgCoupon = "Wert des Gutscheins: " + _myCoupon.Discount + "%.<br />";
                _msgCoupon += "Ersparnis: " + String.Format("{0:C}", _saving);
            }

            return _msgCoupon;
        }

        /// <summary>
        /// Preisberechnung einer Bestellung mit Coupon.
        /// Wird nur beim Aufgeben einer Bestellung benötigt.
        /// </summary>
        /// <param name="_myProductList">Liste der bestellten Produkte.</param>
        /// <param name="_myCoupon">Coupon, der auf Bestellung eingelöst wurde.</param>
        /// <param name="_msgCoupon">Meldung über Vorteile des Gutscheins.</param>
        /// <returns>Gesamtsumme der Bestellung.</returns>
        public static double GetOrderSum(List<clsProductExtended> _myProductList, clsCoupon _myCoupon, out string _msgCoupon)
        {
            double _sum = GetOrderSum(_myProductList);

            _msgCoupon = GetMsgCoupon(_sum, out _sum, _myCoupon);

            return _sum;
        }

        /// <summary>
        /// Preisberechnung einer Bestellung mit Coupon.
        /// Wird nur beim Aufgeben einer Bestellung benötigt.
        /// </summary>
        /// <param name="_myProductList">Liste der bestellten Produkte.</param>
        /// <param name="_myCoupon">Coupon, der auf Bestellung eingelöst wurde.</param>
        /// <returns>Gesamtsumme der Bestellung.</returns>
        public static double GetOrderSum(List<clsProductExtended> _myProductList, clsCoupon _myCoupon)
        {
            String s;
            return GetOrderSum(_myProductList, _myCoupon, out s);
        }

        /// <summary>
        /// Preisberechnung einer Bestellung.
        /// </summary>
        /// <param name="_myProductList">Liste der bestellten Produkte.</param>
        /// <returns>Gesamtsumme der Bestellung.</returns>
        public static double GetOrderSum(List<clsProductExtended> _myProductList)
        {
            double _sum = 0.0;

            foreach (clsProductExtended _product in _myProductList)
            {
                _sum += _product.PricePerUnit * _product.Size + clsProductFacade.GetCostsOfExtras(_product);
            }

            return _sum;

        }

        /// <summary>
        /// Berechnung der Lieferzeit einer Bestellung
        /// </summary>
        /// <param name="_selectedProducts">zu liefernde Produkte</param>
        /// <param name="_distance">Distanz zum Kunden</param>
        /// <param name="_toDeliver">Kennzeichen, ob die Liefertart "Lieferung" ausgewählt wurde</param>
        /// <returns>eine textuelle Darstellung der Wartezeit</returns>
        public static String GetEstimatedTime(List<clsProductExtended> _selectedProducts, double _distance, bool _toDeliver)
        {
            double _minutes = 0;

            if (_toDeliver)
            {
                _minutes += _distance * 2;
            }

            foreach (clsProductExtended _myProduct in _selectedProducts)
            {
                if (_myProduct.CID == 1)
                {
                    _minutes += 10.0;
                }
            }
            return "Die Wartezeit beträgt vorraussichtlich " + Math.Round(_minutes) + " Minuten.";
        }

        /// <summary>
        /// Einfügen der Extras der Produkte einer Bestellung.
        /// </summary>
        /// <param name="_Product">Produkt das die Extras enthält</param>
        /// <param name="_Extras">Extras die einzufügen sind.</param>
        /// <returns>Anzahl der veränderten Zeilen</returns>
        public bool InsertOrderedExtras(clsProductExtended _Product, List<clsExtra> _Extras)
        {
            return (_orderCol.InsertOrderedExtras(_Product, _Extras) > 0);
        }

        /// <summary>
        /// Einfügen der Produkte einer Bestellung.
        /// </summary>
        /// <param name="_Order">Bestellung des Produkt</param>
        /// <param name="_Product">Produkt</param>
        /// <returns>true wenn Einfügen erfolgreich</returns>
        public bool InsertOrderedProduct(clsOrderExtended _Order, clsProductExtended _Product)
        {
            return (_orderCol.InsertOrderedProduct(_Order, _Product) > 0);
        }

        /// <summary>
        /// Einfügen eines Produkts inkl. seiner Extras in die DB.
        /// </summary>
        /// <param name="_myOrder">zug. Bestellung</param>
        /// <param name="_product">einzufügendes Produkt</param>
        /// <returns>true wenn erfolgreich</returns>
        public bool InsertOrderedProductWithExtras(clsOrderExtended _myOrder, clsProductExtended _product)
        {
            bool orderIsCorrect;
            _product.OpID = _product.GetHashCode() + _myOrder.OrderNumber;
            orderIsCorrect = InsertOrderedProduct(_myOrder, _product);
            if (_product.ProductExtras != null)
            {
                if (_product.ProductExtras.Count > 0)
                {
                    orderIsCorrect = InsertOrderedExtras(_product, _product.ProductExtras) && orderIsCorrect;
                }
            }

            return orderIsCorrect;
        }

        /// <summary>
        /// Bestellstatus aktualisieren.
        /// </summary>
        /// <param name="_myOrder">Bestellung deren Status verändert wird.</param>
        /// <returns>true wenn erfolgreich</returns>
        public bool UpdateOrderStatusByONumber(clsOrderExtended _myOrder)
        {
            return (_orderCol.UpdateOrderStatusByONumber(_myOrder) == 1);
        }

        /// <summary>
        /// Gibt den Status einer Bestellung zurück.
        /// </summary>
        /// <param name="_orderNumber">Bestellnummer</param>
        /// <returns>Status der Bestellung</returns>
        public int GetOrderStatusByOrderNumber(int _orderNumber)
        {
            return _orderCol.GetOrderStatusByOrderNumber(_orderNumber);
        }

        /// <summary>
        /// Liefert eine Bestellung anhand ihrer Bestellnummer.
        /// </summary>
        /// <param name="_oNumber">Nummer der Bestellung</param>
        /// <returns>Bestellung</returns>
        public clsOrderExtended GetOrderByOrderNumber(int _oNumber)
        {
            return _orderCol.GetOrderByOrderNumber(_oNumber);
        }

        /// <summary>
        /// Alle Bestellungen sortiert nach Datum.
        /// </summary>
        /// <returns>Liste aller Bestellungen sortiert nach Datum</returns>
        public List<clsOrderExtended> GetOrdersOrderedByDate()
        {
            return _orderCol.GetOrdersOrderedByDate();
        }

        /// <summary>
        /// Alle Bestellungen eines bestimmten Nutzers.
        /// </summary>
        /// <param name="_email">Username/Email</param>
        /// <returns>Liste aller Bestellung des Nutzers.</returns>
        public List<clsOrderExtended> GetOrdersByEmail(String _email)
        {
            return _orderCol.GetOrdersByEmail(_email);
        }

        /// <summary>
        /// Gibt eine Liste von Ergebnistupeln geordnet nach Kategorie zurück.
        /// </summary>
        /// <param name="_category">Kategorie der Produkte</param>
        /// <returns>Liste von Ergebnistupeln</returns>
        public List<Tuple<int, string, double>> GetOrderedProductsSortByCategory(String _category)
        {
            return _orderCol.GetOrderedProductsSortByCategory(_category);
        }


        /// <summary>
        /// Storniert eine Bestellung.
        /// </summary>
        /// <param name="_oNumber">Bestellung die storniert werden soll</param>
        /// <returns>true falls Stornierung erfolgreich war</returns>
        public bool CancelOrderByONumber(int _oNumber)
        {
            return _orderCol.CancelOrderByONumber(_oNumber) == 1;
        }

        /// <summary>
        /// Löscht eine Bestellung inkl. aller Inhalte.
        /// Nur nach erfolgreichem Export verwenden!
        /// </summary>
        /// <param name="_oNumber">Bestellnummer der zu löschenden Bestellungen.</param>
        /// <returns>Anzahl der gelöschten Datensätze.</returns>
        public int DeleteOrderByOrderNumber(int _oNumber)
        {
            return _orderCol.DeleteOrderByOrderNumber(_oNumber);
        }

        /// <summary>
        /// Prüft, ob eine Bestellsumme den Mindestbestellwert (zum Liefern) erreicht.
        /// </summary>
        /// <param name="_sum">Summe der Bestellung.</param>
        /// <param name="_delivery">true wenn geliefert werden soll.</param>
        /// <param name="_msg">Fehlermeldung falls nicht erreicht.</param>
        /// <returns>true wenn Mindestbestellwert erreicht.</returns>
        public static bool CheckMinimumOrder(double _sum, bool _delivery, out string _msg)
        {
            double _diff = 20 - _sum;

            if (_sum <= 20 && _delivery)
            {
                _msg = "Mindestbestellwert nicht erreicht. <br />"
                    + "Bitte bestellen Sie für mindestens 20,00 EUR wenn Sie eine Lieferung wünschen.<br />"
                    + "\n Es fehlen noch " + String.Format("{0:F}", _diff) + " EUR.";
                return false;
            }
            else
            {
                _msg = "";
                return true;
            }
        }


        /// <summary>
        /// Gibt eine Liste von Ergebnistupeln geordnet nach Status zurück.
        /// </summary>
        /// <returns>Liste von Ergebnistupeln</returns>
        public List<Tuple<String, Int32, Double>> GetOrdersOrderedByStatus()
        {
            return _orderCol.GetOrdersOrderedByStatus();
        }

        /// <summary>
        /// Gibt ein Dictionary mit den Ergebniswerten zurück.
        /// </summary>
        /// <returns>Dictionary mit Ergebniswerten</returns>
        public Dictionary<Int32, Int32> GetTimeToDeliverOfOrders()
        {
            return _orderCol.GetTimeToDeliverOfOrders();
        }

        /// <summary>
        /// Hilfsmethode zur Erstellung einer (textuellen) Liste von offenen Bestellnummern.
        /// </summary>
        /// <param name="_orderNumbers">Liste der Bestellnummern</param>
        /// <param name="_selAdmData">Auswahl (1 - Produkt 2 - Extra 3 - Benutzer)</param>
        /// <returns>textuelle Liste von offenen Bestellnummern</returns>
        public static string CreateStringOfOpenOrders(List<Int32> _orderNumbers, int _selAdmData)
        {
            String _openOrders = "";

            switch (_selAdmData)
            {
                case 1:
                    _openOrders = "Produkt";
                    break;
                case 2:
                    _openOrders = "Extra";
                    break;
                case 3:
                    _openOrders = "Benutzer";
                    break;
            }

            _openOrders += " kann nicht gelöscht werden, da in folgenden Bestellungen enthalten: <br />{<br />";
            foreach (int _oNumber in _orderNumbers)
            {
                _openOrders += _oNumber + "<br />";
            }
            _openOrders += "}";
            return _openOrders;
        }

    } // clsOrderFacade
}
