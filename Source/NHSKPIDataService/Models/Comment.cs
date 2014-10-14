using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using NHSKPIDataService.Util;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace NHSKPIDataService.Models
{
    public class Comment
    {
        #region properties
        private int id;       
        private string kpiNumber;
        private int kpiId;        
        private string kpiName;       
        private string comments;       
        private DateTime createdDate;       
        private int createdBy;
        private string commentType;
       
        #endregion

        #region public members
        public string KpiNumber
        {
            get { return kpiNumber; }
            set { kpiNumber = value; }
        }
        public string KpiName
        {
            get { return kpiName; }
            set { kpiName = value; }
        }
        public string Comments
        {
            get { return comments; }
            set { comments = value; }
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
        public int KpiId
        {
            get { return kpiId; }
            set { kpiId = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string CommentType
        {
            get { return commentType; }
            set { commentType = value; }
        }
        #endregion

        #region Add
        /// <summary>
        /// Add a Comment
        /// </summary>
        /// <returns>int</returns>
        public int Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Comment_Insert);
                db.AddInParameter(dbCommand, "@KPIId", DbType.String, this.KpiId);
                db.AddInParameter(dbCommand, "@Comments", DbType.String, this.Comments);
                db.AddInParameter(dbCommand, "@CreatedDate", DbType.Date, this.CreatedDate);
                db.AddInParameter(dbCommand, "@CommentedBy", DbType.String, this.CreatedBy);
                db.AddInParameter(dbCommand, "@CommentType", DbType.String, this.CommentType); 

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

        #region Search
        public DataSet Search(Database db, DbTransaction transaction, int userId, DateTime createdDate, int kpiId)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Comment_Search);
            db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, userId);
            if (createdDate != DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, createdDate);
            }
            else
            {
                db.AddInParameter(dbCommand, "@CreatedDate", DbType.DateTime, DBNull.Value);
            }
            db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
            return db.ExecuteDataSet(dbCommand, transaction);
        }
        #endregion 

        #region Delete
        public int Delete(Database db, DbTransaction transaction, int Id)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Comment_Delete);
            db.AddInParameter(dbCommand, "@Id", DbType.Int32, Id);
            
            return db.ExecuteNonQuery(dbCommand, transaction);
        }
        #endregion 

        #region View
        /// <summary>
        /// Get a Comment by id
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Comment_View);

                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
                db.AddOutParameter(dbCommand, "@Comments", DbType.String, 500);
                db.AddOutParameter(dbCommand, "@CreatedDate", DbType.DateTime, 20);
                db.AddOutParameter(dbCommand, "@CreatedBy", DbType.Int32, 20);
                db.AddOutParameter(dbCommand, "@KPIId", DbType.Int32, 20); 

                db.ExecuteNonQuery(dbCommand);

                this.Comments = db.GetParameterValue(dbCommand, "@Comments").ToString();
                this.CreatedDate = Convert.ToDateTime(db.GetParameterValue(dbCommand, "@CreatedDate").ToString());
                this.CreatedBy = Convert.ToInt32(db.GetParameterValue(dbCommand, "@CreatedBy").ToString());
                this.KpiId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@KPIId").ToString());

                return true;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }
        #endregion      

        #region Get Comment Users

        public DataSet GetCommentUsers(Database db, DbTransaction transaction, int userId)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_Comment_Users);
            db.AddInParameter(dbCommand, "@CreatedBy", DbType.Int32, userId);
            return db.ExecuteDataSet(dbCommand, transaction);
        }
        #endregion 

    }
}
