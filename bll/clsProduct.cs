using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Diese Klasse enthält die Eigenschaften/Attribute eines Produkts.
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
        /// Name des Produkts
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
        /// Preis pro Einheit (z.B. cm)
        /// </summary>
        public double PricePerUnit
        {
            get { return _pricePerUnit; }
            set { _pricePerUnit = value; }
        }
    }
    // weitere Attribute und gg. Methoden sind noch auszuprogrammieren!
} //clsProduct
