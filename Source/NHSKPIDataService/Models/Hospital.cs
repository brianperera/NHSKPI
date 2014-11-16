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
    /// <summary>
    /// Source File: Hospital.cs
    /// Description: This class is used to process/handle hospital entity database operation. 
    public class Hospital
    {
        #region properties
        private int id;       
        private string hospitalCode;       
        private string siteCode;       
        private string hospitalName;       
        private string phoneNumber;        
        private string address;        
        private string logoPath;
        private bool isActive;
        private string hospitalType;
        #endregion

        #region public members
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string HospitalCode
        {
            get { return hospitalCode; }
            set { hospitalCode = value; }
        }
        public string SiteCode
        {
            get { return siteCode; }
            set { siteCode = value; }
        }
        public string HospitalName
        {
            get { return hospitalName; }
            set { hospitalName = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string LogoPath
        {
            get { return logoPath; }
            set { logoPath = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public string HospitalType
        {
            get { return hospitalType; }
            set { hospitalType = value; }
        }
        #endregion

        #region Add 
        /// <summary>
        /// Add a hospital
        /// </summary>
        /// <returns>int</returns>
        public int Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_Insert);

                db.AddInParameter(dbCommand, "@Name", DbType.String, this.HospitalName);
                db.AddInParameter(dbCommand, "@Code", DbType.String, this.HospitalCode);
                db.AddInParameter(dbCommand, "@Type", DbType.String, this.HospitalType);
                db.AddInParameter(dbCommand, "@Address", DbType.String, this.Address);
                db.AddInParameter(dbCommand, "@PhoneNumber", DbType.String, this.PhoneNumber);
                db.AddInParameter(dbCommand, "@LogoPath", DbType.String, this.LogoPath);
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

        public int GetID(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_GetId);

                db.AddInParameter(dbCommand, "@Code", DbType.String, this.HospitalCode);
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
        /// Update a hopital
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
            db.AddInParameter(dbCommand, "@Name", DbType.String, this.HospitalName);
            db.AddInParameter(dbCommand, "@Code", DbType.String, this.HospitalCode);
            db.AddInParameter(dbCommand, "@Type", DbType.String, this.HospitalType);
            db.AddInParameter(dbCommand, "@Address", DbType.String, this.Address);
            db.AddInParameter(dbCommand, "@PhoneNumber", DbType.String, this.PhoneNumber);
            db.AddInParameter(dbCommand, "@LogoPath", DbType.String, this.LogoPath);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);

            db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);
            db.ExecuteNonQuery(dbCommand, transaction);

            return Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
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
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_View);
                
                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
                db.AddOutParameter(dbCommand, "@Name", DbType.String, 250);
                db.AddOutParameter(dbCommand, "@Code", DbType.String, 20);
                db.AddOutParameter(dbCommand, "@Type", DbType.String, 20);
                db.AddOutParameter(dbCommand, "@Address", DbType.String, 500);
                db.AddOutParameter(dbCommand, "@PhoneNumber", DbType.String, 15);
                db.AddOutParameter(dbCommand, "@LogoPath", DbType.String, 250);
                db.AddOutParameter(dbCommand, "@IsActive", DbType.Boolean, 1);

                db.ExecuteNonQuery(dbCommand);

                this.hospitalName = db.GetParameterValue(dbCommand, "@Name").ToString();
                this.hospitalCode = db.GetParameterValue(dbCommand, "@Code").ToString();
                this.hospitalType = db.GetParameterValue(dbCommand, "@Type").ToString();
                this.address = db.GetParameterValue(dbCommand, "@Address").ToString();
                this.phoneNumber = db.GetParameterValue(dbCommand, "@PhoneNumber").ToString();
                this.logoPath = db.GetParameterValue(dbCommand, "@LogoPath").ToString();
                this.isActive = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActive"));
                
                return true;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }

           
        }
        #endregion

        #region Search
        /// <summary>
        /// Search hospitals by name and code
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <param name="hospitalName"></param>
        /// <param name="hospitalCode"></param>
        /// <returns>Hospitals DataSet</returns>
        public DataSet Search(Database db, DbTransaction transaction,string hospitalName, string hospitalCode, bool isActive,int hospitalId)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_Search);

            db.AddInParameter(dbCommand, "@Name", DbType.String, hospitalName);
            db.AddInParameter(dbCommand, "@Code", DbType.String, hospitalCode);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);
            db.AddInParameter(dbCommand, "@Id", DbType.Int32, hospitalId);
            
            return db.ExecuteDataSet(dbCommand, transaction);            
        }
        #endregion

        #region View All Hospital
        /// <summary>
        /// Get All hospital
        /// </summary>
        /// <returns>Hospital Dataset</returns>
        public DataSet ViewAllHospital(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_ViewAll);
                return db.ExecuteDataSet(dbCommand, transaction); 
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }

    
}
