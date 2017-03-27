using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolModel
{
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; 
}
    }

    public abstract class SchoolPerson
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return string.Format("{0}, {1}", LastName, FirstName); }
        }
    }

    public class Teacher: SchoolPerson
    {
        
    }

    public class HeadMaster : Teacher { }


    public class Student : SchoolPerson
    {
        public string Class { get; set; }
        public string Division { get; set; }
        public string ClassFullName { get { return string.Format("{0}, {1}", Class, Division); } }
        public string ParentName { get; set; }
        public string ParentContact { get; set; }
        //public List<StudentDetension> DetensionHistory { get; set; }
    }

    public class StudentDetension
    {
        public int DetentionTypeId { get; set; }
        public int StudentID { get; set; }
        public DateTime StartDate { get; set; }
        public DetentionType Detenton{ get; set; }
        public StudentDetension()
        {
            this.Detenton = new DetentionType();
        }
    }

    public class DetentionType
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double PunishmentMinutes { get; set; }
    }

    public class DetentionCalculationResult
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int EffectiveResult { get; set; }
    }
}
