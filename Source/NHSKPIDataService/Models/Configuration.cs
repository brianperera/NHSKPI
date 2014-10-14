using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using NHSKPIDataService.Util;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace NHSKPIDataService.Models
{
    public class Configuration
    {
        #region Private Variable

        string targetApply;

        #endregion

        #region Properties

        public string TargetApply
        {
            get { return targetApply; }
            set { targetApply = value; }
        }

        #endregion

        #region Update
        /// <summary>
        /// Update Configuration
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("uspConfigurationUpdate");


            db.AddInParameter(dbCommand, "@TargetApply", DbType.String, this.targetApply);
            
            db.ExecuteNonQuery(dbCommand, transaction);

            return true;
        }

        #endregion 

        #region View
        /// <summary>
        /// Get a hospital by id
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand("uspConfigurationView");

                db.AddOutParameter(dbCommand, "@TargetApply", DbType.String, 50);
                
                db.ExecuteNonQuery(dbCommand);

                this.targetApply = db.GetParameterValue(dbCommand, "@TargetApply").ToString();
                

                return true;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }
        #endregion

    }
}
