using System;
using System.Collections.Generic;
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



    } // clsProductFacade

}
