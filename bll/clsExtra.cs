using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class clsExtra
    {
        private int _id;

        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        private String _name;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        
    }
}
