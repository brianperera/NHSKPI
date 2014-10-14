using System;
using System.Collections.Generic;
using System.Text;
using NHSKPIDataService.Models;
using System.Data;
using System.Web;

namespace NHSKPIBusinessControllers
{
   public class SpecialtyController
   {
       #region Private Variable
       NHSKPIDataService.KPIDataService _NHSService = null;
       #endregion

       #region Public Variable
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

       #region Add Specialty
       /// <summary>
       /// Adding Specialty
       /// </summary>
       /// <param name="ward"></param>
       /// <returns>int</returns>
       public int AddSpecialty(Specialty specialty)
       {
           return NHSService.AddSpecialty(specialty);
       } 
       #endregion

       #region Update Specialty
       /// <summary>
       /// Updating Specialty
       /// </summary>
       /// <param name="specialty"></param>
       /// <returns>true or false</returns>
       public bool UpdateSpecialty(Specialty specialty)
       {
           return NHSService.UpdateSpecialty(specialty);
       }
       #endregion

       #region Search Specialty
       /// <summary>
       /// Search Specialty
       /// </summary>
       /// <param name="specialty"></param>
       /// <returns>true or false</returns>
       public DataSet SearchSpecialty(string specialty,bool isActive)
       {
           return NHSService.SearchSpecialty(specialty,isActive);
       }
       #endregion

       #region View Specialty 
       /// <summary>
       /// View Specialty
       /// </summary>
       /// <param name="Id"></param>
       /// <returns>Specialty dataset</returns>
       public Specialty ViewSpecialty(int Id)
       {
           return NHSService.ViewSpecialty(Id);
       }
       #endregion

   }
}
