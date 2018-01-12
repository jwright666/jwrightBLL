using JwrightBLL.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JwrightBLL.DAL
{
    public class DALHelper
    {

        internal DataSet FetchData(string getQuery, SqlConnection sqlCon, SqlTransaction sqlTran)
        {
            try
            {
                //return all data
            }
            catch (Exception ex) { throw new Exception(ex.Message.ToString()); }
            return null;
        }
        internal DataSet FetchData(string getQuery)
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                try
                {
                    //return all data
                    SqlCommand cmd = new SqlCommand(getQuery, sqlCon);
                    cmd.CommandTimeout = 0; 
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }
        
        internal static bool SavePersonelData(Personel personel)
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction();
                try
                {
                    #region insert query
                    string insertQuery = @"Insert Into dbo.Personel
	                                        ([NickName],
                                            [FirstName],
                                            [MiddleName],
                                            [LastName],
                                            [Resident_Address],
                                            [Email_Address],
                                            [ContactNo],
                                            [BirthDate] ,
                                            [Gender],
                                            [SchoolStatus],
                                            [Role],
                                            [Term],
                                            [DateJoined],
                                            [ContactPerson] )

                                        values('{0}',
                                            '{1}',
                                            '{2}',
                                            '{3}',
                                            '{4}',
                                            '{5}',
                                            '{6}',
                                            '{7}',
                                            '{8}',
                                            '{9}',
                                            '{10}',
                                            '{11}',
                                            '{12}',
                                            '{13}'
                                            ) ";
                    #endregion
                    insertQuery = string.Format(insertQuery, personel.NickName,
                                                    personel.FirstName,
                                                    personel.MiddleName,
                                                    personel.LastName,
                                                    personel.Resident_Address,
                                                    personel.Email_Address,
                                                    personel.ContactNo,
                                                    personel.BirthDate.ToShortDateString(),
                                                    personel.Gender == Person_Gender.Female ? "F" : "M",
                                                    personel.SchoolStatus.GetHashCode(),
                                                    personel.Role.GetHashCode(),
                                                    personel.Term.GetHashCode(),
                                                    personel.DateJoined.ToShortDateString(),
                                                    personel.ContactPerson);
                    //insert data into table
                    SqlCommand cmd = new SqlCommand(insertQuery, sqlCon);
                    cmd.Transaction = sqlTran;
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    sqlTran.Commit();
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    throw new Exception(ex.Message.ToString());
                }
                finally { sqlCon.Close(); }
            }
            return true;            
        }
        internal static List<Student> GetAllStudents()
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            List<Student> students = new List<Student>();
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                try
                {
                    string getQuery = @"select * from Personel where Role = 1";
                    SqlCommand cmd = new SqlCommand(getQuery, sqlCon);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = GetStudent(reader);
                        students.Add(student);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            return students;
        }
        internal static Student GetStudent(IDataReader reader)
        {
            Student student = new Student();
            student.IDNo = reader["IDNo"] == DBNull.Value ? 0 : (Int32)reader["IDNo"];
            student.NickName = reader["NickName"] == DBNull.Value ? string.Empty : (string)reader["NickName"];
            student.FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : (string)reader["FirstName"];
            student.MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : (string)reader["MiddleName"];
            student.LastName = reader["LastName"] == DBNull.Value ? string.Empty : (string)reader["LastName"];
            student.Resident_Address = reader["Resident_Address"] == DBNull.Value ? string.Empty : (string)reader["Resident_Address"];
            student.Email_Address = reader["Email_Address"] == DBNull.Value ? string.Empty : (string)reader["Email_Address"];
            student.ContactNo = reader["ContactNo"] == DBNull.Value ? string.Empty : (string)reader["ContactNo"];
            student.BirthDate = reader["BirthDate"] == DBNull.Value ? DateTime.Today : (DateTime)reader["BirthDate"];
            student.Gender = reader["Gender"] == DBNull.Value ? Person_Gender.Male : ((string)reader["Gender"] == "M" ? Person_Gender.Male : Person_Gender.Female);
            int status = Convert.ToInt32(reader["SchoolStatus"].ToString());
            switch (status)
            {
                case 1:
                    student.SchoolStatus = School_Status.Active;
                    break;
                case 2:
                    student.SchoolStatus = School_Status.Inactive;
                    break;
                case 3:
                    student.SchoolStatus = School_Status.Resigned;
                    break;
            }
            int role = Convert.ToInt32(reader["Role"].ToString());
            switch (status)
            {
                case 0:
                    student.Role = School_Role.Admin;
                    break;
                case 1:
                    student.Role = School_Role.Student;
                    break;
                case 2:
                    student.Role = School_Role.Instructor;
                    break;
                case 3:
                    student.Role = School_Role.Staff;
                    break;
                case 4:
                    student.Role = School_Role.Guest;
                    break;
            }
            student.DateJoined = reader["DateJoined"] == DBNull.Value ? DateTime.Today : (DateTime)reader["DateJoined"];
            student.Term = Convert.ToInt32(reader["Term"].ToString()) == 1 ? Term.Regular : Term.Per_Session;
            student.ContactPerson = reader["ContactPerson"] == DBNull.Value ? string.Empty : (string)reader["ContactPerson"];

            return student;
        }
        internal static List<Instructor> GetAllInstructors()
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            List<Instructor> instructors = new List<Instructor>();
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                try
                {
                    string getQuery = @"select * from Personel where Role = 2";
                    SqlCommand cmd = new SqlCommand(getQuery, sqlCon);
                    cmd.CommandTimeout = 0;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Instructor instructor = GetInstructor(reader);
                        instructors.Add(instructor);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            return instructors;
        }
        internal static Instructor GetInstructor(IDataReader reader)
        {
            Instructor instructor = new Instructor();
            instructor.IDNo = reader["IDNo"] == DBNull.Value ? 0 : (Int32)reader["IDNo"];
            instructor.NickName = reader["NickName"] == DBNull.Value ? string.Empty : (string)reader["NickName"];
            instructor.FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : (string)reader["FirstName"];
            instructor.MiddleName = reader["MiddleName"] == DBNull.Value ? string.Empty : (string)reader["MiddleName"];
            instructor.LastName = reader["LastName"] == DBNull.Value ? string.Empty : (string)reader["LastName"];
            instructor.Resident_Address = reader["Resident_Address"] == DBNull.Value ? string.Empty : (string)reader["Resident_Address"];
            instructor.Email_Address = reader["Email_Address"] == DBNull.Value ? string.Empty : (string)reader["Email_Address"];
            instructor.ContactNo = reader["ContactNo"] == DBNull.Value ? string.Empty : (string)reader["ContactNo"];
            instructor.BirthDate = reader["BirthDate"] == DBNull.Value ? DateTime.Today : (DateTime)reader["BirthDate"];
            instructor.Gender = reader["Gender"] == DBNull.Value ? Person_Gender.Male : ((string)reader["Gender"] == "M" ? Person_Gender.Male : Person_Gender.Female);
            int status = (Int32)reader["SchoolStatus"];
            switch(status)
            {
                case 1 :
                    instructor.SchoolStatus = School_Status.Active;
                    break;
                case 2 :
                    instructor.SchoolStatus = School_Status.Inactive;
                    break;
                case 3 :
                    instructor.SchoolStatus = School_Status.Resigned;
                    break;
            }
            int role = (Int32)reader["Role"];
            switch (status)
            {
                case 0:
                    instructor.Role = School_Role.Admin;
                    break;
                case 1:
                    instructor.Role = School_Role.Student;
                    break;
                case 2:
                    instructor.Role = School_Role.Instructor;
                    break;
                case 3:
                    instructor.Role = School_Role.Staff;
                    break;
                case 4:
                    instructor.Role = School_Role.Guest;
                    break;
            }
            instructor.DateJoined = reader["DateJoined"] == DBNull.Value ? DateTime.Today : (DateTime)reader["DateJoined"];
            instructor.Term = (int)reader["Term"] == 1 ? Term.Regular : Term.Per_Session;
            instructor.ContactPerson = reader["ContactPerson"] == DBNull.Value ? string.Empty : (string)reader["ContactPerson"];

            return instructor;
        }

        internal static bool SaveAttendance(Attendance attendance)
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction();
                try
                {
                    #region insert query
                    string insertQuery = @"Insert Into dbo.Attendance ([T_IDNo], [S_IDNo],[DateStarted], [DateStopped])
                                        values('{0}', '{1}', '{2}',  '{3}' ) ";
                    #endregion
                    insertQuery = string.Format(insertQuery, attendance.Teacher_IDNo,
                                                    attendance.Student_IDNo,
                                                    attendance.StartTime.ToString("yyyy-MM-dd HH:mm"),
                                                    attendance.EndTime.ToString("yyyy-MM-dd HH:mm"));

                    //insert data into table
                    SqlCommand cmd = new SqlCommand(insertQuery, sqlCon);
                    cmd.Transaction = sqlTran;
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    sqlTran.Commit();
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    throw new Exception(ex.Message.ToString());
                }
                finally { sqlCon.Close(); }
            }
            return true;            
        }
        internal static Attendance GetAttendance(IDataReader reader)
        {
            Attendance attendance = new Attendance();
            attendance.Teacher_IDNo = reader["T_IDNo"] == DBNull.Value ? string.Empty : (string)reader["T_IDNo"];
            attendance.Student_IDNo = reader["S_IDNo"] == DBNull.Value ? string.Empty : (string)reader["S_IDNo"];
            attendance.StartTime = reader["DateStarted"] == DBNull.Value ? DateTime.Today : (DateTime)reader["DateStarted"];
            attendance.EndTime = reader["DateStopped"] == DBNull.Value ? DateTime.Today : (DateTime)reader["DateStopped"];

            return attendance;
        }
        internal static List<Attendance> GetAllAttendanceByDate(DateTime date)
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            List<Attendance> attendances = new List<Attendance>();
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                try
                {
                    string getQuery = @"select * from Attendance where DateStarted >= '{0}' and DateStarted < '{1}'";

                    getQuery = string.Format(getQuery, date.Date.ToString("yyyy-MM-dd HH:mm"), date.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm"));

                    SqlCommand cmd = new SqlCommand(getQuery, sqlCon);
                    cmd.CommandTimeout = 0;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Attendance attendance = GetAttendance(reader);
                        attendances.Add(attendance);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            return attendances;
        }
        internal static List<Attendance> GetAllAttendanceByStudent(Student student)
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            List<Attendance> attendances = new List<Attendance>();
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                try
                {
                    string getQuery = @"select * from Attendance where S_IDNo = '{0}'";
                    getQuery = string.Format(getQuery, student.IDNo.ToString());

                    SqlCommand cmd = new SqlCommand(getQuery, sqlCon);
                    cmd.CommandTimeout = 0;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Attendance attendance = GetAttendance(reader);
                        attendances.Add(attendance);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            return attendances;
        }
        internal static List<Attendance> GetAllAttendanceByInstructor(Instructor instructor)
        {
            string conString = ConfigurationManager.ConnectionStrings["jwrightCareConString"].ConnectionString;
            List<Attendance> attendances = new List<Attendance>();
            using (SqlConnection sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                try
                {
                    string getQuery = @"select * from Attendance where T_IDNo = '{0}'";
                    getQuery = string.Format(getQuery, instructor.IDNo.ToString());

                    SqlCommand cmd = new SqlCommand(getQuery, sqlCon);
                    cmd.CommandTimeout = 0;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Attendance attendance = GetAttendance(reader);
                        attendances.Add(attendance);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            return attendances;
        }
    
    
    }
}