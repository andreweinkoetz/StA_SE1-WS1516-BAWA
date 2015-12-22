using System;
using System.Collections.Generic;
namespace bll
{
    /// <summary>
    /// 
    /// </summary>
    public class clsProduct
    {
        private int _id;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public double PricePerUnit
        {
            get { return _pricePerUnit; }
            set { _pricePerUnit = value; }
        }

        private String _category;

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
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







    }//clsProduct
    // weitere Attribute und gg. Methoden sind noch auszuprogrammieren!

    /// <summary>
    /// 
    /// </summary>
    public class clsProductExtended : clsProduct
    {
        private List<clsExtra> _productExtras;
        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        public clsProductExtended() : base()
        {
            this._opID = 0;
            this._productExtras = null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_productExtras"></param>
        public clsProductExtended(List<clsExtra> _productExtras) : base()
        {
            this._productExtras = _productExtras;
        }

        /// <summary>
        /// Statische Methode zur Erstellung einer neuen Pizza.
        /// Abgestützt auf generelle Produktfabrik.
        /// </summary>
        /// <param name="_id">ID der Pizza</param>
        /// <param name="_size">gewählte Größe der Pizza.</param>
        /// <param name="_myExtraList">gewählte Extras der Pizza.</param>
        /// <returns>Neues Produkt-Objekt (Pizza).</returns>
        public static clsProductExtended PizzaFactory(int _id, double _size, List<clsExtra> _myExtraList)
        {
            clsProductExtended _myProduct = ProductFactory(_id, _size);
            _myProduct.ProductExtras = _myExtraList;
            return _myProduct;
        }

        /// <summary>
        /// Statische Methode zur Erstellung eines neuen Produkts.
        /// </summary>
        /// <param name="_id">ID des Produkts</param>
        /// <param name="_size">gewählte Größe des Produkts.</param>
        /// <returns>Neues Produkt-Objekt.</returns>
        public static clsProductExtended ProductFactory(int _id, double _size)
        {
            clsProductExtended _myProduct = new clsProductCollection().GetProductById(_id);
            _myProduct.Size = _size;
            return _myProduct;
        }

    }
}



