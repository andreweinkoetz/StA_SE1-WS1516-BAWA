using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die nach außen sichtbaren Methoden bzgl. der Extra-Verwaltung bereit.
    /// Als Grundlage werden die clsExtraCollection-Methoden verwendet.
    /// </summary>
    public class clsExtraFacade
    {
        clsExtraCollection _extraCol; // Objektvariable für Extra-Collection, wird im Konstruktor instantiiert 
        /// <summary>
        /// Konstruktor
        /// TODO: hier weiter!
        /// </summary>
        public clsExtraFacade()
        {   // instantiierung _extraCol
            _extraCol = new clsExtraCollection();
        }

        /// <summary>
        /// Alle Extras lesen.
        /// </summary>
        public List<clsExtra> GetAllExtras()
        {
            return _extraCol.GetAllExtras();
        } //ExtrasGetAll()

        /// <summary>
        /// Alle aktiven Extras lesen.
        /// </summary>
        /// <returns></returns>
        public List<clsExtra> GetAllActiveExtras()
        {
            return _extraCol.GetAllActiveExtras();
        }

        /// <summary>
        /// Preis eines bestimmten Extras ermitteln.
        /// </summary>
        /// <param name="_eID"></param>
        /// <returns></returns>
        public double GetPriceOfExtra(int _eID)
        {
            return _extraCol.GetPriceOfExtra(_eID);
        }

        /// <summary>
        /// Bestimmes Extra ermitteln.
        /// </summary>
        /// <param name="_eID">ID des Extras</param>
        /// <returns>clsExtra</returns>
        public clsExtra GetExtraById(int _eID)
        {
            return _extraCol.GetExtraById(_eID);
        }

        /// <summary>
        /// Ermittelt Bestellnummern eines bestimmten Extras.
        /// </summary>
        /// <param name="_eID">ID des Extras</param>
        /// <returns>Liste von Bestellnummern</returns>
        public List<Int32> GetOrdersOfExtrasByEID(int _eID)
        {
            return _extraCol.GetOrdersOfExtrasByEID(_eID);
        }

        /// <summary>
        /// Fügt ein neues Extra ein.
        /// </summary>
        /// <param name="_myExtra">einzufügendes Extra</param>
        /// <returns>true wenn Einfügen erfolgreich.</returns>
        public bool InsertExtra(clsExtra _myExtra)
        {
            return _extraCol.InsertExtra(_myExtra) == 1;
        }

        /// <summary>
        /// Ändert ein bestehendes Extra in der DB.
        /// </summary>
        /// <param name="_myExtra">geändertes Extra</param>
        /// <returns>true wenn Update erfolgreich</returns>
        public bool UpdateExtra(clsExtra _myExtra)
        {
            return _extraCol.UpdateExtra(_myExtra) == 1;
        }

        /// <summary>
        /// Löscht ein Extra aus der DB.
        /// </summary>
        /// <param name="_eID">Id des zu löschenden Extras.</param>
        /// <returns>true wenn Löschung erfolgreich.</returns>
        public bool DeleteExtraByID(int _eID)
        {
            return _extraCol.DeleteExtraByID(_eID) == 1;
        }
    }
}
