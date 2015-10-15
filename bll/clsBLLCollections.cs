using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace bll
{

    /// <summary>
    /// Abstrakte Oberklasse für die Collection-Klassen
    /// enthält einige viel verwendete Mewthoden
    /// </summary>
    public abstract class clsBLLCollections
    {
        /// <summary>
        /// AddStringFieldValue(): String aus DataRow-Field, mit Handling von DBNull
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="fieldName">feldname in DataRow</param>
        /// <returns>String</returns>
        public static string AddStringFieldValue(DataRow row, string fieldName)
        {
            if ((row[fieldName]) != DBNull.Value)
                return (string)row[fieldName];
            else
                return "";
        }

        /// <summary>
        /// AddIntFieldValue(): Int aus DataRow-Field auslesen, mit Handling von DBNull
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="fieldName">feldname</param>
        /// <returns>int</returns>
        public static int AddIntFieldValue(DataRow row, string fieldName)
        {
            if ((row[fieldName]) != DBNull.Value)
                return Convert.ToInt32(row[fieldName]);
            else
                return Int32.MinValue;
        }

        /// <summary>
        /// AddDoubleFieldValue(): Double aus DataRow-Field, mit Handling von DBNull
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="fieldName">feldname</param>
        /// <returns>double</returns>
        public static double AddDoubleFieldValue(DataRow row, string fieldName)
        {
            if ((row[fieldName]) != DBNull.Value)
                return Convert.ToDouble(row[fieldName]);
            else
                return Double.MinValue;
        }

        /// <summary>
        /// AddBoolFieldValue(): Bool aus DataRow-Field, mit Handling von DBNull
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="fieldName">feldname</param>
        /// <returns>bool</returns>
        public static Boolean AddBoolFieldValue(DataRow row, string fieldName)
        {
            if ((row[fieldName]) != DBNull.Value)
                return (Boolean)row[fieldName];
            else
                return false;
        }

        /// <summary>
        /// AddDateTimeFieldValue(): DateTime aus DataRow-Field, mit Handling von DBNull
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="fieldName">feldname</param>
        /// <returns>DateTime</returns>
        public static DateTime AddDateTimeFieldValue(DataRow row, string fieldName)
        {
            if ((row[fieldName]) != DBNull.Value)
                return Convert.ToDateTime(row[fieldName]);
            else
                return DateTime.MinValue;
        }
    } //clsBLLCollections
}
