using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using NHSKPIDataService.Models;

using NHSKPIDataService.Util;
using System.Data;
using System.Data.Common;

namespace NHSKPIDataService.Services
{
    /// <summary>
    /// This is the service class for KPI related function and handle the data base communication with controller. 
    /// </summary>
    public class KPIService
    {
        #region private varibles

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion             

        #region View All KPIs
        /// <summary>
        /// View All KPI
        /// </summary>
        /// <returns></returns>
        public DataSet ViewAllKPI()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                KPI kpi = new KPI();

                DataSet dsKPIs = kpi.ViewAllKPI(db, transaction);

                transaction.Commit();
                return dsKPIs;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Insert Monthly Ward KPI
        /// <summary>
        /// Insert the Monthky KPI target
        /// </summary>
        /// <param name="kpiMonthlyTarget"></param>
        /// <returns></returns>
        public int InsertKPITarget(KPIWardMonthlyTarget kpiMonthlyTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIWardMonthlyTarget_Insert);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, kpiMonthlyTarget.WardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyTarget.KpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyTarget.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyTarget.TargetMonth);
                db.AddInParameter(dbCommand, "@MonthlyTargetDescription", DbType.String, kpiMonthlyTarget.TargetDescription);

                if (kpiMonthlyTarget.TargetGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetGreen", DbType.Decimal, kpiMonthlyTarget.TargetGreen);
                }

