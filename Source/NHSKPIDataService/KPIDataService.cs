using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NHSKPIDataService.Util;
using System.Data;
using System.Data.Common;


namespace NHSKPIDataService
{
    public class KPIDataService
    {
        #region private varibles

        private DbTransaction transaction;
        private DbConnection connection;       

        #endregion

        #region Hospital Services

        #region Add Hospital
        /// <summary>
        /// This function will add the hospital to data base.
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>int</returns>
        public int AddHospital(Hospital hospital)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
              
                int Id = hospital.Add(db, transaction);
                transaction.Commit();

                return Id;
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

        public int GetHospitalIdFromCode(Hospital hospital)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = hospital.GetID(db, transaction);
                transaction.Commit();

                return Id;
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

        #region Update Hospital
        /// <summary>
        /// This function will update the hospital to data base.
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>true or false</returns>
        public bool UpdateHospital(Hospital hospital)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                
                bool status = hospital.Update(db, transaction);
                transaction.Commit();

                return status;
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

        #region View Hospital
        /// <summary>
        /// View the Hospital Details
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>Hopital Object</returns>
        /// 
        public Hospital ViewHospital(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Hospital hospital = new Hospital();
                hospital.Id = Id;
                hospital.View(db, transaction);

                transaction.Commit();
                return hospital;
               
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion

        #region Search Hospital
        /// <summary>
        /// Search Hospital
        /// </summary>
        /// <param name="hospitalName"></param>
        /// <param name="hospitalCode"></param>
        /// <returns>Hospital result Data Set</returns>
        public DataSet SearchHospital(string hospitalName, string hospitalCode, bool isActive, int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Hospital hospital = new Hospital();

                DataSet dsSearchResult = hospital.Search(db, transaction, hospitalName, hospitalCode, isActive, hospitalId);

                transaction.Commit();
                return dsSearchResult;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion

        #region View All Hospital
        /// <summary>
        /// View the all Hospital Details
        /// </summary>
        /// <returns>Hospital result Data set</returns>
        public DataSet ViewAllHospital()
        {
            try
            {
                DataSet hospitalList = new DataSet();
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Hospital hospital = new Hospital();

                hospitalList = hospital.ViewAllHospital(db, transaction);

                transaction.Commit();

                return hospitalList;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion

        #endregion

        #region Ward Group Services

        #region Add Ward Group
        /// <summary>
        /// Add Ward Group
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>int</returns>
         public int AddWardGroup(WardGroup wardGroup)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = wardGroup.AddWardGroup(db, transaction);                    
                transaction.Commit();

                return Id;
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

        #region Update Ward Group
        /// <summary>
        /// Update Ward Group
        /// </summary>
        /// <param name="wardGroup"></param>
        /// <returns>true or false</returns>
         public bool UpdateWardGroup(WardGroup wardGroup)
         {
             try
             {
                 Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                 connection = db.CreateConnection();
                 connection.Open();
                 transaction = connection.BeginTransaction();

                 bool status = wardGroup.UpdateWardGroup(db, transaction);
                 transaction.Commit();

                 return status;
             }
             catch (Exception ex)
             {
                 transaction.Rollback();
                 if (ex.InnerException == null)
                 { 
                     throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); 
                 }
                 else
                 {
                     throw ex; 
                 }
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

        #region Search Ward Group
        /// <summary>
         /// Search Ward Group
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hospitalid"></param>
        /// <param name="isActive"></param>
        /// <returns>result WardGroup dataset</returns>
         public DataSet SearchWardGroup(string name, int hospitalid, bool isActive)
         {
             try
             {
                 Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                 connection = db.CreateConnection();
                 connection.Open();
                 transaction = connection.BeginTransaction();
                 WardGroup wardGroup = new WardGroup();
                 DataSet dsWardGroup =  wardGroup.SearchWardGroup(db, transaction,name,hospitalid,isActive);
                 transaction.Commit();

                 return dsWardGroup;
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

        #region View Ward Group
        /// <summary>
        /// View Ward Group
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>WardGroup object</returns>
         public WardGroup ViewWardGroup(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                WardGroup wardGroup = new WardGroup();
                wardGroup.Id = Id;
                wardGroup.ViewWardGroup(db, transaction);

                transaction.Commit();
                return wardGroup;
               
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
         #endregion
        
        #endregion

        #region Ward Service

         #region Bulk Upload ward

         public bool BulkUploadWardAndWardGroup(DataTable dtWardData)
         {
             try
             {
                 Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                 connection = db.CreateConnection();
                 connection.Open();

                 DbCommand dbCommand = null;
                 DataSet ds = null;

                 transaction = connection.BeginTransaction();

                 //Add ward group
                 dbCommand = db.GetStoredProcCommand("uspAddUpdateWardData");
                 db.AddInParameter(dbCommand, "@Id", DbType.Int32, "Id", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, "HospitalId", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@WardCode", DbType.String, "WardCode", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@WardName", DbType.String, "WardName", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@WardGroupId", DbType.Int32, "WardGroupId", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@WardGroupName", DbType.String, "WardGroupName", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@Description", DbType.String, "Description", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, "IsActive", DataRowVersion.Current);

                 ds = new DataSet();
                 ds.Tables.Add(dtWardData);

                 int status = 0;
                 status = db.UpdateDataSet(ds, dtWardData.TableName, dbCommand, null, null, transaction);

                 transaction.Commit();

                 return status > 0 ? true : false;
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

         #region Bulk Upload Specialty

         public bool BulkUploadSpecialty(DataTable dtSpecialtyData)
         {
             try
             {
                 Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                 connection = db.CreateConnection();
                 connection.Open();

                 DbCommand dbCommand = null;
                 DataSet ds = null;

                 transaction = connection.BeginTransaction();

                 //Add ward group
                 dbCommand = db.GetStoredProcCommand("uspAddUpdateSpecialtyData");
                 db.AddInParameter(dbCommand, "@Id", DbType.Int32, "Id", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, "HospitalId", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@SpecialtyCode", DbType.String, "SpecialtyCode", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@Specialty", DbType.String, "Specialty", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@GroupId", DbType.String, "GroupId", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@NationalSpecialty", DbType.String, "NationalSpecialty", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@NationalCode", DbType.String, "NationalCode", DataRowVersion.Current);
                 db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, "IsActive", DataRowVersion.Current);

                 ds = new DataSet();
                 ds.Tables.Add(dtSpecialtyData);

                 int status = 0;
                 status = db.UpdateDataSet(ds, dtSpecialtyData.TableName, dbCommand, null, null, transaction);

                 transaction.Commit();

                 return status > 0 ? true : false;
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


         #region Add Ward
         /// <summary>
        /// Add Ward
        /// </summary>
        /// <param name="ward"></param>
        /// <returns>int</returns>
        public int AddWard(Ward ward)
          {
              try
              {
                  Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                  connection = db.CreateConnection();
                  connection.Open();
                  transaction = connection.BeginTransaction();

                  int Id = ward.AddWard(db, transaction);                     
                  transaction.Commit();

                  return Id;
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

        #region Update Ward
        /// <summary>
        /// Update Ward
        /// </summary>
        /// <param name="ward"></param>
        /// <returns>true or false</returns>
        public bool UpdateWard(Ward ward)
          {
              try
              {
                  Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                  connection = db.CreateConnection();
                  connection.Open();
                  transaction = connection.BeginTransaction();

                  bool status = ward.UpdateWard(db, transaction);
                  transaction.Commit();

                  return status;
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

        #region Search Ward
        /// <summary>
        /// Search Ward
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="hospitalId"></param>
        /// <param name="wardGroupId"></param>
        /// <param name="isActive"></param>
        /// <returns>search result ward dataset</returns>
        public DataSet SearchWard(string name, string code, int hospitalId, int wardGroupId, bool isActive)
          {
              try
              {
                  Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                  connection = db.CreateConnection();
                  connection.Open();
                  transaction = connection.BeginTransaction();

                  Ward ward = new Ward();

                  DataSet dsSearchResult = ward.SearchWard(db, transaction, name, code, hospitalId, wardGroupId,isActive);

                  transaction.Commit();
                  return dsSearchResult;
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

        #region View ward
        /// <summary>
        /// View ward
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>ward dataset</returns>
        public Ward ViewWard(int Id)
          {
              try
              {
                  Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                  connection = db.CreateConnection();
                  connection.Open();
                  transaction = connection.BeginTransaction();

                  Ward ward = new Ward();
                  ward.WardId = Id;
                  ward.ViewWard(db, transaction);

                  transaction.Commit();
                  return ward;

              }
              catch (Exception ex)
              {
                  if (ex.InnerException == null)
                  { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                  else
                  { throw ex; }
              }
          }
        #endregion
                
        #endregion

        #region KPI Services

        #region View All Wards
        /// <summary>
        /// View All Wards
        /// </summary>
        /// <returns>result Ward dataset</returns>
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

        #region Add KPI Group
        /// <summary>
        /// Add KPI Group
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>int</returns>
        public int AddKPIGroup(KPIGroup kpiGroup)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = kpiGroup.Add(db, transaction);
                transaction.Commit();

                return Id;
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

        #region Update KPI Group
        /// <summary>
        /// Update KPI Group
        /// </summary>
        /// <param name="kpiGroup"></param>
        /// <returns>true or false</returns>
        public bool UpdateKPIGroup(KPIGroup kpiGroup)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                bool status = kpiGroup.Update(db, transaction);
                transaction.Commit();

                return status;
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

        #region Search KPI Group
        /// <summary>
        /// Search KPI Group
        /// </summary>
        /// <param name="kpiGroupName"></param>
        /// <param name="isActive"></param>
        /// <returns>Search Result DataSet</returns>
        public DataSet SearchKPIGroup(string kpiGroupName, bool isActive)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                KPIGroup kpiGroup = new KPIGroup();

                DataSet dsResult = kpiGroup.Search(db, transaction, kpiGroupName,isActive);
                transaction.Commit();

                return dsResult;
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

        #region View KPI Group
        /// <summary>
        /// View KPI Group
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>KPIGroup object</returns>
        public KPIGroup ViewKPIGroup(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                KPIGroup kpiGroup = new KPIGroup();
                kpiGroup.Id = Id;
                kpiGroup.View(db, transaction);

                transaction.Commit();
                return kpiGroup;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }

        #endregion

        #region Add KPI
        /// <summary>
        /// This function will add the KPI to data base.
        /// </summary>
        /// <param name="kpiGroup"></param>
        /// <returns>int</returns>
        public int AddKPI(KPI kpi)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = kpi.Add(db, transaction);
                transaction.Commit();

                return Id;
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

        #region Update KPI
        /// <summary>
        /// Update KPI
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>true or false</returns>
        public bool UpdateKPI(KPI kpi)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                bool status = kpi.Update(db, transaction);
                transaction.Commit();

                return status;
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

        #region Search KPI
        /// <summary>
        /// Search KPI
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>Search result KPI dataset </returns>
        public DataSet SearchKPI(string kpiNo, string kpiDescription, int kpiGrouoId, bool isActive)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                KPI kpi = new KPI();
                DataSet dsResult = kpi.Search(db, transaction,kpiNo, kpiDescription, kpiGrouoId, isActive);
                transaction.Commit();

                return dsResult;
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

        #region View KPI
        /// <summary>
        /// View KPI
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>KPI object</returns>
        public KPI ViewKpi(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                KPI kpi = new KPI();
                kpi.Id = Id;
                kpi.View(db, transaction);

                transaction.Commit();

                return kpi;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion
                
        #region Add KPI Deails
        /// <summary>
        /// Add KPI Deails
        /// </summary>
        /// <param name="kpiDetails"></param>
        /// <returns>int</returns>
        public int AddKPIDeails(KPIDetails kpiDetails)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = kpiDetails.Add(db, transaction);
                transaction.Commit();

                return Id;
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

        #region Update KPI Details
        /// <summary>
        /// Update KPI Details
        /// </summary>
        /// <param name="kpiDetails"></param>
        /// <returns>true or false</returns>
        public bool UpdateKPIDetails(KPIDetails kpiDetails)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                kpiDetails.Update(db, transaction);
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

        #region View KPIDetails
        /// <summary>
        /// View KPI Details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>KPIDetails object</returns>
        public KPIDetails ViewKPIDetails(int kpiId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                KPIDetails kpiDetails = new KPIDetails();
                kpiDetails.KpiId = kpiId;
                kpiDetails.View(db, transaction);

                transaction.Commit();
                return kpiDetails;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion

        #region Load KPI No
        /// <summary>
        /// Load KPI No
        /// </summary>
        /// <returns>KPI No dataset</returns>
        public DataSet LoadKPINo()
        {
            try
            {
                KPIDetails kpiDetails = new KPIDetails();

                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DataSet dsKpiDetails = kpiDetails.AllKPINo(db, transaction);
                transaction.Commit();

                return dsKpiDetails;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion

        #region Get Auto KPI No
        /// <summary>
        /// Get Auto KPI No
        /// </summary>
        /// <returns>Auto KPI No</returns>
        public int GetAutoKPINo()
        {
            try
            {
                KPIDetails kpiDetails = new KPIDetails();

                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int kpiNo = kpiDetails.GetAutoKPINo(db, transaction);
                transaction.Commit();

                return kpiNo;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion
        
        #region Load KPI Apply For
        /// <summary>
        /// Load KPI applling target
        /// </summary>
        /// <returns></returns>
        public DataSet LoadKPIApplyFor()
        {
            try
            {
                KPI kpi = new KPI();

                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DataSet dsKpi = kpi.AllKPIApplyFor(db, transaction);
                transaction.Commit();

                return dsKpi;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion

        #region Get KPI For Ward Target Level
        /// <summary>
        /// Get KPI For Ward Target Level
        /// </summary>
        /// <returns>KPI dataset</returns>
        public DataSet GetKPIForWardTargetLevel(int target)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_WardTarget_Level_Data);
                db.AddInParameter(dbCommand, "@TargetId", DbType.Int32, target);

                DataSet dsWardInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardInitialData;

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

        #region Get KPI For Data Target Level
        /// <summary>
        /// Get KPI For Data Level
        /// </summary>
        /// <returns>KPI dataset</returns>
        public DataSet GetKPIForWardDataLevel(int target)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_KPI_WardData_Level_Data);
                db.AddInParameter(dbCommand, "@TargetId", DbType.Int32, target);

                DataSet dsWardInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardInitialData;

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

        #region Incomplete KPI

        /// <summary>
        /// Gets the incomplete ward KPI.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="targetApplyFor">The target apply for.</param>
        /// <returns></returns>
        public DataSet GetIncompleteWardKPI(Database db, DbTransaction transaction, int hospitalId, int targetApplyFor)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_Incomplete_Ward_KPI);

            db.AddInParameter(dbCommand, "@TargetId", DbType.Int32, targetApplyFor);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

            return db.ExecuteDataSet(dbCommand, transaction);


        }

        /// <summary>
        /// Gets the incomplete speciality KPI.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="hospitalId">The hospital identifier.</param>
        /// <param name="targetApplyFor">The target apply for.</param>
        /// <returns></returns>
        public DataSet GetIncompleteSpecialityKPI(Database db, DbTransaction transaction, int hospitalId, int targetApplyFor)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_Incomplete_Speciality_KPI);

            db.AddInParameter(dbCommand, "@TargetId", DbType.Int32, targetApplyFor);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

            return db.ExecuteDataSet(dbCommand, transaction);


        }
        #endregion

        #endregion

        #region Specialty Services

        #region Add Specialty
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specialty"></param>
        /// <returns></returns>
        public int AddSpecialty(Specialty specialty)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = specialty.Add(db, transaction);
                transaction.Commit();

                return Id;
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

        #region Update Specialty
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns></returns>
        public bool UpdateSpecialty(Specialty specialty)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

               bool status = specialty.Update(db, transaction);
                transaction.Commit();

                return status;
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

        #region Search Specialty
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns></returns>
        public DataSet SearchSpecialty(string specialtyName, bool isActive)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Specialty specialty = new Specialty();
                DataSet dsSpecialty = specialty.Search(db, transaction, specialtyName,isActive);
                transaction.Commit();

                return dsSpecialty;
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

        #region View Specialty
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Specialty ViewSpecialty(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Specialty specialty = new Specialty();
                specialty.Id = Id;
                specialty.View(db, transaction);

                transaction.Commit();
                return specialty;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion


        #endregion

        #region User Services
        #region Add User
        /// <summary>
        /// This function will add the User to data base.
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>int</returns>
        public int AddUser(User user)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = user.Add(db, transaction);
                transaction.Commit();

                return Id;
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

        #region Update User
        public bool UpdateUser(User user)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                user.Update(db, transaction);
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

        #region Search User
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet SearchUser(string username, string email, int roleId, int hospitalId, bool isActive)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_Search);

                db.AddInParameter(dbCommand, "@UserName", DbType.String, username);
                db.AddInParameter(dbCommand, "@Email", DbType.String, email);
                db.AddInParameter(dbCommand, "@RoleId", DbType.Int32, roleId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
                db.AddInParameter(dbCommand, "@isActive", DbType.Boolean, isActive);

                DataSet dsWardInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardInitialData;

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

        #region Load User Role
        public DataSet LoadUserRole()
        {
            try
            {
                User user = new User();

                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DataSet dsUserRole = user.AllUserRole(db, transaction);
                transaction.Commit();

                return dsUserRole;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion 

        #region View User
        public User ViewUser(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                User user = new User();
                user.Id = Id;
                user.View(db, transaction);

                transaction.Commit();
                return user;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion 

        #region Get User Initial Data
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserInitialData(int roleId, int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_Initial_Data);

                db.AddInParameter(dbCommand, "@RoleId", DbType.Int32, roleId);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

                DataSet dsWardInitialData = db.ExecuteDataSet(dbCommand);

                transaction.Commit();

                return dsWardInitialData;

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

        #region User login
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User UserLogin(string userName, string password, string HospitalCode)
        {
            try
            {
                
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_User_Login);

                db.AddInParameter(dbCommand, "@Email", DbType.String, userName);
                db.AddInParameter(dbCommand, "@Password", DbType.String, password);
                db.AddInParameter(dbCommand, "@HospitalCode", DbType.String, HospitalCode);
                db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 8);
                db.AddOutParameter(dbCommand, "@RoleId", DbType.Int32, 8);
                db.AddOutParameter(dbCommand, "@FirstName", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@LastName", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@HospitalId", DbType.Int32, 8);
                db.AddOutParameter(dbCommand, "@HospitalName", DbType.String, 250);
                db.AddOutParameter(dbCommand, "@HospitalType", DbType.String, 20);

                db.ExecuteDataSet(dbCommand, transaction);

                bool isExist = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
                User user = null;
                if (isExist)
                {
                    user = new User();
                    user.Id = Convert.ToInt32(db.GetParameterValue(dbCommand, "@Id"));
                    user.RoleId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@RoleId"));
                    user.FirstName = Convert.ToString(db.GetParameterValue(dbCommand, "@FirstName"));
                    user.LastName = Convert.ToString(db.GetParameterValue(dbCommand, "@LastName"));
                    user.HospitalId = Convert.ToInt32(db.GetParameterValue(dbCommand, "@HospitalId"));
                    user.HospitalName = db.GetParameterValue(dbCommand, "@HospitalName") != null ? Convert.ToString(db.GetParameterValue(dbCommand, "@HospitalName")) : string.Empty;
                    user.HospitalType = db.GetParameterValue(dbCommand, "@HospitalType") != null ? Convert.ToString(db.GetParameterValue(dbCommand, "@HospitalType")) : string.Empty;
                    user.Password = password;
                    
                }
                
                
                transaction.Commit();

                return user;
                
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

        #region Change Password
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true false</returns>
        public bool ChangePassword(User user)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                user.ChangePassword(db, transaction);
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

        #endregion

        #region Comments
        #region Add Comment
        /// <summary>
        /// This function will add the comment to data base.
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>int</returns>
        public int AddComment(Comment comment)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                int Id = comment.Add(db, transaction);

                transaction.Commit();

                return Id;
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

        #region Search Comments
        public DataSet SearchComment(int userId, DateTime createdDate, int kpiId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                Comment comment = new Comment();
                DataSet dsResult = comment.Search(db, transaction, userId, createdDate, kpiId);
                transaction.Commit();

                return dsResult;
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

        #region Delete Comments
        public int DeleteComment(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                Comment comment = new Comment();
                int x = comment.Delete(db, transaction, Id);
                transaction.Commit();

                return x;
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

        #region View Comment
        /// <summary>
        /// View Comment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Comment object</returns>
        public Comment ViewComment(int Id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Comment commnet = new Comment();
                commnet.Id = Id;
                commnet.View(db, transaction);

                transaction.Commit();

                return commnet;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion

        #region Get Comment Users
        /// <summary>
        /// Get Comment Users
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>User DataSet</returns>
        public DataSet GetCommentUsers(int UserId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Comment commnet = new Comment();
                DataSet dsUser = commnet.GetCommentUsers(db, transaction, UserId);

                transaction.Commit();

                return dsUser;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion
        #endregion

        #region Configuration

        #region View Configuration
        /// <summary>
        /// View Configuration
        /// </summary>
        /// <returns></returns>
        public Configuration ViewConfiguration()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                Configuration configuration = new Configuration();

                configuration.View(db, transaction);

                transaction.Commit();
                return configuration;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }

        public HospitalConfigurations HospitalConfigurationsView(HospitalConfigurations HospitalConfigurations)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                HospitalConfigurations configuration = new HospitalConfigurations(HospitalConfigurations);

                configuration.HospitalConfigurationsView(db, transaction);

                transaction.Commit();
                return configuration;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }


        public HospitalConfigurations HospitalConfigurationsAdd(HospitalConfigurations hospitalConfigurations)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                HospitalConfigurations configuration = new HospitalConfigurations(hospitalConfigurations);

                configuration.HospitalConfigurationsAdd(db, transaction);

                transaction.Commit();
                return configuration;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                { throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex); }
                else
                { throw ex; }
            }
        }
        #endregion 

        #region Update Configuration
        /// <summary>
        /// Update Configuration
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public bool UpdateConfiguration(Configuration configuration)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                configuration.Update(db, transaction);
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

        public bool UpdateHospitalConfiguration(HospitalConfigurations configuration)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                configuration.HospitalConfigurationsUpdate(db, transaction);
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

        #endregion
    }
}
