# Alexa.NET.CustomerProfile
Small helper library for Alexa.NET based skills to access the customer contact API

## Asking your user for consent to get customer information
```csharp
using Alexa.NET.Response

var response = ResponseBuilder.TellWithAskForPermissionConsentCard(
    "sorry, but you have to give me consent",
    new[]{
        CustomerProfilePermissions.FullName,
        CustomerProfilePermissions.Email
    }
);
```
## Getting customer profile information
```csharp
using Alexa.NET.Response

var client = new CustomProfileClient(skillRequest);
var fullName = await client.FullName();
var email = await client.Email();
```
