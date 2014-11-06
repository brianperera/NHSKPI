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

        public void InserKPINews(KPINews news)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                news.Add(db, transaction);

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
        }

        public List<KPINews> SearchKPINews(KPINews news, int newsId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                List<KPINews> list = news.Search(db, transaction, newsId);

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

        public void InserKPIHospitalNews(KPIHospitalNews news,int newsId,int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                news.Add(db, transaction);
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
        }

        public List<KPIHospitalNews> SearchKPIHospitalNews(KPIHospitalNews news, int newsId, int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                List<KPIHospitalNews> list = news.Search(db, transaction, newsId, hospitalId);

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
