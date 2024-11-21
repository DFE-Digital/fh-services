# Acceptance Tests How To

## Use
These tests are to verify the API endpoints in the service directory API. They should test a few scenarios to verify the endpoint happy path and few unhappy paths if applicable.

They should be run as part of an Aure pipeline after the service directory API has been deployed to test - to be setup. But they can also be run locally after the environment test URL is configured inthe appsettings.json.

The test use the builder pattern and HTTPClient to send requests to the API endpoints.

## Prerequisites

1. Install VSCode or your favourite IDE
2. Install .NET8 SDK and Runtime

## Run tests locally
1. Clone ServiceDirectory API and open the FamilyHubs.ServiceDirectory.Api.AcceptanceTests folder in VS Code
2. Run dotnet build
3. Run dotnet tests

### Test Setup
Use secrets to configure the variables, but you can ignore to not use key vault:
```json
"AppConfiguration": {
    "KeyVaultIdentifier": "",
    "KeyVaultPrefix": ""
}
'''

### VS Code specific
- To run tests via extension: install C# Dev Kit or .NET Core Test Explorer, navigate to testing tab and run tests.

## Run tests in Azure pipeline
To be setup