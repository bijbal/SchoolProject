using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// THIS IS A DATABASE + DISTRIBUTED CACHE MOCK UP hosted currently by the api service ***************** 
//AS I DONT HAVE A DB SERVER LOCALLY ; written this up as a backup to the DataService (School Service)
namespace SchoolDataService
{
    //static means simple and threadsafe - again this is a mock up to realtime apps like redis or any memcache among different scaled out services
    public static class CacheMock
    {
        static Dictionary<string, System.Data.DataTable> cache;
        
    }

    public interface IDataService
    {
        Task<SchoolModel.Student> GetStudent(int id); 
        IEnumerable<SchoolModel.StudentDetension> GetStudentDetention(int id, DateTime date);
        IEnumerable<SchoolModel.StudentDetension> GetStudentDetention(int id);


        IEnumerable<SchoolModel.StudentDetension> PreviousInstance(int detentionTypeId, int studentId);
    }
    //Singleton Service
    public class DataService : IDataService
    {
        DataSet School;
        static DataService _instance; 
        static object padLock = new object();
        public static DataService Instance
        {
            get
            {
                if (_instance != null) return _instance;
                else
                    lock (padLock)
                    {
                        _instance = new DataService(); return _instance;
                    }
            }
        }
        private DataService()
        {
            School = new DataSet();
            FillSchool();
        }

        void FillSchool() {

            DataTable dt = new DataTable("Student");
            dt.Columns.Add("Id", typeof(Int32));
            dt.Columns.Add("SchoolId", typeof(Int32));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Class", typeof(string));
            dt.Columns.Add("Division", typeof(string));
            
            School.Tables.Add(dt);
            var johny = dt.NewRow();
            johny["id"] = 1;
            johny["SchoolId"] = 1;
            johny["FirstName"] = "Johny";
            johny["LastName"] = "Johny";
            johny["Class"] = "VI";
            johny["Division"] = "AA";
            dt.Rows.Add(johny);

            var det = new DataTable("StudentDetention");
            det.Columns.Add("Id", typeof(int));
            det.Columns.Add("DetentionType", typeof(int));
            det.Columns.Add("PunishmentMinutes", typeof(int));
            det.Columns.Add("Name", typeof(string));
            School.Tables.Add(det);
            
            var detrow = det.NewRow();
            detrow["Id"] = 1;
            detrow["Id"] = 1;
            detrow["Id"] = 1;
            detrow["Id"] = 1; 

        }


        public Task<SchoolModel.Student> GetStudent(int id)
        {
            var s = School.Tables["Student"].Select(string.Format("Id={0}", id));
            if (s.Count() > 0) return new Task<SchoolModel.Student>(() => new SchoolModel.Student());
            else return null;

        }


        public IEnumerable<SchoolModel.StudentDetension> GetStudentDetention(int id, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SchoolModel.StudentDetension> GetStudentDetention(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<SchoolModel.StudentDetension> PreviousInstance(int detentionTypeId, int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
