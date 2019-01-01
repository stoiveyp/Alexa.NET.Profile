using System;
namespace Alexa.NET.Response
{
	public static class CustomerProfilePermissions
	{
		public const string FullName =     "alexa::profile:name:read";
		public const string GivenName =    "alexa::profile:given_name:read";
		public const string Email =        "alexa::profile:email:read";
		public const string MobileNumber = "alexa::profile:mobile_number:read";
        public const string RegionAndPostalCode = "read::alexa:device:all:address:country_and_postal_code";
        public const string FullAddress = "read::alexa:device:all:address";
    }
}
