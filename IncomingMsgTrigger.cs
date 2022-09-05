using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TwilioSMSWebHook
{
    public class IncomingMsgTrigger 
    {
        [FunctionName("IncomingMsgTrigger")]
        [Produces("application/xml")]
        public ContentResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var msgDetails = new StreamReader(req.Body).ReadToEnd();
            
            string[] msgProperties = msgDetails.Split("&");

            string msgBody = string.Empty;

            msgBody = Array.Find(msgProperties, property => property.Contains("Body=")); //Body=<MSG_SENT>
            msgBody = msgBody.Substring(msgBody.IndexOf("=")+1);

            var response = new MessagingResponse();
            response.Message(
                $"Hello. Thank you for reaching out to us!\n You sent {msgBody}",
                action: new Uri("/api/MessageStatus", UriKind.Absolute), 
                method: Twilio.Http.HttpMethod.Post);

            return new ContentResult{
                ContentType = "application/xml",
                Content = response.ToString(),
                StatusCode = 200
            };

        }
    }
}

//▶ az storage account create --name azfunctionstoragetwilio --location westus --resource-group AzureFunctionsQuickstart-rg --sku Standard_LRS
//twiliowebhooks2009
