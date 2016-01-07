using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Größe eines Produkts.
    /// </summary>
    public class clsSize
    {
        private double _value;

        /// <summary>
        /// Wert der Größe eines Proukts.
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        private String _name;

        /// <summary>
        /// Name der Größe eines Produkts.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
    }
}
