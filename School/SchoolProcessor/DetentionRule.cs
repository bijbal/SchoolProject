using SchoolDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public interface IDetentionRule
    {
        void ProcessRule(SchoolModel.DetentionType detetion, SchoolModel.Student student); 

    }

    public abstract class DetentionRuleBase : IDetentionRule
    {
        protected IDataService schoolService;
        public DetentionRuleBase()
        {
            schoolService = new Castle.Windsor.WindsorContainer().Resolve<IDataService>();

        }

        public abstract void ProcessRule(SchoolModel.DetentionType detetion, SchoolModel.Student student);

        

    }

    public class GoodTimeRule:DetentionRuleBase
    {

        

        public override void ProcessRule(SchoolModel.DetentionType detetion, SchoolModel.Student student)
        {
            var repeates = schoolService.PreviousInstance(detetion.Id, student.Id);
            if (repeates.Count() == 0){

                detetion.PunishmentMinutes = detetion.PunishmentMinutes * 0.9 ;
                
            }
            //    detetion.PunishmentMinutes = 
        }
    }

    public class BadTimeRule : DetentionRuleBase
    {
        public override void ProcessRule(SchoolModel.DetentionType detetion, SchoolModel.Student student)
        {
            var repeates = schoolService.PreviousInstance(detetion.Id, student.Id);
            if (repeates.Count() > 0)
                detetion.PunishmentMinutes = detetion.PunishmentMinutes * 1.1;
        }
    }
}
