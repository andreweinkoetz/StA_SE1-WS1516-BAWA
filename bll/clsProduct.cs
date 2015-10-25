using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class clsProduct
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;
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
        public double PricePerUnit
        {
            get { return _pricePerUnit; }
            set { _pricePerUnit = value; }
        }
    }
    // weitere Attribute und gg. Methoden sind noch auszuprogrammieren!
} //clsProduct
