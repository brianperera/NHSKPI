using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPIHospitalYTDData
    {       
        private int kpiId;

        public int KpiId
        {
            get { return kpiId; }
            set { kpiId = value; }
        }

        private int hospitalId;

        public int HospitalId
        {
            get { return hospitalId; }
            set { hospitalId = value; }
        }

        private DateTime targetYearToDate;

        public DateTime TargetYearToDate
        {
            get { return targetYearToDate; }
            set { targetYearToDate = value; }
        }       


        private double nominator;

        public double Nominator
        {
            get { return nominator; }
            set { nominator = value; }
        }
        private double denominator;

        public double Denominator
        {
            get { return denominator; }
            set { denominator = value; }
        }
    }
}
