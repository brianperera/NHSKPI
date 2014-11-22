using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using NHSKPIDataService.Util;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace NHSKPIDataService.Models
{
    public class Configuration
    {
        #region Private Variable

        string targetApply;

        #endregion

        #region Properties

        public string TargetApply
        {
            get { return targetApply; }
            set { targetApply = value; }
        }

        #endregion

        #region Update
        /// <summary>
        /// Update Configuration
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool Update(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("uspConfigurationUpdate");


            db.AddInParameter(dbCommand, "@TargetApply", DbType.String, this.targetApply);
            
            db.ExecuteNonQuery(dbCommand, transaction);

            return true;
        }

        #endregion 

        #region View
        /// <summary>
        /// Get a hospital by id
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool View(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand("uspConfigurationView");

                db.AddOutParameter(dbCommand, "@TargetApply", DbType.String, 50);
                
                db.ExecuteNonQuery(dbCommand);

                this.targetApply = db.GetParameterValue(dbCommand, "@TargetApply").ToString();
                

                return true;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }
        #endregion

    }

    public class HospitalConfigurations
    {
        public HospitalConfigurations()
        {

        }

        public HospitalConfigurations(HospitalConfigurations hospitalConfigurations)
        {
            Id = hospitalConfigurations.Id;
            EmailFacilities = hospitalConfigurations.EmailFacilities;
            Reminders = hospitalConfigurations.Reminders;
            DownloadDataSets = hospitalConfigurations.DownloadDataSets;
            BenchMarkingModule = hospitalConfigurations.BenchMarkingModule;
            HospitalId = hospitalConfigurations.HospitalId;
        }

        #region Properties

        public int Id { get; set; }
        public bool EmailFacilities { get; set; }
        public bool Reminders { get; set; }
        public bool DownloadDataSets { get; set; }
        public bool BenchMarkingModule { get; set; }        
        public int HospitalId { get; set; }

        #endregion

        #region Update
        /// <summary>
        /// Update Configuration
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool HospitalConfigurationsUpdate(Database db, DbTransaction transaction)
        {
            DbCommand dbCommand = db.GetStoredProcCommand("uspHospitalConfigurationUpdate");

            db.AddInParameter(dbCommand, "@EmailFacilities", DbType.Boolean, this.EmailFacilities);
            db.AddInParameter(dbCommand, "@Reminders", DbType.Boolean, this.Reminders);
            db.AddInParameter(dbCommand, "@DownloadDataSets", DbType.Boolean, this.DownloadDataSets);
            db.AddInParameter(dbCommand, "@BenchMarkingModule", DbType.Boolean, this.BenchMarkingModule);
            db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);
            db.AddOutParameter(dbCommand, "@Id", DbType.Int32, 0);

            db.ExecuteNonQuery(dbCommand, transaction);

            int id = 0;
            int.TryParse(db.GetParameterValue(dbCommand, "@Id").ToString(), out id);
            this.Id = id;

            return Id > 0;

            return true;
        }

        #endregion

        public bool HospitalConfigurationsAdd(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand("uspHospitalConfigurationAdd");

                db.AddInParameter(dbCommand, "@Id", DbType.Boolean, this.Id);
                db.AddInParameter(dbCommand, "@EmailFacilities", DbType.Boolean, this.EmailFacilities);
                db.AddInParameter(dbCommand, "@Reminders", DbType.Boolean, this.Reminders);
                db.AddInParameter(dbCommand, "@DownloadDataSets", DbType.Boolean, this.DownloadDataSets);
                db.AddInParameter(dbCommand, "@BenchMarkingModule", DbType.Boolean, this.BenchMarkingModule);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);

                db.ExecuteNonQuery(dbCommand);

                int id = 0;
                int.TryParse(db.GetParameterValue(dbCommand, "@Id").ToString(), out id);
                this.Id = id;

                return HospitalId > 0;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        #region View
        /// <summary>
        /// Get a hospital by id
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>true or false</returns>
        public bool HospitalConfigurationsView(Database db, DbTransaction transaction)
        {
            try
            {
                DbCommand dbCommand = db.GetStoredProcCommand("uspHospitalConfigurationView");

                db.AddOutParameter(dbCommand, "@Id", DbType.Boolean, 200);
                db.AddOutParameter(dbCommand, "@EmailFacilities", DbType.Boolean, 200);
                db.AddOutParameter(dbCommand, "@Reminders", DbType.Boolean, 200);
                db.AddOutParameter(dbCommand, "@DownloadDataSets", DbType.Boolean, 200);
                db.AddOutParameter(dbCommand, "@BenchMarkingModule", DbType.Boolean, 200);
                db.AddInParameter(dbCommand, "@HospitalId", DbType.Int32, this.HospitalId);

                db.ExecuteNonQuery(dbCommand);

                bool enabled = false;

                bool.TryParse(db.GetParameterValue(dbCommand, "@EmailFacilities").ToString(), out enabled);
                this.EmailFacilities = enabled;

                bool.TryParse(db.GetParameterValue(dbCommand, "@Reminders").ToString(), out enabled);
                this.Reminders = enabled;

                bool.TryParse(db.GetParameterValue(dbCommand, "@DownloadDataSets").ToString(), out enabled);
                this.DownloadDataSets = enabled;

                bool.TryParse(db.GetParameterValue(dbCommand, "@BenchMarkingModule").ToString(), out enabled);
                this.BenchMarkingModule = enabled;

                int hospitalId = 0;
                int.TryParse(db.GetParameterValue(dbCommand, "@HospitalId").ToString(), out hospitalId);
                this.HospitalId = hospitalId;

                int id = 0;
                int.TryParse(db.GetParameterValue(dbCommand, "@Id").ToString(), out id);
                this.Id = id;

                return HospitalId > 0;

            }

            catch (System.Exception ex)
            {
                throw ex;
            }


        }
        #endregion
    }
}
