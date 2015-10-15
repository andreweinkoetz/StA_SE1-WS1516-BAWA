using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// clsOrder: Bestellung
    /// </summary>
    public class clsOrder
    {
        // private Attribute
        private int _id;
        /// <summary>
        /// Id der Order, von DB vergeben, Read-Only, eindeutig
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value;  } 
        }

        private int _productId; 
        /// <summary>
        /// Produkt-ID des bestellten Produktes, Fremdschlüssel)
        /// </summary>
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private int _userId; 
        /// <summary>
        /// User-Id der Bestellers, Fremdschlüssel
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }


        private DateTime _orderDate;   
        /// <summary>
        /// Zeitpunkt wann bestellt wurde
        /// </summary>
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        private int _orderSize;     
        /// <summary>
        /// Größe des bestellten Produkts,  in cm, litern, etc.
        /// </summary>
        public int OrderSize
        {
            get { return _orderSize; }
            set { _orderSize = value; }
        }

        private int _orderExtras; 
        /// <summary>
        /// Anzahl der Extras, speziell bei Pizza
        /// </summary>
        public int OrderExtras
        {
            get { return _orderExtras; }
            set { _orderExtras = value; }
        }

        private int _orderCount;    // nr of products ordered
        /// <summary>
        /// Anzahl der bestellten Produkte
        /// </summary>
        public int OrderCount
        {
            get { return _orderCount; }
            set { _orderCount = value; }
        }

        private double _orderSum; 
        /// <summary>
        /// Gesamtpreis der Bestellung
        /// </summary>
        public double OrderSum
        {
            get { return _orderSum; }
            set { _orderSum = value; }
        }

        private bool _orderDelivery;   

        /// <summary>
        /// true falls Bestellung geliefert werden soll, false bei Selbstabholung
        /// </summary>
        public bool OrderDelivery
        {
            get { return _orderDelivery; }
            set { _orderDelivery = value; }
        }

        private int _orderStatus;  

        /// <summary>
        /// Status der Bestellung, noch zu definieren, 0: kein Status definiert
        /// </summary>
        public int OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }

        /// <summary>
        /// Constructor (mit Default-Werten)
        /// </summary>
        public clsOrder()
        {
            this._id = 0;
            this._productId = 0;
            this._userId = 0;
            this._orderDate = DateTime.MinValue; // default
            this._orderSize = 0;
            this._orderExtras = 0;
            this._orderSum = 0.0;
            this._orderDelivery = false;
            this._orderStatus = 0;
        }

    } // clsOrder

    /// <summary>
    /// clsOrderExtended(): Erweiterung von clsOrder um Attribute ProductName und UserName
    /// diese sind für die Anzeige benutzerfreundlicher als die zugehörigen IDs
    /// </summary>
    public class clsOrderExtended : clsOrder
    {
        private string _productName;    // Name of product ordered; not part of DB table, for viewing purposes, do not update

        /// <summary>
        /// Name des bestellten Produktes
        /// </summary>
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private string _userName;      // user name ordering; not part of DB table, do not update

        /// <summary>
        /// Name des bestellenden Nutzers
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// Constructor (mit Default-Werten)
        /// ruft zunächst Constructor der Oberklasse (clsOrder) auf und setzt dann die zusätzlichen Attribute
        /// </summary>
        public clsOrderExtended() : base()
        {
            this._productName = "";
            this._userName = "";
        }
        
    } // clsOrderExtended
}
