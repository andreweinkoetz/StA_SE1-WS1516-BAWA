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
        /// Konstruktor, instatiiert _orderCol
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
        /// OrderInsert
        /// </summary>
        /// <param name="_newOrder"></param>
        /// <returns>true fals insert erfolgreich</returns>
        public bool OrderInsert(clsOrder _newOrder)
        {
            if (_orderCol.InsertOrder(_newOrder) == 1)
                return true;
            else
                return false;
        } // OrderInsert()

        /// <summary>
        /// Preisberechnung der Bestellung - noch nicht implementiert
        /// </summary>
        /// <param name="_newOrder"></param>
        /// <returns>berechneter Preis</returns>
        public double CalculateOrderPrice(clsOrder _newOrder)
        {
            // hier müsste die Preisberechnungsroutine hin
            return 100.0;
        } // CalculateOrderPrice()
    } // clsOrderFacade
}
