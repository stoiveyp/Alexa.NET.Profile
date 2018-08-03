using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.CustomerProfile
{
	public class MobileNumberResponse
	{
        [JsonProperty("countryCode")]
		public string CountryCode { get; set; }

        [JsonProperty("phoneNumber")]
		public string PhoneNumber { get; set; }
	}
}
