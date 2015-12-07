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
        public clsProductExtended GetProductByID(int ID)
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

        /// <summary>
        /// Gibt eine Liste aller Kategorien inkl. zug. ID zurück.
        /// </summary>
        /// <returns>Liste der Kategorienamen mit ID</returns>
        public Dictionary<Int32, String> GetAllProductCategories()
        {
            return _productCol.GetAllProductCategories();
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

    } // clsProductFacade

}
