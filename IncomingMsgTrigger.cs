using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

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
            var response = new MessagingResponse();
            response.Message("Hello. Thank you for reaching out to us!");

            var xmlResponse = response.ToString();

            return new ContentResult{
                ContentType = "application/xml",
                Content = xmlResponse,
                StatusCode = 200
            };

        }
    }
}
