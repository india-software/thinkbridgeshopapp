using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;


namespace ThinkBridge.Shop.Core.Helper
{
    public static class DataProviderHelper
    {
        #region Utilities

        /// <summary>
        /// Get DB parameter
        /// </summary>
        /// <param name="dbType">Data type</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        private static DbParameter GetParameter( DbType dbType, string parameterName, object parameterValue)
        {
            var parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            parameter.DbType = dbType;

            return parameter;
        }

        /// <summary>
        /// Get output DB parameter
        /// </summary>
        /// <param name="dbType">Data type</param>
        /// <param name="parameterName">Parameter name</param>
        /// <returns>Parameter</returns>
        private static DbParameter GetOutputParameter( DbType dbType, string parameterName)
        {
            var parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Output;

            return parameter;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get string parameter
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetStringParameter( string parameterName, string parameterValue)
        {
            return GetParameter(DbType.String, parameterName, (object)parameterValue ?? DBNull.Value);
        }

        /// <summary>
        /// Get output string parameter
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetOutputStringParameter( string parameterName)
        {
            return GetOutputParameter(DbType.String, parameterName);
        }

        /// <summary>
        /// Get int parameter
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetInt32Parameter( string parameterName, int? parameterValue)
        {
            return GetParameter(DbType.Int32, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        /// <summary>
        /// Get output int32 parameter
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetOutputInt32Parameter( string parameterName)
        {
            return GetOutputParameter(DbType.Int32, parameterName);
        }

        /// <summary>
        /// Get boolean parameter
        /// </summary>    
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetBooleanParameter( string parameterName, bool? parameterValue)
        {
            return GetParameter(DbType.Boolean, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        /// <summary>
        /// Get decimal parameter
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetDecimalParameter( string parameterName, decimal? parameterValue)
        {
            return GetParameter(DbType.Decimal, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        /// <summary>
        /// Get datetime parameter
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetDateTimeParameter( string parameterName, DateTime? parameterValue)
        {
            return GetParameter(DbType.DateTime, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        #endregion
    }
}
