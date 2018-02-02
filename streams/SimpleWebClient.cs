using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Net;

using System.Net.Http;
using System.IO;

namespace streams
{
    class SimpleWebClient
    {

       // private WebClient client;
        private string url;

        public SimpleWebClient(string url)
        {
            //this.client = new WebClient();

            this.url = url;


        }

        public string getData(out bool status, out string msg)
        {

            status = true;
            msg = null;
            string data = "";

            try
            {
                WebRequest request = WebRequest.Create(this.url);
                WebResponse response = request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    data = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                status = false;
                msg = ex.ToString();
            }
            return data;
        }

    }
}
