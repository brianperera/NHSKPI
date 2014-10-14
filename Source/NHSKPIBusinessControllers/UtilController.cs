using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;
using System.Web;
using NHSKPIDataService.Services;

namespace NHSKPIBusinessControllers
{
    public class UtilController
    {
        #region Private Variable

        UtilService _UtilService = null;
                
        #endregion

        #region Properties

        public UtilService UtilService
        {
            get 
            {
                if (_UtilService == null)
                {
                    _UtilService = new UtilService();
                }
                return _UtilService; 
            }
            set 
            { 
                _UtilService = value; 
            }
        }

        #endregion

        #region Get Specialty Level Target Initial Data
        /// <summary>
        /// Get Specialty Level Target Initial Data
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelTargetInitialData(int hospitalId)
        {
            try
            {
                DataSet dsSpecialtyLevelTargetInitialData = UtilService.GetSpecialtyLevelTargetInitialData(hospitalId);
                return dsSpecialtyLevelTargetInitialData;
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

        #region Get Specialty Level YTD Target Initial Data
        /// <summary>
        /// Get Specialty Level YTD Target Initial Data
        /// </summary>
        /// <returns></returns>
        public DataSet GetSpecialtyLevelYTDTargetInitialData()
        {
            try
            {
                DataSet dsSpecialtyLevelYTDTargetInitialData = UtilService.GetSpecialtyLevelYTDTargetInitialData();
                return dsSpecialtyLevelYTDTargetInitialData;
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
