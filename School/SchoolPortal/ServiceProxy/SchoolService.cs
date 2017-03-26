using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;

namespace SchoolPortal
{
    public class SchoolService
    {

        public async Task<Uri> CreateDetention(SchoolModel.StudentDetension detetion)
        {
            string baseUri = System.Configuration.ConfigurationManager.AppSettings["serviceUri"];
            var data = System.Web.Helpers.Json.Encode(detetion);
            StringContent content = new StringContent(data); 
            
            using (HttpClient apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Accept.Add( new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));
                apiClient.BaseAddress = new Uri(baseUri);
                var ersponse = await apiClient.PostAsync("detention", content);

                return ersponse.Headers.Location;
            }
        }
    }
}