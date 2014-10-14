using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NHSKPIDataService.Services;
using NHSKPIDataService.Models;
using NHSKPIDataService.Util;

namespace NHSKPIBusinessControllers
{
    /// <summary>
    /// This class will be brige between the servcie class and the UI interface class. Addiotnaly this class will handle the any business related operation 
    /// with ragards to KPI functionlities.
    /// </summary>
    public class KPIController
    {
        #region Private Variable

        KPIService _KPIService = null;
        private NHSKPIDataService.KPIDataService _NHSService = null;    

        #endregion

        #region Properties

        
        public NHSKPIDataService.KPIDataService NHSService
        {
            get
            {
                if (_NHSService == null)
                {
                    _NHSService = new NHSKPIDataService.KPIDataService();
                }
                return _NHSService;
            }
            set
            {
                _NHSService = value;
            }
        }


        #endregion

        #region Get All Wards Suggestions
        /// <summary>
        /// Get the wards starts with given string
        /// </summary>
        /// <param name="kpiStartsWith"></param>
        /// <returns></returns>
        public DataView GetAllKPIs(string kpiStartsWith)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsKPI = null;

                dsKPI = _KPIService.ViewAllKPI();

                DataView dvKPI = new DataView(dsKPI.Tables[0]);
                dvKPI.RowFilter = string.Format("KPIDescription LIKE '{0}%'", kpiStartsWith);

