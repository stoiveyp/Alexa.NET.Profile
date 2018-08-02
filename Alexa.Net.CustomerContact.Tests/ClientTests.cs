using System;
using System.Net.Http;
using Alexa.NET.Request;
using Xunit;

namespace Alexa.Net.CustomerContact.Tests
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
				Context = new Context{
					System = new AlexaSystem{
						ApiEndpoint = endpoint,
						ApiAccessToken = accesstoken
					}
				}
			};

			var client = new CustomerContactClient(request);

			Assert.Equal(endpoint, client.Client.BaseAddress.ToString());
			Assert.Equal(accesstoken, client.Client.DefaultRequestHeaders.Authorization.Parameter);
        }

        [Fact]
		public void ClientSetDirectlySetCorrectly(){
			var lowClient = new HttpClient();

			var client = new CustomerContactClient(lowClient);

			Assert.Equal(lowClient, client.Client);
		}
    }
}
