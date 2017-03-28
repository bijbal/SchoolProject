using Castle.MicroKernel.Registration;
using SchoolDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProcessor.Tests
{
   public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<ISchoolDataService>().ImplementedBy(typeof(DataMock)));
        }
    }


   public class DataMock: ISchoolDataService
   {
       SchoolModel.Student s = new SchoolModel.Student
       {
           Class = "VI",
           Division = "A",
           FirstName = "Johny",
           Id = 2,
           LastName = "Johny Sr."
           ,
           ParentContact = "9100000100",
           ParentName = "JohnySr. Johny SSr",
           SchoolId = 1
       };

       SchoolModel.DetentionType dt = new SchoolModel.DetentionType { Id = 1, Name = "Lying", PunishmentMinutes = 30 };
       SchoolModel.DetentionType d2 = new SchoolModel.DetentionType { Id = 2, Name = "Fighting", PunishmentMinutes = 30 };


       SchoolModel.StudentDetension sd1 = new SchoolModel.StudentDetension
       {
             DetentionDuration=0
             , DetentionTypeId= 1
             , DetentionTypeName= "Lying"
             , Id=1
             , PunishmentTime=0
             , Remarks ="Lyied at school"
             , StartDate= DateTime.Today
             , StudentId=2
             , StudentName = "Johny Johny"
             , SuggestedPunishment=0
       };

       SchoolModel.StudentDetension sd2 = new SchoolModel.StudentDetension
       {
           DetentionDuration = 0
             ,
           DetentionTypeId = 1
             ,
           DetentionTypeName = "Lying"
             ,
           Id = 1
             ,
           PunishmentTime = 0
             ,
           Remarks = "Lyied at school"
             ,
           StartDate = DateTime.Today
             ,
           StudentId = 2
             ,
           StudentName = "Johny Johny"
             ,
           SuggestedPunishment = 0
       };

        

       public Task<SchoolModel.Student> GetStudentAsync(int id, int schoolId)
       {
           return new Task<SchoolModel.Student>(() =>
           { return s; });

       }

       public Task<IEnumerable<SchoolModel.StudentDetension>> GetStudentDetentionAsync(int studentId, DateTime date)
       {
           return Task.FromResult<IEnumerable<SchoolModel.StudentDetension>>(
               new SchoolModel.StudentDetension[] { sd1, sd2 }
           );
       }

       public Task<int> CreateDetention(SchoolModel.StudentDetension detention)
       {
           return Task.FromResult<int>(0);
       }

       public Task<bool> HasPreviousInstances(SchoolModel.StudentDetension det)
       {
           return Task.Run(() => { return true; });
       }

       public Task<IEnumerable<DetentionFilter>> AvailableFilters()
       {
           var f1 = new DetentionFilter
           {
               Id = 1,
               FilterName = "Good Bad",
               FilterProcessor = "SchoolProcessor.GoodTimeBadTimeFilter, SchoolProcessor",
               IsActive = true
           };

           return Task.Run(() => { return new DetentionFilter[] { f1 }.AsEnumerable(); });
       }
   }
}
