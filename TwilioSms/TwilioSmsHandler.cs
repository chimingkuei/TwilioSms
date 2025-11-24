using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TwilioSms
{
    class TwilioSmsHandler
    {
        public string accountSid { get; set; }
        public string authToken { get; set; }
        public string twilioNumber { get; set; }

        public TwilioSmsHandler(string _accountSid, string _authToken, string _twilioNumber)
        {
             accountSid = _accountSid;
             authToken = _authToken;
             twilioNumber = _twilioNumber;
             TwilioClient.Init(_accountSid, _authToken);
        }

        public void SendSms(string toNumber, string messageBody)
        {
            try
            {
                var message = MessageResource.Create(
                    body: messageBody,
                    from: new PhoneNumber(twilioNumber),
                    to: new PhoneNumber(toNumber)
                );
                Log.Information($"✅ 已發送，Message SID: {message.Sid}");
            }
            catch (Twilio.Exceptions.ApiException ex)
            {
                Log.Error($"❌ 發送失敗: {ex.Message}");
            }
        }

    }
}
