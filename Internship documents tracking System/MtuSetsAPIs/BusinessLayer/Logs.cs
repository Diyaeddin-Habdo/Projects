using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Logs
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public LogsDTO LDTO
        {
            get
            {
                return (new LogsDTO(this.Id,this.TeacherId,this.DocumentsId,this.Status,this.Time));
            }
        }
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int DocumentsId { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }


        public Logs(LogsDTO LDTO, enMode cMode = enMode.AddNew)
        {
            this.Id = LDTO.Id;
            this.TeacherId = LDTO.TeacherId;
            this.DocumentsId = LDTO.DocumentsId;
            this.Status = LDTO.Status;
            this.Time = LDTO.Time;

            Mode = cMode;
        }

        //private bool _AddNewStudent()
        //{
        //    //call DataAccess Layer 

        //    this.Id = StudentData.AddStudent(LDTO);

        //    return (this.Id != -1);
        //}

        //private bool _UpdateStudent()
        //{
        //    return StudentData.UpdateStudent(SDTO);
        //}

        //public static List<LogsDTO> GetAllLogs()
        //{
        //    //return StudentData.GetAllStudents();
        //}

        //public static Student? Find(int ID)
        //{

        //    StudentDTO SDTO = StudentData.GetStudentById(ID);

        //    if (SDTO != null)
        //    //we return new object of that student with the right data
        //    {

        //        return new Student(SDTO, enMode.Update);
        //    }
        //    else
        //        return null;
        //}

        //public bool Save()
        //{
        //    switch (Mode)
        //    {
        //        case enMode.AddNew:
        //            if (_AddNewStudent())
        //            {

        //                Mode = enMode.Update;
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        case enMode.Update:

        //            return _UpdateStudent();

        //    }

        //    return false;
        //}

        //public static bool DeleteStudent(int ID)
        //{
        //    return StudentData.DeleteStudent(ID);
        //}



        /// <summary>
        /// Retrieves a list of approval logs for a specific teacher.
        /// </summary>
        /// <param name="teacherId">The ID of the teacher.</param>
        /// <returns>A list of teacher approval logs, or an empty list if an error occurs.</returns>
        public static List<TeacherApprovalLogsDTO> GetTeacherApprovalLogs(int teacherId)
        {
            return LogsData.GetTeacherApprovalLogs(teacherId);
        }


        /// <summary>
        /// Retrieves a list of approval logs for a specific student based on their ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>A list of approval logs for the student, or an empty list if an error occurs.</returns>
        public static List<TeacherApprovalLogsForStudentDTO> GetTeacherApprovalLogsByStudentId(int studentId)
        {
            return LogsData.GetTeacherApprovalLogsByStudentId(studentId);
        }
    }
}
