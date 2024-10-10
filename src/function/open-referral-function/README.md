This project contains the Azure Functions implementation to our Open Referral work.

#### Prerequisites

1. In the same directory as `Program.cs`, create a file named `local.settings.json`. There is an example file that can just be copied and renamed.
2. Like before, now create `appsettings.Development.json`, this can be copied from `appsettings.json`.
3. Get your local connection URL of the Mock HSDA API and put it under the `ConnectionApi` value in `appsettings.Development.json`.
4. Get your local connection string to the Service Directory database and put it under the `ServiceDirectoryConnection` value in `appsettings.Development.json`.

---

#### Run Instructions

1. Ensure you have gone through the prerequisite steps.
2. Ensure the HSDA Mock API is running locally.
3. Run the project either through your IDE, or in a terminal inside the same directory as `Program.cs` execute the command `dotnet clean && func host start --pause-on-error --port 7275`.
4. In the console output you will see a POST URL to `TriggerPullServicesWebhook`. POST to that endpoint as-is in Postman or your favourite API client and you will see logger output in the terminal.