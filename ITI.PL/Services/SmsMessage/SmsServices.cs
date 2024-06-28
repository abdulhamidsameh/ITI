using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ITI.PL.Services.SmsMessage
{
	public class SmsServices : ISmsServices
	{
		private readonly IConfiguration _configuration;

		public SmsServices(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public MessageResource Send(SmsMessage sms)
		{
			TwilioClient.Init(_configuration["SMS:AccountSID"], _configuration["SMS:AuthToken"]);

			var result = MessageResource.Create(

				body: sms.Body,
				from: new Twilio.Types.PhoneNumber(_configuration["SMS:PhoneNumber"]),
				to: sms.PhoneNumber

				);

			return result;
		}
	}
}
