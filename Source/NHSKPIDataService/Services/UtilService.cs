using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NHSKPIDataService.Util;
using System.Data;
using System.Data.Common;

namespace NHSKPIDataService.Services
{
    public class UtilService
    {
        #region private varibles

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Get Specialty Level Target Initial Data
        /// <summary>
        /// Get Specialty Level Target Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelTargetInitialData(int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Level_KPI_Initial_Data);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

                DataSet dsSpecialtyLevelTargetInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyLevelTargetInitialData;

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

        #region Get Specialty Level YTD Target Initial Data
        /// <summary>
        /// Get Specialty Level YTD Target Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelYTDTargetInitialData()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Specialty_Level_YTD_Target_Initial_Data);

                DataSet dsSpecialtyLevelTargetInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsSpecialtyLevelTargetInitialData;

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

        #region Get Dash Board Data
        /// <summary>
        /// Get Dash Board Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetDashBoardData(int hospitalId, int WardGroupId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                DateTime targetMonthFrom = new DateTime(((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1), 4, 1);
                DateTime targetMonthTo = targetMonthFrom.AddYears(1);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_Dash_Board_Data);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@WardGroupId", DbType.Int32, WardGroupId);
                db.AddInParameter(dbCommand, "@TargetMonthFrom", DbType.Date, targetMonthFrom);
                db.AddInParameter(dbCommand, "@TargetMonthTo", DbType.Date, targetMonthTo);
                DataSet dsDashBoardData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsDashBoardData;

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

        #region Get Dash Board Specialty Data
        /// <summary>
        /// Get Dash Board Specialty Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetDashBoardSpecialtyData(int hospitalId, int WardGroupId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                DateTime targetMonthFrom = new DateTime(((DateTime.Now.Month >= 4 ? DateTime.Now.Year + 1 : DateTime.Now.Year) - 1), 4, 1);
                DateTime targetMonthTo = targetMonthFrom.AddYears(1);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_Dash_Board_Specialty_Data);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@WardGroupId", DbType.Int32, WardGroupId);
                db.AddInParameter(dbCommand, "@TargetMonthFrom", DbType.Date, targetMonthFrom);
                db.AddInParameter(dbCommand, "@TargetMonthTo", DbType.Date, targetMonthTo);
                DataSet dsDashBoardData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsDashBoardData;

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

        public List<Email> GetEmailList(int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_Email_List);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                
                var results = db.ExecuteReader(dbCommand);
                transaction.Commit();

                List<Email> list = new List<Email>();
                Email item;
                int intVal;

                if (results != null)
                {
                    while (results.Read())
                    {
                        item = new Email();

                        if (int.TryParse(results["Id"].ToString(), out intVal))
                            item.Id = intVal;

                        item.EmailAddress = results["Email"].ToString();
                        item.Description = results["Description"].ToString();
                        item.HospitalId = hospitalId;

                        list.Add(item);
                    }
                }
                return list;
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

        public bool InsertEmailToBucket(Email email)
        {
            try
            {
                bool status = false;

                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Insert_Email);

                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, email.Id);
                db.AddInParameter(dbCommand, "@Email", DbType.String, email.EmailAddress);
                db.AddInParameter(dbCommand, "@Description", DbType.String, email.Description);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, email.HospitalId);

                var results = db.ExecuteNonQuery(dbCommand);
                status = true;
                transaction.Commit();

                return status;
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
    }
}
