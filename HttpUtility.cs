using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utilitys
{
    public static class HttpUtility
    {
        public static string HttpGet(string url)
        {
            string retString;

            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";

            try
            {
                var response = request.GetResponse();
                using (var myResponseStream = response.GetResponseStream())
                {
                    var myStreamReader = new StreamReader(myResponseStream);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("get response error: {0}", ex.Message);
                retString = "";
            }

            return retString;
        }

        public static string HttpPost(string url, object body)
        {
            string retString;

            var logMsg = string.Format("url:{0}, body:{1}", url, body);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/json;charset=utf-8";
            var bodyStr = JsonConvert.SerializeObject(body);
            var bytes = Encoding.UTF8.GetBytes(bodyStr);
            request.ContentLength = bytes.Length;

            using (var myRequestStream = request.GetRequestStream())
            {
                myRequestStream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                var response = request.GetResponse();
                using (var myResponseStream = response.GetResponseStream())
                {
                    var myStreamReader = new StreamReader(myResponseStream);
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[MongoApi] get response error: {0}", ex.Message);
                retString = "";
            }

            return retString;
        }
    }
}
