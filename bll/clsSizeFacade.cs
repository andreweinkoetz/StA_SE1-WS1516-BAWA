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
    public class clsSizeFacade
    {
        clsSizeCollection _sizeCol;

        /// <summary>
        /// 
        /// </summary>
        public clsSizeFacade()
        {
            this._sizeCol = new clsSizeCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_category"></param>
        /// <returns></returns>
        public List<clsSize> getSizesByCategory(int _category)
        {
            return _sizeCol.getSizesByCategory(_category);
        }
    }
}
