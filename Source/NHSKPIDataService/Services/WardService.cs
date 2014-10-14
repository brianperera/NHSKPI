using System;
using System.Collections.Generic;
using System.Text;

using NHSKPIDataService.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NHSKPIDataService.Util;
using System.Data;
using System.Data.Common;

namespace NHSKPIDataService.Services
{
    public class WardService
    {
        #region private varibles

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Get Ward Initial Data
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetWardInitialData(int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_Initial_Data);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

                DataSet dsWardInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardInitialData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        #endregion
      
    }
}
