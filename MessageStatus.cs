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
        public OkResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var sentMsgStream = new StreamReader(req.Body).ReadToEnd();

            string[] msgProperties = sentMsgStream.Split("&");

            string SmsSid, MsgSid, SmsStatus, MsgStatus;

            SmsSid = Array.Find(msgProperties, property => property.Contains("SmsSid=")); //SmsSid=<SMS_SID>
            
            MsgSid = Array.Find(msgProperties, property => property.Contains("MessageSid=")); //MessageSid=<MESSAGE_SID>

            SmsStatus = Array.Find(msgProperties, property => property.Contains("SmsSid=")); //SmsStatus=<SMS_STATUS>
            
            MsgStatus = Array.Find(msgProperties, property => property.Contains("MessageStatus=")); //MessageStatus=<MSESSAGE_STATUS>

            log.LogInformation($"{SmsSid} \t {MsgSid} \t {SmsStatus} \t {MsgStatus}");

            return new OkResult();

        }
    }
}
