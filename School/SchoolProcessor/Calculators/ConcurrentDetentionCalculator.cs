using Castle.Windsor;
using SchoolDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProcessor
{
    public class ConcurrentCalculator : DetentionCalculator
    {
        public override async Task<SchoolModel.DetentionCalculationResult> CalculateResult(SchoolModel.StudentDetension detention)
        {
            var suggestion = detention.SuggestedPunishment >= detention.PunishmentTime? detention.SuggestedPunishment: detention.PunishmentTime;
            return new SchoolModel.DetentionCalculationResult { EffectiveResult = suggestion, HasError = false, StudentId = detention.StudentId, StudentName = detention.StudentName };
        }
    }
}