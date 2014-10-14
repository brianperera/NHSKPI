using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPIHospitalYTDTarget
    {
        private int hospitalId;

        public int HospitalId
        {
            get { return hospitalId; }
            set { hospitalId = value; }
        }
        private int kpiId;

        public int KpiId
        {
            get { return kpiId; }
            set { kpiId = value; }
        }

        private DateTime targetYTD;

        public DateTime TargetYTD
        {
            get { return targetYTD; }
            set { targetYTD = value; }
        }
        private string targetDescription;

        public string TargetDescription
        {
            get { return targetDescription; }
            set { targetDescription = value; }
        }
        private double targetGreen;

        public double TargetGreen
        {
            get { return targetGreen; }
            set { targetGreen = value; }
        }
        private double targetAmber;

        public double TargetAmber
        {
            get { return targetAmber; }
            set { targetAmber = value; }
        }
    }
}
