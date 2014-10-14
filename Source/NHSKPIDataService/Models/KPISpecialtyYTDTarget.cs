using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPISpecialtyYTDTarget
    {
        #region Private Variable

        int _SpecialtyId;
        int _KPIId;
        DateTime _TargetYTD;        
        string _YTDTargetDescription;
        double _YTDGreen;
        double _YTDAmber;

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

        public DateTime TargetYTD
        {
            get { return _TargetYTD; }
            set { _TargetYTD = value; }
        }

        public string YTDTargetDescription
        {
            get { return _YTDTargetDescription; }
            set { _YTDTargetDescription = value; }
        }

        public double YTDGreen
        {
            get { return _YTDGreen; }
            set { _YTDGreen = value; }
        }

        public double YTDAmber
        {
            get { return _YTDAmber; }
            set { _YTDAmber = value; }
        }

        #endregion
    }
}
