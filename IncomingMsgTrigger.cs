using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Twilio.TwiML;
using Microsoft.AspNetCore.Mvc;
using System;
using Twilio.AspNet.Core;

namespace TwilioSMSWebHook
{
    public class IncomingMessage 
    {
        [FunctionName("IncomingMessage")]
         public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var form = await req.ReadFormAsync();
            var body = form["Body"];

            var response = new MessagingResponse();
            response.Message(
                $"You sent: {body}",
                action: new Uri("/api/MessageStatus", UriKind.Relative),
                method: Twilio.Http.HttpMethod.Post
            );

            return new TwiMLResult(response);

        }
    }
}

