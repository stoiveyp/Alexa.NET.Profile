# Alexa.NET.Profile
Small helper library for Alexa.NET based skills to access the customer and person profile APIs

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
using Alexa.NET.Profile

var client = new CustomProfileClient(skillRequest);
var fullName = await client.FullName();
var email = await client.Email();
```

# Getting person profile information
```csharp
using Alexa.NET.Profile

var client = new PersonProfileClient(skillRequest);
var fullName = await client.FullName();
```