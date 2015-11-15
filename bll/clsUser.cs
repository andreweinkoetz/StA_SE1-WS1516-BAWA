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
            this._name = "No Name";
            this._prename = "No Name";
            this._street = "No Street";
            this._nr = 0;
            this._place = "No Place";
            this._distance = 0;
            this._isActive = false;
            this._role = 0;
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

        private int _distance;
        /// <summary>
        /// Distanz: Entfernung zum Pizza-Shop
        /// </summary>
        public int Distance
        {
            get { return _distance; }
            set
            {
                if (value < 0)
                    _distance = 0;
                else
                    _distance = value;
            }
        }

        private int _role;
        /// <summary>
        /// 0: Kunde; 1: Manager; 2: Service (z.B. Pizzabäcker)
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
