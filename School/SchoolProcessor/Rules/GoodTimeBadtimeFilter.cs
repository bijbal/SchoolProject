using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public class GoodTimeBadTimeFilter : DetentionFilterBase
    {
        public async override void ProcessRule(SchoolModel.StudentDetension studentDetention)
        {
            var isRepeatedoffence = await schoolService.HasPreviousInstances(studentDetention);
            if (isRepeatedoffence)
            {
                double duration = (double)studentDetention.DetentionDuration;
                studentDetention.PunishmentTime = Convert.ToInt32(duration * 1.1);
            }
            else
            {
                double duration = (double)studentDetention.DetentionDuration;
                studentDetention.PunishmentTime = Convert.ToInt32(duration * 0.9);
            }

        }

        

    }
}
