using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;

namespace SchoolPortal
{
    public class SchoolServiceProxy
    {
        string uribase;
        public SchoolServiceProxy()
        {
            uribase = System.Configuration.ConfigurationManager.AppSettings["ServiceUrlBase"];
        }
        public async Task<Uri> CreateDetention(SchoolModel.StudentDetension detetion)
        {
            var data = System.Web.Helpers.Json.Encode(detetion);
            StringContent content = new StringContent(data);

            using (HttpClient apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));
                apiClient.BaseAddress = new Uri(uribase);
                var ersponse = await apiClient.PostAsync("detention", content);

                return ersponse.Headers.Location;
            }
        }

        public SchoolModel.Student  GetStrudent(int id)
        {
            //var data = System.Web.Helpers.Json.Encode(id); Details/1
            //StringContent content = new StringContent(id.ToString());

            using (HttpClient apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));
                
                string url = string.Format("{1}api/Student/{0}", id, "/");
                apiClient.BaseAddress = new Uri(uribase);
                var response = apiClient.GetStringAsync(url);
                Task.WaitAll();
                string dat = response.Result;
                var stdnt = System.Web.Helpers.Json.Decode<SchoolModel.Student>(dat);
                return stdnt;
            }
        }
    }
}
