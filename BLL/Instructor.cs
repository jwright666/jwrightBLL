using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwrightBLL.BLL
{
    public class Instructor : Personel
    {
        public string Major { get; set; }
        public List<Student> StudentsHandled { get; set; }
        public DateTime DateStopped { get; set; }

        public Instructor()
            : base()
        {
            this.Major = string.Empty;
            this.StudentsHandled = new List<Student>();
            this.DateJoined = DateTime.Today;
            this.DateStopped = DateTime.Today;
        }
    }
}