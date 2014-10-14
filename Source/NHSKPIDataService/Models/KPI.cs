using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using NHSKPIDataService.Util;
using System.Data;

namespace NHSKPIDataService.Models
{    
  
    public class KPI
    {    

        #region private variable
        private int id;
        private string kPINo;
        private string kPIDescription;
        private int groupId;
        private int targetApplyFor;       
        private int weight;
        private int displayOrder;
        private bool isActive;
        private bool staticTarget;
        private string formatCode;
        private bool rangeTarget;
        private bool numeratorOnlyFlag;
        private bool higherTheBetterFlag;
        private string thresholdDetails;
        private bool visibilty;
        private bool canSummeriseFlag;
        private string indicatorLead;
        private int userId;
        private string commentsLead;
        private bool manuallyEntered;
        private string responsibleDivision;       
        private bool separateYTDFigure;
        private bool averageYTDFigure;
        private string numeratorDescription;
        private string denominatorDescription;
        private string ytdValueDescription;
        private List<TargetDescription> targetDescription;
        private List<TargetGreen> targetGreen;
        private List<TargetAmber> targetAmber;
        private List<KPIWardMonthlyTarget> targetMonthlyDetailsList;
        private List<KPIHospitalYTDTarget> targetYTDDetailsList;
        private List<KPIWardMonthlyData> dataMonthlyDetailsList;
        private List<KPIHospitalYTDData> dataYTDDetailsList;
        private List<KPISpecialtyMonthlyTarget> specialtyMonthlyTargetList;
        private List<KPISpecialtyMonthlyData> specialtyMonthlyDataList;
        private List<KPISpecialtyYTDTarget> specialtyYTDTargetList;
        private List<KPISpecialtyYTDData> specialtyYTDDataList;
        private DataSet _KPIWardMonthlyData = null;
        private DataSet _KPIWardMonthlyTarget = null;
        private DataSet _KPISpecialtyMonthlyTarget = null;
        private DataSet _KPISpecialtyMonthlyData = null;

        #endregion

