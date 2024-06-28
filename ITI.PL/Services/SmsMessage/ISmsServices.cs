using Twilio.Rest.Api.V2010.Account;

namespace ITI.PL.Services.SmsMessage
{
	public interface ISmsServices
	{
		public MessageResource Send(SmsMessage sms);
	}
}
