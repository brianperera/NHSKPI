using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;

namespace NHSKPIBusinessControllers
{
    public class HospitalController
    {
        #region Private Variable
        NHSKPIDataService.KPIDataService _NHSService = null;
        #endregion

        #region Properties
        public NHSKPIDataService.KPIDataService NHSService
        {
            get 
            {
                if (_NHSService == null)
                {
                    _NHSService = new NHSKPIDataService.KPIDataService();
                }
                return _NHSService;
            }
            set 
            { 
                _NHSService = value; 
            }
        }
        #endregion

        #region Add Hospital
        /// <summary>
        /// Add Hospital Details
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>Int</returns>
        public int AddHospital(Hospital hospital)
        {            
            return NHSService.AddHospital(hospital);
        }

        public int GetHospitalIdFromCode(Hospital hospital)
        {
            return NHSService.GetHospitalIdFromCode(hospital);
        }

        #endregion

        #region Update Hospital
        /// <summary>
        /// Update Hospital Details
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>true or false</returns>
        public bool UpdateHospital(Hospital hospital)
        {
            return NHSService.UpdateHospital(hospital);
        }
        #endregion

        #region View Hospital
        /// <summary>
        /// View Hopital details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Hospital Object</returns>
        public Hospital ViewHospital(int Id)
        {
            return NHSService.ViewHospital(Id);
        }
        #endregion

        #region Search Hospital
        /// <summary>
        /// Search Hospitals
        /// </summary>
        /// <param name="hospitalName"></param>
        /// <param name="hospitalCode"></param>
        /// <returns>Hospitals result data set</returns>
        public DataSet SearchHospital(string hospitalName, string hospitalCode, bool isActive, int hospitalId)
        {
            return NHSService.SearchHospital(hospitalName, hospitalCode, isActive, hospitalId);
        }
        #endregion

        #region Get All Wards Suggestions
        public DataView GetAllHospitalSuggestion(string hospitalStartsWith)
        {
            try
            {
                DataSet dsHospitals = null;

                dsHospitals = NHSService.SearchHospital(string.Empty, string.Empty, true,0);

                DataView dvHospitals = new DataView(dsHospitals.Tables[0]);
                dvHospitals.RowFilter = string.Format("Name LIKE '{0}%'", hospitalStartsWith);

                return dvHospitals;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }

        public DataView GetAllHospitals()
        {
            try
            {
                DataSet dsHospitals = null;
                DataView dvHospitals = null;
                dsHospitals = NHSService.ViewAllHospital();

                if (dsHospitals != null)
                {
                    dvHospitals = new DataView(dsHospitals.Tables[0]);
                }
   
                return dvHospitals;

            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw new Exception("Stack Trace:" + ex.StackTrace + "Message:" + ex.Message, ex);
                }
                else
                {
                    throw ex;
                }
            }

        }

        #endregion
    }
}
