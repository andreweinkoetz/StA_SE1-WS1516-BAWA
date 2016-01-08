using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    /// <summary>
    /// clsUserFacade: nach außen hin sichtbare Methoden bzgl. User-Verwaltung
    /// das meiste wird direkt an clsUserCollection-Methoden durchgereicht
    /// </summary>
    public class clsUserFacade
    {
        clsUserCollection _usrCol;  // Objektvariable für User-Collection, wird im Konstruktor instantiiert 
        /// <summary>
        /// Konstruktor
        /// </summary>
        public clsUserFacade()
        {
            _usrCol = new clsUserCollection();
        }

        /// <summary>
        /// Alle User lesen
        /// </summary>
        /// <returns>Liste der User</returns>
        public List<clsUser> UsersGetAll()
        {
            return _usrCol.GetAllUsers();
        } // UsersGetAll()

        /// <summary>
        /// Liefert die Kunden sortiert nach Umsatz zurück.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<string, double, int>> GetUsersOrderedByTotalRevenue()
        {
            return _usrCol.GetUsersOrderedByTotalRevenue();
        }

        /// <summary>
        /// UserGetById: Read User with given Id
        /// </summary>
        /// <param name="id">User-Id</param>
        /// <returns>User object or null</returns>
        public clsUser UserGetById(int id)
        {
            return _usrCol.GetUserById(id);
        }

        /// <summary>
        /// Gibt das Passwort des Users zurück.
        /// </summary>
        /// <returns></returns>
        public String getPassword(string _email)
        {
            return _usrCol.GetPasswordOfAUser(_email);
        }

        /// <summary>
        /// Gibt die ID des Users zurück.
        /// </summary>
        /// <param name="_email"></param>
        /// <returns></returns>
        public int GetIDOfUser(string _email)
        {
            return _usrCol.GetIDOfUser(_email);
        }

        /// <summary>
        /// Liefert die Rolle des Users zurück.
        /// </summary>
        /// <param name="_email">E-Mail (Username) des Users</param>
        /// <returns>Rollen-ID</returns>
        public int GetRoleOfUser(string _email)
        {
            return _usrCol.GetRoleOfUser(_email);
        }

        /// <summary>
        /// User Insert
        /// </summary>
        /// <returns>true, wenn Insert erfolgreich</returns>
        public bool UserInsert(clsUser newUser)
        {
            List<clsUser> allUsers = UsersGetAll();

            foreach (clsUser user in allUsers)
            {
                if (user.EMail.Equals(newUser.EMail))
                {
                    return false;
                }
            }

            _usrCol = new clsUserCollection();

            return (_usrCol.InsertUser(newUser) == 1);
      
        } // UserInsert()

        /// <summary>
        /// Update User Object
        /// </summary>
        /// <param name="updUser">User-Objekt mit upzudatenenden Attributen</param>
        /// <returns>true if successful</returns>
        public bool UserUpdate(clsUser updUser)
        {
            return updUser.Update();

        } // UserUpdate()

        /// <summary>
        /// Kennwort eines Benutzers ändern.
        /// </summary>
        /// <param name="_userID">Benutzer dessen Passwort geändert wird.</param>
        /// <param name="_password">Neues Passwort</param>
        /// <returns>true wenn Änderung erfolgreich.</returns>
        public bool ChangeUserPassword(int _userID, String _password)
        {
            return _usrCol.ChangeUserPassword(_userID, _password) == 1;
        }

        /// <summary>
        /// Delete User Object
        /// </summary>
        /// <param name="delUser">zu löschendes User-Objekt</param>
        /// <returns>true if successful</returns>
        public bool UserDelete(clsUser delUser)
        {
            return delUser.Delete();
        } // UserDelete()

        /// <summary>
        /// Löschen eines Users anhand seiner Id.
        /// </summary>
        /// <param name="_userId">Id des zu löschenden Users</param>
        /// <returns>true wenn erfolgreich</returns>
        public bool UserDelete(int _userId)
        {
            return _usrCol.DeleteUser(_userId) == 1;
        }

        /// <summary>
        /// Zählt Anzahl Users, indem erst alle eingelesen werden und dann Länge der Liste zurückgegeben wird
        /// </summary>
        /// <returns>Anzahl Users</returns>
        public int CountUsers()
        {
            int _count;
            List<clsUser> _userList;
            _userList = _usrCol.GetAllUsers();
            _count = _userList.Count;
            return _count;
        } // CountUsers()

        /// <summary>
        /// Zählt Anzahl von aktiven Usern 
        /// </summary>
        /// <returns>Anzahl Users</returns>
        public int CountActiveUsers()
        {
            int _count = 0;
            List<clsUser> _UserList;
            _UserList = _usrCol.GetAllUsers();
            foreach (clsUser u in _UserList)
            {
                if (u.IsActive)
                    _count++;
            }
            return _count;
        } // CountUsers()

        /// <summary>
        /// Berechnet Distanz von Pizza-Shop zum Benutzer.
        /// </summary>
        /// <param name="_id">UserId</param>
        /// <returns>Strecke in km</returns>
        public double GetDistanceByUser(int _id)
        {
            return _usrCol.GetDistanceByUser(_id);
        }

        /// <summary>
        /// Gibt zurück ob User sich anmelden kann.
        /// </summary>
        /// <param name="_id">User-Id</param>
        /// <returns>true wenn User sich anmelden darf</returns>
        public bool GetUserActive(int _id)
        {
            return new clsUserCollection().GetUserActive(_id);
        }

        /// <summary>
        /// Liefert alle Bestellungen eines bestimmten Benutzers.
        /// </summary>
        /// <param name="_userId">ID des Benutzers</param>
        /// <returns>Liste aller Bestellnummern</returns>
        public List<Int32> GetOrdersFromUserById(int _userId)
        {
            return _usrCol.GetOrdersFromUserById(_userId);
        }

    } // clsUserFacade

    
}
