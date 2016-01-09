using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die Eigenschaften einer Bestellung dar.
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
            set { _id = value; }
        }

        private int _orderNumber;
        /// <summary>
        /// Bestellnummer - generiert für Anzeige.
        /// </summary>
        public int OrderNumber
        {
            get
            {
                return _orderNumber;
            }

            set
            {
                _orderNumber = value;
            }
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

        //TODO: hier weiter!
        private double _orderSum;
        /// <summary>
        /// Gesamtpreis der Bestellung.
        /// </summary>
        public double OrderSum
        {
            get { return _orderSum; }
            set { _orderSum = value; }
        }

        private bool _orderDelivery;
        /// <summary>
        /// Zeigt an, ob eine Bestellung geliefert werden soll.
        /// </summary>
        public bool OrderDelivery
        {
            get { return _orderDelivery; }
            set { _orderDelivery = value; }
        }

        private int _orderStatus;
        /// <summary>
        /// Status der Bestellung. 
        /// </summary>
        public int OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }

        /// <summary>
        /// Standard-Konstruktor für ein neues Order-Objekt.
        /// </summary>
        public clsOrder()
        {
            this._id = 0;
            this._userId = 0;
            this._orderDate = DateTime.MinValue;
            this._orderSum = 0.0;
            this._orderDelivery = false;
            this._orderStatus = 0;
        }

        /// <summary>
        /// Generiert eine neue Bestellnummer.
        /// </summary>
        /// <returns>die generierte Bestellnummer</returns>
        public int GenerateOrderNumber()
        {
            Random rnd = new Random();
            int _orderNumber = 2015 + rnd.Next() * 100 + UserId * rnd.Next();
            return _orderNumber;
        }
    }

    /// <summary>
    /// Erweiterung der Klasse clsOrder um weitere benötigte Eigenschaften.
    /// </summary>
    public class clsOrderExtended : clsOrder
    {
        private string _userName;
        /// <summary>
        /// Name des bestellenden Nutzers.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private int _OPId;
        /// <summary>
        /// Verknüpfungs-ID für die Zuordnung zu bestellten Produkten.
        /// </summary>
        public int OPId
        {
            get
            {
                return _OPId;
            }

            set
            {
                _OPId = value;
            }
        }

        private DateTime _orderDeliveryDate;
        /// <summary>
        /// Zeitpunkt, zu dem die Bestellung geliefert wurde.
        /// </summary>
        public DateTime OrderDeliveryDate
        {
            get
            {
                return _orderDeliveryDate;
            }

            set
            {
                _orderDeliveryDate = value;
            }
        }

        private String _orderStatusDescription;
        /// <summary>
        /// Textuelle Beschreibung des Status einer Bestellung.
        /// </summary>
        public string OrderStatusDescription
        {
            get
            {
                return _orderStatusDescription;
            }

            set
            {
                _orderStatusDescription = value;
            }
        }

        private int _couponId;
        /// <summary>
        /// ID des Gutscheins.
        /// </summary>
        public int CouponId
        {
            get
            {
                return _couponId;
            }

            set
            {
                _couponId = value;
            }
        }

        private clsCoupon _myCoupon;
        /// <summary>
        /// Gutschein, der bei einer Bestellung eingelöst werden kann.
        /// </summary>
        public clsCoupon MyCoupon
        {
            get
            {
                return _myCoupon;
            }

            set
            {
                _myCoupon = value;
            }
        }

        /// <summary>
        /// Standard-Konstruktor für ein neues OrderExtended-Objekt.
        /// Ruft zunächst den Konstruktor der Oberklasse (clsOrder) auf und setzt dann die zusätzlichen Attribute.
        /// </summary>
        public clsOrderExtended() : base()
        {
            this._userName = "";
            this._OPId = 0;
            this._orderDeliveryDate = DateTime.MaxValue;
            this._orderStatusDescription = "";
            this._couponId = 0;
            this._myCoupon = null;
        }

        /// <summary>
        /// Erstellt eine Tabelle mit allen Produkten einer Bestellung.
        /// </summary>
        /// <param name="_selectedProducts">alle Produkte der Bestellung</param>
        /// <returns>die neu erstellte Tabelle mit Produkten</returns>
        public DataTable CreateDataTableOfOrder(List<clsProductExtended> _selectedProducts)
        {
            DataTable dt = new DataTable();

            dt = new DataTable("MyOrder");
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Größe");
            dt.Columns.Add("Extras");
            dt.Columns.Add("Preis Gesamt");

            foreach (clsProductExtended _product in _selectedProducts)
            {
                String _extraText = "";
                String _sizeText = "";
                if (_product.ProductExtras != null)
                {
                    foreach (clsExtra _extra in _product.ProductExtras)
                    {
                        _extraText += _extra.Name + "\n";
                    }
                }
                _sizeText = SetSizeText(_product);
                dt.LoadDataRow(new object[] { _product.Id, _product.Name, _sizeText, _extraText, String.Format("{0:C}", (_product.PricePerUnit * _product.Size + clsProductFacade.GetCostsOfExtras(_product))) }, true);
            }
            return dt;
        }

        /// <summary>
        /// Setzt die zum Produkt passende Beschreibung der Größe bzw. Größeneinheit.
        /// </summary>
        /// <param name="_product">Produkt, für das die Größeneinheit ermittelt werden soll</param>
        /// <returns>die passende Größeneinheit</returns>
        private String SetSizeText(clsProductExtended _product)
        {
            switch (_product.CID)
            {
                case 1:
                    return _product.Size + " cm";
                case 2:
                    return _product.Size + " Liter";
                case 3:
                    return _product.Size + " Stück";
            }

            switch (_product.Category)
            {
                case "Pizza":
                    return _product.Size + " cm";
                case "Getränk":
                    return _product.Size + " Liter";
                case "Dessert":
                    return _product.Size + " Stück";
            }
            return "Fehler in der Verarbeitung";
        }
    }
}
