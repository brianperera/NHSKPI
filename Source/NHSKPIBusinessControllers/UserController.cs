using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;

namespace NHSKPIBusinessControllers
{
    public class UserController
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

        #region Add User

        public int AddUser(User user)
        {
            return NHSService.AddUser(user);
        }

        #endregion 

        #region Update User
        public bool UpdateUser(User user)
        {
            return NHSService.UpdateUser(user);
        }
        #endregion

        #region Search User

        public DataSet SearchUser(string username, string email, int roleId, int hospitalid, bool isActive)
        {
            return NHSService.SearchUser( username,  email,  roleId,  hospitalid,  isActive);
        }

        #endregion 

        #region User Login

        public User UserLogin(string userName, string password, string HospitalCode)
        {
            return NHSService.UserLogin(userName, password, HospitalCode);
        }

        #endregion 

        #region Load User Role
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserRole()
        {
            return NHSService.LoadUserRole();
        }
        #endregion 

        #region View User
        public User ViewUser(int Id)
        {
            return NHSService.ViewUser(Id);
        }
        #endregion

        #region Load User Initial Data

        public DataSet LoadUserInitialData(int roleId, int hospitalId)
        {
            return NHSService.GetUserInitialData(roleId, hospitalId);
        }

        #endregion

        #region Change Password
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true false</returns>
        public bool ChangePassword(User user)
        {
            return NHSService.ChangePassword(user);
        }
        #endregion

        #region Configuration

        #region Update Configuration
        public bool UpdateConfiguration(Configuration configuration)
        {
            return NHSService.UpdateConfiguration(configuration);
        }
        #endregion

        #region View Configuration
        public Configuration ViewConfiguration()
        {
            return NHSService.ViewConfiguration();
        }
        #endregion

        #endregion

    }
}
