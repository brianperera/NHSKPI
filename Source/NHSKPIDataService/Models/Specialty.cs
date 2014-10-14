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
    public class Specialty
    {
        #region properties
        private int id;        
        private string specialtyName;
        private bool isActive;
        private string specialtyCode;
        private string nationalSpecialty;
        private string nationalCode;
        private int groupId;

        
        #endregion

        #region Public members
        public string SpecialtyName
        {
            get { return specialtyName; }
            set { specialtyName = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string SpecialtyCode
        {
            get { return specialtyCode; }
            set { specialtyCode = value; }
        }


        public string NationalSpecialty
        {
            get { return nationalSpecialty; }
            set { nationalSpecialty = value; }
        }


        public string NationalCode
        {
            get { return nationalCode; }
            set { nationalCode = value; }
        }


        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }
        #endregion

        #region Add
        /// <summary>
        /// Add Specialty
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>int</returns>
        public int Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Insert);

                db.AddInParameter(dbCommand, "@Specialty", DbType.String, this.SpecialtyName);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);
                db.AddInParameter(dbCommand, "@SpecialtyCode", DbType.String, this.SpecialtyCode);
                db.AddInParameter(dbCommand, "@NationalSpecialty", DbType.String, this.NationalSpecialty);
                db.AddInParameter(dbCommand, "@NationalCode", DbType.String, this.NationalCode);
                db.AddInParameter(dbCommand, "@GroupId", DbType.Int32, this.GroupId);

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
        /// Update Specialty 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
            db.AddInParameter(dbCommand, "@Specialty", DbType.String, this.SpecialtyName);            
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);
            db.AddInParameter(dbCommand, "@SpecialtyCode", DbType.String, this.SpecialtyCode);
            db.AddInParameter(dbCommand, "@NationalSpecialty", DbType.String, this.NationalSpecialty);
            db.AddInParameter(dbCommand, "@NationalCode", DbType.String, this.NationalCode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int32, this.GroupId);

            db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);
            db.ExecuteNonQuery(dbCommand, transaction);

            return Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
        }
        #endregion

        #region Search
        /// <summary>
        /// Search Specialty
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>Specialty dataset</returns>
        public DataSet Search(Database db, DbTransaction transaction, string name, bool isActive)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Search);

            db.AddInParameter(dbCommand, "@Specialty", DbType.String, name);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);

            return db.ExecuteDataSet(dbCommand, transaction);
             
        }
        #endregion

        #region View
        /// <summary>
        /// View Specialty
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_View);

                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
                db.AddOutParameter(dbCommand, "@Specialty", DbType.String, 100);                
                db.AddOutParameter(dbCommand, "@IsActive", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@SpecialtyCode", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@NationalSpecialty", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@NationalCode", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@GroupId", DbType.Int32, 10);

                db.ExecuteNonQuery(dbCommand);

                this.specialtyName = db.GetParameterValue(dbCommand, "@Specialty").ToString();                
                this.isActive = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActive"));
                this.specialtyCode = db.GetParameterValue(dbCommand, "@SpecialtyCode").ToString();
                this.nationalSpecialty = db.GetParameterValue(dbCommand, "@NationalSpecialty").ToString();
                this.nationalCode = db.GetParameterValue(dbCommand, "@NationalCode").ToString();
                this.groupId =Convert.ToInt32(db.GetParameterValue(dbCommand, "@GroupId").ToString());
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
