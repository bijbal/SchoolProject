using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolService.Controllers
{
    public class StudentController : ApiController
    {
        //dEPENDENCY RESOLVER NEED TO DEBUGG SO ALTERNATE
        SchoolDataService.ISchoolDataService dataService; 
        
        
        public StudentController(SchoolDataService.ISchoolDataService dataSrvc )
        {

            this.dataService = dataSrvc;
        }

        //DI IS NOT WORKING AT THE MOMENT SO SWITCHING TO CONVENSIONAL 
        public StudentController()
        {
            this.dataService = new SchoolDataService.DataService();
        }
        // GET api/student
        public HttpResponseMessage Get()
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Implemented");
        }
           
        // GET api/student/5
        public HttpResponseMessage Get(int id)
        {
            var s1 = dataService.GetStudentAsync(id, 0).Result;
            

            return Request.CreateResponse(HttpStatusCode.OK, s1);
        }

        // POST api/student
        public void Post([FromBody]SchoolModel.Student value)
        {

        }

        // PUT api/student/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/student/5
        public void Delete(int id)
        {
        }

        private IEnumerable<SchoolModel.StudentDetension> CreateDetention(int sid)
        {
            return null;
        }
    }
}
