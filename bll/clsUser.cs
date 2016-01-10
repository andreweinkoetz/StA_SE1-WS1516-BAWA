using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace bll
{
    /// <summary>
    /// clsUser: Klasse für Benutzer von Pizza
    /// nur Attribute, keine Methoden
    /// </summary>
    public class clsUser
    {

        /// <summary>
        /// Constructor (mit default-Werten)
        /// </summary>
        public clsUser()
        {
            this._id = 0;
            this._title = "No Title";
            this._name = "No Name";
            this._prename = "No Prename";
            this._street = "No Street";
            this._nr = 0;
            this._postcode = 0;
            this._place = "No Place";
            this._phone = "No Phone";
            this._isActive = false;
            this._role = 0;
            this._email = "No E-Mail";
            this._password = "";
        }

        private int _id;
        // properties
        /// <summary>
        /// ID des User, von DB vergeben, eindeutig, readonly
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _title;
        /// <summary>
        /// Anrede des Nutzers
        /// </summary>
        public String Title
        {
            get { return _title; }
            set
            {
                if ((value != null) && (value != ""))
                    _title = value;
                else
                    _title = "No Title";
            }
        }

        private string _name;
        /// <summary>
        /// Name des Nutzers
        /// </summary>
        public String Name
        {
            get { return _name; }
            set
            {
                if ((value != null) && (value != ""))
                    _name = value;
                else
                    _name = "No Name";
            }
        }

        private string _prename;
        /// <summary>
        /// Vorname des Nutzers
        /// </summary>
        public String Prename
        {
            get { return _prename; }
            set
            {
                if ((value != null) && (value != ""))
                    _prename = value;
                else
                    _prename = "No Name";
            }
        }

        private string _street;
        /// <summary>
        /// Adresse des Nutzers
        /// </summary>
        public String Street
        {
            get { return _street; }
            set
            {
                if ((value != null) && (value != ""))
                    _street = value;
                else
                    _street = "No Street";
            }
        }

        private int _nr;
        /// <summary>
        /// Adresse des Nutzers
        /// </summary>
        public int Nr
        {
            get { return _nr; }
            set
            {
                if (value < 0)
                    _nr = 0;
                else
                    _nr = value;
            }
        }

        private int _postcode;
        /// <summary>
        /// Postleitzahl des Users
        /// </summary>
        public int Postcode
        {
            get { return _postcode; }
            set
            {
                if (value < 0)
                    _postcode = 0;
                else
                    _postcode = value;
            }
        }

        private string _place;
        /// <summary>
        /// Adresse des Nutzers
        /// </summary>
        public String Place
        {
            get { return _place; }
            set
            {
                if ((value != null) && (value != ""))
                    _place = value;
                else
                    _place = "No Place";
            }
        }

        private string _phone;
        /// <summary>
        /// Telefonnummer des Users
        /// </summary>
        public String Phone
        {
            get { return _phone; }
            set
            {
                if ((value != null) && (value != ""))
                    _phone = value;
                else
                    _phone = "No Phone";
            }
        }

        private int _role;
        /// <summary>
        /// 3: Kunde; 2: Service (z.B. Pizzabäcker); 1: Manager
        /// </summary>
        public int Role
        {
            get { return _role; }
            set { _role = value; }
        }

        private bool _isActive;
        /// <summary>
        /// True if Active (User may log in)
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private string _email;
        /// <summary>
        /// E-Mail Adresse des Users
        /// </summary>
        public string EMail
        {
            get { return _email; }
            set
            {
                if (value == null) _email = null;
                else _email = value;
            }
        }

        private string _password;
        /// <summary>
        /// Passwort (optional)
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (value == null) _password = null;
                else _password = value;
            }
        }

        /// <summary>
        /// Erstellt verschlüsseltes Passwort.
        /// </summary>
        /// <param name="md5Hash">HashWert (md5)</param>
        /// <param name="password">Klartext-Passwort</param>
        /// <returns>Verschlüsseltes Passwort</returns>
        public static string CreateMD5Hash(MD5 md5Hash, string password)
        {
            // Converts the password to a byte array and computes the MD5 hash
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sBuilder = new StringBuilder();

            // Formats each byte into a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// Einfügen dieses Users in die Datenbank
        /// </summary>
        /// <returns>true if successful</returns>
        public bool Insert()
        {
            clsUserCollection _usrCol = new clsUserCollection();
            return (_usrCol.InsertUser(this) == 1);
        } // Insert()

        /// <summary>
        /// Update des Users
        /// </summary>
        /// <returns>true if successful</returns>
        public bool Update()
        {
            clsUserCollection _usrCol = new clsUserCollection();
            return (_usrCol.UpdateUser(this) == 1);
        } // Update()

        /// <summary>
        /// Lösche den Benutzer
        /// </summary>
        /// <returns>true if successful</returns>
        public bool Delete()
        {
            clsUserCollection _usrCol = new clsUserCollection();
            return (_usrCol.DeleteUser(this) == 1);
        } // Update()
    } // clsUser
}
