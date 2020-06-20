using Newtonsoft.Json;

namespace Alexa.NET.Profile.Customer
{
    public class RegionAndPostalCode
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        //{
        //    "countryCode" : "US",
        //    "postalCode" : "98109"
        //}
    }
}
