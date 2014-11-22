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
        DepartmentHead _DepartmentHeadService = null;
        private UtilController utilController = null;

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

        public DepartmentHead DepartmentHeadService
        {
            get
            {
                if (_DepartmentHeadService == null)
                {
                    _DepartmentHeadService = new DepartmentHead();
                }
                return _DepartmentHeadService;
            }
            set
            {
                _DepartmentHeadService = value;
            }
        }

        #endregion

        #region Add User

        public int AddUser(User user)
        {
            return NHSService.AddUser(user);
        }

        public int AddUser(User user, bool sendNotification)
        {
            int status = NHSService.AddUser(user);

            if (status > 0)
            { 
                //Send Email
                utilController = new UtilController();
                Email emailMessage = new NHSKPIDataService.Models.Email
                {
                    EmailTo = user.Email,
                    Subject = "Added to KPI Portal",
                    Body = "Your KPI Portal free trail has been approved. You can use this trail version for 60 days." + "<br><br>URL:" + System.Configuration.ConfigurationManager.AppSettings["SiteURL"].ToString() + "<br><br>User Name:" + user.UserName + "<br><br>Password:" + user.Password + "<br><br>Note: This is a auto generated message. Please do not reply to this email."
                };

                utilController.SendEmailNotification(emailMessage);
            }

            return status;
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

        public bool UpdateHospitalConfiguration(HospitalConfigurations configuration)
        {
            return NHSService.UpdateHospitalConfiguration(configuration);
        }
        #endregion

        #region View Configuration
        public Configuration ViewConfiguration()
        {
            return NHSService.ViewConfiguration();
        }

        public HospitalConfigurations HospitalConfigurationsView(HospitalConfigurations HospitalConfigurations)
        {
            return NHSService.HospitalConfigurationsView(HospitalConfigurations);
        }

        public HospitalConfigurations HospitalConfigurationsAdd(HospitalConfigurations hospitalConfigurations)
        {
            return NHSService.HospitalConfigurationsAdd(hospitalConfigurations);
        }

        #endregion

        #endregion

        #region Add Department Heads

        public bool InsertUpdateDepartmentHead(DepartmentHead departmentHead)
        {
            try
            {
                int targetId = 0;

                targetId = DepartmentHeadService.InsertUpdateDepartmentHead(departmentHead);

                if (targetId > 0)
                {
                    return true;
                }

                return false;
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