                if (kpiMonthlyTarget.TargetAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetAmber", DbType.Decimal, kpiMonthlyTarget.TargetAmber);
                }

                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiMonthlyTarget.TargetDescriptionYTD);

                if (kpiMonthlyTarget.TargetGreenYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, kpiMonthlyTarget.TargetGreenYTD);
                }

                if (kpiMonthlyTarget.TargetAmberYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, kpiMonthlyTarget.TargetAmberYTD);
                }

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Get Ward KPI Target
        /// <summary>
        /// Get the Ward KPI Target
        /// </summary>
        /// <returns></returns>
        public DataSet GetWardKPITarget(int wardId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_KPI_Target_View);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, wardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsWardKPITas = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardKPITas;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Get Ward KPI Data
        /// <summary>
        /// Get Ward KPI Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetWardKPIData(int wardId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_KPI_Data_View);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, wardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsWardKPIData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardKPIData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Get Hospital KPI YTD Target
        /// <summary>
        /// Get Hospital YTD Target
        /// </summary>
        /// <returns></returns>
        public DataSet GetHospitalKPIYTDTarget(int hospitalId, int kpiId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_KPI_YTD_Target_View);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsWardKPITas = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardKPITas;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Insert  KPI YTD Target
        /// <summary>
        /// Insert KPI YTD Target
        /// </summary>
        /// <param name="kpiYTDTarget"></param>
        /// <returns></returns>
        public int InsertKPIYTDTarget(KPIHospitalYTDTarget kpiYTDTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_KPI_YTD_Target_Insert);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiYTDTarget.HospitalId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiYTDTarget.KpiId);
                db.AddInParameter(dbCommand, "@TargetYTD", DbType.Date, kpiYTDTarget.TargetYTD);
                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiYTDTarget.TargetDescription);

                if (kpiYTDTarget.TargetGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, kpiYTDTarget.TargetGreen);
                }

                if (kpiYTDTarget.TargetAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {

                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, kpiYTDTarget.TargetAmber);
                }

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Update Ward KPI
        /// <summary>
        /// Update Monthly KPI Target
        /// </summary>
        /// <param name="kpiMonthlyTarget"></param>
        /// <returns></returns>
        public bool UpdateKPITarget(KPIWardMonthlyTarget kpiMonthlyTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIWardMonthlyTarget_Update);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, kpiMonthlyTarget.WardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyTarget.KpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyTarget.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyTarget.TargetMonth);
                db.AddInParameter(dbCommand, "@MonthlyTargetDescription", DbType.String, kpiMonthlyTarget.TargetDescription);                

                if (kpiMonthlyTarget.TargetGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetGreen", DbType.Decimal, kpiMonthlyTarget.TargetGreen);
                }

                if (kpiMonthlyTarget.TargetAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@MonthlyTargetAmber", DbType.Decimal, kpiMonthlyTarget.TargetAmber);
                }

                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiMonthlyTarget.TargetDescriptionYTD);

                if (kpiMonthlyTarget.TargetGreenYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, kpiMonthlyTarget.TargetGreenYTD);
                }

                if (kpiMonthlyTarget.TargetAmberYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, kpiMonthlyTarget.TargetAmberYTD);
                }

               
                int i = db.ExecuteNonQuery(dbCommand, transaction);
               
                transaction.Commit();

                return true;

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

        #region Insert KPI Data
        /// <summary>
        /// Insert KPI Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertKPIData(KPIWardMonthlyData kpiMonthlyData)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIWardMonthlyData_Insert);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, kpiMonthlyData.WardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyData.KpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyData.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyData.TargetMonth);
                if (kpiMonthlyData.Nominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiMonthlyData.Nominator);
                }
                if (kpiMonthlyData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiMonthlyData.Denominator);
                }

                if (kpiMonthlyData.YTDValue == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, kpiMonthlyData.YTDValue);
                }

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Insert Bulk Ward Data
        /// <summary>
        /// Insert Bulk Ward Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertBulkWardData(DataSet dsWardKPIData, DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulKWardData(int.Parse(dsWardKPIData.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsWardKPIData.Tables[0].Rows[0]["KPIId"].ToString()), fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPIWardMonthlyData";
                    sqlBulkCopy.WriteToServer(dsWardKPIData.Tables[0]);
                    
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }

        #endregion

        #region Insert Bulk KPI Data
        /// <summary>
        /// Insert Bulk KPI Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertBulkKPIData(DataSet dsWardKPIData,DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulkKPIData(int.Parse(dsWardKPIData.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsWardKPIData.Tables[0].Rows[0]["WardId"].ToString()),fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPIWardMonthlyData";
                    sqlBulkCopy.WriteToServer(dsWardKPIData.Tables[0]);

                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Insert Bulk Ward Target
        /// <summary>
        /// Insert Bulk Ward Target
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertBulkWardTarget(DataSet dsWardKPIData, DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulkWardTarget(int.Parse(dsWardKPIData.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsWardKPIData.Tables[0].Rows[0]["KPIId"].ToString()),fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPIWardMonthlyTarget";
                    sqlBulkCopy.WriteToServer(dsWardKPIData.Tables[0]);

                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Insert Bulk KPI Target
        /// <summary>
        /// Insert Bulk KPI Target
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertBulkKPITarget(DataSet dsWardKPIData,DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulkKPITarget(int.Parse(dsWardKPIData.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsWardKPIData.Tables[0].Rows[0]["WardId"].ToString()),fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPIWardMonthlyTarget";
                    sqlBulkCopy.WriteToServer(dsWardKPIData.Tables[0]);

                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Get Connection string
        /// <summary>
        /// Get Connection string
        /// </summary>
        private string GetConnectionString()
        {
            try
            {
                string con = System.Configuration.ConfigurationManager.ConnectionStrings[Constant.NHS_Database_Connection_Name].ConnectionString;
                return con;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        
        #region Delete Bulk Ward Data

        public bool DeleteBulKWardData(int hospitalId, int kpiId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIBulkWardData_Delete);

                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);
                
                db.ExecuteNonQuery(dbCommand, transaction);

                
                transaction.Commit();

                return true;
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

        #region Delete Bulk Ward Target

        public bool DeleteBulkWardTarget(int hospitalId, int kpiId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIBulkWardTarget_Delete);

                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);

                db.ExecuteNonQuery(dbCommand, transaction);


                transaction.Commit();

                return true;
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

        #region Delete Bulk KPI Target

        public bool DeleteBulkKPITarget(int hospitalId, int WardId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIBulkKPITarget_Delete);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, WardId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);

                db.ExecuteNonQuery(dbCommand, transaction);


                transaction.Commit();

                return true;
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

        #region Delete Bulk KPI Data

        public bool DeleteBulkKPIData(int hospitalId, int wardId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIBulkKPIData_Delete);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, wardId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);

                db.ExecuteNonQuery(dbCommand, transaction);


                transaction.Commit();

                return true;
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

        #region Update Ward Monthly Data
        /// <summary>
        /// Update the KPI Monthly target
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public bool UpdateKPIData(KPIWardMonthlyData kpiMonthlyData)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIWardMonthlyData_Update);

                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, kpiMonthlyData.WardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyData.KpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyData.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyData.TargetMonth);
                if (kpiMonthlyData.Nominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiMonthlyData.Nominator);
                }

                if (kpiMonthlyData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiMonthlyData.Denominator);
                }

                if (kpiMonthlyData.YTDValue == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, kpiMonthlyData.YTDValue);
                }

                int i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();

                return true;

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

        #region Update KPI YTD Target
        /// <summary>
        /// Update KPI YTD Target
        /// </summary>
        /// <param name="kpiMonthlyTarget"></param>
        /// <returns></returns>
        public bool UpdateKPIYTDTarget(KPIHospitalYTDTarget kpiYTDTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_KPI_YTD_Target_Update);
               
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiYTDTarget.KpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiYTDTarget.HospitalId);
                db.AddInParameter(dbCommand, "@TargetYTD", DbType.Date, kpiYTDTarget.TargetYTD);
                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiYTDTarget.TargetDescription);
                if (kpiYTDTarget.TargetGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, kpiYTDTarget.TargetGreen);
                }
                if (kpiYTDTarget.TargetAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, kpiYTDTarget.TargetAmber);
                }
                int i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();

                return true;

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

        #region Get Ward Level KPI Initial Data
        /// <summary>
        /// Get Ward Level KPI Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetWardLevelKPIInitialData(int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_Level_KPI_Initial_Data);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

                DataSet dsWardLevelKPIInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardLevelKPIInitialData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Get Hospital Level KPI Initial Data
        /// <summary>
        /// Get Hospital Level KPI Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetHospitalLevelKPIInitialData(int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_Level_KPI_Initial_Data);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

                DataSet dsHospitalLevelKPIInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsHospitalLevelKPIInitialData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Ward Level KPI Search
        /// <summary>
        /// Ward Level KPI Search
        /// </summary>
        /// <returns></returns>
        public DataSet WardLevelKPISearch(int hospitalId, int wardId, int kpiId, DateTime startDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_Level_KPI_Search);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, wardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, startDate);


                DataSet dsWardLevelKPI = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardLevelKPI;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Hospital Level KPI Search
        /// <summary>
        /// Hospital KPI Search
        /// </summary>
        /// <returns></returns>
        public DataSet HospitalKPISearch(int hospitalId, int kpiId, DateTime startDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_Level_KPI_Search);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@TargetYTD", DbType.Date, startDate);


                DataSet dsHospitalLevelKPI = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsHospitalLevelKPI;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Get Hospital YTD KPI Data
        /// <summary>
        /// Get Ward KPI Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetHospitalYTDKPIData(int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Hospital_YTD_KPI_Data_View);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);                
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);               
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsWardKPIData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardKPIData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Insert KPI YTD Data
        /// <summary>
        /// Insert KPI YTD Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertKPIYTDData(KPIHospitalYTDData kpiYTDData)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIHospitalYTDData_Insert);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiYTDData.HospitalId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiYTDData.KpiId);
                db.AddInParameter(dbCommand, "@TargetYearToDate", DbType.Date, kpiYTDData.TargetYearToDate);
                if (kpiYTDData.Nominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiYTDData.Nominator);
                }
                if (kpiYTDData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiYTDData.Denominator);
                }

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Update KPI YTD Data
        /// <summary>
        /// Update the KPI YTD Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public bool UpdateKPIYTDData(KPIHospitalYTDData kpiData)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIHospitlaYTDData_Update);
               
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiData.KpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiData.HospitalId);
                db.AddInParameter(dbCommand, "@TargetYearToDate", DbType.Date, kpiData.TargetYearToDate);
                if (kpiData.Nominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiData.Nominator);
                }

                if (kpiData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiData.Denominator);
                }

                int i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();

                return true;

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

        #region Get Specialty KPI Target
        /// <summary>
        /// Get the Specialty KPI Target
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyKPITarget(int specialtyId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_Target_View);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, specialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsWardKPITas = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardKPITas;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Insert Monthly Specialty KPI
        /// <summary>
        /// Insert the Monthky Specialty KPI target
        /// </summary>
        /// <param name="kpiMonthlyTarget"></param>
        /// <returns></returns>
        public int InsertSpecialtyKPIMonthlyTarget(KPISpecialtyMonthlyTarget kpiMonthlyTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_Monthly_Target_Insert);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiMonthlyTarget.SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyTarget.KPIId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyTarget.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyTarget.TargetMonth);
                db.AddInParameter(dbCommand, "@SpecialtyTargetDescription", DbType.String, kpiMonthlyTarget.SpecialtyTargetDescription);
                if (kpiMonthlyTarget.SpecialtyGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@SpecialtyGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@SpecialtyGreen", DbType.Decimal, kpiMonthlyTarget.SpecialtyGreen);
                }

                if (kpiMonthlyTarget.SpecialtyAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@SpecialtyAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@SpecialtyAmber", DbType.Decimal, kpiMonthlyTarget.SpecialtyAmber);
                }

                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiMonthlyTarget.TargetDescriptionYTD);

                if (kpiMonthlyTarget.TargetGreenYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, kpiMonthlyTarget.TargetGreenYTD);
                }

                if (kpiMonthlyTarget.TargetAmberYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, kpiMonthlyTarget.TargetAmberYTD);
                }



                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Update Specialty KPI Monthly Target
        /// <summary>
        /// Update Specialty KPI Monthly Target
        /// </summary>
        /// <param name="kpiMonthlyTarget"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPIMonthlyTarget(KPISpecialtyMonthlyTarget kpiMonthlyTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_Monthly_Target_Update);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiMonthlyTarget.SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyTarget.KPIId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyTarget.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyTarget.TargetMonth);
                db.AddInParameter(dbCommand, "@SpecialtyTargetDescription", DbType.String, kpiMonthlyTarget.SpecialtyTargetDescription);
                if (kpiMonthlyTarget.SpecialtyGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@SpecialtyGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@SpecialtyGreen", DbType.Decimal, kpiMonthlyTarget.SpecialtyGreen);
                }

                if (kpiMonthlyTarget.SpecialtyAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@SpecialtyAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@SpecialtyAmber", DbType.Decimal, kpiMonthlyTarget.SpecialtyAmber);
                }

                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiMonthlyTarget.TargetDescriptionYTD);

                if (kpiMonthlyTarget.TargetGreenYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetGreen", DbType.Decimal, kpiMonthlyTarget.TargetGreenYTD);
                }

                if (kpiMonthlyTarget.TargetAmberYTD == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDTargetAmber", DbType.Decimal, kpiMonthlyTarget.TargetAmberYTD);
                }

                int i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();

                return true;

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

        #region Get Specialty KPI Data
        /// <summary>
        /// Get Specialty KPI Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyKPIData(int specialtyId, int kpiId, int hospitalId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_Data_View);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, specialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsWardKPIData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardKPIData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Insert Specialty KPI Data
        /// <summary>
        /// Insert Specialty KPI Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertSpecialtyKPIData(KPISpecialtyMonthlyData kpiMonthlyData)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_Monthly_Data_Insert);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiMonthlyData.SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyData.KPIId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyData.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyData.TargetMonth);

                if (kpiMonthlyData.Numerator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiMonthlyData.Numerator);
                }
                if (kpiMonthlyData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiMonthlyData.Denominator);
                }
                if (kpiMonthlyData.YTDValue == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, kpiMonthlyData.YTDValue);
                }

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Update Specialty Monthly Data
        /// <summary>
        /// Update the Specialty KPI Monthly target
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPIData(KPISpecialtyMonthlyData kpiMonthlyData)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_Monthly_Data_Update);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiMonthlyData.SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiMonthlyData.KPIId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, kpiMonthlyData.HospitalId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, kpiMonthlyData.TargetMonth);
                if (kpiMonthlyData.Numerator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiMonthlyData.Numerator);
                }
                if (kpiMonthlyData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiMonthlyData.Denominator);
                }
                if (kpiMonthlyData.YTDValue == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, kpiMonthlyData.YTDValue);
                }

                int i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();

                return true;

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

        #region Get Specialty Level KPI Initial Data
        /// <summary>
        /// Get Specialty Level KPI Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelKPIInitialData(int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Level_KPI_Initial_Data);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

                DataSet dsSpecialtyLevelKPIInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyLevelKPIInitialData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Specialty Level KPI Search
        /// <summary>
        /// Specialty Level KPI Search
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyLevelKPISearch(int hospitalId, int specialtyId, int kpiId, DateTime startDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Level_KPI_Search);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, specialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, startDate);


                DataSet dsWardLevelKPI = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardLevelKPI;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Get Specialty KPI YTD Target
        /// <summary>
        /// Get Specialty KPI YTD Target
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyKPIYTDTarget(int specialtyId, int kpiId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_YTD_Target_View);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, specialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsSpecialtyKPITas = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyKPITas;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Insert Specialty KPI YTD Target
        /// <summary>
        /// Insert Specialty KPI YTD Target
        /// </summary>
        /// <param name="kpiYTDTarget"></param>
        /// <returns></returns>
        public int InsertSpecialtyKPIYTDTarget(KPISpecialtyYTDTarget kpiYTDTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_YTD_Target_Insert);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiYTDTarget.SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiYTDTarget.KPIId);
                db.AddInParameter(dbCommand, "@TargetYTD", DbType.Date, kpiYTDTarget.TargetYTD);
                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiYTDTarget.YTDTargetDescription);
                if (kpiYTDTarget.YTDGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDGreen", DbType.Decimal, kpiYTDTarget.YTDGreen);
                }

                if (kpiYTDTarget.YTDAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDAmber", DbType.Decimal, kpiYTDTarget.YTDAmber);
                }

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Update Specialty KPI YTD Target
        /// <summary>
        /// Update Specialty KPI YTD Target
        /// </summary>
        /// <param name="kpiMonthlyTarget"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPIYTDTarget(KPISpecialtyYTDTarget kpiYTDTarget)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_KPI_YTD_Target_Update);

                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiYTDTarget.KPIId);
                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiYTDTarget.SpecialtyId);
                db.AddInParameter(dbCommand, "@TargetYTD", DbType.Date, kpiYTDTarget.TargetYTD);
                db.AddInParameter(dbCommand, "@YTDTargetDescription", DbType.String, kpiYTDTarget.YTDTargetDescription);
                if (kpiYTDTarget.YTDGreen == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDGreen", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDGreen", DbType.Decimal, kpiYTDTarget.YTDGreen);
                }

                if (kpiYTDTarget.YTDAmber == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@YTDAmber", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@YTDAmber", DbType.Decimal, kpiYTDTarget.YTDAmber);
                }

                int i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();

                return true;

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

        #region Get Specialty YTD KPI Data
        /// <summary>
        /// Get Specialty YTD KPI Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyYTDKPIData(int kpiId, int specialtyId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_YTD_KPI_Data_View);
                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, specialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@StartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.Date, endDate);

                DataSet dsSpecialtyKPIData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyKPIData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Update Specialty KPI YTD Data
        /// <summary>
        /// Update Specialty KPI YTD Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public bool UpdateSpecialtyKPIYTDData(KPISpecialtyYTDData kpiData)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_YTD_KPI_Data_Update);

                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiData.KPIId);
                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiData.SpecialtyId);
                db.AddInParameter(dbCommand, "@TargetYearToDate", DbType.Date, kpiData.TargetYearToDate);
                if (kpiData.Numerator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiData.Numerator);
                }

                if (kpiData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiData.Denominator);
                }

                int i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();

                return true;

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

        #region Insert Specialty KPI YTD Data
        /// <summary>
        /// Insert Specialty KPI YTD Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertSpecialtyKPIYTDData(KPISpecialtyYTDData kpiYTDData)
        {
            try
            {
                int id = 0;
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                //Update the KPI target

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_YTD_KPI_Data_Insert);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, kpiYTDData.SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiYTDData.KPIId);
                db.AddInParameter(dbCommand, "@TargetYearToDate", DbType.Date, kpiYTDData.TargetYearToDate);

                if (kpiYTDData.Numerator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, kpiYTDData.Numerator);
                }

                if (kpiYTDData.Denominator == double.MinValue)
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, kpiYTDData.Denominator);
                }
                else
                {
                    db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, DBNull.Value);
                }

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 10);
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

        #region Specialty Level KPI YTD Search
        /// <summary>
        /// Specialty Level KPI YTD Search
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyYTDKPISearch(int specialtyId, int kpiId, DateTime startDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_YTD_KPI_Data_Search);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, specialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@TargetYTD", DbType.Date, startDate);


                DataSet dsSpecialtyLevelKPI = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyLevelKPI;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Ward Level KPI Search
        /// <summary>
        /// Ward Level KPI Search
        /// </summary>
        /// <returns></returns>
        public DataSet WardLevelBulkWardSearch(int hospitalId, int wardId, int kpiId, DateTime startDate, int typeId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Ward_Level_BulkWard_Search);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@WardId", DbType.Int32, wardId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, typeId);


                DataSet dsWardLevelKPI = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardLevelKPI;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Specialty Level Bulk Specialty Search
        /// <summary>
        /// Specialty Level Bulk Specialty Search
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyLevelBulkSpecialtySearch(int hospitalId, int SpecialtyId, int kpiId, DateTime startDate, int typeId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Level_BulkSpecialty_Search);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, typeId);


                DataSet dsSpecialtyLevelBulkSpecialty = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyLevelBulkSpecialty;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Specialty Level Bulk KPI Search
        /// <summary>
        /// Specialty Level Bulk KPI Search
        /// </summary>
        /// <returns></returns>
        public DataSet SpecialtyLevelBulkKPISearch(int hospitalId, int SpecialtyId, int kpiId, DateTime startDate, int typeId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Level_BulkKPI_Search);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, SpecialtyId);
                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, typeId);


                DataSet dsSpecialtyLevelBulkKPI = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyLevelBulkKPI;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Insert Bulk Specialty Target
        /// <summary>
        /// Insert Bulk Specialty Target
        /// </summary>
        /// <param name="dsSpecialtyKPITarget"></param>
        /// <returns></returns>
        public int InsertBulkSpecialtyTarget(DataSet dsSpecialtyKPITarget,DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulkSpecialtyTarget(int.Parse(dsSpecialtyKPITarget.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsSpecialtyKPITarget.Tables[0].Rows[0]["KPIId"].ToString()),fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPISpecialtyMonthlyTarget";
                    sqlBulkCopy.WriteToServer(dsSpecialtyKPITarget.Tables[0]);

                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Delete Bulk Specialty Target

        public bool DeleteBulkSpecialtyTarget(int hospitalId, int kpiId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIBulkSpecialtyTarget_Delete);

                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);

                db.ExecuteNonQuery(dbCommand, transaction);


                transaction.Commit();

                return true;
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

        #region Insert Bulk KPI Specialty Target
        /// <summary>
        /// Insert Bulk Specialty Target
        /// </summary>
        /// <param name="dsSpecialtyKPITarget"></param>
        /// <returns></returns>
        public int InsertBulkKPISpecialtyTarget(DataSet dsSpecialtyKPITarget, DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulkKPISpecialtyTarget(int.Parse(dsSpecialtyKPITarget.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsSpecialtyKPITarget.Tables[0].Rows[0]["SpecialtyId"].ToString()),fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPISpecialtyMonthlyTarget";
                    sqlBulkCopy.WriteToServer(dsSpecialtyKPITarget.Tables[0]);

                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Delete Bulk KPI Specialty Target

        public bool DeleteBulkKPISpecialtyTarget(int hospitalId, int specialtyId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_BulkKPISpecialtyTarget_Delete);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, specialtyId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);

                db.ExecuteNonQuery(dbCommand, transaction);


                transaction.Commit();

                return true;
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

        #region Insert Bulk Specialty Data
        /// <summary>
        /// Insert Bulk Specialty Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertBulkSpecialtyData(DataSet dsSpecialtyKPIData, DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulKSpecialtyData(int.Parse(dsSpecialtyKPIData.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsSpecialtyKPIData.Tables[0].Rows[0]["KPIId"].ToString()),fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPISpecialtyMonthlyData";
                    sqlBulkCopy.WriteToServer(dsSpecialtyKPIData.Tables[0]);

                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Delete Bulk Specialty Data

        public bool DeleteBulKSpecialtyData(int hospitalId, int kpiId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPIBulkSpecialtyData_Delete);

                db.AddInParameter(dbCommand, "@KPIId", DbType.Int32, kpiId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);

                db.ExecuteNonQuery(dbCommand, transaction);


                transaction.Commit();

                return true;
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

        #region Insert Bulk Specialty KPI Data
        /// <summary>
        /// Insert Bulk Specialty KPI Data
        /// </summary>
        /// <param name="kpiMonthlyData"></param>
        /// <returns></returns>
        public int InsertBulkSpecialtyKPIData(DataSet dsSpecialtyKPIData,DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (DeleteBulKSpecialtyKPIData(int.Parse(dsSpecialtyKPIData.Tables[0].Rows[0]["HospitalId"].ToString()), int.Parse(dsSpecialtyKPIData.Tables[0].Rows[0]["SpecialtyId"].ToString()),fromDate,toDate))
                {

                    System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(GetConnectionString(), System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                    sqlBulkCopy.DestinationTableName = "tblKPISpecialtyMonthlyData";
                    sqlBulkCopy.WriteToServer(dsSpecialtyKPIData.Tables[0]);

                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        #endregion

        #region Delete Bulk Specialty KPI Data

        public bool DeleteBulKSpecialtyKPIData(int hospitalId, int SpecialtyId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_BulkSpecialtyKPIData_Delete);

                db.AddInParameter(dbCommand, "@SpecialtyId", DbType.Int32, SpecialtyId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, toDate);

                db.ExecuteNonQuery(dbCommand, transaction);


                transaction.Commit();

                return true;
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

        #region Get CSV Upload Initial Data
        /// <summary>
        /// Get  CSV Upload Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetCSVUploadInitialData()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand("uspGetCSVUploadInitialData");

                DataSet dsCSVUploadInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsCSVUploadInitialData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Update CSV Ward Data
        /// <summary>
        /// Update  CSV Ward Data
        /// </summary>
        /// <returns></returns>
        public bool UpdateCSVWardData(DataTable dtWardData)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand("uspUpdateCSVWardDataUpload");

                db.AddInParameter(dbCommand, "@WardCode", DbType.String, "WardCode", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@KPINo", DbType.String, "KPINo", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, "HospitalId", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.DateTime, "TargetMonth", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, "Numerator", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, "Denominator", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, "YTDValue", DataRowVersion.Current);

                DataSet ds = new DataSet();
                ds.Tables.Add(dtWardData);
                db.UpdateDataSet(ds, dtWardData.TableName, dbCommand, null, null, transaction);
                transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Update CSV Specialty Data
        /// <summary>
        /// Update  CSV Specialty Data
        /// </summary>
        /// <returns></returns>
        public bool UpdateCSVSpecialtyData(DataTable dtSpecialtyData)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand("uspUpdateCSVSpecialtyDataUpload");

                db.AddInParameter(dbCommand, "@SpecialtyCode", DbType.String, "SpecialtyCode", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@KPINo", DbType.String, "KPINo", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, "HospitalId", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.DateTime, "TargetMonth", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, "Numerator", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@Denominator", DbType.Decimal, "Denominator", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@YTDValue", DbType.Decimal, "YTDValue", DataRowVersion.Current);

                DataSet ds = new DataSet();
                ds.Tables.Add(dtSpecialtyData);
                db.UpdateDataSet(ds, dtSpecialtyData.TableName, dbCommand, null, null, transaction);
                transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Update Benchmarck CSV Data
        /// <summary>
        /// Update benchmark CSV Data
        /// </summary>
        /// <returns></returns>
        public bool UpdateBenchmarckCSVData(DataTable dtData)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand("uspUpdateBenchmarkCSVDataUpload");

                db.AddInParameter(dbCommand, "@TrustCode", DbType.String, "TrustCode", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@TrustName", DbType.String, "TrustName", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@KPINo", DbType.String, "KPINo", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@TargetMonth", DbType.DateTime, "TargetMonth", DataRowVersion.Current);
                db.AddInParameter(dbCommand, "@Numerator", DbType.Decimal, "Numerator", DataRowVersion.Current);
                
                DataSet ds = new DataSet();
                ds.Tables.Add(dtData);
                db.UpdateDataSet(ds, dtData.TableName, dbCommand, null, null, transaction);
                transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Get Benchmark Initial Data
        /// <summary>
        /// Get Benchmark Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetBenchmarkInitialData()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand("uspGetBenchmarkInitialData");

                DataSet dsInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsInitialData;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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

        #region Get Benchmark Report Data
        /// <summary>
        /// Get Benchmark Report Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetBenchmarkReportData(DateTime FromDate, DateTime ToDate, string KPINo, string TrustCodeList, int HospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand("uspGetBenchmarkReportData");

                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, FromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, ToDate);
                db.AddInParameter(dbCommand, "@KPINo", DbType.String, KPINo);
                db.AddInParameter(dbCommand, "@TrustCodeList", DbType.String, TrustCodeList);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, HospitalId);
                
                DataSet ds = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return ds;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
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
