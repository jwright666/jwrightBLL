using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwrightBLL.BLL
{
    public class Student : Personel
    {
        public string Subject { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateStopped { get; set; }

        public Student()
            : base()
        {
            this.Subject = string.Empty;
            this.DateStarted = DateTime.Today;
            this.DateStopped = DateTime.Today;
        }
    }
}