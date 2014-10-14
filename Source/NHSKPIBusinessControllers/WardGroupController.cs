using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;
using System.Web;

namespace NHSKPIBusinessControllers
{
    public class WardGroupController
    {
        #region Private Variable

        NHSKPIDataService.KPIDataService _NHSService = null;

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

        #endregion

        #region Add Ward Group
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ward"></param>
        /// <returns>int</returns>
        public int AddWardGroup(WardGroup wardGroup)
        {
            return NHSService.AddWardGroup(wardGroup);
        }

        #endregion

        #region Update Ward Group

        public bool UpdateWardGroup(WardGroup wardGroup)
        {
            return NHSService.UpdateWardGroup(wardGroup);
        }

        #endregion

        #region Search Ward Group

        public DataSet SearchWardGroup(string name, int hospitalId, bool isActive)
        {
            return NHSService.SearchWardGroup(name, hospitalId, isActive);
        }

        #endregion

        #region View Ward Group

        public WardGroup ViewWardGroup(int Id)
        {
            return NHSService.ViewWardGroup(Id);
        }
        #endregion

        #region Load Hospitals

        public DataSet LoadHospitals(int hospitalId)
        {
            return NHSService.SearchHospital(string.Empty, string.Empty, true, hospitalId);
        }

        #endregion
    }
}
