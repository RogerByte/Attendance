using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.ServiceContracts
{
    public class Credentials
    {
        public Credentials()
        {
            username = "";
            password = "";
        }
        public string username { get; set; }
        public string password { get; set; }
    }
}