        #region public members
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string KPINo
        {
            get { return kPINo; }
            set { kPINo = value; }
        }
        public string KPIDescription
        {
            get { return kPIDescription; }
            set { kPIDescription = value; }
        }
        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }
        public int TargetApplyFor
        {
            get { return targetApplyFor; }
            set { targetApplyFor = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
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
        public bool StaticTarget
        {
            get { return staticTarget; }
            set { staticTarget = value; }
        }
        public string FormatCode
        {
            get { return formatCode; }
            set { formatCode = value; }
        }
        public bool RangeTarget
        {
            get { return rangeTarget; }
            set { rangeTarget = value; }
        }
        public bool NumeratorOnlyFlag
        {
            get { return numeratorOnlyFlag; }
            set { numeratorOnlyFlag = value; }
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
        public bool Visibilty
        {
            get { return visibilty; }
            set { visibilty = value; }
        }
        public bool CanSummeriseFlag
        {
            get { return canSummeriseFlag; }
            set { canSummeriseFlag = value; }
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
        public bool ManuallyEntered
        {
            get { return manuallyEntered; }
            set { manuallyEntered = value; }
        }
        public string ResponsibleDivision
        {
            get { return responsibleDivision; }
            set { responsibleDivision = value; }
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
        public string NumeratorDescription
        {
            get { return numeratorDescription; }
            set { numeratorDescription = value; }
        }
        public string DenominatorDescription
        {
            get { return denominatorDescription; }
            set { denominatorDescription = value; }
        }
        public string YTDValueDescription
        {
            get { return ytdValueDescription; }
            set { ytdValueDescription = value; }
        }
        public List<TargetDescription> TargetDescription
        {
            get { return targetDescription; }
            set { targetDescription = value; }
        }
        public List<TargetGreen> TargetGreen
        {
            get { return targetGreen; }
            set { targetGreen = value; }
        }
        public List<TargetAmber> TargetAmber
        {
            get { return targetAmber; }
            set { targetAmber = value; }
        }
        public List<KPIWardMonthlyTarget> TargetMonthlyDetailsList
        {
            get { return targetMonthlyDetailsList; }
            set { targetMonthlyDetailsList = value; }
        }

        public List<KPIHospitalYTDTarget> TargetYTDDetailsList
        {
            get { return targetYTDDetailsList; }
            set { targetYTDDetailsList = value; }
        }

        public List<KPIWardMonthlyData> DataMonthlyDetailsList
        {
            get { return dataMonthlyDetailsList; }
            set { dataMonthlyDetailsList = value; }
        }

        public List<KPIHospitalYTDData> DataYTDDetailsList
        {
            get { return dataYTDDetailsList; }
            set { dataYTDDetailsList = value; }
        }

        public List<KPISpecialtyMonthlyTarget> SpecialtyTargetMonthlyList
        {
            get { return specialtyMonthlyTargetList; }
            set { specialtyMonthlyTargetList = value; }
        }

        public List<KPISpecialtyMonthlyData> SpecialtyMonthlyDataList
        {
            get { return specialtyMonthlyDataList; }
            set { specialtyMonthlyDataList = value; }
        }

        public List<KPISpecialtyYTDTarget> SpecialtyYTDTargetList
        {
            get { return specialtyYTDTargetList; }
            set { specialtyYTDTargetList = value; }
        }

        public List<KPISpecialtyYTDData> SpecialtyYTDDataList
        {
            get { return specialtyYTDDataList; }
            set { specialtyYTDDataList = value; }
        }

        public DataSet DsKPIWardMonthlyData
        {
            get 
            {
                if (_KPIWardMonthlyData == null)
                {
                    _KPIWardMonthlyData = new DataSet();
                    DataTable dtKPIWardMonthlyData = new DataTable();
                    DataColumn[] dcColumns = { new DataColumn("Id"), new DataColumn("WardId"), new DataColumn("KPIId"), new DataColumn("HospitalId"), new DataColumn("TargetMonth"), new DataColumn("Numerator"), new DataColumn("Denominator"), new DataColumn("YTDValue") };
                    dtKPIWardMonthlyData.Columns.AddRange(dcColumns);
                    _KPIWardMonthlyData.Tables.Add(dtKPIWardMonthlyData);
                }
                return _KPIWardMonthlyData;
            }
            set 
            { 
                _KPIWardMonthlyData = value; 
            }
        }

        public DataSet DsKPIWardMonthlyTarget
        {
            get
            {
                if (_KPIWardMonthlyTarget == null)
                {
                    _KPIWardMonthlyTarget = new DataSet();
                    DataTable dtKPIWardMonthlyTarget = new DataTable();
                    DataColumn[] dcColumns = { new DataColumn("Id"), new DataColumn("WardId"), new DataColumn("KPIId"), new DataColumn("HospitalId"), new DataColumn("TargetMonth"), new DataColumn("MonthlyTargetDescription"), new DataColumn("MonthlyTargetGreen"), new DataColumn("MonthlyTargetAmber"), new DataColumn("YTDTargetDescription"), new DataColumn("YTDTargetGreen"), new DataColumn("YTDTargetAmber") };
                    dtKPIWardMonthlyTarget.Columns.AddRange(dcColumns);
                    _KPIWardMonthlyTarget.Tables.Add(dtKPIWardMonthlyTarget);
                }
                return _KPIWardMonthlyTarget;
            }
            set
            {
                _KPIWardMonthlyTarget = value;
            }
        }

        public DataSet DsKPISpecialtyMonthlyTarget
        {
            get
            {
                if (_KPISpecialtyMonthlyTarget == null)
                {
                    _KPISpecialtyMonthlyTarget = new DataSet();
                    DataTable dtKPISpecialtyMonthlyTarget = new DataTable();
                    DataColumn[] dcColumns = { new DataColumn("Id"), new DataColumn("SpecialtyId"), new DataColumn("KPIId"), new DataColumn("HospitalId"), new DataColumn("TargetMonth"), new DataColumn("SpecialtyTargetDescription"), new DataColumn("SpecialtyGreen"), new DataColumn("SpecialtyAmber"), new DataColumn("YTDTargetDescription"), new DataColumn("YTDTargetGreen"), new DataColumn("YTDTargetAmber") };
                    dtKPISpecialtyMonthlyTarget.Columns.AddRange(dcColumns);
                    _KPISpecialtyMonthlyTarget.Tables.Add(dtKPISpecialtyMonthlyTarget);
                }
                return _KPISpecialtyMonthlyTarget;
            }
            set
            {
                _KPISpecialtyMonthlyTarget = value;
            }
        }

        public DataSet DsKPISpecialtyMonthlyData
        {
            get
            {
                if (_KPISpecialtyMonthlyData == null)
                {
                    _KPISpecialtyMonthlyData = new DataSet();
                    DataTable dtKPISpecialtyMonthlyData = new DataTable();
                    DataColumn[] dcColumns = { new DataColumn("Id"), new DataColumn("SpecialtyId"), new DataColumn("KPIId"), new DataColumn("HospitalId"), new DataColumn("TargetMonth"), new DataColumn("Numerator"), new DataColumn("Denominator"), new DataColumn("YTDValue") };
                    dtKPISpecialtyMonthlyData.Columns.AddRange(dcColumns);
                    _KPISpecialtyMonthlyData.Tables.Add(dtKPISpecialtyMonthlyData);
                }
                return _KPISpecialtyMonthlyData;
            }
            set
            {
                _KPISpecialtyMonthlyData = value;
            }
        }

        #endregion

        #region Add
        /// <summary>
        /// Add a KPI
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>int</returns>
        public int Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_Insert);

                db.AddInParameter(dbCommand, "@KPINo", DbType.String, this.KPINo);
                db.AddInParameter(dbCommand, "@KPIDescription", DbType.String, this.KPIDescription);                           
                db.AddInParameter(dbCommand, "@KPIGroupId", DbType.Int32, this.GroupId);
                db.AddInParameter(dbCommand, "@TargetApplyFor", DbType.Int32, this.TargetApplyFor);
                db.AddInParameter(dbCommand, "@Weight", DbType.Int32, this.Weight);
                db.AddInParameter(dbCommand, "@DisplayOrder", DbType.Int32, this.DisplayOrder);
                db.AddInParameter(dbCommand, "@FormatCode", DbType.String, this.FormatCode);
                db.AddInParameter(dbCommand, "@ThresholdDetails", DbType.String, this.ThresholdDetails);
                db.AddInParameter(dbCommand, "@IndicatorLead", DbType.String, this.IndicatorLead);
                db.AddInParameter(dbCommand, "@CommentsLead", DbType.String, this.CommentsLead);
                db.AddInParameter(dbCommand, "@ResponsibleDivision", DbType.String, this.ResponsibleDivision);
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, this.UserId);
                db.AddInParameter(dbCommand, "@StaticTarget", DbType.Boolean, this.StaticTarget);
                db.AddInParameter(dbCommand, "@NumeratorOnlyFlag", DbType.Boolean, this.NumeratorOnlyFlag); 
                db.AddInParameter(dbCommand, "@RangeTarget", DbType.Boolean, this.RangeTarget);                
                db.AddInParameter(dbCommand, "@HigherTheBetterFlag", DbType.Boolean, this.HigherTheBetterFlag);
                db.AddInParameter(dbCommand, "@SeparateYTDFigure", DbType.Boolean, this.SeparateYTDFigure);
                db.AddInParameter(dbCommand, "@AverageYTDFigure", DbType.Boolean, this.AverageYTDFigure);
                db.AddInParameter(dbCommand, "@CanSummariesFlag", DbType.Boolean, this.CanSummeriseFlag);               
                db.AddInParameter(dbCommand, "@ManuallyEntered", DbType.Boolean, this.ManuallyEntered);
                db.AddInParameter(dbCommand, "@Visibility", DbType.Boolean, this.Visibilty);            
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);
                db.AddInParameter(dbCommand, "@NumeratorDescription", DbType.String, this.NumeratorDescription);
                db.AddInParameter(dbCommand, "@DenominatorDescription", DbType.String, this.DenominatorDescription);
                db.AddInParameter(dbCommand, "@YTDValueDescription", DbType.String, this.YTDValueDescription);
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
        /// Update a KPI
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_Update);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
            db.AddInParameter(dbCommand, "@KPINo", DbType.String, this.KPINo);
            db.AddInParameter(dbCommand, "@KPIDescription", DbType.String, this.KPIDescription);
            db.AddInParameter(dbCommand, "@KPIGroupId", DbType.Int32, this.GroupId);
            db.AddInParameter(dbCommand, "@TargetApplyFor", DbType.Int32, this.TargetApplyFor);
            db.AddInParameter(dbCommand, "@Weight", DbType.Int32, this.Weight);
            db.AddInParameter(dbCommand, "@DisplayOrder", DbType.Int32, this.DisplayOrder);
            db.AddInParameter(dbCommand, "@FormatCode", DbType.String, this.FormatCode);
            db.AddInParameter(dbCommand, "@ThresholdDetails", DbType.String, this.ThresholdDetails);
            db.AddInParameter(dbCommand, "@IndicatorLead", DbType.String, this.IndicatorLead);
            db.AddInParameter(dbCommand, "@CommentsLead", DbType.String, this.CommentsLead);
            db.AddInParameter(dbCommand, "@ResponsibleDivision", DbType.String, this.ResponsibleDivision);
            db.AddInParameter(dbCommand, "@UserId", DbType.Int32, this.UserId);
            db.AddInParameter(dbCommand, "@StaticTarget", DbType.Boolean, this.StaticTarget);
            db.AddInParameter(dbCommand, "@NumeratorOnlyFlag", DbType.Boolean, this.NumeratorOnlyFlag);
            db.AddInParameter(dbCommand, "@RangeTarget", DbType.Boolean, this.RangeTarget);
            db.AddInParameter(dbCommand, "@HigherTheBetterFlag", DbType.Boolean, this.HigherTheBetterFlag);
            db.AddInParameter(dbCommand, "@SeparateYTDFigure", DbType.Boolean, this.SeparateYTDFigure);
            db.AddInParameter(dbCommand, "@AverageYTDFigure", DbType.Boolean, this.AverageYTDFigure);
            db.AddInParameter(dbCommand, "@CanSummariesFlag", DbType.Boolean, this.CanSummeriseFlag);
            db.AddInParameter(dbCommand, "@ManuallyEntered", DbType.Boolean, this.ManuallyEntered);
            db.AddInParameter(dbCommand, "@Visibility", DbType.Boolean, this.Visibilty);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, this.IsActive);
            db.AddInParameter(dbCommand, "@NumeratorDescription", DbType.String, this.NumeratorDescription);
            db.AddInParameter(dbCommand, "@DenominatorDescription", DbType.String, this.DenominatorDescription);
            db.AddInParameter(dbCommand, "@YTDValueDescription", DbType.String, this.YTDValueDescription);

