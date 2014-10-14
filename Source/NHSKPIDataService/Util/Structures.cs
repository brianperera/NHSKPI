using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Util
{
    public class Structures
    {
        /// <summary>
        /// Role 
        /// </summary>
        public enum Role
        {
            SuperUser = 1,
            Admin = 2,
            DataEntryOperator = 3,
            ViewUser = 4
        }

        /// <summary>
        /// Ward Target
        /// </summary>
        public enum Ward
        { 
            WardOnly = 1,
            SpecialtyOnly = 2,
            WardAndSpecialty = 3
        }
       
    }
}
