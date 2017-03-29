using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDataService
{
    public interface ISchoolDataService
    {
        Task<SchoolModel.Student> GetStudentAsync(int id, int schoolId);
        Task<IEnumerable<SchoolModel.StudentDetension>> GetStudentDetentionAsync(int studentId, DateTime date);
        Task<int> CreateDetention(SchoolModel.StudentDetension detention);
        Task<bool> HasPreviousInstances(SchoolModel.StudentDetension det);
        Task<IEnumerable<DetentionFilter>> AvailableFilters();
    }

    
    public sealed partial class DataService : ISchoolDataService, IDisposable
    {
        Entities schoolContext;
        bool isDisposed;
        public DataService(Entities schoolContext)
        {
            if (schoolContext == null) this.schoolContext = new Entities();
            isDisposed = false; 
        }
        public DataService()
            : this(null)
        {

        }

        public async Task<SchoolModel.Student> GetStudentAsync(int id, int schoolId)
        {
            var student = (from s in schoolContext.Students
                          where s.Id==id 
                          select s).FirstOrDefault();

            if (student == null) return null;// Task.FromResult<SchoolModel.Student>(null);
            //automapper candidate
            var studentb = new SchoolModel.Student
            {
                Class = student.Class,
                Division = student.Division,
                FirstName = student.FirstName,
                Id = student.Id,
                LastName = student.LastName,
                ParentContact = student.ParentContact,
                ParentName = student.ParentName,
                SchoolId = student.SchoolId.Value
            };
            return studentb;// Task.FromResult<SchoolModel.Student>(studentb);
        }

        public Task<IEnumerable<SchoolModel.StudentDetension>> GetStudentDetentionAsync(int id, DateTime date)
        {
            var studentDetetion = (from s in schoolContext.Students
                                   join d in schoolContext.StudentDetentions on s.Id equals d.StudentId
                                   join dt in schoolContext.Detentions on d.DetentionId equals dt.Id
                                   where s.Id==id 
                                   select new SchoolModel.StudentDetension
                                   {
                                       Id = d.Id,
                                       DetentionDuration = dt.PenaltyInMinutes,
                                       DetentionTypeId = dt.Id,
                                       DetentionTypeName = dt.Name,
                                       PunishmentTime = d.Duration,
                                       StartDate = d.StartDate,
                                       StudentId = s.Id,
                                       StudentName = s.FirstName,
                                       Remarks = d.Remarks
                                       ,
                                       DetentionActionID = d.DetentionActionId.Value
                                   }
                                   );
            if (studentDetetion != null && studentDetetion.Count() > 0)
                return Task.FromResult<IEnumerable<SchoolModel.StudentDetension>>(studentDetetion.ToList());

            return Task.FromResult<IEnumerable<SchoolModel.StudentDetension>>(null);
        }

        public Task<int> CreateDetention(SchoolModel.StudentDetension detention)
        {
            string qry = @"INSERT INTO [StudentDetention] ([StudentId] ,[DetentionId],[StartDate],[Duration],[Remarks],[DetentionActionId]) 
            VALUES  @studentId, @detentionId, @date, @duration, @remarks, @actionId";
            var qryparams = new object[] {
                 CreateParameter("@studentId", SqlDbType.Int, detention.StudentId),
                 CreateParameter("@detentionId", SqlDbType.Int, detention.DetentionTypeId),
                 CreateParameter("@date", SqlDbType.Int, detention.StartDate),
                 CreateParameter("@duration", SqlDbType.Int, detention.PunishmentTime),
                 CreateParameter("@remarks", SqlDbType.Int, detention.Remarks), //refresh schema
                 CreateParameter("@actionId", SqlDbType.Int, detention.DetentionActionID), //refresh schema

             };
            //schoolContext.Database.ExecuteSqlCommand(qry,
            int rows = schoolContext.Database.ExecuteSqlCommand(qry, qryparams);
            return Task.FromResult<int>(rows);
        }

        private SqlParameter CreateParameter(string name, SqlDbType DbType, object val)
        {
            return new SqlParameter { Value = val, SqlDbType = DbType, ParameterName = name };
        }

        public Task<bool> HasPreviousInstances(SchoolModel.StudentDetension det)
        {
            //throw new NotImplementedException();
            var pds = (from m in schoolContext.StudentDetentions
                       where m.StudentId == det.StudentId && m.DetentionId == det.DetentionTypeId
                       select m).Take(1);
            var result = pds.Count() > 0;
            return Task.FromResult<bool>(result);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool canDispose)
        {
            if (canDispose)
            {
                if (schoolContext != null)
                    schoolContext.Dispose();
                isDisposed = true;
            }
        }
        
    }
}
