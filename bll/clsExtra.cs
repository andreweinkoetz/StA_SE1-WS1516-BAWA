using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die Eigenschaften eines Extras dar.
    /// Extras können optional bei der Bestellung von Pizzen gewählt werden.
    /// </summary>
    public class clsExtra
    {
        private int _id;
        /// <summary>
        /// ID des Extras (wird von der Datenbank vergeben).
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
        /// Bezeichnung des Extras.
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
        /// Preis des Extras.
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

        private bool _toSell;
        /// <summary>
        /// Zeigt, ob das Extra angeboten wird.
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
        /// Custom-Konstruktor für ein neues Extra.
        /// </summary>
        /// <param name="_id">ID des Extras</param>
        /// <param name="_name">Name des Extras</param>
        /// <param name="_price">Preis des Extras</param>
        public clsExtra(int _id, String _name, double _price)
        {
            this._id = _id;
            this._name = _name;
            this._price = _price;
            this._toSell = true;
        }

        /// <summary>
        /// Standard-Konstruktor für ein neues Extra-Objekt.
        /// </summary>
        public clsExtra() : this(0, "", 0)
        {
            this._toSell = false;
        }

        /// <summary>
        /// Kopierkonstruktor für ein Extra.
        /// </summary>
        /// <param name="_extraToCopy">das zu kopierende Extra</param>
        public clsExtra(clsExtra _extraToCopy)
        {
            this.ID = _extraToCopy._id;
            this._name = _extraToCopy._name;
            this._price = _extraToCopy._price;
            this._toSell = _extraToCopy._toSell;
        }

        /// <summary>
        /// Erstellt eine Liste von möglichen Extras für die Pizzen.
        /// </summary>
        /// <param name="_idOfExtras">IDs der gewählten Extras</param>
        /// <returns>Liste der Extras für die Pizzen</returns>
        public static List<clsExtra> ExtraListFactory(params int[] _idOfExtras)
        {
            List<clsExtra> _myExtraList = new List<clsExtra>();
            foreach (int _id in _idOfExtras)
            {
                clsExtraFacade _myExtraFacade = new clsExtraFacade();
                _myExtraList.Add(_myExtraFacade.GetExtraById(_id));
            }
            return _myExtraList;
        }
    }
}
