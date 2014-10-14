using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPIWardMonthlyData
    {
        private int wardId;

        public int WardId
        {
            get { return wardId; }
            set { wardId = value; }
        }
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

        private DateTime targetMonth;

        public DateTime TargetMonth
        {
            get { return targetMonth; }
            set { targetMonth = value; }
        }
        private string targetDescription;

        
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

        private double yTDValue;

        public double YTDValue
        {
            get { return yTDValue; }
            set { yTDValue = value; }
        }
    }
}
