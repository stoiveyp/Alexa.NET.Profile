using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Alexa.NET.CustomerProfile
{
    public class FullAddress
    {
        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("addressLine3")]
        public string AddressLine3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("stateOrRegion")]
        public string StateOrRegion { get; set; }

        [JsonProperty("districtOrCounty")]
        public string DistrictOrCounty { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }


        //{
        //    "stateOrRegion" : "WA",
        //    "city" : "Seattle",
        //    "countryCode" : "US",
        //    "postalCode" : "98109",
        //    "addressLine1" : "410 Terry Ave North",
        //    "addressLine2" : "",
        //    "addressLine3" : "aeiou",
        //    "districtOrCounty" : ""
        //}
    }
}
