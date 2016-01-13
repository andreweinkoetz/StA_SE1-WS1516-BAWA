using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Nach Außen sichtbare Produktklasse.
    /// </summary>
    public class clsProductFacade
    {
        clsProductCollection _productCol; //Objektvariable für Product-Collection,
        /// <summary>
        /// Konstruktor
        /// </summary>
        public clsProductFacade()
        {   // instatiierung _productCol
            _productCol = new clsProductCollection();
        }

        /// <summary>
        /// Alle Produkte lesen.
        /// </summary>
        public List<clsProduct> ProductsGetAll()
        {
            return _productCol.GetAllProducts();
        } //ProductsGetAll()


        /// <summary>
        /// Liefert alle Produkte einer Kategorie
        /// </summary>
        /// <param name="_category">Kategorie-ID</param>
        /// <returns>Liste aller Produkte</returns>
        public List<clsProduct> ProductsGetAllByCategory(int _category)
        {
            return _productCol.GetAllProductsByCategory(_category);
        }

        /// <summary>
        /// Gibt Produkt mit gegebener ID zurück
        /// </summary>
        /// <param name="_id">ID des gesuchten Produkts</param>
        /// <returns>Produkt-Objekt (oder NULL) </returns>
        public clsProductExtended GetProductByID(int _id)
        {
            return _productCol.GetProductById(_id);
        }

        /// <summary>
        /// Aktualisiert ein Produkt.
        /// </summary>
        /// <param name="_product">Produkt das aktualisiert werden soll.</param>
        /// <returns>true wenn erfolgreich</returns>
        public bool UpdateProduct(clsProductExtended _product)
        {
            return _productCol.UpdateProduct(_product);
        }

        /// <summary>
        /// Gibt eine Liste aller Kategorien inkl. zug. ID zurück.
        /// </summary>
        /// <returns>Liste der Kategorienamen mit ID</returns>
        public Dictionary<Int32, String> GetAllProductCategories()
        {
            return _productCol.GetAllProductCategories();
        }

        /// <summary>
        /// Zeigt die Beliebtheit der verschiedenen Produkte an.
        /// </summary>
        /// <returns>OrderedDictionary (Name, Verkäufe)</returns>
        public OrderedDictionary GetMostFanciestProduct()
        {
            return _productCol.GetMostFanciestProduct();
        }

        /// <summary>
        /// Liefert die Umsätze aller Produkte zurück.
        /// </summary>
        /// <returns>Dictionary (Name, Umsatz)</returns>
        public Dictionary<string, double> GetProductsOrderedByTotalRevenue()
        {
            return _productCol.GetProductsOrderedByTotalRevenue();
        }

        /// <summary>
        /// Einfügen eines neuen Produkts.
        /// </summary>
        /// <param name="_myProduct">einzufügendes Produkt</param>
        /// <returns>true falls einfügen erfolgreich</returns>
        public bool InsertNewProduct(clsProductExtended _myProduct)
        {
            return _productCol.InsertNewProduct(_myProduct) == 1;
        }

        /// <summary>
        /// Erstellt eine Liste aller Bestellnummern, in denen das Produkt enthalten ist.
        /// </summary>
        /// <param name="_pid">Produkt-ID</param>
        /// <returns>Liste aller Bestellnummern.</returns>
        public List<Int32> GetOrdersOfProductByPid(int _pid)
        {
            return _productCol.GetOrdersOfProductByPid(_pid);
        }

        /// <summary>
        /// Löscht Produkt aus Datenbank.
        /// </summary>
        /// <param name="_pid">Id des zu löschenden Produkts</param>
        /// <returns>true wenn Produkt gelöscht</returns>
        public bool DeleteProductByPid(int _pid)
        {
            return _productCol.DeleteProductByPid(_pid) == 1;
        }

        /// <summary>
        /// Liefert zu einem angegebenen Produkt die Kosten aller zug. Extras.
        /// </summary>
        /// <param name="_product">Produkt dessen Extra-Kosten summiert werden soll.</param>
        /// <returns>Kosten für Extras des Produkts.</returns>
        public static double GetCostsOfExtras(clsProductExtended _product)
        {

            double _costsOfExtras = 0;

            if (_product.ProductExtras == null)
            {
                return _costsOfExtras;
            }

            foreach (clsExtra _extra in _product.ProductExtras)
            {
                _costsOfExtras += _extra.Price;
            }

            return _costsOfExtras;

        }

    } // clsProductFacade

}
