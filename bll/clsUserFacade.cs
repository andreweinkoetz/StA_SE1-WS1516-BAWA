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
            return _usrCol.getPasswordOfAUser(_email);
        }

        /// <summary>
        /// Gibt die ID des Users zurück.
        /// </summary>
        /// <param name="_email"></param>
        /// <returns></returns>
        public int getIDOfUser(string _email)
        {
            return _usrCol.getIDOfUser(_email);
        }

        public int getRoleOfUser(string _email)
        {
            return _usrCol.getRoleOfUser(_email);
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
        /// Delete User Object
        /// </summary>
        /// <param name="delUser">zu löschendes User-Objekt</param>
        /// <returns>true if successful</returns>
        public bool UserDelete(clsUser delUser)
        {
            // hier müsste noch gecheckt werden, ob der User gelöscht werden darf!? z.B. wenn er schon bestellt hat!?

            //if (_usrCol.InsertUser(delUser) == 1)
            //    return true;
            //else
            //    return false;
            return delUser.Delete();
        } // UserDelete()

        /// <summary>
        /// Zählt Anzahl Users, indem erst alle eingelesen werden und dann Länge der Liste zurückgegeben wird
        /// </summary>
        /// <returns>Anzahl Users</returns>
        public int CountUsers()
        {
            int _count;
            List<clsUser> _UserList;
            _UserList = _usrCol.GetAllUsers();
            _count = _UserList.Count;
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
    } // clsUserFacade
}
