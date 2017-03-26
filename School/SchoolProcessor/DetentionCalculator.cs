using Castle.Windsor;
using SchoolDataService;
using System;
using System.Linq;

namespace SchoolProcessor
{
    public delegate void NotifyParents (string message, SchoolModel.Student student); 

    public interface IDetentionCalculator
    {
        SchoolModel.DetentionCalculationResult CalculateResult(SchoolModel.Student student, DateTime effectiveDate);
    }

    public class DetentionCalculator :IDetentionCalculator
    {
        public event NotifyParents NotifyOnExceedingLimit;

        public SchoolModel.DetentionCalculationResult CalculateResult(SchoolModel.Student student, DateTime effectiveDate)
        {
            var container = new WindsorContainer();
            var dataservice = container.Resolve<IDataService>();
            var detetions = dataservice.GetStudentDetention(student.Id, effectiveDate).ToArray();

            //GET ALL THE DEFINED RULES
            var type = typeof(DetentionRuleBase);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            
            //APPLY ALL RULES BEFORE CALCULATION  (bad /good etc)

            for (int i = 0; i < detetions.Count(); i++)
            {
                foreach (var thisType in types)
                {
                    thisType.InvokeMember("ProcessRule", System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { detetions[i] });
                }
            }
            //todo - Apply reflection to automate 
            var concurrent = (from c in detetions
                              where c.Detenton.Id == 1 //concurrent
                              select c.Detenton.PunishmentMinutes).Max();

            var concecutive = (from c in detetions
                               where c.Detenton.Id == 1 //concurrent
                               select c.Detenton.PunishmentMinutes).Sum();

            var total = concecutive + concurrent;

            if (total > 8 && this.NotifyOnExceedingLimit != null)
                NotifyOnExceedingLimit("Need to contact to School Immediately", student);

            return new SchoolModel.DetentionCalculationResult { EffectiveResult =Convert.ToInt32( total), StudentId = student.Id, StudentName = student.FullName };
            
        }
    }

    


}
