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
    public class KPINews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public void Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Insert_KPIHospitalNews);

                db.AddInParameter(dbCommand, "@Title", DbType.String, Title);
                db.AddInParameter(dbCommand, "@Description", DbType.String, Description);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, IsActive);

                db.ExecuteNonQuery(dbCommand, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KPINews> Search(Database db, DbTransaction transaction, int newsId)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Search_KPINews);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, newsId);

            var results = db.ExecuteReader(dbCommand);

            List<KPINews> list = new List<KPINews>();
            KPINews item;
            int intVal;
            bool boolVal;
            DateTime dateVal;

            if (results != null)
            {
                while (results.Read())
                {
                    item = new KPINews();

                    if (int.TryParse(results["Id"].ToString(), out intVal))
                        item.Id = intVal;

                    item.Title = results["Title"].ToString();
                    item.Title = results["Description"].ToString();

                    if (bool.TryParse(results["IsActive"].ToString(), out boolVal))
                        item.IsActive = boolVal;

                    if (DateTime.TryParse(results["CreatedDate"].ToString(), out dateVal))
                        item.CreatedDate = dateVal;

                    list.Add(item);
                }
            }

            return list;
        }
    }

    public class KPIHospitalNews : KPINews
    {
        public int HospitalId { get; set; }

        public void Add(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Insert_KPIHospitalNews);

                db.AddInParameter(dbCommand, "@Title", DbType.String, Title);
                db.AddInParameter(dbCommand, "@Description", DbType.String, Description);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, IsActive);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, HospitalId);

                db.ExecuteNonQuery(dbCommand, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KPIHospitalNews> Search(Database db, DbTransaction transaction, int newsId, int hospitalId)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Search_KPINews);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, newsId);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);

            var results = db.ExecuteReader(dbCommand);

            List<KPIHospitalNews> list = new List<KPIHospitalNews>();
            KPIHospitalNews item;
            int intVal;
            bool boolVal;
            DateTime dateVal;

            if (results != null)
            {
                while (results.Read())
                {
                    item = new KPIHospitalNews();

                    if (int.TryParse(results["Id"].ToString(), out intVal))
                        item.Id = intVal;

                    item.Title = results["Title"].ToString();
                    item.Title = results["Description"].ToString();

                    if (bool.TryParse(results["IsActive"].ToString(), out boolVal))
                        item.IsActive = boolVal;

                    if (DateTime.TryParse(results["CreatedDate"].ToString(), out dateVal))
                        item.CreatedDate = dateVal;

                    item.HospitalId = hospitalId;

                    list.Add(item);
                }
            }

            return list;
        }
    }
}
