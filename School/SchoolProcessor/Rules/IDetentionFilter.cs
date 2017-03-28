using SchoolDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public interface IDetentionFilter
    {
        void ProcessRule(SchoolModel.StudentDetension studentDetention); 
    }

    public abstract class DetentionFilterBase : IDetentionFilter
    {
        protected ISchoolDataService schoolService;
        public DetentionFilterBase()
        {
            schoolService = new Castle.Windsor.WindsorContainer().Resolve<ISchoolDataService>();
        }

        public abstract void ProcessRule(SchoolModel.StudentDetension studentDetention);
        
        public static IDetentionFilter Factory(string typeName)
        {
            string[] types = typeName.Split(new char[] { ',' });
            return Activator.CreateInstance(types[1], types[0]) as IDetentionFilter;
        }
    }
}
