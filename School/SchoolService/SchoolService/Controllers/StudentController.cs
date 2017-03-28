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
        // GET api/student
        public HttpResponseMessage Get()
        {
            var s1=  new SchoolModel.Student
            {
                Id = 1,
                Division = "AA",
                Class = "VI",
                FirstName = "Johny Jr.",
                LastName = "Johny",
                SchoolId = 1,
                
            };

            var s2 = new SchoolModel.Student
            {
                Id = 2,
                Division = "AA",
                Class = "VI",
                FirstName = "ABC Jr.",
                LastName = "ABC",
                SchoolId = 1,
                
            };
            var ret = new List<SchoolModel.Student>(); 
            ret.Add(s1);
            ret.Add(s2);
            return Request.CreateResponse( HttpStatusCode.OK, ret);
        }
           
        // GET api/student/5
        public HttpResponseMessage Get(int id)
        {
            var s1 = new SchoolModel.Student
            {
                Id = id,
                Division = "AA",
                Class = "VI",
                FirstName = "Johny Jr.",
                LastName = "Johny",
                SchoolId = 1,

            };

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
