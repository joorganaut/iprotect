using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoreBusinessLogic
{
    public class HTTPEngine : IDisposable
    {
        public string GetMessage(string url, string request, out string errMsg)
        {
            string response = string.Empty;
            errMsg = string.Empty;
            try
            {
                Console.WriteLine("Connecting to {0}", url);
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.Proxy = null;
                req.KeepAlive = false;
                req.Method = "GET";// methodName;//Method.ToUpper();
                byte[] buffer = Encoding.ASCII.GetBytes(request);
                req.ContentLength = buffer.Length;
                req.ContentType = "text/plain";
                //InfoLogger.WriteLog("Before getting request stream");
                Stream PostData = req.GetRequestStream();
                //InfoLogger.WriteLog("I got Request Stream");
                PostData.Write(buffer, 0, buffer.Length);
                //InfoLogger.WriteLog("Wrote POST Data");
                PostData.Close();
                //nfoLogger.WriteLog("Before getting Response");
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                //InfoLogger.WriteLog("I got Response");
                Encoding enc = System.Text.Encoding.UTF8;
                //InfoLogger.WriteLog("Before getting Response Stream");
                StreamReader loResponseStream =
                new StreamReader(resp.GetResponseStream(), enc);
                //InfoLogger.WriteLog("I got Response Stream");
                response = loResponseStream.ReadToEnd();
                //InfoLogger.WriteLog("This is the response: " + response);
                loResponseStream.Close();
                resp.Close();
            }
            catch (WebException we)
            {
                StreamReader loResponseStream =
                new StreamReader(we.Response.GetResponseStream(), System.Text.Encoding.UTF8);
                string errMsgXml = loResponseStream.ReadToEnd();
                errMsg = errMsgXml;
                //InfoLogger.WriteLog(errMsgXml);
            }
            catch (Exception e)
            {
                //InfoLogger.WriteLog(string.Format("Error Connecting to {0}, {1}", url, e.Message));
                //InfoLogger.WriteLog(e.InnerException != null ? e.InnerException.Message : "");
                errMsg = e.Message;
            }
            return response;
        }
        public string PostMessage(string url, string request, out string errMsg)
        {
            string response = string.Empty;
            errMsg = string.Empty;
            try
            {
                Console.WriteLine("Connecting to {0}", url);
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.Proxy = null;
                req.KeepAlive = false;
                req.Method = "POST";// methodName;//Method.ToUpper();
                byte[] buffer = Encoding.ASCII.GetBytes(request);
                req.ContentLength = buffer.Length;
                req.ContentType = "text/plain";
                //InfoLogger.WriteLog("Before getting request stream");
                Stream PostData = req.GetRequestStream();
                //InfoLogger.WriteLog("I got Request Stream");
                PostData.Write(buffer, 0, buffer.Length);
                //InfoLogger.WriteLog("Wrote POST Data");
                PostData.Close();
                //InfoLogger.WriteLog("Before getting Response");
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                //InfoLogger.WriteLog("I got Response");
                Encoding enc = System.Text.Encoding.UTF8;
                //InfoLogger.WriteLog("Before getting Response Stream");
                StreamReader loResponseStream =
                new StreamReader(resp.GetResponseStream(), enc);
                //InfoLogger.WriteLog("I got Response Stream");
                response = loResponseStream.ReadToEnd();
                //InfoLogger.WriteLog("This is the response: " + response);
                loResponseStream.Close();
                resp.Close();
            }
            catch (WebException we)
            {
                StreamReader loResponseStream =
                new StreamReader(we.Response.GetResponseStream(), System.Text.Encoding.UTF8);
                string errMsgXml = loResponseStream.ReadToEnd();
                errMsg = errMsgXml;
                //InfoLogger.WriteLog(errMsgXml);
            }
            catch (Exception e)
            {
                //InfoLogger.WriteLog(string.Format("Error Connecting to {0}, {1}", url, e.Message));
                //InfoLogger.WriteLog(e.InnerException != null ? e.InnerException.Message : "");
                errMsg = e.Message;
            }
            return response;
        }

        public string PostMessage(string url, string request, string contentType, out string errMsg, bool isNew)
        {
            string response = string.Empty;
            errMsg = string.Empty;
            try
            {
                Console.WriteLine("Connecting to {0}", url);
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.Proxy = null;
                req.KeepAlive = false;
                req.Method = "POST";// methodName;//Method.ToUpper();
                byte[] buffer = Encoding.ASCII.GetBytes(request);
                req.ContentLength = buffer.Length;
                req.ContentType = contentType;
                //InfoLogger.WriteLog("Before getting request stream");
                Stream PostData = req.GetRequestStream();
                //InfoLogger.WriteLog("I got Request Stream");
                PostData.Write(buffer, 0, buffer.Length);
                //InfoLogger.WriteLog("Wrote POST Data");
                PostData.Close();
                //InfoLogger.WriteLog("Before getting Response");
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                //InfoLogger.WriteLog("I got Response");
                Encoding enc = System.Text.Encoding.UTF8;
                //InfoLogger.WriteLog("Before getting Response Stream");
                StreamReader loResponseStream =
                new StreamReader(resp.GetResponseStream(), enc);
                //InfoLogger.WriteLog("I got Response Stream");
                response = loResponseStream.ReadToEnd();
                //InfoLogger.WriteLog("This is the response: " + response);
                loResponseStream.Close();
                resp.Close();
            }
            catch (WebException we)
            {
                StreamReader loResponseStream =
                new StreamReader(we.Response.GetResponseStream(), System.Text.Encoding.UTF8);
                string errMsgXml = loResponseStream.ReadToEnd();
                errMsg = errMsgXml;
                //InfoLogger.WriteLog(errMsgXml);
            }
            catch (Exception e)
            {
                //InfoLogger.WriteLog(string.Format("Error Connecting to {0}, {1}", url, e.Message));
                //InfoLogger.WriteLog(e.InnerException != null ? e.InnerException.Message : "");
                errMsg = e.Message;
            }
            return response;
        }


        public String PostMessage(string uri, string data, string action, out string errMsg)
        {
            string retVal = "";//Constants.Failed.Value;
            errMsg = string.Empty;
            try
            {
                using (var client = new WebClient())
                {
                    // read the raw SOAP request message from a file
                    client.Proxy = null;
                    // the Content-Type needs to be set to XML
                    client.Headers.Add("Content-Type", "text/xml;charset=utf-8");
                    // The SOAPAction header indicates which method you would like to invoke
                    // and could be seen in the WSDL: <soap:operation soapAction="..." /> element
                    client.Headers.Add("SOAPAction", action);

                    retVal = client.UploadString(uri, data);
                    //InfoLogger.WriteLog(retVal);
                }
            }
            catch (WebException we)
            {
                StreamReader loResponseStream =
                               new StreamReader(we.Response.GetResponseStream(), System.Text.Encoding.UTF8);
                string errMsgXml = loResponseStream.ReadToEnd();
                errMsg = errMsgXml;
                //InfoLogger.WriteLog(errMsgXml);
            }
            return retVal;
        }
        public void Dispose()
        {

        }
        //public static string GetClientIP()
        //{
        //    string clientAddress = string.Empty;
        //    string IP = Request.GetOwinContext().Request.RemoteIpAddress;
        //    clientAddress = HttpContext.Current.Request.UserHostAddress;
        //    return clientAddress;
        //}
    }

    public static class HttpClientExtension
    {
        public static async Task<T> PostAsync<T>(this HttpClient client, object obj, string uri)
        {
            T result = default(T);
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8);
            var stringResponse = await (await client.PostAsync(uri, content)).Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<T>(stringResponse);
            if (client != null) client.Dispose();
            return result;
        }
        public static async Task<T> PostAsync<T>(this HttpClient client, string uri)
        {
            T result = default(T);
             var stringResponse = await (await client.PostAsync(uri, null)).Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<T>(stringResponse);
            if (client != null) client.Dispose();
            return result;
        }
        public static async Task<T> GetAsync<T>(this HttpClient client, string url, params IDictionary<string, string>[] param)
        {
            T response = default(T);
            StringBuilder _params = new StringBuilder();
            if (param != null && param.Length > 0)
            {
                _params.Append("?");

                _params.Append(param.Select(x => {

                    return $"{x.Keys}={x.Values}";

                }).Aggregate((current, next) => current + "&" + next));
            }
            var stringResponse = await (await client.GetAsync(url + _params)).Content.ReadAsStringAsync();
            response = JsonConvert.DeserializeObject<T>(stringResponse);
            if (client != null) client.Dispose();
            return response;
        }
    }
}
