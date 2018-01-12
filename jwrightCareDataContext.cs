using JwrightBLL.BLL;
using JwrightBLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jwrightBLL
{
    public class jwrightCareDataContext
    {
        public IEnumerable<Student> Students { get { return DALHelper.GetAllStudents(); } }
        public IEnumerable<Instructor> Instructors { get { return DALHelper.GetAllInstructors(); } }
        

        public bool Add(Personel personel)
        { 
            try
            {
                DALHelper.SavePersonelData(personel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return true;
        }
        public IEnumerable<Attendance> GetAttendanceByDate(DateTime? date = null)
        {
            try
            {
                if(date == null)
                    return DALHelper.GetAllAttendanceByDate(DateTime.Today);
                else
                    return DALHelper.GetAllAttendanceByDate((DateTime)date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public IEnumerable<Attendance> GetAttendanceByStudent(Student student)
        {
            try
            {
                return DALHelper.GetAllAttendanceByStudent(student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public IEnumerable<Attendance> GetAttendanceByInstructor(Instructor instructor)
        {
            try
            {
                return DALHelper.GetAllAttendanceByInstructor(instructor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