            db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);
            db.ExecuteNonQuery(dbCommand, transaction);

            return Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
        }
        #endregion

        #region Search
        /// <summary>
        /// Search KPI
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <param name="kpiNo"></param>
        /// <param name="kpiDescription"></param>
        /// <param name="kpiGrouoId"></param>
        /// <param name="isActive"></param>
        /// <returns>Search Result dataSet</returns>
        public DataSet Search(Database db, DbTransaction transaction, string kpiNo, string kpiDescription, int kpiGrouoId, bool isActive)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_Search);

            db.AddInParameter(dbCommand, "@KPINo", DbType.String, kpiNo);
            db.AddInParameter(dbCommand, "@KPIDescription", DbType.String, kpiDescription);
            db.AddInParameter(dbCommand, "@KPIGroupId", DbType.Int32, kpiGrouoId);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);

            return db.ExecuteDataSet(dbCommand, transaction);

            
        }
        #endregion

        #region View
        /// <summary>
        /// View a KPI
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_View);

                db.AddInParameter(dbCommand, "@Id", DbType.Int32, this.Id);
                db.AddOutParameter(dbCommand, "@KPINo", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@KPIDescription", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@KPIGroupId", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@TargetApplyFor", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@Weight", DbType.Int32, 10);               
                db.AddOutParameter(dbCommand, "@DisplayOrder", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@FormatCode", DbType.String, 20);
                db.AddOutParameter(dbCommand, "@UserId", DbType.Int32, 10);
                db.AddOutParameter(dbCommand, "@ThresholdDetails", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@IndicatorLead", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@CommentsLead", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@ResponsibleDivision", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@StaticTarget", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@NumeratorOnlyFlag", DbType.Boolean, 1);             
                db.AddOutParameter(dbCommand, "@RangeTarget", DbType.Boolean, 1);               
                db.AddOutParameter(dbCommand, "@HigherTheBetterFlag", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@SeparateYTDFigure", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@AverageYTDFigure", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@CanSummariesFlag", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@ManuallyEntered", DbType.Boolean, 1);                
                db.AddOutParameter(dbCommand, "@Visibility", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@IsActive", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@NumeratorDescription", DbType.String, 250);
                db.AddOutParameter(dbCommand, "@DenominatorDescription", DbType.String, 250);
                db.AddOutParameter(dbCommand, "@YTDValueDescription", DbType.String, 250);

                db.ExecuteNonQuery(dbCommand);

                this.kPINo          = db.GetParameterValue(dbCommand, "@KPINo").ToString();
                this.kPIDescription = db.GetParameterValue(dbCommand, "@KPIDescription").ToString();
                this.groupId        = Convert.ToInt32(db.GetParameterValue(dbCommand, "@KPIGroupId"));
                this.targetApplyFor = Convert.ToInt32(db.GetParameterValue(dbCommand, "@TargetApplyFor"));
                this.Weight = Convert.ToInt32(db.GetParameterValue(dbCommand, "@Weight"));
                this.DisplayOrder = Convert.ToInt32(db.GetParameterValue(dbCommand, "@DisplayOrder"));
                this.RangeTarget = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@RangeTarget"));
                this.FormatCode = db.GetParameterValue(dbCommand, "@FormatCode").ToString();
                this.HigherTheBetterFlag = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@HigherTheBetterFlag"));
                this.ThresholdDetails = db.GetParameterValue(dbCommand, "@ThresholdDetails").ToString();
                this.Visibilty = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@Visibility"));
                this.CanSummeriseFlag = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@CanSummariesFlag"));
                this.IndicatorLead = db.GetParameterValue(dbCommand, "@IndicatorLead").ToString();
                this.UserId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@UserId").ToString());
                this.CommentsLead = db.GetParameterValue(dbCommand, "@CommentsLead").ToString();
                this.ResponsibleDivision = db.GetParameterValue(dbCommand, "@ResponsibleDivision").ToString();
                this.ManuallyEntered = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@ManuallyEntered"));
                this.SeparateYTDFigure = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@SeparateYTDFigure"));
                this.AverageYTDFigure = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@AverageYTDFigure"));
                this.StaticTarget = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@StaticTarget"));
                this.NumeratorOnlyFlag = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@NumeratorOnlyFlag"));
                this.isActive       = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsActive"));
                this.NumeratorDescription = db.GetParameterValue(dbCommand, "@NumeratorDescription").ToString();
                this.DenominatorDescription = db.GetParameterValue(dbCommand, "@DenominatorDescription").ToString();
                this.YTDValueDescription = db.GetParameterValue(dbCommand, "@YTDValueDescription").ToString();
                return true;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }
        #endregion
        
        #region All KPI Target Levels
        /// <summary>
        ///  Get KPI Target Levels
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>KPIApplyFor dataset</returns>
        public DataSet AllKPIApplyFor(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_All_TargetApplyFor);

            return db.ExecuteDataSet(dbCommand);
        }
        #endregion

        #region View All KPI
        /// <summary>
        /// Get All KPI
        /// </summary>
        /// <returns>KPI Dataset</returns>
        public DataSet ViewAllKPI(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_View_All);

                return db.ExecuteDataSet(dbCommand);

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
