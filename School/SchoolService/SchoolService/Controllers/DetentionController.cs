using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolService.Controllers
{
    public class DetentionController : ApiController
    {
        // GET api/detention
        public IEnumerable<string> Get()
        {
            yield return "Detention 1";
            yield return "Detention 2";
            yield return "Detention 3";
        }

        // GET api/detention/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/detention
        public void Post([FromBody]string value)
        {
        }

        // PUT api/detention/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/detention/5
        public void Delete(int id)
        {
        }
    }
}
