using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using NHSKPIDataService.Util;


namespace NHSKPIDataService.Models
{
    public class WardGroup
    {
        #region properties

        #region Ward Group Properties
        private int id;
        private string wardGroupName;
        private string description;
        private bool isActive;
        private int hospitalId;

        #endregion
        
        #endregion

        #region public members

        #region Ward Group Public Members
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string WardGroupName
        {
            get { return wardGroupName; }
            set { wardGroupName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public int HospitalId
        {
            get { return hospitalId; }
            set { hospitalId = value; }
        }
        #endregion
        
        #endregion

        #region Add Ward Group
        /// <summary>
        /// Add the Ward Group
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>Int</returns>
        public int AddWardGroup(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_WardGroup_Insert);

                db.AddInParameter(dbCommand, "@WardGroupName", DbType.String, this.WardGroupName);
                db.AddInParameter(dbCommand, "@Description", DbType.String, this.Description);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.String, this.HospitalId);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);

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

        #region Update Ward Group
        /// <summary>
        /// Update Ward Group
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>

        public bool UpdateWardGroup(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_WardGroup_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
            db.AddInParameter(dbCommand, "@WardGroupName", DbType.String, this.WardGroupName);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
            db.AddInParameter(dbCommand, "@Description", DbType.String, this.Description);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);

            db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);
            db.ExecuteNonQuery(dbCommand, transaction);

            return Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
        }

        #endregion

        #region View Ward Group
        /// <summary>
        /// Get Ward Group Details
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>
        public bool ViewWardGroup(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_WardGroup_View);

                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
                db.AddOutParameter(dbCommand, "@WardGroupName", DbType.String,50);
                db.AddOutParameter(dbCommand, "@HospitalId", DbType.Int32,10);
                db.AddOutParameter(dbCommand, "@Description", DbType.String,100);
                db.AddOutParameter(dbCommand, "@IsActive", DbType.Boolean, 1);

                db.ExecuteNonQuery(dbCommand);

                this.wardGroupName = db.GetParameterValue(dbCommand, "@WardGroupName").ToString();
                this.hospitalId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@HospitalId"));
                this.description = db.GetParameterValue(dbCommand, "@Description").ToString();
                this.isActive = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActive"));

                return true;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region Search Ward Group
        /// <summary>
        /// Search Ward Group
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>

        public DataSet SearchWardGroup(Database db, DbTransaction transaction,string name, int hospitalId, bool isActive)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_WardGroup_Search);

            db.AddInParameter(dbCommand, "@WardGroupName", DbType.String, name);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);

            return db.ExecuteDataSet(dbCommand, transaction);

            
        }

        #endregion
    }
}
