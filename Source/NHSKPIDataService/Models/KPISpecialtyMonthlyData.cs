using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPISpecialtyMonthlyData
    {
        #region Private Variable

        int _SpecialtyId;
        int _KPIId;
        int _HospitalId;
        DateTime _TargetMonth;
        double _Numerator;
        double _Denominator;
        double _YTDValue;

        #endregion

        #region Properties

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


        public int HospitalId
        {
            get { return _HospitalId; }
            set { _HospitalId = value; }
        }


        public DateTime TargetMonth
        {
            get { return _TargetMonth; }
            set { _TargetMonth = value; }
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

        public double YTDValue
        {
            get { return _YTDValue; }
            set { _YTDValue = value; }
        }

        #endregion
    }
}
