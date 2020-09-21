using System;
using System.IO;
using System.Net;

namespace WSExterno
{
    public class ServiceCaller
    {
        public HttpWebResponse POST(string url, string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}
