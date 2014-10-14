using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;

namespace NHSKPIBusinessControllers
{
    public class CommentController
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

        #region Add Comment
        /// <summary>
        /// Add Comments
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns>Int</returns>
        public int AddComment(Comment comment)        {
           
            return NHSService.AddComment(comment);
        }

        #endregion

        #region Search Comment
        public DataSet SearchComment(int userID, DateTime createdDate,int kpiId)
        {
            return NHSService.SearchComment(userID, createdDate, kpiId);
        }
        #endregion

        #region Delete Comment
        public int DeleteComment(int Id)
        {
            return NHSService.DeleteComment(Id);
        }
        #endregion

        #region View Comment
        /// <summary>
        /// View Comment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Comment object</returns>
        public Comment ViewComment(int Id)
        {
            return NHSService.ViewComment(Id);
        }
        #endregion

        #region Get Comment Users
        /// <summary>
        /// Get Comment Users
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>User DataSet</returns>
        public DataSet GetCommentUsers(int UserId)
        {
            return NHSService.GetCommentUsers(UserId);
        }
        #endregion

    }
}
