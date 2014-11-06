using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;
using System.Web;
using NHSKPIDataService.Util;

namespace NHSKPIBusinessControllers
{
    public class WardController
    {
        #region Private Variable

        NHSKPIDataService.KPIDataService _NHSService = null;
        NHSKPIDataService.Services.WardService _WardService = null;

        #endregion

        #region Properties

        private NHSKPIDataService.KPIDataService NHSService
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

        private NHSKPIDataService.Services.WardService WardService
        {
            get
            {
                if (_WardService == null)
                {
                    _WardService = new NHSKPIDataService.Services.WardService();
                }
                return _WardService;
            }
            set
            {
                _WardService = value;
            }
        }

        #endregion
        
        #region Add Ward
        public int AddWard(Ward ward)
        {
            return NHSService.AddWard(ward);
        }

        public bool BulkUploadWardAndWardGroup(DataTable dtWardData)
        {
            return NHSService.BulkUploadWardAndWardGroup(dtWardData);
        }
        #endregion

        #region Update Ward
        public bool UpdateWard(Ward ward)
        {
            return NHSService.UpdateWard(ward);
        }
        #endregion

        #region Search Ward

        public DataSet SearchWard(string name, string code, int hospitalId, int wardGroupId, bool isActive)
        {
            return NHSService.SearchWard(name, code, hospitalId,wardGroupId,isActive);
        }
        #endregion

        #region View Ward
        public Ward ViewWards(int Id)
        {
            return NHSService.ViewWard(Id);
        } 
        #endregion
        
        #region Get All Wards Suggestions
        public DataView GetAllWards(int hospitalId, string wardStartsWith)
        {
            try
            {     

                DataSet dsWards = null;

                dsWards = NHSService.SearchWard(string.Empty, string.Empty, hospitalId, 0, true);

                DataView dvWards = new DataView(dsWards.Tables[0]);
                dvWards.RowFilter = string.Format("WardName LIKE '{0}%'", wardStartsWith);

                return dvWards;

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

        #region Load Ward Initial Data

        public DataSet LoadWardInitialData(int hospitalId)
        {
            return WardService.GetWardInitialData(hospitalId);
        }
       
        #endregion      

        #region Get All Wards for hospital
        public DataSet GetAllWardsForHospital(int hospitalId)
        {
            try
            {

                DataSet dsWards = null;

                dsWards = NHSService.SearchWard(string.Empty, string.Empty, hospitalId,0,true);

                return dsWards;

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
