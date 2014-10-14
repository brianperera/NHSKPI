using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using NHSKPIDataService.Util;

namespace NHSKPIDataService.Models
{
    public class Ward
    {
        #region properties

        #region Ward properties

        private int wardId;       
        private string wardCode;       
        private string wardName;        
        private int hospitalId;       
        private int wardGroupId;           
        private bool isActiveWard;      

        #endregion

        #endregion

        #region public members

        #region Ward Publuc Members
         public int WardId
        {
            get { return wardId; }
            set { wardId = value; }
        }
         public string WardCode
         {
             get { return wardCode; }
             set { wardCode = value; }
         }
         public string WardName
         {
             get { return wardName; }
             set { wardName = value; }
         }
         public int HospitalId
         {
             get { return hospitalId; }
             set { hospitalId = value; }
         }
         public int WardGroupId
         {
             get { return wardGroupId; }
             set { wardGroupId = value; }
         }        
         public bool IsActiveWard
         {
             get { return isActiveWard; }
             set { isActiveWard = value; }
         }
        #endregion

        #endregion
        
        #region Add Ward
        /// <summary>
        /// Add the Ward
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>int</returns>
        public int AddWard(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_Insert);

                db.AddInParameter(dbCommand, "@WardCode", DbType.String, this.WardCode);
                db.AddInParameter(dbCommand, "@WardName", DbType.String, this.WardName);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
                db.AddInParameter(dbCommand, "@WardGroupId", DbType.Int32, this.WardGroupId);                
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActiveWard);

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

        #region Update Ward
        /// <summary>
        /// Update the Ward
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool UpdateWard(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.WardId);
            db.AddInParameter(dbCommand, "@WardCode", DbType.String, this.WardCode);
            db.AddInParameter(dbCommand, "@WardName", DbType.String, this.WardName);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
            db.AddInParameter(dbCommand, "@WardGroupId", DbType.Int32, this.WardGroupId);            
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActiveWard);

            db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);
            db.ExecuteNonQuery(dbCommand, transaction);

            return Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
        }

        #endregion

        #region Search Ward

        /// <summary>
        /// Search Ward
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="hospitalId"></param>
        /// <param name="wardGroupId"></param>
        /// <param name="specialtyId"></param>
        /// <param name="isActive"></param>
        /// <returns>Wards DataSet</returns>
        public DataSet SearchWard(Database db, DbTransaction transaction, string name, string code, int hospitalId, int wardGroupId, bool isActive)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_Search);

            db.AddInParameter(dbCommand, "@WardName", DbType.String, name);
            db.AddInParameter(dbCommand, "@WardCode", DbType.String, code);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
            db.AddInParameter(dbCommand, "@WardGroupId", DbType.Int32, wardGroupId);            
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);

            return db.ExecuteDataSet(dbCommand, transaction);
        }

        #endregion

        #region View Ward
        /// <summary>
        /// Get the Ward Details
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool ViewWard(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_View);

                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.WardId);
                db.AddOutParameter(dbCommand, "@WardCode", DbType.String, 10);
                db.AddOutParameter(dbCommand, "@WardName", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@HospitalId", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@WardGroupId", DbType.Int32,10);                
                db.AddOutParameter(dbCommand, "@IsActive", DbType.Boolean, 1);               

                db.ExecuteNonQuery(dbCommand);

                this.wardCode       = db.GetParameterValue(dbCommand, "@WardCode").ToString();
                this.wardName       = db.GetParameterValue(dbCommand, "@WardName").ToString();
                this.hospitalId     = Convert.ToInt32(db.GetParameterValue(dbCommand, "@HospitalId"));
                this.wardGroupId    = Convert.ToInt32(db.GetParameterValue(dbCommand, "@WardGroupId"));               
                this.isActiveWard   = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActive"));

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
