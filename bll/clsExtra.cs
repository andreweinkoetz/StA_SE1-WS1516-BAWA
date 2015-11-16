using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Klasse für Extras.
    /// </summary>
    public class clsExtra
    {
        private int _id;

        /// <summary>
        /// Extra-ID wird von DB-vergeben.
        /// </summary>
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        private String _name;

        /// <summary>
        /// Sprechender Bezeichner des Extras.
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

        private double _price;
        /// <summary>
        /// Preis für das Extra.
        /// </summary>
        public double Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
            }
        }

        public clsExtra(int _id, String _name, double _price)
        {
            this._id = _id;
            this._name = _name;
            this._price = _price;
        }

        public clsExtra()
        {
            this._id = 0;
            this._name = "";
            this._price = 0;
        }
    }
}
