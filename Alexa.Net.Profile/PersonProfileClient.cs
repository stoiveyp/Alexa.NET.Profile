using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.Profile.Customer;
using Alexa.NET.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Profile
{
    public class PersonProfileClient
    {
        public HttpClient Client { get; set; }

        public PersonProfileClient(SkillRequest request) : this(
            request.Context.System.ApiEndpoint,
            request.Context.System.ApiAccessToken)
        {
            
        }

        public PersonProfileClient(string endpointUrl, string accessToken)
        {
            var client = new HttpClient { BaseAddress = new Uri(endpointUrl, UriKind.Absolute) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client = client;
        }

        public PersonProfileClient(HttpClient client)
        {
            Client = client;
        }

        public async Task<string> FullName()
        {
            var nameResponse = await Client.GetStringAsync("/v2/persons/~current/profile/name");
            var response = JToken.Parse(nameResponse);
            return response.Value<string>();
        }

        public async Task<string> GivenName()
        {
            var givenNameResponse = await Client.GetStringAsync("/v2/persons/~current/profile/givenName");
            var response = JToken.Parse(givenNameResponse);
            return response.Value<string>();
        }

        public async Task<MobileNumberResponse> MobileNumber()
        {
            var mobileNumberResponse = await Client.GetStringAsync("/v2/persons/~current/profile/mobileNumber");
            return JsonConvert.DeserializeObject<MobileNumberResponse>(mobileNumberResponse);
        }
    }
}