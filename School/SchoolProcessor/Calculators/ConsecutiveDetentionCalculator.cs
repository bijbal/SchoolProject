using Castle.Windsor;
using SchoolDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public abstract class ConsecutiveCalculator : DetentionCalculator {



        public override async Task<SchoolModel.DetentionCalculationResult> CalculateResult(SchoolModel.StudentDetension detention)
        {
            var suggestion = detention.SuggestedPunishment + detention.PunishmentTime;
            var rslt = new SchoolModel.DetentionCalculationResult { EffectiveResult = suggestion, HasError = false, StudentId = detention.StudentId, StudentName = detention.StudentName };
           // return Task.FromResult<SchoolModel.DetentionCalculationResult>(rslt);
            return rslt;
        }
    
    }
}