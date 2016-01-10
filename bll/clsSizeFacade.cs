using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die nach außen sichtbaren Methoden bzgl. der Größen-Verwaltung bereit.
    /// Als Grundlage werden die clsSizeCollection-Methoden verwendet.
    /// </summary>
    public class clsSizeFacade
    {
        clsSizeCollection _sizeCol; // Objektvariable für die Size-Collection, wird im Konstruktor instantiiert

        /// <summary>
        /// Erstellt ein neues Objekt der clsSizeCollection.
        /// </summary>
        public clsSizeFacade()
        {
            this._sizeCol = new clsSizeCollection();
        }

        /// <summary>
        /// Liefert alle Größen einer Kategorie zurück.
        /// </summary>
        /// <param name="_category">ID der Kategorie</param>
        /// <returns>alle zu dieser Kategorie gehörenden Größen</returns>
        public List<clsSize> getSizesByCategory(int _category)
        {
            return _sizeCol.GetSizesByCategory(_category);
        }

        /// <summary>
        /// Liefert alle vorhandenen Größen zurück.
        /// </summary>
        /// <returns>alle vorhandenen Größen</returns>
        public List<clsSize> GetAllSizes()
        {
            return _sizeCol.GetAllSizes();
        }

        /// <summary>
        /// Liefert eine Größe anhand ihrer ID zurück.
        /// </summary>
        /// <param name="_sid">ID der Größe</param>
        /// <returns>die Größe mit der angegebenen ID</returns>
        public clsSize GetSizeById(int _sid)
        {
            return _sizeCol.GetSizeById(_sid);
        }

        /// <summary>
        /// Löscht die Größe mit der angegebenen ID.
        /// </summary>
        /// <param name="_sid">ID der Größe</param>
        /// <returns>true, falls das Löschen erfolgreich ist</returns>
        public bool DeleteSizeById(int _sid)
        {
            return _sizeCol.DeleteSizeById(_sid) == 1;
        }

        /// <summary>
        /// Ändert die angegebene Größe.
        /// </summary>
        /// <param name="_size">die zu ändernde Größe</param>
        /// <returns>true, falls die Änderung erfolgreich ist</returns>
        public bool UpdateSize(clsSize _size)
        {
            return _sizeCol.UpdateSize(_size) == 1;
        }

        /// <summary>
        /// Fügt eine neue Größe in die Datenbank ein.
        /// </summary>
        /// <param name="_size">die einzufügende Größe</param>
        /// <returns>true, falls das Einfügen erfolgreich ist</returns>
        public bool InsertSize(clsSize _size)
        {
            return _sizeCol.InsertSize(_size) == 1;
        }
    }
}
