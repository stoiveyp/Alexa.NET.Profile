using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response
{
    public class CustomerProfileClient
    {
		public HttpClient Client { get; set; }
		public CustomerProfileClient(SkillRequest request):this(
			request.Context.System.ApiEndpoint,
			request.Context.System.ApiAccessToken){}

		public CustomerProfileClient(string endpointUrl, string accessToken)
        {
            var client = new HttpClient { BaseAddress = new Uri(endpointUrl, UriKind.Absolute) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client = client;
        }

		public CustomerProfileClient(HttpClient client)
        {
            Client = client;
        }

		public async Task<string> FullName()
		{
			var nameResponse = await Client.GetStringAsync("/v2/accounts/~current/settings/Profile.name");
			var response = JValue.Parse(nameResponse);
			return response.Value<string>();
		}
    }
}
