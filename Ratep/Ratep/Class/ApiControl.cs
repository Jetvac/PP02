using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ratep.Class
{
    public class ApiControl
    {
        public static async Task<string> GetRequest(string method)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(App.URL + method);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        public static async Task<string> POSTRequest(string args, Dictionary<string, string> body = null)
        {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(body == null ? new Dictionary<string, string>() : body);

            var response = await client.PostAsync(App.URL + args, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
