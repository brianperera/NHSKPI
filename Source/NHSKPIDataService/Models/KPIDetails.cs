using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using NHSKPIDataService.Util;
using System.Data;

namespace NHSKPIDataService.Models
{
    public class KPIDetails
    {
        #region properties

        private int id;       
        private int kpiId;       
        private int weight;       
        private int displayOrder;       
        private bool rangeTarget;      
        private string formatCode;        
        private bool higherTheBetterFlag;        
        private string thresholdDetails;       
        private bool visibility;        
        private bool canSummariesFlag;       
        private string indicatorLead;        
        private int userId;       
        private string commentsLead;        
        private string responsibleDivision;       
        private bool manuallyEntered;       
        private bool separateYTDFigure;       
        private bool averageYTDFigure;
        private bool isExist;
       
        #endregion

        #region public members

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int KpiId
        {
            get { return kpiId; }
            set { kpiId = value; }
        }
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }
        public bool RangeTarget
        {
            get { return rangeTarget; }
            set { rangeTarget = value; }
        }
        public string FormatCode
        {
            get { return formatCode; }
            set { formatCode = value; }
        }
        public bool HigherTheBetterFlag
        {
            get { return higherTheBetterFlag; }
            set { higherTheBetterFlag = value; }
        }
        public string ThresholdDetails
        {
            get { return thresholdDetails; }
            set { thresholdDetails = value; }
        }
        public bool Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }
        public bool CanSummariesFlag
        {
            get { return canSummariesFlag; }
            set { canSummariesFlag = value; }
        }
        public string IndicatorLead
        {
            get { return indicatorLead; }
            set { indicatorLead = value; }
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string CommentsLead
        {
            get { return commentsLead; }
            set { commentsLead = value; }
        }
        public string ResponsibleDivision
        {
            get { return responsibleDivision; }
            set { responsibleDivision = value; }
        }
        public bool ManuallyEntered
        {
            get { return manuallyEntered; }
            set { manuallyEntered = value; }
        }
        public bool SeparateYTDFigure
        {
            get { return separateYTDFigure; }
            set { separateYTDFigure = value; }
        }
        public bool AverageYTDFigure
        {
            get { return averageYTDFigure; }
            set { averageYTDFigure = value; }
        }

        public bool IsExist
        {
            get { return isExist; }
            set { isExist = value; }
        }

        #endregion

        #region Add
        /// <summary>
        /// Add the KPI Details
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>int</returns>
        public int Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIDetail_Insert);

                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, this.KpiId);
                db.AddInParameter(dbCommand, "@Weight", DbType.Int32, this.Weight);
                db.AddInParameter(dbCommand, "@DisplayOrder", DbType.Int32, this.DisplayOrder);
                db.AddInParameter(dbCommand, "@RangeTarget", DbType.Boolean, this.RangeTarget);
                db.AddInParameter(dbCommand, "@FormatCode", DbType.String, this.FormatCode);
                db.AddInParameter(dbCommand, "@HigherTheBetterFlag", DbType.Boolean, this.HigherTheBetterFlag);
                db.AddInParameter(dbCommand, "@ThresholdDetails", DbType.String, this.ThresholdDetails);
                db.AddInParameter(dbCommand, "@Visibility", DbType.Boolean, this.Visibility);
                db.AddInParameter(dbCommand, "@CanSummariesFlag", DbType.Boolean, this.CanSummariesFlag);
                db.AddInParameter(dbCommand, "@IndicatorLead", DbType.String, this.IndicatorLead);
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, this.UserId);
                db.AddInParameter(dbCommand, "@CommentsLead", DbType.String, this.CommentsLead);
                db.AddInParameter(dbCommand, "@ResponsibleDivision", DbType.String, this.ResponsibleDivision);
                db.AddInParameter(dbCommand, "@ManuallyEntered", DbType.Boolean, this.ManuallyEntered);
                db.AddInParameter(dbCommand, "@SeparateYTDFigure", DbType.Boolean, this.SeparateYTDFigure);
                db.AddInParameter(dbCommand, "@AverageYTDFigure", DbType.Boolean, this.AverageYTDFigure);

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
        /// Update KPI Details
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIDetail_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
            db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, this.KpiId);
            db.AddInParameter(dbCommand, "@Weight", DbType.Int32, this.Weight);
            db.AddInParameter(dbCommand, "@DisplayOrder", DbType.Int32, this.DisplayOrder);
            db.AddInParameter(dbCommand, "@RangeTarget", DbType.Boolean, this.RangeTarget);
            db.AddInParameter(dbCommand, "@FormatCode", DbType.String, this.FormatCode);
            db.AddInParameter(dbCommand, "@HigherTheBetterFlag", DbType.Boolean, this.HigherTheBetterFlag);
            db.AddInParameter(dbCommand, "@ThresholdDetails", DbType.String, this.ThresholdDetails);
            db.AddInParameter(dbCommand, "@Visibility", DbType.Boolean, this.Visibility);
            db.AddInParameter(dbCommand, "@CanSummariesFlag", DbType.Boolean, this.CanSummariesFlag);
            db.AddInParameter(dbCommand, "@IndicatorLead", DbType.String, this.IndicatorLead);
            db.AddInParameter(dbCommand, "@UserId", DbType.Int32, this.UserId);
            db.AddInParameter(dbCommand, "@CommentsLead", DbType.String, this.CommentsLead);
            db.AddInParameter(dbCommand, "@ResponsibleDivision", DbType.String, this.ResponsibleDivision);
            db.AddInParameter(dbCommand, "@ManuallyEntered", DbType.Boolean, this.ManuallyEntered);
            db.AddInParameter(dbCommand, "@SeparateYTDFigure", DbType.Boolean, this.SeparateYTDFigure);
            db.AddInParameter(dbCommand, "@AverageYTDFigure", DbType.Boolean, this.AverageYTDFigure);

            db.ExecuteNonQuery(dbCommand, transaction);

            return true;
        }
        #endregion

        #region View
        /// <summary>
        /// View th KPI Details
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIDetail_View);

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, this.KpiId);
                db.AddOutParameter(dbCommand, "@Weight", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@DisplayOrder", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@RangeTarget", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@FormatCode", DbType.String, 20);
                db.AddOutParameter(dbCommand, "@HigherTheBetterFlag", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@ThresholdDetails", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@Visibility", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@CanSummariesFlag", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@IndicatorLead", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@UserId", DbType.Int32,10);
                db.AddOutParameter(dbCommand, "@CommentsLead", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@ResponsibleDivision", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@ManuallyEntered", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@SeparateYTDFigure", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@AverageYTDFigure", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);

                db.ExecuteNonQuery(dbCommand);

                if (Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist")))
                {

                    this.Id = Convert.ToInt32(db.GetParameterValue(dbCommand, "@Id"));
                    this.Weight = Convert.ToInt32(db.GetParameterValue(dbCommand, "@Weight"));
                    this.DisplayOrder = Convert.ToInt32(db.GetParameterValue(dbCommand, "@DisplayOrder"));
                    this.RangeTarget = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@RangeTarget"));
                    this.FormatCode = db.GetParameterValue(dbCommand, "@FormatCode").ToString();
                    this.HigherTheBetterFlag = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@HigherTheBetterFlag"));
                    this.ThresholdDetails = db.GetParameterValue(dbCommand, "@ThresholdDetails").ToString();
                    this.Visibility = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@Visibility"));
                    this.CanSummariesFlag = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@CanSummariesFlag"));
                    this.IndicatorLead = db.GetParameterValue(dbCommand, "@IndicatorLead").ToString();
                    this.UserId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@UserId").ToString());
                    this.CommentsLead = db.GetParameterValue(dbCommand, "@CommentsLead").ToString();
                    this.ResponsibleDivision = db.GetParameterValue(dbCommand, "@ResponsibleDivision").ToString();
                    this.ManuallyEntered = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@ManuallyEntered"));
                    this.SeparateYTDFigure = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@SeparateYTDFigure"));
                    this.AverageYTDFigure = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@AverageYTDFigure"));
                    this.IsExist = true;
                    return true;
                }
                else
                {
                    this.IsExist = false;
                    return false;
                }

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region All KPI Name
        /// <summary>
        /// Get All KPI Name
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>KPI Dataset</returns>
        public DataSet AllKPINo(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_All_KPI_No);

            return db.ExecuteDataSet(dbCommand);
        }
        #endregion

        #region Get Auto KPI No
        /// <summary>
        /// Get Auto KPI No
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>KPI Dataset</returns>
        public int GetAutoKPINo(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_Auto_KPI_No);
            db.AddOutParameter(dbCommand, "@AutoKPINo", DbType.Int32, 10);
            db.ExecuteNonQuery(dbCommand);
            return Convert.ToInt32(db.GetParameterValue(dbCommand, "@AutoKPINo"));
        }
        #endregion


    }
}
