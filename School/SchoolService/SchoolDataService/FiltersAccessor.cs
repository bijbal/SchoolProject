using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDataService
{
    public partial class DataService:ISchoolDataService
    {


        public async Task<IEnumerable<DetentionFilter>> AvailableFilters()
        {
            var filters = (from f in schoolContext.DetentionFilters
                           where f.IsActive == true
                           select f).ToList();

            return filters;
        }
    }
}
