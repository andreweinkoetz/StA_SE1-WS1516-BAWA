using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace bll
{
    /// <summary>
    /// clsUserCollection: Zugriff auf DAL, Abbildung auf clsUser- und List-clsUser-Objekten
    /// nur in bll sichtbar
    /// </summary>
    internal class clsUserCollection : clsBLLCollections
    {
        string _databaseFile;   // String zur Access-Datei, wird im Konstruktor initialisiert
        DAL.DALObjects.dDataProvider _myDAL;   // DAL Objekt für den Zugriff auf die Datenbank

        /// <summary>
        /// User-Collection Konstruktor 
        /// stellt Verbindung zur Datenbank her
        /// </summary>
        public clsUserCollection()
        {
            // hier wird der Pfad zur Access-Datei aus web.config gelesen und eine DAL-Instanz erzeugt, die den Zugriff auf die DB beitet
            _databaseFile = System.Configuration.ConfigurationManager.AppSettings["AccessFileName"];
            _myDAL = DAL.DataFactory.GetAccessDBProvider(_databaseFile);
        }

        /// <summary>
        /// Liest alle User aus der DB und gibt sie als Liste zurück
        /// </summary>
        /// <returns></returns>
        public List<clsUser> GetAllUsers()
        {
            //Hier wird unser Dataset aus der DB mit allen Usern befüllt, 
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QUGetAllUsers");

            //das DataSet enthält nur eine DataTable
            DataTable _myDataTable = _myDataSet.Tables[0];

            //Instantiieren eine Liste von User-Objekten
            List<clsUser> _myUserList = new List<clsUser>();

            //Lesen wir jetzt Zeile (DataRow) für Zeile
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                //Wir füllen unsere Liste nach und nach mit neuen Usern
                clsUser _myUser = new clsUser();
                _myUser = DatarowToClsUser(_dr);
                _myUserList.Add(DatarowToClsUser(_dr));
            }
            return _myUserList;
        } //getAllUsers() 

        /// <summary>
        /// Update eines Userobjekts
        /// </summary>
        /// <param name="_user">User-Objekt mit geänderten Attributen</param>
        /// <returns>1 falls Update erfolgreich </returns>
        internal int UpdateUser(clsUser _user)
        {
            bool changePassword = !String.IsNullOrEmpty(_user.Password);

            // Übergabeparameter hinzufügen 
            // (Parameter in derselben Reihenfolge wie in der Access-Query)
            _myDAL.AddParam("Title", _user.Title, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Name", _user.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Prename", _user.Prename, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Street", _user.Street, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Nr", _user.Nr, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("Postcode", _user.Postcode, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("Place", _user.Place, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Phone", _user.Phone, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("IsActive", _user.IsActive, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myDAL.AddParam("Role", _user.Role, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("EMail", _user.EMail, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            if (changePassword)
            {
                _myDAL.AddParam("Password", _user.Password, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            }
            _myDAL.AddParam("ID", _user.ID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int _changedSets;
            if (changePassword)
            {
                _changedSets = _myDAL.MakeStoredProcedureAction("QUUpdateUserWithPasswordById");
            }
            else
            {
                _changedSets = _myDAL.MakeStoredProcedureAction("QUUpdateUserById");
            }

            return _changedSets;
        } //updateUser()

        internal int ChangeUserPassword(int _userID, String _password)
        {
            _myDAL.AddParam("UPassword", _password, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("UserId", _userID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            int _changedSets = _myDAL.MakeStoredProcedureAction("QUChangeUserPasswordById");

            return _changedSets;
        }

        /// <summary>
        /// Löschen eines Userobjekts
        /// </summary>
        /// <param name="_User">User-Objekt mit geänderten Attributen</param>
        /// <returns>1 falls delete erfolgreich </returns>
        public int DeleteUser(clsUser _User)
        {

            // Übergabeparameter ID hinzufügen 
            _myDAL.AddParam("ID", _User.ID, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Ausführen
            int _changedSets = _myDAL.MakeStoredProcedureAction("QUDeleteUserById");

            return _changedSets;
        } //deleteUser()

        /// <summary>
        /// Löschen eines Userobjekts
        /// </summary>
        /// <param name="_userId">User-Id des zu löschenden Users</param>
        /// <returns>1 falls delete erfolgreich </returns>
        public int DeleteUser(int _userId)
        {

            // Übergabeparameter ID hinzufügen 
            _myDAL.AddParam("ID", _userId, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //Ausführen
            int _changedSets = _myDAL.MakeStoredProcedureAction("QUDeleteUserById");

            return _changedSets;
        } //deleteUser()

        /// <summary>
        /// Insert eines Userobjekts
        /// </summary>
        /// <param name="_User">User-Objekt</param>
        /// <returns>1 falls Insert erfolgreich </returns>
        public int InsertUser(clsUser _User)
        {

            // die Übergabeparameter hinzufügen 
            // (Parameter in derselben Reihenfolge wie in der Access-Query)
            _myDAL.AddParam("Title", _User.Title, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Name", _User.Name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Prename", _User.Prename, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Street", _User.Street, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Nr", _User.Nr, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("Postcode", _User.Postcode, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("Place", _User.Place, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Phone", _User.Phone, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("IsActive", _User.IsActive, DAL.DataDefinition.enumerators.SQLDataType.BOOL);
            _myDAL.AddParam("Role", _User.Role, DAL.DataDefinition.enumerators.SQLDataType.INT);
            _myDAL.AddParam("EMail", _User.EMail, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            _myDAL.AddParam("Password", _User.Password, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);

            //Ausführen und veränderte Zeilen zurückgeben
            int _changedSets = _myDAL.MakeStoredProcedureAction("QUInsertUser");

            return _changedSets;
        } //insertUser()

        /// <summary>
        /// Gibt User mit gegebener ID zurück
        /// </summary>
        /// <param name="_id">ID des gesuchten Users</param>
        /// <returns>User-Objekt (oder NULL) </returns>
        public clsUser GetUserById(int _id)
        {
            //Jetzt müssen wir erstmal den Übergabeparameter hinzufügen
            _myDAL.AddParam("ID", _id, DAL.DataDefinition.enumerators.SQLDataType.INT);

            //QUGetUserId in DB aufrufen
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QUGetUserById");

            //es sollte eine Zeile (Datarow) zurückkommen
            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                return DatarowToClsUser(_dr);
            }
            else
            {
                return null;
            }
        }

        internal int GetIDOfUser(string _email)
        {
            _myDAL.AddParam("Email", _email, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QUGetIDOfUser");

            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                return AddIntFieldValue(_dr, "UID");
            }
            return -1;
        }

        internal int GetRoleOfUser(string _email)
        {
            _myDAL.AddParam("Email", _email, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QUGetRoleOfUser");

            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                return AddIntFieldValue(_dr, "URole");
            }
            return -1;
        }

        /// <summary>
        /// Gibt zurück ob User sich anmelden kann.
        /// </summary>
        /// <param name="_id">User-Id</param>
        /// <returns>true wenn User sich anmelden darf</returns>
        internal bool GetUserActive(int _id)
        {
            bool isActive = false;
            _myDAL.AddParam("ID", _id, DAL.DataDefinition.enumerators.SQLDataType.INT);
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QUGetUserActive");
            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                isActive = AddBoolFieldValue(_dr, "UIsActive");
            }
            return isActive;
        }

        internal string GetPasswordOfAUser(string _name)
        {

            //Jetzt müssen wir erstmal den Übergabeparameter hinzufügen
            _myDAL.AddParam("EMail", _name, DAL.DataDefinition.enumerators.SQLDataType.VARCHAR);

            //QUGetPasswordOfUser in DB aufrufen
            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QUGetPasswordOfUser");

            //es sollte eine Zeile (Datarow) zurückkommen
            if (_myDataSet.Tables[0].Rows.Count != 0)
            {
                DataRow _dr = _myDataSet.Tables[0].Rows[0];
                clsUser _myUser = new clsUser();
                _myUser.Password = AddStringFieldValue(_dr, "UPassword");
                return _myUser.Password;
            }
            else
            {
                return null;
            }

        }

        internal double GetDistanceByUser(int _id)
        {
            clsUser _myUser = GetUserById(_id);
            XDocument doc = XDocument.Load(String.Format(@"http://maps.google.com/maps/api/directions/xml?origin=Lothstraße 64, 80335 Muenchen&destination={0} {1},{2} {3}&sensor=false", _myUser.Street, _myUser.Nr, _myUser.Postcode, _myUser.Place));
            XElement dirResp = doc.Element(XName.Get("DirectionsResponse"));
            XElement eStatus = dirResp.Element(XName.Get("status"));
            if (eStatus.Value == "OK")
            {
                XElement eRoute = dirResp.Element(XName.Get("route"));
                XElement eLeg = eRoute.Element(XName.Get("leg"));
                XElement eDistance = eLeg.Element(XName.Get("distance"));
                return Convert.ToDouble(eDistance.Element(XName.Get("value")).Value) / 1000;
            }
            else
            {
                return -1;
            }

        }

        internal List<Int32> GetOrdersFromUserById(int _userId)
        {
            _myDAL.AddParam("UserId", _userId, DAL.DataDefinition.enumerators.SQLDataType.INT);

            List<Int32> _userOrders = new List<Int32>();

            DataSet _myDataSet = _myDAL.GetStoredProcedureDSResult("QUGetOrdersFromUserById");

            DataTable _myDataTable = _myDataSet.Tables[0];
            foreach (DataRow _dr in _myDataTable.Rows)
            {
                _userOrders.Add(AddIntFieldValue(_dr, "ONumber"));
            }

            return _userOrders;
        }

        /// <summary>
        /// DatarowToClsUser(): Transforms a DataRow into a User Object
        /// </summary>
        /// <param name="_dr">DataRow</param>
        /// <returns>User-Objekt</returns>
        private clsUser DatarowToClsUser(DataRow _dr)
        {

            clsUser _myUser = new clsUser();
            //und hier die Daten nach Index
            _myUser.ID = (int)_dr["UID"];
            _myUser.Title = AddStringFieldValue(_dr, "UTitle");
            _myUser.Name = AddStringFieldValue(_dr, "UName");
            _myUser.Prename = AddStringFieldValue(_dr, "UPrename");
            _myUser.Street = AddStringFieldValue(_dr, "UStreet");
            _myUser.Nr = AddIntFieldValue(_dr, "UNr");
            _myUser.Postcode = AddIntFieldValue(_dr, "UPostcode");
            _myUser.Place = AddStringFieldValue(_dr, "UPlace");
            _myUser.Phone = AddStringFieldValue(_dr, "UPhone");
            _myUser.IsActive = AddBoolFieldValue(_dr, "UIsActive");
            _myUser.Role = AddIntFieldValue(_dr, "URole");
            _myUser.EMail = AddStringFieldValue(_dr, "UEmail");
            _myUser.Password = AddStringFieldValue(_dr, "UPassword");
            return _myUser;
        } //DatarowToClsUser()


    }
}
