using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Alexa.NET.Request;

namespace Alexa.Net.CustomerContact
{
    public class CustomerContactClient
    {
		public HttpClient Client { get; set; }
		public CustomerContactClient(SkillRequest request):this(
			request.Context.System.ApiEndpoint,
			request.Context.System.ApiAccessToken){}

		public CustomerContactClient(string endpointUrl, string accessToken)
        {
            var client = new HttpClient { BaseAddress = new Uri(endpointUrl, UriKind.Absolute) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client = client;
        }

		public CustomerContactClient(HttpClient client)
        {
            Client = client;
        }
    }
}
