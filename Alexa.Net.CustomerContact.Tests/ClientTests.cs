using System;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Xunit;

namespace Alexa.NET.CustomerProfile.Tests
{
	public class ClientTests
	{
		[Fact]
		public void CreateFromSkillRequestSetupCorrectly()
		{
			const string endpoint = "https://testclient/";
			const string accesstoken = "accesstoken";

			var request = new SkillRequest
			{
				Context = new Context
				{
					System = new AlexaSystem
					{
						ApiEndpoint = endpoint,
						ApiAccessToken = accesstoken
					}
				}
			};

			var client = new CustomerProfileClient(request);

			Assert.Equal(endpoint, client.Client.BaseAddress.ToString());
			Assert.Equal(accesstoken, client.Client.DefaultRequestHeaders.Authorization.Parameter);
		}

		[Fact]
		public void ClientSetDirectlySetCorrectly()
		{
			var lowClient = new HttpClient();

			var client = new CustomerProfileClient(lowClient);

			Assert.Equal(lowClient, client.Client);
		}

		[Fact]
		public async Task GetFullNameGeneratesCorrectRequest()
		{
			const string expectedResult = "Steven Pears";
			const string fullNamePath = "/v2/accounts/~current/settings/Profile.name";
			var runHandler = false;
			var httpClient = new HttpClient(new ActionMessageHandler(req =>
			{
				runHandler = true;
				Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
				return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
				{
					Content = new StringContent(Utility.GetExampleJson("FullName.json"))
				};
			}))
			{BaseAddress = new Uri("https://testclient.com",UriKind.Absolute)};

			var client = new CustomerProfileClient(httpClient);         
			var nameResult = await client.FullName();

			Assert.True(runHandler);
			Assert.Equal(expectedResult, nameResult);
		}

		[Fact]
        public async Task GetGivenNameGeneratesCorrectRequest()
        {
            const string expectedResult = "Steven";
            const string fullNamePath = "/v2/accounts/~current/settings/Profile.givenName";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
            {
                runHandler = true;
                Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(Utility.GetExampleJson("GivenName.json"))
                };
            }))
            { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new CustomerProfileClient(httpClient);
            var givenNameResult = await client.GivenName();

            Assert.True(runHandler);
            Assert.Equal(expectedResult, givenNameResult);
        }

		[Fact]
        public async Task GetEmailGeneratesCorrectRequest()
        {
            const string expectedResult = "test@test.com";
            const string fullNamePath = "/v2/accounts/~current/settings/Profile.email";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
            {
                runHandler = true;
                Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(Utility.GetExampleJson("Email.json"))
                };
            }))
            { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new CustomerProfileClient(httpClient);
            var emailResult = await client.Email();

            Assert.True(runHandler);
            Assert.Equal(expectedResult, emailResult);
        }

		[Fact]
        public async Task GetMobileNumberGeneratesCorrectRequest()
        {
            const string countryCode = "+1";
			const string phoneNumber = "999-999-9999";
            const string fullNamePath = "/v2/accounts/~current/settings/Profile.mobileNumber";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
            {
                runHandler = true;
                Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(Utility.GetExampleJson("MobileNumber.json"))
                };
            }))
            { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new CustomerProfileClient(httpClient);
            var phoneResult = await client.MobileNumber();

            Assert.True(runHandler);
            Assert.Equal(countryCode,phoneResult.CountryCode);
			Assert.Equal(phoneNumber,phoneResult.PhoneNumber);
        }

        [Fact]
        public async Task GetCustomerAddress()
        {
            const string countryCode = "GB";
            const string postcode = "NG1 1RE";
            const string fullNamePath = "/v1/devices/amzn1.ask.device.deviceid/settings/address";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
                {
                    runHandler = true;
                    Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(Utility.GetExampleJson("FullAddress.json"))
                    };
                }))
                { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new CustomerProfileClient(httpClient);
            var fullAddress = await client.FullAddress("amzn1.ask.device.deviceid");

            Assert.True(runHandler);
            Assert.Equal("Lace Market",fullAddress.AddressLine2);
            Assert.Equal(countryCode, fullAddress.CountryCode);
            Assert.Equal(postcode, fullAddress.PostalCode);
        }

        [Fact]
        public async Task GetCustomerAddressThrowsOnNullDevice()
        {
            var client = new CustomerProfileClient(new HttpClient());
            var exc = await Assert.ThrowsAsync<ArgumentNullException>(() => client.FullAddress(null));
            Assert.Equal("deviceId", exc.ParamName);
        }

        [Fact]
        public async Task GetRegionAndPostalCode()
        {
            const string countryCode = "GB";
            const string postcode = "NG1 1RE";
            const string fullNamePath = "/v1/devices/amzn1.ask.device.deviceid/settings/address/countryAndPostalCode";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
                {
                    runHandler = true;
                    Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(Utility.GetExampleJson("RegionAndPostalCode.json"))
                    };
                }))
                { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new CustomerProfileClient(httpClient);
            var regionAndPostalCode = await client.RegionAndPostalCode("amzn1.ask.device.deviceid");

            Assert.True(runHandler);
            Assert.Equal(countryCode, regionAndPostalCode.CountryCode);
            Assert.Equal(postcode, regionAndPostalCode.PostalCode);
        }

        [Fact]
        public async Task GetRegionAndPostalCodeThrowsOnNullDevice()
        {
            var client = new CustomerProfileClient(new HttpClient());
            var exc = await Assert.ThrowsAsync<ArgumentNullException>(() => client.RegionAndPostalCode(null));
            Assert.Equal("deviceId", exc.ParamName);
        }
    }
}
