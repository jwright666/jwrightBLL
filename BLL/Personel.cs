using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwrightBLL.BLL
{
    public class Personel
    {
        public int IDNo { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Resident_Address { get; set; }
        public string Email_Address { get; set; }
        public string ContactNo { get; set; }
        public DateTime BirthDate { get; set; }
        public Person_Gender Gender { get; set; }
        public School_Status SchoolStatus { get; set; }
        public School_Role Role { get; set; }
        public Term Term { get; set; }
        public string ContactPerson { get; set; }
        public DateTime DateJoined { get; set; }

        public Personel()
        {
            this.IDNo = 0;
            this.NickName = "";
            this.FirstName = "";
            this.MiddleName = "";
            this.LastName = "";
            this.Resident_Address = "";
            this.Email_Address = "";
            this.ContactNo = "";
            this.BirthDate = new DateTime(2000, 1, 1);
            this.Gender = Person_Gender.Male;
            this.SchoolStatus = School_Status.Active;
            this.Role = School_Role.Student;
            this.DateJoined = DateTime.Today;
            this.Term = Term.Regular;
            this.ContactPerson = string.Empty;
        }
    }
}