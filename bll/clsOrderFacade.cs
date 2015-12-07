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
        /// <param name="_newOrder"></param>
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
        /// <param name="_userID"></param>
        /// <returns></returns>
        public List<clsOrderExtended> GetOrdersByUserID(int _userID)
        {
            return _orderCol.GetOrdersByUserID(_userID);
        }

        /// <summary>
        /// Alle Produkte inkl. Extras einer Bestellung.
        /// </summary>
        /// <param name="_orderNumber"></param>
        /// <returns></returns>
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
        /// Preisberechnung.
        /// </summary>
        /// <param name="_pricePerProduct"></param>
        /// <returns></returns>
        public double CalculateOrderPrice(int[] _pricePerProduct)
        {
            int _orderPrice = 0;
            foreach (int i in _pricePerProduct)
            {
                _orderPrice += i;
            }

            return _orderPrice;

        } // CalculateOrderPrice()

        /// <summary>
        /// Einfügen der Extras der Produkte einer Bestellung.
        /// </summary>
        /// <param name="_Product"></param>
        /// <param name="_Extras"></param>
        /// <returns></returns>
        public bool InsertOrderedExtras(clsProductExtended _Product, List<clsExtra> _Extras)
        {
            return (_orderCol.InsertOrderedExtras(_Product, _Extras) > 0);
        }

        /// <summary>
        /// Einfügen der Produkte einer Bestellung.
        /// </summary>
        /// <param name="_Order"></param>
        /// <param name="_Product"></param>
        /// <returns></returns>
        public bool InsertOrderedProduct(clsOrderExtended _Order, clsProductExtended _Product)
        {
            return (_orderCol.InsertOrderedProduct(_Order, _Product) > 0);
        }

        /// <summary>
        /// Bestellstatus aktualisieren.
        /// </summary>
        /// <param name="_myOrder"></param>
        /// <returns></returns>
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

    } // clsOrderFacade
}
