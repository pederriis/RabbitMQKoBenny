using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CowService.Controllers
{
    public class LoginClient
    {
        private HttpClient client;

        public LoginClient(HttpClient client)
        {
            this.client = client;
        }

        public string GetData()
        {
            var result = client.GetAsync("/test/").Result;
            var content = result.Content.ReadAsStringAsync().Result;

            return content;
        }
    }
}
