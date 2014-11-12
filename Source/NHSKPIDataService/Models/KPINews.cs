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
        public string NewsType { get; set; }

        public KPINews()
        {
            NewsType = "KPI";
        }

        public virtual bool Add(Database db, DbTransaction transaction)
        {
            int result = 0;

            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Insert_KPINews);
                //db.AddInParameter(dbCommand, "@Id", DbType.Int32, Id);
                db.AddInParameter(dbCommand, "@Title", DbType.String, Title);
                db.AddInParameter(dbCommand, "@Description", DbType.String, Description);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, IsActive);

                result = db.ExecuteNonQuery(dbCommand, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > 0 ? true : false;
        }

        public virtual bool Update(Database db, DbTransaction transaction)
        {
            int result = 0;

            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Update_KPINews);
                db.AddInParameter(dbCommand, "@Id", DbType.Int32, Id);
                db.AddInParameter(dbCommand, "@Title", DbType.String, Title);
                db.AddInParameter(dbCommand, "@Description", DbType.String, Description);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, IsActive);

                result = db.ExecuteNonQuery(dbCommand, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > 0 ? true : false;
        }

        public List<KPINews> Search(Database db, DbTransaction transaction, int newsId,bool? isActive)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Search_KPINews);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, newsId);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);

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
                    item.Description = results["Description"].ToString();

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

        public KPIHospitalNews()
        {
            NewsType = "Hospital";
        }

        public override bool Add(Database db, DbTransaction transaction)
        {
            int result = 0;

            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Insert_KPIHospitalNews);
                //db.AddInParameter(dbCommand, "@Id", DbType.Int32, Id);
                db.AddInParameter(dbCommand, "@Title", DbType.String, Title);
                db.AddInParameter(dbCommand, "@Description", DbType.String, Description);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, IsActive);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, HospitalId);

                result = db.ExecuteNonQuery(dbCommand, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > 0 ? true : false;
        }

        public override bool Update(Database db, DbTransaction transaction)
        {
            int result = 0;

            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Update_KPIHospitalNews);
                db.AddInParameter(dbCommand, "@Id", DbType.Int32, Id);
                db.AddInParameter(dbCommand, "@Title", DbType.String, Title);
                db.AddInParameter(dbCommand, "@Description", DbType.String, Description);
                db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, IsActive);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, HospitalId);

                result = db.ExecuteNonQuery(dbCommand, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result > 0 ? true : false;
        }

        public List<KPIHospitalNews> Search(Database db, DbTransaction transaction, int newsId, int hospitalId, bool? isActive)
        {
            DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Search_KPIHospitalNews);

            db.AddInParameter(dbCommand, "@Id", DbType.Int32, newsId);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, hospitalId);
            db.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, isActive);

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
                    item.Description = results["Description"].ToString();

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
