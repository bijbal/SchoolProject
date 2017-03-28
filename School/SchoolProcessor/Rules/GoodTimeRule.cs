using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public class GoodTimeFilter : DetentionFilterBase
    {

        public async override void ProcessRule(SchoolModel.StudentDetension studentDetention)
        {
            var repeatedOffence = await schoolService.HasPreviousInstances(studentDetention.StudentId, studentDetention.DetentionTypeId);

        }


    }
}
