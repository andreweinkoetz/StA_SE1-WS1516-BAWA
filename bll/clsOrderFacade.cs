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
            return _orderCol.getAllOrders();
        } // OrdersGetAll()

        /// <summary>
        /// Alle Bestellungen die noch nicht geliefert wurden anzeigen.
        /// </summary>
        /// <returns></returns>
        public List<clsOrderExtended> getOrdersNotDelivered()
        {
            return _orderCol.getOrdersNotDelivered();
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
        public List<clsOrderExtended> getOrdersByUserID(int _userID)
        {
            return _orderCol.getOrdersByUserID(_userID);
        }

        /// <summary>
        /// Alle Bestellungen sortiert nach Datum.
        /// </summary>
        /// <returns></returns>
        public List<clsOrderExtended> getOrdersOrderedByDate()
        {
            return _orderCol.getOrdersOrderedByDate();
        }

        /// <summary>
        /// Alle Bestellungen eines bestimmten Nutzers.
        /// </summary>
        /// <param name="_email"></param>
        /// <returns></returns>
        public List<clsOrderExtended> getOrdersByEmail(String _email)
        {
            return _orderCol.getOrdersByEmail(_email);
        }

        /// <summary>
        /// Alle Produkte inkl. Extras einer Bestellung.
        /// </summary>
        /// <param name="_orderNumber"></param>
        /// <returns></returns>
        public List<clsProductExtended> getOrderedProductsByOrderNumber(int _orderNumber)
        {
            return _orderCol.getOrderedProductsByOrderNumber(_orderNumber);
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
        public bool updateOrderStatusByONumber(clsOrderExtended _myOrder)
        {
            return (_orderCol.updateOrderStatusByONumber(_myOrder) == 1);
        }

        /// <summary>
        /// Liefert die Produkte pro Kategorie zurück.
        /// </summary>
        /// <param name="_category"></param>
        /// <returns></returns>
        public List<Tuple<int, string, double>> GetOrderedProductsSortByCategory(String _category)
        {
            return _orderCol.GetOrderedProductsSortByCategory(_category);
        }
    } // clsOrderFacade
}
