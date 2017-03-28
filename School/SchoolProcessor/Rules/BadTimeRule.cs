using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public class BadTimeRule : DetentionFilterBase
    {

        public override async void ProcessRule(SchoolModel.StudentDetension studentDetention)
        {
            var repeatedOffence = await schoolService.HasPreviousInstances(studentDetention.StudentId, studentDetention.DetentionTypeId);
            if (repeatedOffence)
            {
                double duration = (double)studentDetention.DetentionDuration;
                studentDetention.PunishmentTime = Convert.ToInt32(duration * 1.1);
            }
        }
    }
}
