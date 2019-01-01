using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.CustomerProfile
{
    public class CustomerProfileClient
    {
        public HttpClient Client { get; set; }

        public string DeviceId { get; set; }

        public CustomerProfileClient(SkillRequest request) : this(
            request.Context.System.ApiEndpoint,
            request.Context.System.ApiAccessToken)
        {
            DeviceId = request.Context.System.Device.DeviceID;
        }

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
            var response = JToken.Parse(nameResponse);
            return response.Value<string>();
        }

        public async Task<string> GivenName()
        {
            var givenNameResponse = await Client.GetStringAsync("/v2/accounts/~current/settings/Profile.givenName");
            var response = JToken.Parse(givenNameResponse);
            return response.Value<string>();
        }

        public async Task<string> Email()
        {
            var emailResponse = await Client.GetStringAsync("/v2/accounts/~current/settings/Profile.email");
            var response = JToken.Parse(emailResponse);
            return response.Value<string>();
        }

        public async Task<MobileNumberResponse> MobileNumber()
        {
            var mobileNumberResponse = await Client.GetStringAsync("/v2/accounts/~current/settings/Profile.mobileNumber");
            return JsonConvert.DeserializeObject<MobileNumberResponse>(mobileNumberResponse);
        }

        public Task<RegionAndPostalCode> RegionAndPostalCode()
        {
            return RegionAndPostalCode(DeviceId);
        }

        public async Task<RegionAndPostalCode> RegionAndPostalCode(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentNullException(nameof(deviceId), "DeviceId is required - specify as parameter or ensure SkillRequest contains a valid device ID");
            }

            var regionResponse = await Client.GetStringAsync("/v1/devices/" + deviceId + "/settings/address/countryAndPostalCode");
            return JsonConvert.DeserializeObject<RegionAndPostalCode>(regionResponse);
        }

        public Task<FullAddress> FullAddress()
        {
            return FullAddress(DeviceId);
        }

        public async Task<FullAddress> FullAddress(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentNullException(nameof(deviceId),"DeviceId is required - specify as parameter or ensure SkillRequest contains a valid device ID");
            }

            var addressResponse = await Client.GetStringAsync("/v1/devices/" + deviceId + "/settings/address");
            return JsonConvert.DeserializeObject<FullAddress>(addressResponse);
        }
    }
}
