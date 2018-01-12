using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwrightBLL.BLL
{
    public class Attendance
    {
        public string Teacher_IDNo { get; set; }
        public string Student_IDNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}