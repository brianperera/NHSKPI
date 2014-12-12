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
    public class DepartmentHead
    {
        #region private properties

        private int id = 0;
        private string name = null;
        private string jobTitle = null;
        private string mobileNo = null;
        private string email = null;
        private int approvedUserId = 0;

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region public members

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string JobTitle
        {
            get { return jobTitle; }
            set { jobTitle = value; }
        }        

        public string MobileNo
        {
            get { return mobileNo; }
            set { mobileNo = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int ApprovedUserId
        {
            get { return approvedUserId; }
            set { approvedUserId = value; }
        }

        #endregion

        #region Insert Update Department Head

        public int InsertUpdateDepartmentHead(DepartmentHead departmentHead)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Set_Update_Department_Head);

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 5);
                //db.AddInParameter(dbCommand, "@Id", DbType.Int32, 0);
                db.AddInParameter(dbCommand, "@Name", DbType.String, departmentHead.Name);
                db.AddInParameter(dbCommand, "@JobTile", DbType.String, departmentHead.JobTitle);
                db.AddInParameter(dbCommand, "@MobileNo", DbType.String, departmentHead.MobileNo);
                db.AddInParameter(dbCommand, "@Email", DbType.String, departmentHead.Email);
                db.AddInParameter(dbCommand, "@ApprovedUserId", DbType.Int32, departmentHead.ApprovedUserId);

                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "@Id"));
                transaction.Commit();

                return id;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
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
