using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die Eigenschaften einer zu einer Kategorie gehörenden Größe dar.
    /// </summary>
    public class clsSize
    {
        private int _id;
        /// <summary>
        /// ID der Größe.
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
        /// Wert der Größe.
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
        /// Die zur Größe zugehörige Kategorie-ID.
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
        /// Die zur Größe zugehörige Kategorie.
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
        /// Standard-Konstruktor für ein neues Größe-Objekt.
        /// </summary>
        public clsSize()
        {
            this._cid = 0;
            this._name = "";
            this._value = 0.0;
        }
    }
}
