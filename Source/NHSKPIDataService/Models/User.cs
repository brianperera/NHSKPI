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
    public class User
    {
        #region Private Members

        private int id;        
        private string userName;
        private string password;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string email;        
        private string mobileNo;       
        private DateTime lastLogDate;       
        private int roleId;
        private bool isActive;
        private bool isActiveDirectoryUser;
        private DateTime createdDate;
        private int createdBy;
        private int hospitalId;
        private string hospitalName;
        private string hospitalType;

        #endregion

        #region Public Members

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string MobileNo
        {
            get { return mobileNo; }
            set { mobileNo = value; }
        }
        public DateTime LastLogDate
        {
            get { return lastLogDate; }
            set { lastLogDate = value; }
        }
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public bool IsActiveDirectoryUser
        {
            get { return isActiveDirectoryUser; }
            set { isActiveDirectoryUser = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public int HospitalId
        {
            get { return hospitalId; }
            set { hospitalId = value; }
        }

        public string HospitalName
        {
            get { return hospitalName; }
            set { hospitalName = value; }
        }

        public string HospitalType
        {
            get { return hospitalType; }
            set { hospitalType = value; }
        }

        #endregion

        #region Add
        /// <summary>
        /// Add a user
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>int</returns>
        public int Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_Insert);

                db.AddInParameter(dbCommand, "@UserName", DbType.String, this.UserName);
                db.AddInParameter(dbCommand, "@Password", DbType.String, this.Password);
                db.AddInParameter(dbCommand, "@FirstName", DbType.String, this.FirstName);
                db.AddInParameter(dbCommand, "@LastName", DbType.String, this.LastName);                
                db.AddInParameter(dbCommand, "@Email", DbType.String, this.Email);
                db.AddInParameter(dbCommand, "@MobileNo", DbType.String, this.MobileNo);
                db.AddInParameter(dbCommand, "@LastLogDate", DbType.DateTime, this.LastLogDate);
                db.AddInParameter(dbCommand, "@RoleId", DbType.Int32, this.RoleId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);
                db.AddInParameter(dbCommand, "@IsActiveDirectoryUser", DbType.Boolean, this.IsActiveDirectoryUser);
                db.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, this.CreatedDate);
                db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, this.CreatedBy);

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
        /// Update a user
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
            db.AddInParameter(dbCommand, "@UserName", DbType.String, this.UserName);
            db.AddInParameter(dbCommand, "@Password", DbType.String, this.Password);
            db.AddInParameter(dbCommand, "@FirstName", DbType.String, this.FirstName);
            db.AddInParameter(dbCommand, "@LastName", DbType.String, this.LastName);            
            db.AddInParameter(dbCommand, "@Email", DbType.String, this.Email);
            db.AddInParameter(dbCommand, "@MobileNo", DbType.String, this.MobileNo);
            db.AddInParameter(dbCommand, "@LastLogDate", DbType.DateTime, this.LastLogDate);
            db.AddInParameter(dbCommand, "@RoleId", DbType.Int32, this.RoleId);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);
            db.AddInParameter(dbCommand, "@IsActiveDirectoryUser", DbType.Boolean, this.IsActiveDirectoryUser);
            db.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, this.CreatedDate);
            db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, this.CreatedBy);

            db.ExecuteNonQuery(dbCommand, transaction);

            return true;
        }
        #endregion

        #region View
        /// <summary>
        /// View a user
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_View);

                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
                db.AddOutParameter(dbCommand, "@UserName", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@Password", DbType.String, 250);
                db.AddOutParameter(dbCommand, "@FirstName", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@LastName", DbType.String, 100);             
                db.AddOutParameter(dbCommand, "@Email", DbType.String, 150);
                db.AddOutParameter(dbCommand, "@MobileNo", DbType.String, 15);
                db.AddOutParameter(dbCommand, "@LastLogDate", DbType.DateTime, 10);
                db.AddOutParameter(dbCommand, "@RoleId", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@HospitalId", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@IsActive", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@IsActiveDirectoryUser", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@CreatedDate", DbType.DateTime, 10);
                db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, 10);

                db.ExecuteNonQuery(dbCommand);

                this.userName = db.GetParameterValue(dbCommand, "@UserName").ToString();
                this.password = db.GetParameterValue(dbCommand, "@Password").ToString();
                this.firstName = db.GetParameterValue(dbCommand, "@FirstName").ToString();
                this.lastName = db.GetParameterValue(dbCommand, "@LastName").ToString();                
                this.Email = db.GetParameterValue(dbCommand, "@Email").ToString();
                this.MobileNo = db.GetParameterValue(dbCommand, "@MobileNo").ToString();
                this.lastLogDate = Convert.ToDateTime(db.GetParameterValue(dbCommand, "@LastLogDate").ToString());
                this.roleId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@RoleId"));
                this.hospitalId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@HospitalId"));
                this.isActive = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActive"));
                this.isActiveDirectoryUser = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActiveDirectoryUser"));
                this.createdDate = Convert.ToDateTime(db.GetParameterValue(dbCommand, "@CreatedDate"));
                this.createdBy = Convert.ToInt32(db.GetParameterValue(dbCommand, "@CreatedBy"));

                return true;
            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region Load User Role
        /// <summary>
        /// List all user role
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>UserRole dataset</returns>
        public DataSet AllUserRole(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_All_UserRole);

            return db.ExecuteDataSet(dbCommand);
        }
        #endregion

        #region Change Password
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool ChangePassword (Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Password_Change);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);            
            db.AddInParameter(dbCommand, "@Password", DbType.String, this.Password);           
           
            db.ExecuteNonQuery(dbCommand, transaction);

            return true;
        }
        #endregion
    }
}