                return dvKPI;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert KPI Target
        /// <summary>
        /// Add the KPI Target
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertKPITarget(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();                

                if ((kpi != null) && (kpi.TargetMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyTarget monthlyTarget in kpi.TargetMonthlyDetailsList)
                    {
                      targetId = _KPIService.InsertKPITarget(monthlyTarget);
                    }
                }
                if (targetId > 0)
                {
                    return true; 
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Bulk Ward Target
        /// <summary>
        /// Insert Bulk Ward Target
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkWardTarget(KPI kpi, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.TargetMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyTarget monthlyTarget in kpi.TargetMonthlyDetailsList)
                    {
                        DataRow row = kpi.DsKPIWardMonthlyTarget.Tables[0].NewRow();
                        row["WardId"] = monthlyTarget.WardId;
                        row["KPIId"] = monthlyTarget.KpiId;
                        row["HospitalId"] = monthlyTarget.HospitalId;
                        row["TargetMonth"] = monthlyTarget.TargetMonth;
                        row["MonthlyTargetDescription"] = monthlyTarget.TargetDescription;
                        if (monthlyTarget.TargetGreen == double.MinValue)
                        {
                            row["MonthlyTargetGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["MonthlyTargetGreen"] = monthlyTarget.TargetGreen;
                        }
                        if (monthlyTarget.TargetAmber == double.MinValue)
                        {
                            row["MonthlyTargetAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["MonthlyTargetAmber"] = monthlyTarget.TargetAmber;
                        }
                        row["YTDTargetDescription"] = monthlyTarget.TargetDescriptionYTD;
                        if (monthlyTarget.TargetGreenYTD == double.MinValue)
                        {
                            row["YTDTargetGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetGreen"] = monthlyTarget.TargetGreenYTD;
                        }
                        if (monthlyTarget.TargetAmberYTD == double.MinValue)
                        {
                            row["YTDTargetAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetAmber"] = monthlyTarget.TargetAmberYTD;
                        }

                        kpi.DsKPIWardMonthlyTarget.Tables[0].Rows.Add(row);

                    }

                    _KPIService.InsertBulkWardTarget(kpi.DsKPIWardMonthlyTarget,fromDate,toDate);
                    kpi.DsKPIWardMonthlyTarget = null;
                }

                return true;
                
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Bulk KPI Target
        /// <summary>
        /// Insert Bulk KPI Target
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkKPITarget(KPI kpi,DateTime fromDate, DateTime toDate)
        {
            try
            {
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.TargetMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyTarget monthlyTarget in kpi.TargetMonthlyDetailsList)
                    {
                        DataRow row = kpi.DsKPIWardMonthlyTarget.Tables[0].NewRow();
                        row["WardId"] = monthlyTarget.WardId;
                        row["KPIId"] = monthlyTarget.KpiId;
                        row["HospitalId"] = monthlyTarget.HospitalId;
                        row["TargetMonth"] = monthlyTarget.TargetMonth;
                        row["MonthlyTargetDescription"] = monthlyTarget.TargetDescription;
                        if (monthlyTarget.TargetGreen == double.MinValue)
                        {
                            row["MonthlyTargetGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["MonthlyTargetGreen"] = monthlyTarget.TargetGreen;
                        }
                        if (monthlyTarget.TargetAmber == double.MinValue)
                        {
                            row["MonthlyTargetAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["MonthlyTargetAmber"] = monthlyTarget.TargetAmber;
                        }
                        row["YTDTargetDescription"] = monthlyTarget.TargetDescriptionYTD;
                        if (monthlyTarget.TargetGreenYTD == double.MinValue)
                        {
                            row["YTDTargetGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetGreen"] = monthlyTarget.TargetGreenYTD;
                        }
                        if (monthlyTarget.TargetAmberYTD == double.MinValue)
                        {
                            row["YTDTargetAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetAmber"] = monthlyTarget.TargetAmberYTD;
                        }

                        kpi.DsKPIWardMonthlyTarget.Tables[0].Rows.Add(row);

                    }

                    _KPIService.InsertBulkKPITarget(kpi.DsKPIWardMonthlyTarget,fromDate,toDate);
                    kpi.DsKPIWardMonthlyTarget = null;
                }

                return true;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert KPI Data
        /// <summary>
        /// Add KPI data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertKPIData(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();                

                if ((kpi != null) && (kpi.DataMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyData monthlyData in kpi.DataMonthlyDetailsList)
                    {
                        targetId = _KPIService.InsertKPIData(monthlyData);
                    }
                }
                if (targetId > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Bulk Ward Data
        /// <summary>
        /// Add KPI data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkWardData(KPI kpi,DateTime fromDate, DateTime toDate)
        {
            try
            {

                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.DataMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyData monthlyData in kpi.DataMonthlyDetailsList)
                    {

                        DataRow row = kpi.DsKPIWardMonthlyData.Tables[0].NewRow();
                        row["WardId"] = monthlyData.WardId;
                        row["KPIId"] = monthlyData.KpiId;
                        row["HospitalId"] = monthlyData.HospitalId;
                        row["TargetMonth"] = monthlyData.TargetMonth;
                        if (monthlyData.Nominator == double.MinValue)
                        {
                            row["Numerator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Numerator"] = monthlyData.Nominator;
                        }
                        if (monthlyData.Denominator == double.MinValue)
                        {
                            row["Denominator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Denominator"] = monthlyData.Denominator;
                        }
                        if (monthlyData.YTDValue == double.MinValue)
                        {
                            row["YTDValue"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDValue"] = monthlyData.YTDValue;
                        }


                        kpi.DsKPIWardMonthlyData.Tables[0].Rows.Add(row);
                    }

                    _KPIService.InsertBulkWardData(kpi.DsKPIWardMonthlyData,fromDate,toDate);
                    kpi.DsKPIWardMonthlyData = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Bulk KPI Data
        /// <summary>
        /// Add KPI data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkKPIData(KPI kpi, DateTime fromDate,DateTime toDate)
        {
            try
            {

                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.DataMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyData monthlyData in kpi.DataMonthlyDetailsList)
                    {

                        DataRow row = kpi.DsKPIWardMonthlyData.Tables[0].NewRow();
                        row["WardId"] = monthlyData.WardId;
                        row["KPIId"] = monthlyData.KpiId;
                        row["HospitalId"] = monthlyData.HospitalId;
                        row["TargetMonth"] = monthlyData.TargetMonth;
                        if (monthlyData.Nominator == double.MinValue)
                        {
                            row["Numerator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Numerator"] = monthlyData.Nominator;
                        }
                        if (monthlyData.Denominator == double.MinValue)
                        {
                            row["Denominator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Denominator"] = monthlyData.Denominator;
                        }
                        if (monthlyData.YTDValue == double.MinValue)
                        {
                            row["YTDValue"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDValue"] = monthlyData.YTDValue;
                        }


                        kpi.DsKPIWardMonthlyData.Tables[0].Rows.Add(row);
                    }

                    _KPIService.InsertBulkKPIData(kpi.DsKPIWardMonthlyData,fromDate,toDate);
                    kpi.DsKPIWardMonthlyData = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get ward level target
        /// <summary>
        /// Get the ward level target
        /// </summary>
        /// <param name="wardId"></param>
        /// <param name="kpiId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetWardKPITarget(int wardId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();
         
               DataSet dsWardKpiTarget = _KPIService.GetWardKPITarget(wardId, kpiId, hospitalId, startDate, endDate);

               return dsWardKpiTarget;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get Wards KPI Data
        /// <summary>
        /// Get the KPI Data
        /// </summary>
        /// <param name="wardId"></param>
        /// <param name="kpiId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetWardKPIData(int wardId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsWardKpiData = _KPIService.GetWardKPIData(wardId, kpiId, hospitalId, startDate, endDate);

                return dsWardKpiData;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get YTD KPI Target
        /// <summary>
        /// Get the YTD Target
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <param name="kpiId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetHospitalKPIYTDTarget(int hospitalId, int kpiId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsWardKpiTarget = _KPIService.GetHospitalKPIYTDTarget(hospitalId, kpiId, startDate, endDate);

                return dsWardKpiTarget;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Add KPIGroup
        /// <summary>
        /// Add KPI Group
        /// </summary>
        /// <param name="kpiGroup"></param>
        /// <returns>int</returns>
        public int AddKPIGroup(KPIGroup kpiGroup)
        {
            return NHSService.AddKPIGroup(kpiGroup);
        }
        #endregion

        #region Update KPIGroup
        /// <summary>
        /// Update KPI Group
        /// </summary>
        /// <param name="kpiGroup"></param>
        /// <returns>true or false</returns>
        public bool UpdateKPIGroup(KPIGroup kpiGroup)
        {
            return NHSService.UpdateKPIGroup(kpiGroup);
        }
        #endregion

        #region Search KPI Group
        /// <summary>
        /// Search KPI Group
        /// </summary>
        /// <param name="kpiGroupName"></param>
        /// <param name="isActive"></param>
        /// <returns>Search Result data set</returns>
        public DataSet SearchKPIGroup(string kpiGroupName, bool isActive)
        {
            return NHSService.SearchKPIGroup(kpiGroupName, isActive);
        }
        #endregion

        #region View KPIGroup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public KPIGroup ViewKPIGroup(int Id)
        {
            return NHSService.ViewKPIGroup(Id);
        } 
        #endregion

        #region Add KPI
        /// <summary>
        /// Add KPI
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>int</returns>
        public int AddKPI(KPI kpi)
        {
            return NHSService.AddKPI(kpi);
        }
        #endregion

        #region Update KPI
        /// <summary>
        /// Update KPI
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>true or false</returns>
        public bool UpdateKPI(KPI kpi)
        {
            return NHSService.UpdateKPI(kpi);
        }
        #endregion

        #region Search KPI
        /// <summary>
        /// Search KPI
        /// </summary>
        /// <param name="kpiNo"></param>
        /// <param name="kpiDescription"></param>
        /// <param name="kpiGrouoId"></param>
        /// <param name="isActive"></param>
        /// <returns>KPI result KPI dataset</returns>
        public DataSet SearchKPI(string kpiNo, string kpiDescription, int kpiGrouoId, bool isActive)
        {
            return NHSService.SearchKPI(kpiNo,kpiDescription,kpiGrouoId,isActive);
        }
        #endregion

        #region View KPI
        /// <summary>
        /// View KPI
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>KPI object</returns>
        public KPI ViewKPI(int Id)
        {
            return NHSService.ViewKpi(Id);
        } 
        #endregion

        #region Add KPI Details
        /// <summary>
        /// Add KPI Details
        /// </summary>
        /// <param name="kpiDetails"></param>
        /// <returns>int</returns>
        public int AddKPIDetails(KPIDetails kpiDetails)
        {
            return NHSService.AddKPIDeails(kpiDetails);
        }
        #endregion

        #region Update KPI Details
        /// <summary>
        /// Update KPI Details
        /// </summary>
        /// <param name="kpiDetails"></param>
        /// <returns>true or false</returns>
        public bool UpdateKPIDetails(KPIDetails kpiDetails)
        {
            return NHSService.UpdateKPIDetails(kpiDetails);
        }
        #endregion

        #region View KPI Details
        /// <summary>
        /// View KPI Details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>KPIDetails object</returns>
        public KPIDetails ViewKPIDetails(int kpiId)
        {
            return NHSService.ViewKPIDetails(kpiId);
        } 
        #endregion

        #region Load KPI NO
        /// <summary>
        /// List KPI No
        /// </summary>
        /// <returns>result KPINo dataset</returns>
        public DataSet GetKPINo()
        {
            return NHSService.LoadKPINo();
        }
        #endregion 
     
        #region Get Auto KPI NO
        /// <summary>
        /// Get Auto KPI NO
        /// </summary>
        /// <returns> KPINo </returns>
        public int GetAutoKPINo()
        {
            return NHSService.GetAutoKPINo();
        }
        #endregion 
        
        #region Load KPI Apply For
        /// <summary>
        /// Load KPI Apply Target
        /// </summary>
        /// <returns>KPI Target dataset</returns>
        public DataSet GetKPIApplyFor()
        {
            return NHSService.LoadKPIApplyFor();
        }      
        #endregion

        #region Insert KPI YTD Target
        /// <summary>
        /// Add the YTD target values
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>true or false</returns>
        public bool InsertKPIYTDTarget(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.TargetYTDDetailsList != null))
                {
                    foreach (KPIHospitalYTDTarget monthlyTarget in kpi.TargetYTDDetailsList)
                    {
                        targetId = _KPIService.InsertKPIYTDTarget(monthlyTarget);
                    }
                }
                if (targetId > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion
        
        #region Update KPI YTD Target
        /// <summary>
        /// Update the YTD Target values
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateKPITarget(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.TargetMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyTarget monthlyTarget in kpi.TargetMonthlyDetailsList)
                    {
                        success = _KPIService.UpdateKPITarget(monthlyTarget);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Update KPI Data
        /// <summary>
        /// Update the KPI Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateKPIData(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.DataMonthlyDetailsList != null))
                {
                    foreach (KPIWardMonthlyData monthlyData in kpi.DataMonthlyDetailsList)
                    {
                        success = _KPIService.UpdateKPIData(monthlyData);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Update KPI YTD Target
        /// <summary>
        /// Update the KPI YTD Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateKPIYTDTarget(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.TargetYTDDetailsList != null))
                {
                    foreach (KPIHospitalYTDTarget YTDData in kpi.TargetYTDDetailsList)
                    {
                        success = _KPIService.UpdateKPIYTDTarget(YTDData);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get Ward Level KPI Initial Data
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetWardLevelKPIInitialData(int hospitalId)
        {
            return new KPIService().GetWardLevelKPIInitialData(hospitalId);
        }
        #endregion

        #region Get Hospital Level KPI Initial Data
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetHospitalLevelKPIInitialData(int hospitalId)
        {
            return new KPIService().GetHospitalLevelKPIInitialData(hospitalId);
        }
        #endregion

        #region Ward Level KPI Search
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet WardLevelKPISearch(int hospitalId, int wardId, int kpiId, DateTime StartDate)
        {
            return new KPIService().WardLevelKPISearch(hospitalId, wardId, kpiId, StartDate);
        }
        #endregion

        #region Hospital Level KPI Search
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet HospitalLevelKPISearch(int hospitalId, int kpiId, DateTime StartDate)
        {
            return new KPIService().HospitalKPISearch(hospitalId, kpiId, StartDate);
        }
        #endregion

        #region Get Hospital YTD KPI Data
        /// <summary>
        /// Get Hospital YTD KPI Data
        /// </summary>
        /// <param name="wardId"></param>
        /// <param name="kpiId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetHospitalYTDKPIData(int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsWardKpiData = _KPIService.GetHospitalYTDKPIData(kpiId, hospitalId, startDate, endDate);

                return dsWardKpiData;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert KPI Hospital YTD Data
        /// <summary>
        /// Insert KPI Hospital YTD Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertHospitalKPIData(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.DataYTDDetailsList != null))
                {
                    foreach (KPIHospitalYTDData data in kpi.DataYTDDetailsList)
                    {
                        targetId = _KPIService.InsertKPIYTDData(data);
                    }
                }
                if (targetId > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Update KPI YTD Data
        /// <summary>
        /// Update the KPI YTD Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateKPIYTDData(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.DataYTDDetailsList != null))
                {
                    foreach (KPIHospitalYTDData data in kpi.DataYTDDetailsList)
                    {
                        success = _KPIService.UpdateKPIYTDData(data);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get KPI For Ward Data Level
        public DataSet GetKPIForWardDataLevel()
        {
            return NHSService.GetKPIForWardDataLevel((int)Structures.Ward.SpecialtyOnly);
        }
        #endregion

        #region Get KPI For Specialty Data Level
        public DataSet GetKPIForSpecialtyDataLevel()
        {
            return NHSService.GetKPIForWardDataLevel((int)Structures.Ward.WardOnly);
        }
        #endregion

        #region Get KPI For Ward Target Level
        public DataSet GetKPIForWardTargetLevel()
        {
            return NHSService.GetKPIForWardTargetLevel((int)Structures.Ward.SpecialtyOnly);
        }
        #endregion

        #region Get KPI For Specialty Target Level
        public DataSet GetKPIForSpecialtyTargetLevel()
        {
            return NHSService.GetKPIForWardTargetLevel((int)Structures.Ward.WardOnly);
        }
        #endregion        

        #region Get Specialty level target
        /// <summary>
        /// Get the Specialty level target
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <param name="kpiId"></param>
        /// <param name="hospitalId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetSpecialtyKPITarget(int specialtyId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsSpecialtyKpiTarget = _KPIService.GetSpecialtyKPITarget(specialtyId, kpiId, hospitalId, startDate, endDate);

                return dsSpecialtyKpiTarget;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Specialty KPI Target
        /// <summary>
        /// Add the Specialty KPI Target
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertSpecialtyKPITarget(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.SpecialtyTargetMonthlyList != null))
                {
                    foreach (KPISpecialtyMonthlyTarget monthlyTarget in kpi.SpecialtyTargetMonthlyList)
                    {
                        targetId = _KPIService.InsertSpecialtyKPIMonthlyTarget(monthlyTarget);
                    }
                }
                if (targetId > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Update Specialty KPI Target
        /// <summary>
        /// Update Specialty KPI Target values
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPITarget(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.SpecialtyTargetMonthlyList != null))
                {
                    foreach (KPISpecialtyMonthlyTarget monthlyTarget in kpi.SpecialtyTargetMonthlyList)
                    {
                        success = _KPIService.UpdateSpecialtyKPIMonthlyTarget(monthlyTarget);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get Specialty KPI Data
        /// <summary>
        /// Get the Specialty KPI Data
        /// </summary>
        /// <param name="wardId"></param>
        /// <param name="kpiId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetSpecialtyKPIData(int specialtyId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsSpecialtyKpiData = _KPIService.GetSpecialtyKPIData(specialtyId, kpiId, hospitalId, startDate, endDate);

                return dsSpecialtyKpiData;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Specialty KPI Data
        /// <summary>
        /// Add Specialty KPI data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertSpecialtyKPIData(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.SpecialtyMonthlyDataList != null))
                {
                    foreach (KPISpecialtyMonthlyData monthlyData in kpi.SpecialtyMonthlyDataList)
                    {
                        targetId = _KPIService.InsertSpecialtyKPIData(monthlyData);
                    }
                }
                if (targetId > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Update Specialty KPI Data
        /// <summary>
        /// Update the Specialty KPI Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPIData(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.SpecialtyMonthlyDataList != null))
                {
                    foreach (KPISpecialtyMonthlyData monthlyData in kpi.SpecialtyMonthlyDataList)
                    {
                        success = _KPIService.UpdateSpecialtyKPIData(monthlyData);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get Specialty Level KPI Initial Data
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelKPIInitialData(int hospitalId)
        {
            return new KPIService().GetSpecialtyLevelKPIInitialData(hospitalId);
        }
        #endregion

        #region Specialty Level KPI Search
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyLevelKPISearch(int hospitalId, int specialtyId, int kpiId, DateTime StartDate)
        {
            return new KPIService().SpecialtyLevelKPISearch(hospitalId, specialtyId, kpiId, StartDate);
        }
        #endregion

        #region Get Specialty YTD KPI Target
        /// <summary>
        /// Get the Specialty YTD Target
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <param name="kpiId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetSpecialtyKPIYTDTarget(int specialtyId, int kpiId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsSpecialtyKpiTarget = _KPIService.GetSpecialtyKPIYTDTarget(specialtyId, kpiId, startDate, endDate);

                return dsSpecialtyKpiTarget;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Specialty KPI YTD Target
        /// <summary>
        /// Add the Specialty KPI YTD target values
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>true or false</returns>
        public bool InsertSpecialtyKPIYTDTarget(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.SpecialtyYTDTargetList != null))
                {
                    foreach (KPISpecialtyYTDTarget monthlyTarget in kpi.SpecialtyYTDTargetList)
                    {
                        targetId = _KPIService.InsertSpecialtyKPIYTDTarget(monthlyTarget);
                    }
                }
                if (targetId > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Update Specialty KPI YTD Target
        /// <summary>
        /// Update the Specialty KPI YTD Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPIYTDTarget(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.SpecialtyYTDTargetList != null))
                {
                    foreach (KPISpecialtyYTDTarget YTDData in kpi.SpecialtyYTDTargetList)
                    {
                        success = _KPIService.UpdateSpecialtyKPIYTDTarget(YTDData);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get Specialty YTD KPI Data
        /// <summary>
        /// Get Specialty YTD KPI Data
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <param name="kpiId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetSpecialtyYTDKPIData(int kpiId, int specialtyId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsSpecialtyKpiData = _KPIService.GetSpecialtyYTDKPIData(kpiId, specialtyId, startDate, endDate);

                return dsSpecialtyKpiData;


            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Update Specialty KPI YTD Data
        /// <summary>
        /// Update Specialty KPI YTD Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPIYTDData(KPI kpi)
        {
            try
            {
                bool success = false;
                _KPIService = new KPIService();
                // for all month deatils call insert values

                if ((kpi != null) && (kpi.SpecialtyYTDDataList != null))
                {
                    foreach (KPISpecialtyYTDData data in kpi.SpecialtyYTDDataList)
                    {
                        success = _KPIService.UpdateSpecialtyKPIYTDData(data);
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert KPI Specialty YTD Data
        /// <summary>
        /// Insert KPI Specialty YTD Data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertSpecialtyKPIYTDData(KPI kpi)
        {
            try
            {
                int targetId = 0;
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.SpecialtyYTDDataList != null))
                {
                    foreach (KPISpecialtyYTDData data in kpi.SpecialtyYTDDataList)
                    {
                        targetId = _KPIService.InsertSpecialtyKPIYTDData(data);
                    }
                }
                if (targetId > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Specialty Level YTD KPI Search
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyLevelYTDKPISearch(int specialtyId, int kpiId, DateTime StartDate)
        {
            return new KPIService().SpecialtyYTDKPISearch(specialtyId, kpiId, StartDate);
        }
        #endregion

        #region Ward Level Bulk Ward Search
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet WardLevelBulkWardSearch(int hospitalId, int wardId, int kpiId, DateTime StartDate, int type)
        {
            return new KPIService().WardLevelBulkWardSearch(hospitalId, wardId, kpiId, StartDate, type);
        }
        #endregion

        #region Specialty Level Bulk KPI Search
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyLevelBulkKPISearch(int hospitalId, int specialtyId, int kpiId, DateTime StartDate, int type)
        {
            return new KPIService().SpecialtyLevelBulkKPISearch(hospitalId, specialtyId, kpiId, StartDate, type);
        }
        #endregion

        #region Specialty Level Bulk Specialty Search
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyLevelBulkSpecialtySearch(int hospitalId, int SpecialtyId, int kpiId, DateTime StartDate, int type)
        {
            return new KPIService().SpecialtyLevelBulkSpecialtySearch(hospitalId, SpecialtyId, kpiId, StartDate, type);
        }
        #endregion

        #region Insert Bulk Specialty Target
        /// <summary>
        /// Insert Bulk Specialty Target
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkSpecialtyTarget(KPI kpi, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.SpecialtyTargetMonthlyList != null))
                {
                    foreach (KPISpecialtyMonthlyTarget monthlyTarget in kpi.SpecialtyTargetMonthlyList)
                    {
                        DataRow row = kpi.DsKPISpecialtyMonthlyTarget.Tables[0].NewRow();
                        row["SpecialtyId"] = monthlyTarget.SpecialtyId;
                        row["KPIId"] = monthlyTarget.KPIId;
                        row["HospitalId"] = monthlyTarget.HospitalId;
                        row["TargetMonth"] = monthlyTarget.TargetMonth;
                        row["SpecialtyTargetDescription"] = monthlyTarget.SpecialtyTargetDescription;
                        if (monthlyTarget.SpecialtyGreen == double.MinValue)
                        {
                            row["SpecialtyGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["SpecialtyGreen"] = monthlyTarget.SpecialtyGreen;
                        }
                        if (monthlyTarget.SpecialtyAmber == double.MinValue)
                        {
                            row["SpecialtyAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["SpecialtyAmber"] = monthlyTarget.SpecialtyAmber;
                        }
                        row["YTDTargetDescription"] = monthlyTarget.TargetDescriptionYTD;
                        if (monthlyTarget.TargetGreenYTD == double.MinValue)
                        {
                            row["YTDTargetGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetGreen"] = monthlyTarget.TargetGreenYTD;
                        }
                        if (monthlyTarget.TargetAmberYTD == double.MinValue)
                        {
                            row["YTDTargetAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetAmber"] = monthlyTarget.TargetAmberYTD;
                        }

                        kpi.DsKPISpecialtyMonthlyTarget.Tables[0].Rows.Add(row);

                    }

                    _KPIService.InsertBulkSpecialtyTarget(kpi.DsKPISpecialtyMonthlyTarget,fromDate,toDate);
                    kpi.DsKPISpecialtyMonthlyTarget = null;
                }

                return true;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Bulk Specialty Target
        /// <summary>
        /// Insert Bulk Specialty Target
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkKPISpecialtyTarget(KPI kpi, DateTime fromDate, DateTime toDate)
        {
            try
            {
                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.SpecialtyTargetMonthlyList != null))
                {
                    foreach (KPISpecialtyMonthlyTarget monthlyTarget in kpi.SpecialtyTargetMonthlyList)
                    {
                        DataRow row = kpi.DsKPISpecialtyMonthlyTarget.Tables[0].NewRow();
                        row["SpecialtyId"] = monthlyTarget.SpecialtyId;
                        row["KPIId"] = monthlyTarget.KPIId;
                        row["HospitalId"] = monthlyTarget.HospitalId;
                        row["TargetMonth"] = monthlyTarget.TargetMonth;
                        row["SpecialtyTargetDescription"] = monthlyTarget.SpecialtyTargetDescription;
                        if (monthlyTarget.SpecialtyGreen == double.MinValue)
                        {
                            row["SpecialtyGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["SpecialtyGreen"] = monthlyTarget.SpecialtyGreen;
                        }
                        if (monthlyTarget.SpecialtyAmber == double.MinValue)
                        {
                            row["SpecialtyAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["SpecialtyAmber"] = monthlyTarget.SpecialtyAmber;
                        }
                        row["YTDTargetDescription"] = monthlyTarget.TargetDescriptionYTD;
                        if (monthlyTarget.TargetGreenYTD == double.MinValue)
                        {
                            row["YTDTargetGreen"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetGreen"] = monthlyTarget.TargetGreenYTD;
                        }
                        if (monthlyTarget.TargetAmberYTD == double.MinValue)
                        {
                            row["YTDTargetAmber"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDTargetAmber"] = monthlyTarget.TargetAmberYTD;
                        }

                        kpi.DsKPISpecialtyMonthlyTarget.Tables[0].Rows.Add(row);

                    }

                    _KPIService.InsertBulkKPISpecialtyTarget(kpi.DsKPISpecialtyMonthlyTarget,fromDate,toDate);
                    kpi.DsKPISpecialtyMonthlyTarget = null;
                }

                return true;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Bulk Specialty Data
        /// <summary>
        /// Add KPI data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkSpecialtyData(KPI kpi, DateTime fromDate, DateTime toDate)
        {
            try
            {

                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.SpecialtyMonthlyDataList != null))
                {
                    foreach (KPISpecialtyMonthlyData monthlyData in kpi.SpecialtyMonthlyDataList)
                    {

                        DataRow row = kpi.DsKPISpecialtyMonthlyData.Tables[0].NewRow();
                        row["SpecialtyId"] = monthlyData.SpecialtyId;
                        row["KPIId"] = monthlyData.KPIId;
                        row["HospitalId"] = monthlyData.HospitalId;
                        row["TargetMonth"] = monthlyData.TargetMonth;
                        if (monthlyData.Numerator == double.MinValue)
                        {
                            row["Numerator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Numerator"] = monthlyData.Numerator;
                        }
                        if (monthlyData.Denominator == double.MinValue)
                        {
                            row["Denominator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Denominator"] = monthlyData.Denominator;
                        }
                        if (monthlyData.YTDValue == double.MinValue)
                        {
                            row["YTDValue"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDValue"] = monthlyData.YTDValue;
                        }


                        kpi.DsKPISpecialtyMonthlyData.Tables[0].Rows.Add(row);
                    }

                    _KPIService.InsertBulkSpecialtyData(kpi.DsKPISpecialtyMonthlyData, fromDate,toDate);
                    kpi.DsKPISpecialtyMonthlyData = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Insert Bulk KPI Data
        /// <summary>
        /// Add KPI data
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns></returns>
        public bool InsertBulkSpecialtyKPIData(KPI kpi, DateTime fromDate, DateTime toDate)
        {
            try
            {

                _KPIService = new KPIService();

                if ((kpi != null) && (kpi.SpecialtyMonthlyDataList != null))
                {
                    foreach (KPISpecialtyMonthlyData monthlyData in kpi.SpecialtyMonthlyDataList)
                    {

                        DataRow row = kpi.DsKPISpecialtyMonthlyData.Tables[0].NewRow();
                        row["SpecialtyId"] = monthlyData.SpecialtyId;
                        row["KPIId"] = monthlyData.KPIId;
                        row["HospitalId"] = monthlyData.HospitalId;
                        row["TargetMonth"] = monthlyData.TargetMonth;
                        if (monthlyData.Numerator == double.MinValue)
                        {
                            row["Numerator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Numerator"] = monthlyData.Numerator;
                        }
                        if (monthlyData.Denominator == double.MinValue)
                        {
                            row["Denominator"] = DBNull.Value;
                        }
                        else
                        {
                            row["Denominator"] = monthlyData.Denominator;
                        }
                        if (monthlyData.YTDValue == double.MinValue)
                        {
                            row["YTDValue"] = DBNull.Value;
                        }
                        else
                        {
                            row["YTDValue"] = monthlyData.YTDValue;
                        }


                        kpi.DsKPISpecialtyMonthlyData.Tables[0].Rows.Add(row);
                    }

                    _KPIService.InsertBulkSpecialtyKPIData(kpi.DsKPISpecialtyMonthlyData, fromDate,toDate);
                    kpi.DsKPISpecialtyMonthlyData = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Get CSV Upload Initial Data
        /// <summary>
        /// Get CSV Upload Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetCSVUploadInitialData()
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsCSVInitialData = _KPIService.GetCSVUploadInitialData();

                return dsCSVInitialData;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Update CSV Ward Data
        /// <summary>
        /// Update CSV Ward Data
        /// </summary>
        /// <returns></returns>
        public bool UpdateCSVWardData(DataTable dtWardData)
        {
            try
            {
                _KPIService = new KPIService();

                bool sucess = _KPIService.UpdateCSVWardData(dtWardData);

                return sucess;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Update CSV Specialty Data
        /// <summary>
        /// Update CSV Specialty Data
        /// </summary>
        /// <returns></returns>
        public bool UpdateCSVSpecialtyData(DataTable dtSpecialtyData)
        {
            try
            {
                _KPIService = new KPIService();

                bool sucess = _KPIService.UpdateCSVSpecialtyData(dtSpecialtyData);

                return sucess;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Update Benchmarck CSV Data
        /// <summary>
        /// Update Benchmarck CSV Data
        /// </summary>
        /// <returns></returns>
        public bool UpdateBenchmarkCSVData(DataTable dtData)
        {
            try
            {
                _KPIService = new KPIService();

                bool sucess = _KPIService.UpdateBenchmarckCSVData(dtData);

                return sucess;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Get Benchmark Initial Data
        /// <summary>
        /// Get Benchmark Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetBenchmarkInitialData()
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsInitialData = _KPIService.GetBenchmarkInitialData();

                return dsInitialData;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Get Benchmark Report Data
        /// <summary>
        /// Get Benchmark Report Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetBenchmarkReportData(DateTime FromDate, DateTime ToDate, string KPINo, string TrustCodeList, int HospitalId)
        {
            try
            {
                _KPIService = new KPIService();

                DataSet dsInitialData = _KPIService.GetBenchmarkReportData(FromDate, ToDate, KPINo, TrustCodeList, HospitalId);

                return dsInitialData;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion
    }
}
