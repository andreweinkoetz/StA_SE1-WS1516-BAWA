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


    }
    // weitere Attribute und gg. Methoden sind noch auszuprogrammieren!
} //clsProduct
