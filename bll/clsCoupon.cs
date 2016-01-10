using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// Stellt die Eigenschaften eines Gutscheins dar.
    /// Gutscheine können von einem Kunden eingelöst werden.
    /// </summary>
    public class clsCoupon
    {
        private int _id;
        /// <summary>
        /// ID des Gutscheins.
        /// </summary>
        public int Id
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

        private int _discount;
        /// <summary>
        /// Prozentualer Rabattwert des Gutscheins.
        /// </summary>
        public int Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
            }
        }

        private String _code;
        /// <summary>
        /// Code, der zum Einlösen des Gutscheins benötigt wird.
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
            }
        }

        private bool _isValid;
        /// <summary>
        /// Zeigt an, ob der Gutschein aktiv bzw. einlösbar ist.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }

            set
            {
                _isValid = value;
            }
        }

        private int _uid;
        /// <summary>
        /// User-ID des Benutzers, der den Gutschein besitzt.
        /// </summary>
        public int Uid
        {
            get
            {
                return _uid;
            }

            set
            {
                _uid = value;
            }
        }

        private String _userName;
        /// <summary>
        /// Name des Besitzers des Gutscheins.
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }

        /// <summary>
        /// Standard-Konstruktor für ein neues Gutschein-Objekt.
        /// </summary>
        public clsCoupon()
        {
            this._code = "";
            this._discount = 0;
            this._isValid = false;
            this._uid = 0;
            this._userName = "";
        }

        /// <summary>
        /// Erstellt einen per Zufallsgenerator ermittelten Code.
        /// Dieser besteht aus vier Blöcken mit je vier alphanumerischen Zeichen, die durch Bindestriche getrennt sind.
        /// </summary>
        /// <returns>den generierten Code</returns>
        public static String GenerateCode()
        {
            String _code = "";
            Random r = new Random();

            for (int i = 0; i < 16; i++)
            {
                //Bindestrich zwischen den Vierer-Blöcken
                if (i % 4 == 0 && i != 0)
                {
                    _code += '-';
                }

                //Erstellung des Codes 
                if (i % 2 == 0 && i != 0)
                {
                    _code += r.Next(1, 10);
                }
                else
                {
                    _code += (char)r.Next(65, 91);
                }
            }
            return _code;
        }
    }
}
