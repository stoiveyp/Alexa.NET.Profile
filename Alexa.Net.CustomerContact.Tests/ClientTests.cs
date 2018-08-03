using System;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Xunit;

namespace Alexa.Net.CustomerProfile.Tests
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
	}
}
