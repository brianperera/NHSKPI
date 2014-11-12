using Microsoft.Practices.EnterpriseLibrary.Data;
using NHSKPIDataService.Models;
using NHSKPIDataService.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NHSKPIDataService.Services
{
    public class NewsService
    {
        #region private varibles

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion     

        public bool InserKPINews(KPINews news)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                result = news.Add(db, transaction);

                transaction.Commit();
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

            return result;
        }

        public bool UpdateKPINews(KPINews news)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                result = news.Update(db, transaction);

                transaction.Commit();
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

            return result;
        }

        public List<KPINews> SearchKPINews(KPINews news, int newsId, bool? isActive)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                List<KPINews> list = news.Search(db, transaction, newsId,isActive);

                transaction.Commit();

                return list;
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

        public bool InserKPIHospitalNews(KPIHospitalNews news)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                result = news.Add(db, transaction);
                transaction.Commit();
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

            return result;
        }

        public bool UpdateKPIHospitalNews(KPIHospitalNews news)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                result = news.Update(db, transaction);
                transaction.Commit();
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

            return result;
        }

        public List<KPIHospitalNews> SearchKPIHospitalNews(KPIHospitalNews news, int newsId, int hospitalId,bool? isActive)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                List<KPIHospitalNews> list = news.Search(db, transaction, newsId, hospitalId,isActive);

                transaction.Commit();

                return list;
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
    }
}
