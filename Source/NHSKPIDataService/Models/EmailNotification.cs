using Microsoft.Practices.EnterpriseLibrary.Data;
using NHSKPIDataService.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class EmailNotification
    {
        #region Fields
        private int id;
        private int hospitalId;
        private int reminder1;
        private int reminder2;
        private int managerEscalation;
        private string reminderEmail;
        private string escalationEmail;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int HospitalId
        {
            get { return hospitalId; }
            set { hospitalId = value; }
        }

        public int Reminder1
        {
            get { return reminder1; }
            set { reminder1 = value; }
        }

        public int Reminder2
        {
            get { return reminder2; }
            set { reminder2 = value; }
        }

        public int ManagerEscalation
        {
            get { return managerEscalation; }
            set { managerEscalation = value; }
        }

        public string ReminderEmail
        {
            get { return reminderEmail; }
            set { reminderEmail = value; }
        }

        public string EscalationEmail
        {
            get { return escalationEmail; }
            set { escalationEmail = value; }
        }
        #endregion

        #region Add
        public void Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Insert_EmailNotification);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
                db.AddInParameter(dbCommand, "@Reminder1", DbType.Int32, this.Reminder1);
                db.AddInParameter(dbCommand, "@Reminder2", DbType.Int32, this.Reminder2);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.Int32, this.ManagerEscalation);
                db.AddInParameter(dbCommand, "@ReminderEmail", DbType.String, this.ReminderEmail);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.String, EscalationEmail);
                db.ExecuteNonQuery(dbCommand, transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update
        public void Update(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Update_EmailNotification);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
                db.AddInParameter(dbCommand, "@Reminder1", DbType.Int32, this.Reminder1);
                db.AddInParameter(dbCommand, "@Reminder2", DbType.Int32, this.Reminder2);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.Int32, this.ManagerEscalation);
                db.AddInParameter(dbCommand, "@ReminderEmail", DbType.String, this.ReminderEmail);
                db.AddInParameter(dbCommand, "@ManagerEscalation", DbType.String, EscalationEmail);
                db.ExecuteNonQuery(dbCommand, transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public void Delete(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Delete_EmailNotification);

                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int16, this.HospitalId);
                db.ExecuteNonQuery(dbCommand, transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Search
        public EmailNotification Search(Database db, DbTransaction transaction, int hospitalId)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_EmailNotification);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
            var results = db.ExecuteReader(dbCommand);

            EmailNotification item = new EmailNotification();
            item.HospitalId = hospitalId;
            int intVal;

            if (results != null)
            {
                while (results.Read())
                {
                    if (int.TryParse(results["Id"].ToString(), out intVal))
                        item.Id = intVal;

                    item.ReminderEmail = results["ReminderEmail"].ToString();
                    item.EscalationEmail = results["ManagerEscalationEmail"].ToString();

                    if (int.TryParse(results["Reminder1"].ToString(), out intVal))
                        item.Reminder1 = intVal;
                    if (int.TryParse(results["Reminder2"].ToString(), out intVal))
                        item.Reminder2 = intVal;
                    if (int.TryParse(results["ManagerEscalation"].ToString(), out intVal))
                        item.ManagerEscalation = intVal;
                }
            }

            return item;
        }
        #endregion

        #region SearchAll
        public List<EmailNotification> SearchAll(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Get_EmailNotification);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, -1);
            var results = db.ExecuteReader(dbCommand);

            List<EmailNotification> list=new List<EmailNotification>();
            EmailNotification item;
            int intVal;

            if (results != null)
            {
                while (results.Read())
                {
                    item = new EmailNotification();

                    if (int.TryParse(results["Id"].ToString(), out intVal))
                        item.Id = intVal;

                    if (int.TryParse(results["HospitalId"].ToString(), out intVal))
                        item.Id = intVal;

                    item.ReminderEmail = results["ReminderEmail"].ToString();
                    item.EscalationEmail = results["ManagerEscalationEmail"].ToString();

                    if (int.TryParse(results["Reminder1"].ToString(), out intVal))
                        item.Reminder1 = intVal;
                    if (int.TryParse(results["Reminder2"].ToString(), out intVal))
                        item.Reminder2 = intVal;
                    if (int.TryParse(results["ManagerEscalation"].ToString(), out intVal))
                        item.ManagerEscalation = intVal;

                    list.Add(item);
                }
            }

            return list;
        }
        #endregion

    }
}
