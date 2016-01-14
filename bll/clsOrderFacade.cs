using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die nach außen sichtbaren Methoden bzgl. der Order-Verwaltung bereit.
    /// Als Grundlage werden die clsOrderCollection-Methoden verwendet.
    /// </summary>
    public class clsOrderFacade
    {
        clsOrderCollection _orderCol; // Objektvariable für die Order-Collection, wird im Konstruktor instantiiert

        /// <summary>
        /// Erstellt ein neues Objekt der clsOrderCollection.
        /// </summary>
        public clsOrderFacade()
        {
            _orderCol = new clsOrderCollection();
        }

        /// <summary>
        /// Liefert alle vorhandenen Bestellungen zurück.
        /// </summary>
        /// <returns>alle vorhandenen Bestellungen</returns>
        public List<clsOrderExtended> OrdersGetAll()
        {
            return _orderCol.GetAllOrders();
        }

        /// <summary>
        /// Liefert alle noch nicht gelieferten Bestellungen zurück.
        /// </summary>
        /// <returns>alle noch nicht gelieferten Bestellungen</returns>
        public List<clsOrderExtended> GetOrdersNotDelivered()
        {
            return _orderCol.GetOrdersNotDelivered();
        }

        /// <summary>
        /// Fügt eine neue Bestellung in die Datenbank ein.
        /// </summary>
        /// <param name="_newOrder">die einzufügende Bestellung</param>
        /// <returns>true, falls das Einfügen der Bestellung erfolgreich ist</returns>
        public bool InsertOrder(clsOrderExtended _newOrder)
        {
            return _orderCol.InsertOrder(_newOrder) == 1;
        }

        /// <summary>
        /// Liefert alle Bestellungen eines Benutzers zurück.
        /// </summary>
        /// <param name="_userID">ID des Benutzers</param>
        /// <returns>alle Bestellungen des Benutzers</returns>
        public List<clsOrderExtended> GetOrdersByUserID(int _userID)
        {
            return _orderCol.GetOrdersByUserID(_userID);
        }

        /// <summary>
        /// Liefert die Status und Summe aller Bestellungen eines Benutzers.
        /// </summary>
        /// <param name="_uId">ID des Benutzers</param>
        /// <returns>Status und Summe aller Bestellungen</returns>
        public Dictionary<String, Double> GetOrderSumAndStatusByUserId(int _uId)
        {
            return _orderCol.GetOrderSumAndStatusByUserId(_uId);
        }

        /// <summary>
        /// Liefert alle Produkte inkl. Extras einer Bestellung zurück. 
        /// </summary>
        /// <param name="_orderNumber">Bestellnummer der Bestellung</param>
        /// <returns>alle Produkte inkl. Extras einer Bestellung</returns>
        public List<clsProductExtended> GetOrderedProductsByOrderNumber(int _orderNumber)
        {
            return _orderCol.GetOrderedProductsByOrderNumber(_orderNumber);
        }

        /// <summary>
        /// Liefert alle gelieferten bzw. abgeschlossenen Bestellungen zurück.
        /// </summary>
        /// <returns>alle gelieferten Bestellungen </returns>
        public List<clsOrderExtended> GetFinishedOrders()
        {
            return _orderCol.GetFinishedOrders();
        }

        /// <summary>
        /// Errechnet nach dem Einlösen eines Gutscheins die neue Gesamtsumme der Bestellung und liefert einen passenden Text zurück.
        /// </summary>
        /// <param name="_sum">alte Summe der Bestellung</param>
        /// <param name="_newSum">neue Summe der Bestellung</param>
        /// <param name="_myCoupon">Gutschein, der bei der Bestellung eingelöst wird</param>
        /// <returns>einen passenden Text basierend auf dem Gutschein</returns>
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
        /// Preisberechnung einer Bestellung, bei der ein Gutschein eingelöst wird.
        /// Wird nur beim Aufgeben einer Bestellung benötigt.
        /// </summary>
        /// <param name="_myProductList">die bestellten Produkte</param>
        /// <param name="_myCoupon">Gutschein, der bei der Bestellung eingelöst wird</param>
        /// <param name="_msgCoupon">Meldung über die Vorteile bzw. Auswirkungen des Gutscheins</param>
        /// <returns>neue Gesamtsumme der Bestellung</returns>
        public static double GetOrderSum(List<clsProductExtended> _myProductList, clsCoupon _myCoupon, out string _msgCoupon)
        {
            double _sum = GetOrderSum(_myProductList);
            _msgCoupon = GetMsgCoupon(_sum, out _sum, _myCoupon);
            return _sum;
        }

        /// <summary>
        /// Preisberechnung einer Bestellung, bei der ein Gutschein eingelöst wird.
        /// Wird nur beim Aufgeben einer Bestellung benötigt.
        /// </summary>
        /// <param name="_myProductList">die bestellten Produkte</param>
        /// <param name="_myCoupon">Gutschein, der bei der Bestellung eingelöst wird</param>
        /// <returns>neue Gesamtsumme der Bestellung</returns>
        public static double GetOrderSum(List<clsProductExtended> _myProductList, clsCoupon _myCoupon)
        {
            String s;
            return GetOrderSum(_myProductList, _myCoupon, out s);
        }

        /// <summary>
        /// Berechnet die Gesamtsumme einer Bestellung.
        /// </summary>
        /// <param name="_myProductList">die bestellten Produkte</param>
        /// <returns>Gesamtsumme der Bestellung</returns>
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
        /// Berechnet die Lieferzeit einer Bestellung.
        /// </summary>
        /// <param name="_selectedProducts">die zu liefernden Produkte</param>
        /// <param name="_distance">Distanz zum Kunden</param>
        /// <param name="_toDeliver">Kennzeichen, ob die Lieferart "Lieferung" ausgewählt wurde</param>
        /// <returns>die textuelle Darstellung der Wartezeit</returns>
        public static String GetEstimatedTime(List<clsProductExtended> _selectedProducts, double _distance, bool _toDeliver)
        {
            double _minutes = 0;
            String _estimatedTime = "";

            foreach (clsProductExtended _myProduct in _selectedProducts)
            {
                if (_myProduct.CID == 1)
                {
                    _minutes += 10.0;
                }
            }

            if (_toDeliver)
            {
                if (_distance > 0)
                {
                    _minutes += _distance * 2;
                }
                else
                {
                    _estimatedTime = "Bei der Berechnung der Entfernung zum Lieferort ist ein Fehler aufgetreten.<br /><b>Bitte überprüfen Sie Ihre Angaben</b>. Ihre Bestellung kann bei uns vor Ort abgeholt werden.<br />";
                }
            }

            _estimatedTime += "Die Wartezeit beträgt vorraussichtlich <b>" + Math.Round(_minutes) + " Minuten</b>.";

            return _estimatedTime;
        }

        /// <summary>
        /// Fügt die Extras des Produkts einer Bestellung in die Datenbank ein.
        /// </summary>
        /// <param name="_product">Produkt, zu dem die Extras gewählt wurden</param>
        /// <param name="_extras">die einzufügenden Extras</param>
        /// <returns>true, falls das Einfügen der Extras erfolgreich ist</returns>
        public bool InsertOrderedExtras(clsProductExtended _product, List<clsExtra> _extras)
        {
            return _orderCol.InsertOrderedExtras(_product, _extras) > 0;
        }

        /// <summary>
        /// Fügt das Produkt einer Bestellung in die Datenbank ein.
        /// </summary>
        /// <param name="_order">die zugehörige Bestellung zum Produkt</param>
        /// <param name="_product">das einzufügende Produkt</param>
        /// <returns>true, falls das Einfügen des Produkts erfolgreich ist</returns>
        public bool InsertOrderedProduct(clsOrderExtended _order, clsProductExtended _product)
        {
            return _orderCol.InsertOrderedProduct(_order, _product) > 0;
        }

        /// <summary>
        /// Fügt das Produkt einer Bestellung inkl. seiner Extras in die Datenbank ein.
        /// </summary>
        /// <param name="_myOrder">die zugehörige Bestellung zum Produkt</param>
        /// <param name="_product">das einzufügende Produkt</param>
        /// <returns>true, falls das Einfügen des Produkts inkl. seiner Extras erfolgreich ist</returns>
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
        /// Ändert den Status einer Bestellung.
        /// </summary>
        /// <param name="_myOrder">Bestellung, deren Status geändert wird</param>
        /// <returns>true, falls das Ändern des Status der Bestellung erfolgreich ist</returns>
        public bool UpdateOrderStatusByONumber(clsOrderExtended _myOrder)
        {
            return _orderCol.UpdateOrderStatusByONumber(_myOrder) == 1;
        }

        /// <summary>
        /// Gibt den Status einer Bestellung zurück.
        /// </summary>
        /// <param name="_orderNumber">Bestellnummer der Bestellung</param>
        /// <returns>Status der Bestellung</returns>
        public int GetOrderStatusByOrderNumber(int _orderNumber)
        {
            return _orderCol.GetOrderStatusByOrderNumber(_orderNumber);
        }

        /// <summary>
        /// Liefert die Bestellung mit der angegebenen Bestellnummer zurück.
        /// </summary>
        /// <param name="_oNumber">Bestellnummer der Bestellung</param>
        /// <returns>die Bestellung mit der angegebenen Bestellnummer</returns>
        public clsOrderExtended GetOrderByOrderNumber(int _oNumber)
        {
            return _orderCol.GetOrderByOrderNumber(_oNumber);
        }

        /// <summary>
        /// Liefert alle Bestellungen sortiert nach Datum zurück.
        /// </summary>
        /// <returns>alle Bestellungen sortiert nach Datum</returns>
        public List<clsOrderExtended> GetOrdersOrderedByDate()
        {
            return _orderCol.GetOrdersOrderedByDate();
        }

        /// <summary>
        /// Liefert alle Bestellungen eines Benutzers zurück.
        /// </summary>
        /// <param name="_email">Email-Adresse des Benutzers</param>
        /// <returns>alle Bestellungen des Benutzers</returns>
        public List<clsOrderExtended> GetOrdersByEmail(String _email)
        {
            return _orderCol.GetOrdersByEmail(_email);
        }

        /// <summary>
        /// Liefert alle Produkte aus Bestellungen zurück, die zur gewählten Kategorie gehören.
        /// </summary>
        /// <param name="_category">gewählte Kategorie der Produkte</param>
        /// <returns>alle Produkte, die zur gewählten Kategorie gehören</returns>
        public List<Tuple<int, string, double>> GetOrderedProductsSortByCategory(String _category)
        {
            return _orderCol.GetOrderedProductsSortByCategory(_category);
        }

        /// <summary>
        /// Storniert eine Bestellung.
        /// </summary>
        /// <param name="_oNumber">Bestellnummer der Bestellung</param>
        /// <returns>true, falls die Stornierung erfolgreich ist</returns>
        public bool CancelOrderByONumber(int _oNumber)
        {
            return _orderCol.CancelOrderByONumber(_oNumber) == 1;
        }

        /// <summary>
        /// Löscht eine Bestellung inkl. deren Produkte und Extras.
        /// Anmerkung: Nur nach einem erfolgreichem Export der Bestellung verwenden.
        /// </summary>
        /// <param name="_oNumber">Bestellnummer der Bestellung</param>
        /// <returns>Anzahl der gelöschten Datensätze</returns>
        public int DeleteOrderByOrderNumber(int _oNumber)
        {
            return _orderCol.DeleteOrderByOrderNumber(_oNumber);
        }

        /// <summary>
        /// Prüft, ob eine Bestellsumme den Mindestbestellwert zum Liefern erreicht hat.
        /// </summary>
        /// <param name="_sum">Summe der Bestellung.</param>
        /// <param name="_delivery">zeigt an, ob die Bestellung geliefert werden soll</param>
        /// <param name="_msg">Fehlermeldung, falls der Mindestbestellwert nicht erreicht ist</param>
        /// <returns>true, falls der Mindestbestellwert erreicht ist</returns>
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
        /// Liefert zurück, wieviele Bestellungen in den verschiedenen Bestellstatus sind.
        /// </summary>
        /// <returns>Anzahl der Bestellungen in den verschiedenen Bestellstatus</returns>
        public List<Tuple<String, Int32, Double>> GetOrdersOrderedByStatus()
        {
            return _orderCol.GetOrdersOrderedByStatus();
        }

        /// <summary>
        /// Liefert alle abgeschlossenen Bestellungen mit ihrer benötigten Lieferzeit zurück.
        /// </summary>
        /// <returns>alle abgeschlossenen Bestellungen mit ihrer benötigten Lieferzeit</returns>
        public Dictionary<Int32, Int32> GetTimeToDeliverOfOrders()
        {
            return _orderCol.GetTimeToDeliverOfOrders();
        }

        /// <summary>
        /// Erstellt eine Liste von Bestellnummern aller offenen Bestellungen, die von der Auswahl betroffen sind.
        /// </summary>
        /// <param name="_orderNumbers">alle Bestellnummern von offenen Bestellungen, die von der Auswahl betroffen sind</param>
        /// <param name="_selAdmData">die ausgewählten Daten, die geändert werden sollen</param>
        /// <returns>alle betroffenen Bestellnummern der offenen Bestellungen</returns>
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
    }
}
