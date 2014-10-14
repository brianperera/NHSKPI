using System;
using System.Collections.Generic;
using System.Text;

namespace NHSKPIDataService.Models
{
    public class KPIWardMonthlyTarget
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

        private string targetDescriptionYTD;

        public string TargetDescriptionYTD
        {
            get { return targetDescriptionYTD; }
            set { targetDescriptionYTD = value; }
        }
        private double targetGreenYTD;

        public double TargetGreenYTD
        {
            get { return targetGreenYTD; }
            set { targetGreenYTD = value; }
        }
        private double targetAmberYTD;

        public double TargetAmberYTD
        {
            get { return targetAmberYTD; }
            set { targetAmberYTD = value; }
        }
              
    }
}
