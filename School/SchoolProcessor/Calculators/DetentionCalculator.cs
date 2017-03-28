using Castle.Windsor;
using SchoolDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public delegate void NotifyParents (string message, SchoolModel.Student student);

    public interface IDetentionCalculator
    {
        Task<SchoolModel.DetentionCalculationResult> CalculateResult(SchoolModel.StudentDetension detention);
        Task<SchoolModel.DetentionCalculationResult> CalculateResult(IEnumerable<SchoolModel.StudentDetension> detentions);
        Task<SchoolModel.DetentionCalculationResult> CalculateResult(int studentId, DateTime effectiveDate);
    }

    public abstract class DetentionCalculator :IDetentionCalculator
    {
        protected IWindsorContainer container = new WindsorContainer();


        //pre rules 

        //post rules 
      //  public event NotifyParents NotifyOnExceedingLimit;

        //public async Task<SchoolModel.DetentionCalculationResult> CalculateResult(SchoolModel.Student student, DateTime effectiveDate)
        //{
        //    var container = new WindsorContainer();
        //    var dataservice = container.Resolve<ISchoolDataService>();

        //    var filters = Task.Run(new Func<Task<IEnumerable<DetentionFilter>>>(dataservice.AvailableFilters));

        //    //GET ALL THE DEFINED RULES
        //    var type = typeof(DetentionFilterBase);
        //    var types = AppDomain.CurrentDomain.GetAssemblies()
        //        .SelectMany(s => s.GetTypes())
        //        .Where(p => type.IsAssignableFrom(p));

            
        //    //APPLY ALL RULES BEFORE CALCULATION  (bad /good etc)

        //    //for (int i = 0; i < detetions.Count(); i++)
        //    //{
        //    //    foreach (var thisType in types)
        //    //    {
        //    //        thisType.InvokeMember("ProcessRule", System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { detetions[i] });
        //    //    }
        //    //}
        //    //todo - Apply reflection to automate 
        //    //var concurrent = (from c in detetions
        //    //                  where c.Detenton.Id == 1 //concurrent
        //    //                  select c.Detenton.PunishmentMinutes).Max();

        //    //var concecutive = (from c in detetions
        //    //                   where c.Detenton.Id == 1 //concurrent
        //    //                   select c.Detenton.PunishmentMinutes).Sum();

        //    //var total = concecutive + concurrent;

        //    //if (total > 8 && this.NotifyOnExceedingLimit != null)
        //    //    NotifyOnExceedingLimit("Need to contact to School Immediately", student);

        //    //return new SchoolModel.DetentionCalculationResult { EffectiveResult =Convert.ToInt32( total), StudentId = student.Id, StudentName = student.FullName };
        //    return null;
        //}
        public virtual async Task<SchoolModel.DetentionCalculationResult> CalculateResult(SchoolModel.StudentDetension detention)
        {
            var dataService = container.Resolve<ISchoolDataService>();
            //var isRepeated = Task.Run(new Func<in SchoolModel.StudentDetension, out Task<bool>>(dataService.HasPreviousInstances));
            var filters = Task.Run(new Func<Task<IEnumerable<DetentionFilter>>>(dataService.AvailableFilters));
            await Task.WhenAll((new Task[] { filters }));

            foreach (var item in filters.Result)
            {
                var filter = DetentionFilterBase.Factory(item.FilterProcessor);
                filter.ProcessRule(detention);
            }

            return new SchoolModel.DetentionCalculationResult { EffectiveResult = 0 };
        }

        public virtual Task<SchoolModel.DetentionCalculationResult> CalculateResult(IEnumerable<SchoolModel.StudentDetension> detentions)
        {
            throw new NotImplementedException();
        }


        public async Task<SchoolModel.DetentionCalculationResult> CalculateResult(int studentId, DateTime effectiveDate)
        {
            var service = container.Resolve<ISchoolDataService>();
            var task1 = Task.Run(() => {
                return service.GetStudentDetentionAsync(studentId, effectiveDate);
            });
            var task2 = Task.Run(()=> {
            return service.GetStudentAsync(studentId,0);
            });
            try{
            Task.WaitAll(); }
            catch(AggregateException ex)
            {
                return new SchoolModel.DetentionCalculationResult{ HasError=true, 
                    ErrorMessage = string.Format("{0} - {1}", "Error processing Request",ex.StackTrace)};
            }

            var detentsions =task1.Result; 
            var student = task2.Result; 

            int result = 0; 
            var dt = detentsions.ToList(); 
            for(int i=0; i<dt.Count; i++)
            {
                var item= dt[i];
                item.SuggestedPunishment= result;
                var processor = DetentionCalculator.Factory(item);
                var tempResult = await processor.CalculateResult(item);
                result = tempResult.EffectiveResult;
            }
            return new SchoolModel.DetentionCalculationResult { EffectiveResult = result, HasError = false, StudentId = studentId, StudentName = student.FullName };
        }

        public static IDetentionCalculator Factory(SchoolModel.StudentDetension detention)
        {
            IDetentionCalculator calculator = null;
            // var service = new WindsorContainer().Resolve<ISchoolDataService>(); 
            //static factory at the moment but can be made dynamic as in Filteres

            if (detention.DetentionTypeId == 1)
                calculator = new ConcurrentCalculator();
            if (detention.DetentionTypeId == 2)
                calculator = new ConcurrentCalculator();

            return calculator;
        }
    }

    public class Generalcalculator : DetentionCalculator
    {
    }

    


}
