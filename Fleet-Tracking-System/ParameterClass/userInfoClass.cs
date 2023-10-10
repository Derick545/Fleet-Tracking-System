using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet_Tracking_System.ParameterClass
{
    class userInfoClass
    {
        //Declaring Variables
        public string userName { get; set; }
        public string passWord { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string role { get; set; }
        public string hashPassword { get; set; }
        public string saltPassword { get; set; }

    }
}
