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
   public class KPIGroup
    {
        #region properties

        private int id;
        private string kpiGroupName;
        private bool isActive;
        private int hospitalID;        

        #endregion

        #region public members
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string KpiGroupName
        {
            get { return kpiGroupName; }
            set { kpiGroupName = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public int HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        #endregion

        #region Add
        /// <summary>
        /// Add a KPI Group
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>int</returns>
        public int Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_Group_Insert);

                db.AddInParameter(dbCommand, "@KPIGroupName", DbType.String, this.KpiGroupName);               
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);
                db.AddInParameter(dbCommand, "@HospitalID", DbType.Int32, this.HospitalID);
                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
                db.ExecuteNonQuery(dbCommand, transaction);

                return Convert.ToInt32(db.GetParameterValue(dbCommand, "@Id"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update 
       /// <summary>
       /// Update a KPI Group
       /// </summary>
       /// <param name="db"></param>
       /// <param name="transaction"></param>
       /// <returns>true or false</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_Group_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
            db.AddInParameter(dbCommand, "@KPIGroupName", DbType.String, this.KpiGroupName);               
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);

            db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);
            db.ExecuteNonQuery(dbCommand, transaction);

            return Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
        }
        
        #endregion

        #region Search
        /// <summary>
        /// Search KPI Group
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <param name="kpiGroupName"></param>
        /// <param name="isActive"></param>
        /// <returns>Search Result KPIGroup dataset</returns>
        public DataSet Search(Database db, DbTransaction transaction, string kpiGroupName, bool isActive)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_Group_Search);

            db.AddInParameter(dbCommand, "@KPIGroupName", DbType.String, kpiGroupName);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);

            return db.ExecuteDataSet(dbCommand, transaction);

            
        }

        #endregion

        # region View
       /// <summary>
       /// View a KPI Group
       /// </summary>
       /// <param name="db"></param>
       /// <param name="transaction"></param>
       /// <returns>bool</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_Group_View);

                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
                db.AddOutParameter(dbCommand, "@KPIGroupName", DbType.String, 100);               
                db.AddOutParameter(dbCommand, "@IsActive", DbType.Boolean, 1);

                db.ExecuteNonQuery(dbCommand);

                this.kpiGroupName = db.GetParameterValue(dbCommand, "@KPIGroupName").ToString();         
                this.isActive     = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActive"));

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
