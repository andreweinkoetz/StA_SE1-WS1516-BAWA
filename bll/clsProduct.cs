using System;
using System.Collections.Generic;
namespace bll
{
    /// <summary>
    /// Klasse repräsentiert Produktobjekt.
    /// </summary>
    public class clsProduct
    {
        private int _id;
        /// <summary>
        /// ID des Produkts
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;
        /// <summary>
        /// Name des Produkts.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    _name = "Product without a Name";
                else
                    _name = value;
            }
        }

        private double _pricePerUnit;
        /// <summary>
        /// Preis pro Einheit
        /// </summary>
        public double PricePerUnit
        {
            get { return _pricePerUnit; }
            set { _pricePerUnit = value; }
        }

        private String _category;

        /// <summary>
        /// Kategorie des Produkts
        /// </summary>
        public String Category
        {
            get
            {
                return _category;
            }

            set
            {
                _category = value;
            }
        }

        private String _cUnit;

        /// <summary>
        /// Einheit des Produkts
        /// </summary>
        public string CUnit
        {
            get
            {
                return _cUnit;
            }

            set
            {
                _cUnit = value;
            }
        }

        private double _size;
        /// <summary>
        /// Größe eines gewählten Produkts.
        /// </summary>
        public double Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }


        private bool _toSell;
        /// <summary>
        /// Anzeige ob Produkt zum Verkauf angeboten wird.
        /// </summary>
        public bool ToSell
        {
            get
            {
                return _toSell;
            }

            set
            {
                _toSell = value;
            }
        }

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        public clsProduct() { }

        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="_productToCopy">zu kopierendes Produkt</param>
        public clsProduct(clsProduct _productToCopy)
        {
            this._id = _productToCopy._id;
            this._name = _productToCopy._name;
            this._pricePerUnit = _productToCopy._pricePerUnit;
            this._size = _productToCopy._size;
            this._toSell = _productToCopy._toSell;
            this._cUnit = _productToCopy._cUnit;
            this._category = _productToCopy._category;
        }





    }//clsProduct

    /// <summary>
    /// Erweiterungen der Poduktklasse
    /// </summary>
    public class clsProductExtended : clsProduct
    {
        private List<clsExtra> _productExtras;
        /// <summary>
        /// Liste aller Extras eines Produkts
        /// </summary>
        public List<clsExtra> ProductExtras
        {
            get
            {
                return _productExtras;
            }

            set
            {
                _productExtras = value;
            }
        }

        private int _opID;
        /// <summary>
        /// Bestellposition eines Produkts innerhalb einer Bestellung. (DB-PK)
        /// </summary>
        public int OpID
        {
            get
            {
                return _opID;
            }

            set
            {
                _opID = value;
            }
        }

        private int _cID;
        /// <summary>
        /// Kategorie-ID des Produkts
        /// </summary>
        public int CID
        {
            get
            {
                return _cID;
            }

            set
            {
                _cID = value;
            }
        }

        /// <summary>
        /// Standard-Konstruktor für neues erw. Produkt
        /// </summary>
        public clsProductExtended() : base()
        {
            this._opID = 0;
            this._productExtras = null;

        }



        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="_productToCopy">zu kopierendes Produkt</param>
        public clsProductExtended(clsProductExtended _productToCopy) : base(_productToCopy)
        {
            this._cID = _productToCopy._cID;
            this._opID = _productToCopy._opID;
            List<clsExtra> _productExtras = new List<clsExtra>();
            if (_productToCopy.ProductExtras != null)
            {
                foreach (clsExtra _myExtra in _productToCopy._productExtras)
                {
                    _productExtras.Add(new clsExtra(_myExtra));
                }
                this._productExtras = _productExtras;
            }
        }

        /// <summary>
        /// Konstruktor für neues erw. Produkt inkl. Extras.
        /// </summary>
        /// <param name="_productExtras">Produktextras</param>
        public clsProductExtended(List<clsExtra> _productExtras) : base()
        {
            this._productExtras = _productExtras;
        }

        /// <summary>
        /// Statische Methode zur Erstellung einer neuen Pizza.
        /// Abgestützt auf generelle Produktfabrik.
        /// </summary>
        /// <param name="_id">ID der Pizza</param>
        /// <param name="_myExtraList">gewählte Extras der Pizza.</param>
        /// <returns>Neues Produkt-Objekt (Pizza).</returns>
        public static clsProductExtended PizzaFactory(int _id, List<clsExtra> _myExtraList)
        {
            clsProductExtended _myProduct = ProductFactory(_id);
            _myProduct.ProductExtras = _myExtraList;
            return _myProduct;
        }

        /// <summary>
        /// Statische Methode zur Erstellung eines neuen Produkts.
        /// </summary>
        /// <param name="_id">ID des Produkts</param>
        /// <returns>Neues Produkt-Objekt.</returns>
        public static clsProductExtended ProductFactory(int _id)
        {
            clsProductExtended _myProduct = new clsProductCollection().GetProductById(_id);
            _myProduct.Size = 0;
            return _myProduct;
        }

    }
}



