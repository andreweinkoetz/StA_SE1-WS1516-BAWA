using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Die GUI kann Methoden aus dieser Klasse nutzen.
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
        /// Alle Produkte lesen
        /// </summary>
        public List<clsProduct> ProductsGetAll()
        {
            return _productCol.getAllProducts();
        } //ProductsGetAll()
    } // clsProductFacade
}
