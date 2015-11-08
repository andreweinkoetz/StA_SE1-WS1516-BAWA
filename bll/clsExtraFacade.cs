using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// 
    /// </summary>
    public class clsExtraFacade
    {

        clsExtraCollection _extraCol; //Objektvariable für Extra-Collection,
        /// <summary>
        /// Konstruktor
        /// </summary>
        public clsExtraFacade()
        {   // instantiierung _extraCol
            _extraCol = new clsExtraCollection();
        }

        /// <summary>
        /// Alle Extras lesen.
        /// </summary>
        public List<clsExtra> ExtrasGetAll()
        {
            return _extraCol.getAllExtras();
        } //ExtrasGetAll()
    }
}
