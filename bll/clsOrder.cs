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
        private int _id;
        /// <summary>
        /// ID der Bestellung (wird von der Datenbank vergeben).
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _orderNumber;
        /// <summary>
        /// Bestellnummer der Bestellung.
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
        /// User-ID des bestellenden Benutzers.
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private DateTime _orderDate;
        /// <summary>
        /// Zeitpunkt, zu dem bestellt wurde.
        /// </summary>
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

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
        /// Zeigt an, ob die Bestellung geliefert werden soll.
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
        /// Überschriebene HashCode-Methode für 
        /// eindeutige Bestellnummerngenerierung.
        /// </summary>
        /// <returns>HashCode zur Verwendung als Bestellnummer.</returns>
        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            hash += 17 * ID + OrderDate.GetHashCode() + 31 * UserId;
            if (OrderDelivery)
            {
                hash += 31;
            }

            return hash;
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
        /// ID des Gutscheins, der eingelöst werden kann.
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
            dt.Columns.Add("Nr.");
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

        /// <summary>
        /// Erstellt einen Report aus Eigenschaften der Bestellung.
        /// </summary>
        /// <returns>Reportinhalt in textueller Form vorbereitet für einen CSV-Export</returns>
        public String CreateCSVString()
        {
            clsOrderFacade _orderFacade = new clsOrderFacade();
            List<clsProductExtended> _myProductList = _orderFacade.GetOrderedProductsByOrderNumber(OrderNumber);

            double _sum = 0.0;

            StringBuilder _toCSV = new StringBuilder();

            _toCSV.Append("Bestellung: #" + OrderNumber);
            _toCSV.AppendLine();
            _toCSV.Append("Bestellnummer;Kunde;Lieferzeitpunkt;Lieferung;Endstatus;Gesamtsumme");
            _toCSV.AppendLine();
            _toCSV.Append(OrderNumber);
            _toCSV.Append(';');
            _toCSV.Append(UserName);
            _toCSV.Append(';');
            _toCSV.Append(OrderDeliveryDate);
            _toCSV.Append(';');
            _toCSV.Append(OrderDelivery);
            _toCSV.Append(';');
            _toCSV.Append(OrderStatusDescription);
            _toCSV.Append(';');
            _toCSV.Append(String.Format("{0:N}", OrderSum));
            _toCSV.Append(" EUR");
            _toCSV.AppendLine();
            _toCSV.Append("Enthaltene Produkte:");
            _toCSV.AppendLine();
            _toCSV.Append("Produktname;Preis pro Einheit;Größe;Produktkategorie;Enthaltene Extras;Preis des Produkts");
            _toCSV.AppendLine();

            foreach (clsProductExtended _myProduct in _myProductList)
            {
                double _productPrice = _myProduct.Size * _myProduct.PricePerUnit;
                _toCSV.Append(_myProduct.Name);
                _toCSV.Append(';');
                _toCSV.Append(String.Format("{0:N}", _myProduct.PricePerUnit));
                _toCSV.Append(" EUR");
                _toCSV.Append(';');
                _toCSV.Append(_myProduct.Size);
                _toCSV.Append(';');
                _toCSV.Append(_myProduct.Category);
                _toCSV.Append(';');

                double _extrasPrice = 0.0;
                foreach (clsExtra _myExtra in _myProduct.ProductExtras)
                {
                    _extrasPrice += _myExtra.Price;
                    _toCSV.Append(_myExtra.Name);
                    _toCSV.Append(" ");
                }
                _toCSV.Append(';');
                _productPrice += _extrasPrice;
                _sum += _productPrice;
                _toCSV.Append(String.Format("{0:N}", _productPrice));
                _toCSV.Append(" EUR");
                _toCSV.AppendLine();
            }
            _toCSV.AppendLine();
            if (MyCoupon != null)
            {
                double _discount = _sum - OrderSum;
                _toCSV.Append("Gutschein# " + CouponId + " (" + MyCoupon.Code + ") eingelöst.");
                _toCSV.Append(";;;;");
                _toCSV.Append("Wert: ");
                _toCSV.Append(';');
                _toCSV.Append("-" + MyCoupon.Discount + "%");
                _toCSV.AppendLine();
                _toCSV.Append(";;;;");
                _toCSV.Append("Rabatt: ");
                _toCSV.Append(';');
                _toCSV.Append(String.Format("{0:0.00}", _discount) + " EUR");
                _toCSV.AppendLine();
                _toCSV.Append(";;;;");
                _toCSV.Append("Alte Gesamtsumme: ");
                _toCSV.Append(';');
                _toCSV.Append(String.Format("{0:0.00}", _sum) + " EUR");
                _toCSV.AppendLine();
            }

            _toCSV.Append("Exportiert am " + DateTime.Now + " Uhr");

            return _toCSV.ToString();
        }
    }
}
