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
    public class EmailNotificationService
    {
        #region private varibles

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion  

        #region Methods
        public void InsertEmailNotification(EmailNotification emailNotification)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                emailNotification.Add(db, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
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

        public void UpdateEmailNotification(EmailNotification emailNotification)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                emailNotification.Update(db, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
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

        public EmailNotification SearchEmailNotification(int hospitalId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                EmailNotification emailNotification = new EmailNotification();
                var result = emailNotification.Search(db, transaction, hospitalId);

                transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (transaction != null)
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

        public List<EmailNotification> SearchAllEmailNotifications()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                EmailNotification emailNotification = new EmailNotification();
                var result = emailNotification.SearchAll(db, transaction);

                transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (transaction != null)
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

        public void DeleteEmailNotification(EmailNotification emailNotification)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.NHS_Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                emailNotification.Delete(db, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
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
