using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace CSharpRestClient
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum authenticationType
    {
        Baic,
        NTLM
    }

    public enum AuthenticationTechnique
    {
        RollYourOwn,
        NetworkCredential
    }

    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        public authenticationType authType { get; set; }
        public AuthenticationTechnique authTech { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            string authHeader = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(userName + ":" + userPassword));
            request.Headers.Add("Authorization", authType.ToString() + " " + authHeader);

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    if(responseStream != null)
                    {
                        using(StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                strResponseValue = "{\"errorMessage\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if(response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            

            return strResponseValue;
        }
    }
}
