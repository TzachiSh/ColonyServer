using ColonyServer.App_Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace ColonyServer.Models
{
    public class Message
    {
        private ColonyDBContext db = new ColonyDBContext();


        public string SenderName { get; set; }
        [Required]
        public string ReceiverName { get; set; }
        public string Body { get; set; }

      
               
   


        public string sendMessage()
        {
            FindToken();

            string postDataContentType = "application/json";
            string apiKey = "AIzaSyDRbCQLV_-PS68YPqhOs7c47qf6ycPaC3o"; // api of fcm
            
            string token = ReceiverName;
            string message = Body;
            string contentTitle = SenderName;
            string tickerText = "example test GCM";



            string postData =
            "{ \"registration_ids\": [ \"" + token + "\" ], " +
              "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
                         "\"contentTitle\":\"" + contentTitle + "\", " +
                         "\"message\": \"" + message + "\"}}";


            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    var text = "Unauthorized - need new token";
                    return text;
                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    var text = "Response from web service isn't OK";
                    return text;
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            return "error";
        }

        public static bool ValidateServerCertificate(
        object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        public void FindToken()
        {
            try
            {
                this.ReceiverName = db.Users.FirstOrDefault(acc => acc.UserName == this.ReceiverName).Token;
                 
            }
            catch (NullReferenceException)
            {
                throw;
            }

        }
    }
}