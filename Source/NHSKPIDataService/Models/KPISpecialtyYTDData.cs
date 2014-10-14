using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPISpecialtyYTDData
    {
        #region Private Variable

        int _SpecialtyId;
        int _KPIId;
        DateTime _TargetYearToDate;
        double _Numerator;
        double _Denominator;

        #endregion

        #region properties

        public int SpecialtyId
        {
            get { return _SpecialtyId; }
            set { _SpecialtyId = value; }
        }

        public int KPIId
        {
            get { return _KPIId; }
            set { _KPIId = value; }
        }

        public DateTime TargetYearToDate
        {
            get { return _TargetYearToDate; }
            set { _TargetYearToDate = value; }
        }

        public double Numerator
        {
            get { return _Numerator; }
            set { _Numerator = value; }
        }

        public double Denominator
        {
            get { return _Denominator; }
            set { _Denominator = value; }
        }

        #endregion
    }
}
