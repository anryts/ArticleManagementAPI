using Common.Options;
using Microsoft.Extensions.Options;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using UserAPI.Providers.Interfaces;

namespace UserAPI.Providers;

public class TwilioProvider : ISmsProvider
{
    private readonly IOptions<TwilioOptions> _options;
    private readonly ITwilioRestClient _twilioRestClient;

    public TwilioProvider(IOptions<TwilioOptions> options, ITwilioRestClient twilioRestClient)
    {
        _options = options;
        _twilioRestClient = twilioRestClient;
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        var fromPhoneNumber = _options.Value.FromPhoneNumber;
        var to = new PhoneNumber(phoneNumber);
        var from = new PhoneNumber(fromPhoneNumber);
        await MessageResource.CreateAsync(
            from: from,
            to: to,
            body: message,
            client: _twilioRestClient);
    }
}