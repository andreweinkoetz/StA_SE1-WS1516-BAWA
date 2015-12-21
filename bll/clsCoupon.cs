using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// clsCoupon: repräsentiert Gutscheine, die von einem Benutzer eingelöst werden können.
    /// </summary>
    public class clsCoupon
    {
        private int _id;
        /// <summary>
        /// Id des Gutscheins.
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
        /// Prozentualer Rabattwert eines Gutscheins.
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
        /// Code der zum Einlösen des Gutscheinwerts benötigt wird.
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
        /// Zeigt an ob Gutschein einlösbar ist.
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
        /// User-ID des Benutzers, der über den Gutschein verfügt.
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
        /// Dieser besteht aus vier Blöcken à vier alphanumerischen Zeichen getrennt durch Bindestriche.
        /// </summary>
        /// <returns>Neuen generierten Code.</returns>
        public static String GenerateCode()
        {
            String _code = "";
            Random r = new Random();

            for (int i = 0; i<16; i++)
            {
                if (i % 4 == 0 && i != 0)
                {
                    _code += '-';
                }

                if (i%2 == 0 && i!=0)
                {
                    _code += r.Next(1, 10);
                } else
                {
                    _code += (char)r.Next(65, 91);
                }
            }

            return _code;

        }

    }
}
