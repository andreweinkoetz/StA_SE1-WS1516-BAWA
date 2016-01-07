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
            return _productCol.getAllProducts();
        } //ProductsGetAll()

        /// <summary>
        /// Zeigt die Beliebtheit der verschiedenen Produkte an.
        /// </summary>
        /// <returns></returns>
        public OrderedDictionary getMostFanciedProduct()
        {
            return _productCol.getMostFanciedProduct();
        }

        /// <summary>
        /// Liefert den Preis pro Unit eines Produkts zurück.
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public double getProductPricePerUnit(String _name)
        {
            return _productCol.getProductPricePerUnit(_name);
        }

        /// <summary>
        /// Liefert die Kategorie eines Produkts zurück.
        /// </summary>
        /// <param name="_category"></param>
        /// <returns></returns>
        public String getCategory(int _category)
        {
            return _productCol.getCategory(_category);
        }


        /// <summary>
        /// Alle Produkte einer Kategorie lesen.
        /// </summary>
        /// <param name="_category"></param>
        /// <returns></returns>
        public List<clsProduct> ProductsGetAllByCategory(int _category)
        {
            return _productCol.getAllProductsByCategory(_category);
        }

        /// <summary>
        /// Produkt mittels ID finden.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public clsProduct GetProductByID(int ID)
        {
            return _productCol.GetProductById(ID);
        }

        /// <summary>
        /// Produkt-Liste erstellen.
        /// </summary>
        /// <param name="_Products"></param>
        /// <returns></returns>
        public List<clsProduct> createListofProducts(params clsProduct[] _Products)
        {
            return _productCol.createListofProducts(_Products);
        }

        /// <summary>
        /// Einzelnes Produkt in DB aktualisieren.
        /// </summary>
        /// <param name="_product"></param>
        /// <returns></returns>
        public bool UpdateProduct(clsProductExtended _product)
        {
            return _productCol.UpdateProduct(_product);
        }

    } // clsProductFacade

}
