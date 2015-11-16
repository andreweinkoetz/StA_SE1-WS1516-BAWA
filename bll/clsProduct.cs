using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      

    }
}



