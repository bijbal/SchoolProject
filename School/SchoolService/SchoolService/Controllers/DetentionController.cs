using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolService.Controllers
{
    public class DetentionController : ApiController
    {
        SchoolDataService.ISchoolDataService dataService; 
        
        
        public DetentionController(SchoolDataService.ISchoolDataService dataSrvc )
        {

            this.dataService = dataSrvc;
        }

        //DI IS NOT WORKING AT THE MOMENT SO SWITCHING TO CONVENSIONAL 
        public DetentionController()
        {
            this.dataService = new SchoolDataService.DataService();
        }
        // GET api/detention
        public IEnumerable<string> Get()
        {
            return new string[] { };

        }

        // GET api/detention/5

        public HttpResponseMessage Get(int studentId, string dateStr)
        {
            try
            {
                var date = DateTime.Today;
                DateTime.TryParseExact(dateStr, "DD-MM-YYYY", new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out date);
                var response = dataService.GetStudentDetentionAsync(studentId, date).Result;
                if (response==null || response.Count() == 0) throw new Exception("No Records found!");
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error processing Request", ex);
            }
        }

        
        // POST api/detention
        public void Post(int studentId, string dateStr)
        {
            var processor = new SchoolProcessor.Generalcalculator();
            var result = processor.CalculateResult(studentId, DateTime.Today);

        }

    }
}
