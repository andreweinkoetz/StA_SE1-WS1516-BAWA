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

        private bool _toSell;

        /// <summary>
        /// Zeigt ob Extra angeboten wird.
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
        /// Konstruktor für neues Extra.
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
        /// Standard-Konstruktor für neues Extra-Objekt.
        /// </summary>
        public clsExtra()
        {
            this._id = 0;
            this._name = "";
            this._price = 0;
            this._toSell = false;
        }

        /// <summary>
        /// Statische Methode zur Erstellung einer Liste von Extras für Pizzen.
        /// </summary>
        /// <param name="_idOfExtras">Ids gewählter Extras.</param>
        /// <returns>Liste von Extras für Pizzen.</returns>
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
