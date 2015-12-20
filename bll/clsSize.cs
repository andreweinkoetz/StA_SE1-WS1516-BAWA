using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Repräsentiert eine Größe, zugehörig zu einer Kategorie.
    /// </summary>
    public class clsSize
    {

        private int _id;

        /// <summary>
        /// Id einer Größe.
        /// </summary>
        public int Id
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

        private double _value;
        /// <summary>
        /// Wert einer Größe.
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
        /// Bezeichnung der Größe.
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

        private int _cid;

        /// <summary>
        /// Zur Größe zugehörige Kategorie-ID.
        /// </summary>
        public int CID
        {
            get
            {
                return _cid;
            }

            set
            {
                _cid = value;
            }
        }

        private String _category;

        /// <summary>
        /// Zur Größe zugehörige Kategorie.
        /// </summary>
        public string Category
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

        /// <summary>
        /// Standardkonstruktor für neue Größe.
        /// </summary>
        public clsSize()
        {
            this._cid = 0;
            this._name = "";
            this._value = 0.0;
        }

    }
}
