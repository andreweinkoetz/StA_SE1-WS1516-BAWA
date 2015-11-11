using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class clsSize
    {
        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
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
