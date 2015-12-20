using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// clsSizeFacade: nach außen hin sichtbare Methoden bzgl. Größen-Verwaltung.
    /// </summary>
    public class clsSizeFacade
    {
        clsSizeCollection _sizeCol;

        /// <summary>
        /// Standardkonstruktor für neue SizeFacade.
        /// </summary>
        public clsSizeFacade()
        {
            this._sizeCol = new clsSizeCollection();
        }

        /// <summary>
        /// Erstellt eine Liste aller Größen einer Kategorie.
        /// </summary>
        /// <param name="_category">Id der Kategorie</param>
        /// <returns>Liste aller Größen einer Kategorie</returns>
        public List<clsSize> getSizesByCategory(int _category)
        {
            return _sizeCol.GetSizesByCategory(_category);
        }

        /// <summary>
        /// Erstellt eine Liste aller Größen.
        /// </summary>
        /// <returns>Liste aller Größen</returns>
        public List<clsSize> GetAllSizes()
        {
            return _sizeCol.GetAllSizes();
        }

        /// <summary>
        /// Liefert eine Größe anhand ihrer Id.
        /// </summary>
        /// <param name="_sid">Id der Größe</param>
        /// <returns>Größe</returns>
        public clsSize GetSizeById(int _sid)
        {
            return _sizeCol.GetSizeById(_sid);
        }

        /// <summary>
        /// Löscht eine angegebene Größe.
        /// </summary>
        /// <param name="_sid">Id der zu löschenden Größe.</param>
        /// <returns>true wenn Löschen erfolgreich.</returns>
        public bool DeleteSizeById(int _sid)
        {
            return _sizeCol.DeleteSizeById(_sid) == 1; 
        }

        /// <summary>
        /// Ändert eine angegebene Größe.
        /// </summary>
        /// <param name="_size">Zu ändernde Größe.</param>
        /// <returns>true wenn Änderung erfolgreich.</returns>
        public bool UpdateSize(clsSize _size)
        {
            return _sizeCol.UpdateSize(_size) == 1;
        }

        /// <summary>
        /// Fügt eine neue Größe in die Datenbank.
        /// </summary>
        /// <param name="_size">einzufügende Größe.</param>
        /// <returns>true wenn Einfügen erfolgreich.</returns>
        public bool InsertSize(clsSize _size)
        {
            return _sizeCol.InsertSize(_size) == 1;
        }
    }
}
