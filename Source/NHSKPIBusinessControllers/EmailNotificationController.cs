using NHSKPIDataService;
using NHSKPIDataService.Models;
using NHSKPIDataService.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NHSKPIBusinessControllers
{
    public class EmailNotificationController
    {
        #region Private Variable
        EmailNotificationService _NHSEmailService;
        KPIDataService _NHSDataService = null;
        List<int> _HospitalIds;

        #endregion

        #region Properties
        public EmailNotificationService NHSEmailService
        {
            get 
            {
                if (_NHSEmailService == null)
                    _NHSEmailService = new EmailNotificationService();

                return _NHSEmailService; 
            }
        }

        public KPIDataService NHSDataService
        {
            get
            {
                if (_NHSDataService == null)
                {
                    _NHSDataService = new NHSKPIDataService.KPIDataService();
                }
                return _NHSDataService;
            }
        }

        public List<int> HospitalIds
        {
            get 
            {
                if (_HospitalIds == null)
                {
                    _HospitalIds = new List<int>();
                    DataSet allHospitals = NHSDataService.ViewAllHospital();
                    _HospitalIds = allHospitals.Tables[0].AsEnumerable().Select(dataRow => dataRow.Field<int>("Id")).ToList();
                }

                return _HospitalIds; 
            }
        }
        #endregion

        #region Public Methods
        public Dictionary<EmailNotification, DataSet> GetIncompleteKPI()
        {
            Dictionary<EmailNotification, DataSet> result = new Dictionary<EmailNotification, DataSet>();
            DataSet tempDS;

            foreach (int id in HospitalIds)
            {
                tempDS = new DataSet();
                tempDS.Merge(GetIncompleteWardKPI(id));
                tempDS.Merge(GetIncompleteSpecialityKPI(id));

                result.Add(NHSEmailService.SearchEmailNotification(id), tempDS);
            }

            return result;
        }

        public DataSet GetIncompleteKPI(int hospitaId)
        {
            DataSet tempDS = new DataSet();
            tempDS.Merge(GetIncompleteWardKPI(hospitaId));
            tempDS.Merge(GetIncompleteSpecialityKPI(hospitaId));

            if (tempDS.Tables != null && tempDS.Tables.Count > 0)
            {
                if (tempDS.Tables[0].Columns.Contains("WardID"))
                    tempDS.Tables[0].Columns.Remove("WardID");
                if (tempDS.Tables[0].Columns.Contains("KPIId"))
                    tempDS.Tables[0].Columns.Remove("KPIId");
            }

            return tempDS;
        }

        public List<EmailNotification> GetAllEmailNotifications()
        {
            return NHSEmailService.SearchAllEmailNotifications();
        }
        #endregion

        #region Internal Methods
        internal DataSet GetIncompleteWardKPI(int hospitaId)
        {
            DataSet target = new DataSet();

            //foreach (int id in HospitalIds)
            {
                target.Merge(NHSDataService.GetIncompleteWardKPI(hospitaId, (int)NHSKPIDataService.Util.Structures.Ward.WardOnly));
                target.Merge(NHSDataService.GetIncompleteWardKPI(hospitaId, (int)NHSKPIDataService.Util.Structures.Ward.WardAndSpecialty));
            }

            return target;
        }

        internal DataSet GetIncompleteSpecialityKPI(int hospitaId)
        {
            DataSet target = new DataSet();

            //foreach (int id in HospitalIds)
            {
                target.Merge(NHSDataService.GetIncompleteWardKPI(hospitaId, (int)NHSKPIDataService.Util.Structures.Ward.SpecialtyOnly));
                target.Merge(NHSDataService.GetIncompleteWardKPI(hospitaId, (int)NHSKPIDataService.Util.Structures.Ward.WardAndSpecialty));
            }

            return target;
        }
        #endregion
    }
}
