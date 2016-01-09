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
        clsExtraCollection _extraCol; // Objektvariable für die Extra-Collection, wird im Konstruktor instantiiert

        /// <summary>
        /// Erstellt ein neues Objekt der clsExtraCollection.
        /// </summary>
        public clsExtraFacade()
        {
            _extraCol = new clsExtraCollection();
        }

        /// <summary>
        /// Liefert alle vorhandenen (d.h. auch inaktive) Extras zurück.
        /// </summary>
        /// <returns>alle vorhandenen Extras</returns>
        public List<clsExtra> GetAllExtras()
        {
            return _extraCol.GetAllExtras();
        }

        /// <summary>
        /// Liefert alle aktiven Extras zurück.
        /// </summary>
        /// <returns>alle aktiven Extras</returns>
        public List<clsExtra> GetAllActiveExtras()
        {
            return _extraCol.GetAllActiveExtras();
        }

        /// <summary>
        /// Liefert den Preis des Extras mit der gewählten ID zurück.
        /// </summary>
        /// <param name="_extraID">ID des Extras</param>
        /// <returns>Preis des Extras</returns>
        public double GetPriceOfExtra(int _extraID)
        {
            return _extraCol.GetPriceOfExtra(_extraID);
        }

        /// <summary>
        /// Liefert das Extra mit der gewählten ID zurück.
        /// </summary>
        /// <param name="_extraID">ID des Extras</param>
        /// <returns>Extra mit der gewählten ID</returns>
        public clsExtra GetExtraById(int _extraID)
        {
            return _extraCol.GetExtraById(_extraID);
        }

        /// <summary>
        /// Ermittelt alle Bestellungen, in denen das Extra verwendet wird.
        /// </summary>
        /// <param name="_extraID">ID des Extras</param>
        /// <returns>alle betroffenen Bestellungen</returns>
        public List<Int32> GetOrdersOfExtrasByEID(int _extraID)
        {
            return _extraCol.GetOrdersOfExtrasByEID(_extraID);
        }

        /// <summary>
        /// Fügt ein neues Extra in die Datenbank ein.
        /// </summary>
        /// <param name="_myExtra">das einzufügende Extra</param>
        /// <returns>true, wenn das Einfügen erfolgreich war</returns>
        public bool InsertExtra(clsExtra _myExtra)
        {
            return _extraCol.InsertExtra(_myExtra) == 1;
        }

        /// <summary>
        /// Ändert ein bestehendes Extra in der Datenbank.
        /// </summary>
        /// <param name="_myExtra">das zu ändernde Extra</param>
        /// <returns>true, wenn die Änderung erfolgreich war</returns>
        public bool UpdateExtra(clsExtra _myExtra)
        {
            return _extraCol.UpdateExtra(_myExtra) == 1;
        }

        /// <summary>
        /// Löscht ein Extra aus der Datenbank.
        /// </summary>
        /// <param name="_extraID">ID des zu löschenden Extras</param>
        /// <returns>true, wenn das Löschen erfolgreich war</returns>
        public bool DeleteExtraByID(int _extraID)
        {
            return _extraCol.DeleteExtraByID(_extraID) == 1;
        }
    }
}
