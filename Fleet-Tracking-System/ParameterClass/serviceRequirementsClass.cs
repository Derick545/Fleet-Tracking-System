using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet_Tracking_System.ParameterClass
{
    class serviceRequirementsClass
    {
        public string vehicleNumber { get; set; }
        public string serviceType { get; set; }
        public string appointmentDate { get; set; }
        public string appointmentTime { get; set; }
        public string workToBeCompleted { get; set; }
        public decimal cost { get; set; }
        public string completedDate { get; set; }
    }
}
