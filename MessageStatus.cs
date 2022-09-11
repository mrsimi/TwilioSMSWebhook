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
    public class MessageStatus
    {
        [FunctionName("MessageStatus")]
        [Produces("application/xml")]
         public async Task<OkResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var form = await req.ReadFormAsync();
            string messageSid = form["MessageSid"];
            string messageStatus = form["MessageStatus"];

            log.LogInformation(
                "Status changed to {MessageStatus} for Message {MessageSid}", 
                messageStatus,
                messageSid   
            );

            return new OkResult();
        }

    }
}
