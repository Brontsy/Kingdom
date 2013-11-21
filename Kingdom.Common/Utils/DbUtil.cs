using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Kingdom.Common.Utils
{
    /// <summary>
    /// Helper class to get data from a data reader
    /// </summary>
    public static class DbUtil
    {
        public static void ExecuteNonQuery(string sql, string connString, Action<SqlCommand> addParams)
        {
            using (var conn = new SqlConnection(connString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    addParams(cmd);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ToDbValue(DateTime? dataValue)
        {
            object result = DBNull.Value;
            if (dataValue.HasValue)
            {
                result = dataValue.Value;
            }
            return result;
        }

        public static object ToDbValue(int? dataValue)
        {
            object result = DBNull.Value;
            if (dataValue.HasValue)
            {
                result = dataValue.Value;
            }
            return result;
        }

        public static object ToDbValue(decimal? dataValue)
        {
            object result = DBNull.Value;
            if (dataValue.HasValue)
            {
                result = dataValue.Value;
            }
            return result;
        }

        public static object ToDbValue(string dataValue)
        {
            object result = DBNull.Value;
            if (dataValue != null)
            {
                result = dataValue;
            }
            return result;
        }

        public static object ToDbValue(string dataValue, int maxLength)
        {
            object result = DBNull.Value;
            if (dataValue != null)
            {
                result = dataValue;

                if (dataValue.Length > maxLength)
                {
                    result = dataValue.Substring(0, maxLength);
                }
            }
            return result;
        }

        public static T GetDbValue<T>(IDataReader reader, string fieldName)
        {
            var result = default(T);

            if (reader[fieldName] != null && reader[fieldName] != DBNull.Value)
            {
                try
                {
                    result = (T)Convert.ChangeType(reader[fieldName], typeof(T));
                }
                catch (Exception)
                {
                    result = default(T);
                }
            }

            return result;
        }

        public static T GetDbValue<T>(IDataReader reader, string fieldName, T defaultValue)
        {
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
            {
                throw new ArgumentException("This method does not handle nullable objects. Please use GetValueOrNull<T>.");
            }

            var result = defaultValue;

            if (reader[fieldName] != null && reader[fieldName] != DBNull.Value)
            {
                try
                {
                    result = (T)Convert.ChangeType(reader[fieldName], typeof(T));
                }
                catch (Exception)
                {
                    result = defaultValue;
                }
            }

            return result;
        }

        public static T GetDbEnum<T>(IDataReader reader, string fieldName, T defaultValue)
        {
            var result = defaultValue;

            if (reader[fieldName] != null && reader[fieldName] != DBNull.Value)
            {
                string dbValue = reader[fieldName].ToString();
                if (!string.IsNullOrEmpty(dbValue))
                {
                    try
                    {
                        result = (T)Enum.Parse(typeof(T), dbValue, true);
                    }
                    catch (Exception)
                    {
                        result = defaultValue;
                    }
                }
            }

            return result;
        }

        public static Nullable<T> GetDbEnumOrNull<T>(IDataReader reader, string columnName) where T : struct
        {
            Nullable<T> result = null;


            if (reader[columnName] != DBNull.Value)
            {
                string dbValue = reader[columnName].ToString();

                if (!string.IsNullOrEmpty(dbValue))
                {
                    try
                    {
                        result = (T)Enum.Parse(typeof(T), dbValue, true);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            return result;
        }

        public static Nullable<T> GetValueOrNull<T>(IDataReader reader, string columnName) where T : struct
        {
            Nullable<T> result = null;

            object columnValue = reader[columnName];

            if (columnValue != DBNull.Value)
            {
                try
                {
                    var resultValue = (T)Convert.ChangeType(columnValue, typeof(T));
                    result = new Nullable<T>(resultValue);
                }
                catch (Exception)
                {
                    //Oh look an exception... *stern look*... get in the sack exception... 
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a string from the data reader or returns the default value specified in the params
        /// </summary>
        /// <param name="reader">A data reader</param>
        /// <param name="fieldName">The name of the field</param>
        /// <param name="defaultValue">The default value to use if the value is null</param>
        /// <returns></returns>
        public static string GetDbValue(IDataReader reader, string fieldName, string defaultValue)
        {
            string result = defaultValue;
            if (reader[fieldName] != null && reader[fieldName] != DBNull.Value)
            {
                result = reader[fieldName].ToString();
            }
            return result;
        }

        public static int GetDbValue(IDataReader reader, string fieldName, int defaultValue)
        {
            int result = defaultValue;
            int temp = 0;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (int.TryParse(dbVal, out temp))
            {
                result = temp;
            }
            return result;
        }

        public static int? GetDbValue(IDataReader reader, string fieldName)
        {
            int? result = null;
            int temp = 0;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (int.TryParse(dbVal, out temp))
            {
                result = temp;
            }
            return result;
        }

        public static bool TryGetDbValue(IDataReader reader, string fieldName, out int result)
        {
            bool valueExists = false;
            result = 0;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (int.TryParse(dbVal, out result))
            {
                valueExists = true;
            }
            return valueExists;
        }

        public static long GetDbValue(IDataReader reader, string fieldName, long defaultValue)
        {
            long result = defaultValue;
            long temp = 0;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (long.TryParse(dbVal, out temp))
            {
                result = temp;
            }
            return result;
        }

        public static bool TryGetDbValue(IDataReader reader, string fieldName, out long result)
        {
            bool valueExists = false;
            result = 0;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (long.TryParse(dbVal, out result))
            {
                valueExists = true;
            }
            return valueExists;
        }

        public static decimal GetDbValue(IDataReader reader, string fieldName, decimal defaultValue)
        {
            decimal result = defaultValue;
            decimal temp = 0;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (decimal.TryParse(dbVal, out temp))
            {
                result = temp;
            }
            return result;
        }

        public static bool TryGetDbValue(IDataReader reader, string fieldName, out decimal result)
        {
            bool valueExists = false;
            result = 0;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (decimal.TryParse(dbVal, out result))
            {
                valueExists = true;
            }
            return valueExists;
        }

        public static bool GetDbValue(IDataReader reader, string fieldName, bool defaultValue)
        {
            bool result = defaultValue;
            string dbVal = GetDbValue(reader, fieldName, string.Empty).ToLower();
            if (dbVal == "1" || dbVal == "yes" || dbVal == "true" || dbVal == "t")
            {
                result = true;
            }
            else if (dbVal == "0" || dbVal == "no" || dbVal == "false" || dbVal == "f")
            {
                result = false;
            }
            return result;
        }

        public static bool TryGetDbValue(IDataReader reader, string fieldName, out bool result)
        {
            bool valueExists = false;
            result = false;
            string dbVal = GetDbValue(reader, fieldName, string.Empty).ToLower();
            if (dbVal == "1" || dbVal == "yes" || dbVal == "true" || dbVal == "t")
            {
                result = true;
                valueExists = true;
            }
            else if (dbVal == "0" || dbVal == "no" || dbVal == "false" || dbVal == "f")
            {
                result = false;
                valueExists = true;
            }
            return valueExists;
        }

        public static DateTime GetDbValue(IDataReader reader, string fieldName, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            DateTime temp = DateTime.Now;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (DateTime.TryParse(dbVal, out temp))
            {
                result = temp;
            }
            return result;
        }

        public static DateTime? GetDatetimeDbValue(IDataReader reader, string fieldName)
        {
            DateTime? result = null;
            DateTime temp = DateTime.Now;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (DateTime.TryParse(dbVal, out temp))
            {
                result = temp;
            }
            return result;
        }

        public static bool TryGetDbValue(IDataReader reader, string fieldName, out DateTime result)
        {
            bool dateFound = false;
            result = DateTime.Now;
            DateTime temp = DateTime.Now;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (DateTime.TryParse(dbVal, out temp))
            {
                result = temp;
                dateFound = true;
            }
            return dateFound;
        }

        public static Guid GetGuidDbValue(IDataReader reader, string fieldName)
        {
            Guid result = Guid.Empty;
            Guid temp = Guid.Empty;
            string dbVal = GetDbValue(reader, fieldName, string.Empty);
            if (Guid.TryParse(dbVal, out temp))
            {
                result = temp;
            }
            return result;
        }
    }
}