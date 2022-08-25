using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio.TwiML;

namespace TwilioSMSWebHook
{
    public class StatusCallBack
    {
        [FunctionName("StatusCallBack")]
        [Produces("application/xml")]
        public ContentResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            Console.WriteLine(new StreamReader(req.Body).ReadToEnd());

            var response = new MessagingResponse();
            
            var xmlResponse = response.ToString();

            return new ContentResult{
                ContentType = "application/xml",
                Content = xmlResponse,
                StatusCode = 200
            };

        }
    }
}
