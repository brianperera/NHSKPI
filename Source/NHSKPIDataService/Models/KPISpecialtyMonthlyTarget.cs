using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPISpecialtyMonthlyTarget
    {
        #region Private Variable

        int _SpecialtyId;
        int _KPIId;
        int _HospitalId;
        DateTime _TargetMonth;
        string _SpecialtyTargetDescription;
        double _SpecialtyGreen;
        double _SpecialtyAmber;
        string _TargetDescriptionYTD;
        double _TargetGreenYTD;
        double _TargetAmberYTD;

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


        public string SpecialtyTargetDescription
        {
            get { return _SpecialtyTargetDescription; }
            set { _SpecialtyTargetDescription = value; }
        }


        public double SpecialtyGreen
        {
            get { return _SpecialtyGreen; }
            set { _SpecialtyGreen = value; }
        }


        public double SpecialtyAmber
        {
            get { return _SpecialtyAmber; }
            set { _SpecialtyAmber = value; }
        }

        public string TargetDescriptionYTD
        {
            get { return _TargetDescriptionYTD; }
            set { _TargetDescriptionYTD = value; }
        }

        public double TargetGreenYTD
        {
            get { return _TargetGreenYTD; }
            set { _TargetGreenYTD = value; }
        }

        public double TargetAmberYTD
        {
            get { return _TargetAmberYTD; }
            set { _TargetAmberYTD = value; }
        }

        #endregion
    }
}
