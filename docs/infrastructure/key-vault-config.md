# Key Vault Confguration

## Available Key Vaults

There are 3 key vaults required to drive the Connect and Manage applications:

- s181\<env>-kv-fh-idam: contains the government OneLogin service private key used to authenticate service requests
- s181\<env>-kv-fh-referral: contains the ASP.NET data protection key used by the Connect service to encrypt sensitive data
- s181\<env>-kv-fh-admin: the main key vault that contains the secrets which feed the configuration of the system components

## Key Vault Contents

*s181\<env>-kv-fh-idam*

| **Name** | **Type** | **Description** | **Example Value** |
| ---- | ---- | ----------- | -- |
| s181p01-gov-uk-one-login-referral-sd-ui-private-key | Key | OneLogin private key for Connect | PFX / PEM |
| s181p01-gov-uk-one-login-sd-admin-ui-private-key | Key | OneLogin private key for Manage | PFX / PEM |

Note that these are the production names for the keys. The keys may be accessed by different names and even versions of the key, depending upon the environment. Two secrets in the `s181<env>-kv-fh-admin` vault, `CONNECT-UI-GovUkOidcConfiguration--Oidc--KeyVaultIdentifier` and `MANAGE-UI-GovUkOidcConfiguration--Oidc--KeyVaultIdentifier` specify the IDAM OneLogin secret name / version to use for the Connect and Manage services respectively. E.g. for the production environment / key vault, the `CONNECT-UI-GovUkOidcConfiguration--Oidc--KeyVaultIdentifier` secret is set to:

`https://s181p01-kv-fh-idam.vault.azure.net/keys/s181p01-gov-uk-one-login-referral-sd-ui-private-key/dec9f48c68f84d90b41bfa11cab9b895`

*s181\<env>-kv-fh-referral*

This key is automatically persisted by the ASP.NET data protection services used by the Connect service to encrypt sensitive data.

| **Name** | **Type** | **Description** |  **Example Value** |
| ---- | ---- | ----------- | --- |
| s181<env>-data-protection-key | Key | ASP.NET data protection service | PFX / PEM managed by .NET |

*s181\<env>-kv-fh-admin*

This is the main key vault used by the services for storage of application secrets. Each item listed in the table is an individual secret.

| Name | Format | Description | Example Value |
|------|--------|-------------|---------------|
| CONNECT-UI-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for Connect UI app service | |
| CONNECT-UI-ConnectionStrings--SharedKernelConnection | DB Connection String | A connection to the Referral database. Used by the IDataProtection provider to store encrypted keys | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-referral-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| CONNECT-UI-DataProtection--KeyIdentifier | URL | URL of encryption key used by the ASP.NET data protection services within the Connect UI service | https://s181t01-kv-fh-referral.vault.azure.net/keys/s181t01-data-protection-key |
| CONNECT-UI-FamilyHubsUi--Analytics--ClarityId | Clarity ID | Unique Microsoft Clarity ID for the Connect website | a1rv8q9ezk |
| CONNECT-UI-FamilyHubsUi--Analytics--ContainerId | Google Container ID | Google Analytics tag for the Connect UI | HXVL3XGHE2 |
| CONNECT-UI-FamilyHubsUi--Analytics--MeasurementId | Google Measurement Id | Google Analytics Measurement ID for the Connect website | G-DHJLDOLSNE |
| CONNECT-UI-FamilyHubsUi--Urls--DashboardWeb | URL | URL to the "My referrals" section of the Single Directory | https://test.connect-families-to-support.education.gov.uk/referrals |
| CONNECT-UI-FamilyHubsUi--Urls--GovUkLoginAccountPage | URL | URL to the GovUK One Login website | https://home.integration.account.gov.uk |
| CONNECT-UI-FamilyHubsUi--Urls--ManageWeb | URL | URL to the Manage service | https://test.manage-family-support-services-and-accounts.education.gov.uk |
| CONNECT-UI-FeatureManagement--ConnectDashboard--EnabledFor--0--Parameters--Value | Boolean | Feature Flag that controls whether or not the "My requests" sub-system is activated or disabled | true |
| CONNECT-UI-FeatureManagement--VcfsServices--EnabledFor--0--Parameters--Value | Boolean | Feature Flag that controls whether or not VCFS services are returned in the Single Directory. If set to False, will also turn off the Connect Dashboard feature flag | true |
| CONNECT-UI-GovUkOidcConfiguration--AppHost | URL | The URL GovUK One Login will return the user to once they have successfully logged in | https://test.connect-families-to-support.education.gov.uk |
| CONNECT-UI-GovUkOidcConfiguration--BearerTokenSigningKey | Random 64-char Hex String | Signing key to authenticate OneLogin requests for the Connect website | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| CONNECT-UI-GovUkOidcConfiguration--IdamsApiBaseUrl | URL | The URL of the IDAM API, used to authenticate users and validate Claims internally.| https://s181t01-as-fh-idam-api.azurewebsites.net/ |
| CONNECT-UI-GovUkOidcConfiguration--Oidc--BaseUrl | URL | The base URL of the GovUK One Login authentication service | https://oidc.integration.account.gov.uk |
| CONNECT-UI-GovUkOidcConfiguration--Oidc--ClientId | OneLogin Client Id | Unique ID representing the OneLogin configuration to use for the Connect website | CBwPLKSDJFLSDKJFSLDFJkjkjkk |
| CONNECT-UI-GovUkOidcConfiguration--Oidc--KeyVaultIdentifier | URL | The private key used to sign OneLogin requests - OneLogin is configured with the corresponding public key | https://s181t01-kv-fh-idam.vault.azure.net/keys/s181t01-gov-uk-one-login-private-key/8c695031cb9a44118bc41bab07a9234c |
| CONNECT-UI-GovUkOidcConfiguration--Oidc--TwoFactorEnabled | Boolean | Whether 2FA is required to login | false |
| CONNECT-UI-GovUkOidcConfiguration--PathBasedRouting--DiscriminatorPath | Path | Going to this relative path will trigger the GovUK One Login authentication mechanism to ensure a user is signed in and valid | /referrals |
| CONNECT-UI-GovUkOidcConfiguration--PathBasedRouting--SubSiteTriggerPaths | Path | Going to these relative paths will trigger the GovUK One Login authentication mechanism to ensure a user is signed in and valid | /la,/vcs,/start |
| CONNECT-UI-GovUkOidcConfiguration--StubAuthentication--UseStubAuthentication | Boolean | Used for mocking authentication for local development | false |
| CONNECT-UI-GovUkOidcConfiguration--StubAuthentication--UseStubClaims | Boolean | Used for mocking Claims for local development | false |
| CONNECT-UI-Idams--Endpoint | URL |  Used for the Connect UI to access the IDAM API | https://s181t01-as-fh-idam-api.azurewebsites.net |
| CONNECT-UI-Notification--Endpoint | URL | Used for the Connect UI to access the Nottification API - when referrals are created, updated or deleted emails are sent to relevant parties through this API | https://s181t01-as-fh-notification-api.azurewebsites.net/api/notify |
| CONNECT-UI-ReferralApiUrl | URL | Used to allow the Connect UI to perform CRUD operations on Referrals through the appropriate API endpoints | https://s181t01-as-fh-referral-api.azurewebsites.net/ |
| CONNECT-UI-ServiceDirectoryUrl | URL | Used in the "Search for service" functionality to get the lists of services, locations etc | https://s181t01-as-fh-sd-api.azurewebsites.net/ |
| CONNECT-UI-SqlServerCache--Connection | DB Connection | A cache database that persists encoded user session data | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-referral-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| FIND-UI-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for the Find UI app service |
| FIND-UI-FamilyHubsUi--Analytics--ClarityId | Clarity ID | Microsoft Clarity analytics ID for Find | skjdkskkss |
| FIND-UI-FamilyHubsUi--Analytics--ContainerId | Container ID | Google Analytics container ID for Find | DGHEFGDG6 |
| FIND-UI-FamilyHubsUi--Analytics--MeasurementId | Measurement ID | Google Analytics measurement ID for Find | G-DGHEFGDG6 |
| FIND-UI-ServiceDirectoryAPI--Endpoint | URL | Used to pull services, locations, etc for the search results and service details pages | https://s181t01-as-fh-sd-api.azurewebsites.net/ |
| IDAM-API-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for IDAM API app service |  |
| IDAM-API-ConnectionStrings--IdamConnection | DB Connection String | Database connection for the IDAM API to perform CRUD operations | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-idam-db;Persist Security Info=False;User ID=\*\*\*;Password=;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| IDAM-API-Crypto--DbEncryptionIVKey | 16 Byte String | Used for column encryption on certain tables | 233,111,138,177,134,42,180,7,131,207,253,154,27,229,102,82 |
| IDAM-API-Crypto--DbEncryptionKey | 32 Byte String | Used for column encryption on certain tables | 53,2,59,171,31,193,231,222,12,172,72,163,61,220,196,244,182,244,81,190,56,153,158,236,60,28,228,155,108,229,254,153 |
| IDAM-API-GovUkOidcConfiguration--BearerTokenSigningKey | Random 64-char Hex String | Used to authenticate user roles incoming from Connect or Manage | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| IDAM-API-ServiceDirectoryApiBaseUrl | URL | Used to get organisation information related to users | https://s181t01-as-fh-sd-api.azurewebsites.net/ |
| IDAM-MAINTENANCE-UI-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for IDAM Maintenance UI app service |  |
| IDAM-MAINTENANCE-UI-ConnectionStrings--IdamConnection | DB Connection String | Used by the IDAM Maintenance UI to perform CRUD operations on user related entities | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-idam-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| IDAM-MAINTENANCE-UI-Crypto--DbEncryptionIVKey | 16 Byte String | Used to perform column encryption on certain tables | 233,111,138,177,134,42,180,7,131,207,253,154,27,229,102,82 |
| IDAM-MAINTENANCE-UI-Crypto--DbEncryptionKey | 32 Byte String | Used to perform column encryption on certain tables | 53,2,59,171,31,193,231,222,12,172,72,163,61,220,196,244,182,244,81,190,56,153,158,236,60,28,228,155,108,229,254,153 |
| IDAM-MAINTENANCE-UI-ServiceDirectoryApiBaseUrl | URL | Used for getting organisation information related to users | https://s181t01-as-fh-sd-api.azurewebsites.net/ |
| IDAM-MAINTENANCE-UI-SqlServerCache--Connection | DB Connection String | Used for persisting user session information | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-idam-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| MANAGE-UI-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for Manage UI app service |  |
| MANAGE-UI-CacheConnection | DB Connection String |  Used for persisting user session information | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-idam-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| MANAGE-UI-FamilyHubsUi--Analytics--ClarityId | Clarity ID | Microsoft Clarity analytics ID for Manage | KJK2JKKJ2D |
| MANAGE-UI-FamilyHubsUi--Analytics--ContainerId | Container ID | Google Analytics container ID for Manage | SDJKKSDJSK1 |
| MANAGE-UI-FamilyHubsUi--Analytics--MeasurementId | Measurement ID | Google Analytics measurement ID for Manage | G-SDJKKSDJSK1 |
| MANAGE-UI-FamilyHubsUi--Urls--ConnectWeb | URL |  Used on the home page of Manage to link to the Connect service | https://test.connect-families-to-support.education.gov.uk |
| MANAGE-UI-FamilyHubsUi--Urls--FindWeb | URL |  Used on the home page of Manage to link to the Find service | https://test.find-support-for-your-family.education.gov.uk |
| MANAGE-UI-FamilyHubsUi--Urls--ManageWeb | URL | Used to provide a link for users to the service when being emailed by the Notification API | https://test.manage-family-support-services-and-accounts.education.gov.uk/ |
| MANAGE-UI-GovUkLoginAccountPage | URL |  GovUK One Login page for signing in to the service | https://home.integration.account.gov.uk |
| MANAGE-UI-GovUkOidcConfiguration--AppHost | URL | The redirect for GovUK One Login after a successful sign in | https://test.manage-family-support-services-and-accounts.education.gov.uk |
| MANAGE-UI-GovUkOidcConfiguration--BearerTokenSigningKey | Random 64-char Hex String | Used to authenticate GovUK One Login and when using the APIs | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| MANAGE-UI-GovUkOidcConfiguration--IdamsApiBaseUrl | URL | Used to authenticate claims in Manage | https://s181t01-as-fh-idam-api.azurewebsites.net/ |
| MANAGE-UI-GovUkOidcConfiguration--Oidc--BaseUrl | URL | The GovUK One Login authentication service | https://oidc.integration.account.gov.uk |
| MANAGE-UI-GovUkOidcConfiguration--Oidc--ClientId | OneLogin Client Id | Unique ID representing the OneLogin configuration to use for the Manage website | S3LDKFJSDLKFpUsGOLKFLK_JSDH |
| MANAGE-UI-GovUkOidcConfiguration--Oidc--KeyVaultIdentifier | URL | The private key used to sign OneLogin requests - OneLogin is configured with the corresponding public key | https://s181t01-kv-fh-idam.vault.azure.net/keys/s181t01-gov-uk-one-login-private-key/8c695031cb9a44118bc41bab07a9234c |
| MANAGE-UI-GovUkOidcConfiguration--Oidc--TwoFactorEnabled | Boolean | Whether the user requires 2FA | false |
| MANAGE-UI-GovUkOidcConfiguration--StubAuthentication--UseStubAuthentication | Boolean | Used for mocking authentication for local development | false |
| MANAGE-UI-GovUkOidcConfiguration--StubAuthentication--UseStubClaims | Boolean | Used for mocking user claims for local development | false |
| MANAGE-UI-GovUkOidcConfiguration--Urls--NoClaimsRedirect | URL | The redirect page on the site after a user does not have adequate permissions to use the service | https://test.manage-family-support-services-and-accounts.education.gov.uk/Error/401 |
| MANAGE-UI-GovUkOidcConfiguration--Urls--SignedOutRedirect | URL | The page the user gets redirected to when they sign out | https://test.manage-family-support-services-and-accounts.education.gov.uk/signout |
| MANAGE-UI-GovUkOidcConfiguration--Urls--TermsAndConditionsRedirect | URL | When the user signs in for the first time, they are redirected here to agree to the T&Cs - until they accept they will always be redirected here | https://test.manage-family-support-services-and-accounts.education.gov.uk/AgreeToTermsAndConditions |
| MANAGE-UI-IdamApi | URL | Used by Manage to manipulate user data via the IDAM API | https://s181t01-as-fh-idam-api.azurewebsites.net/ |
| MANAGE-UI-Notification--Endpoint | URL | Used by Manage to email users when changing information | https://s181t01-as-fh-notification-api.azurewebsites.net/api/notify |
| MANAGE-UI-ReferralApiBaseUrl | URL |  | https://s181t01-as-fh-referral-api.azurewebsites.net/ |
| MANAGE-UI-ReportingApiBaseUrl | URL | Used to populate the performance dashboard section of Manage via the Reporting API | https://s181t01-as-fh-report-api.azurewebsites.net/ |
| MANAGE-UI-ServiceDirectoryApiBaseUrl | URL | Used to manipulate service, location and organisation information via the Service Directory API | https://s181t01-as-fh-sd-api.azurewebsites.net/ |
| MOCK-HSDA-API-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for the Mock HSDA API app service |  |
| MOCK-HSDA-API-ConnectionStrings--HsdaMockResponsesConnection | DB Connection String | Database connection for the Mock HSDA API | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-open-referral-mock-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| NOTIFICATIONS-API-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for Notifications API app service |  |
| NOTIFICATIONS-API-ConnectionStrings--NotificationConnection | DB Connection String |  Database connection for the Notifications API | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-notification-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| NOTIFICATIONS-API-Crypto--DbEncryptionIVKey | 16 Byte String | Used for column encryption on certain tables | 233,111,138,177,134,42,180,7,131,207,253,154,27,229,102,82 |
| NOTIFICATIONS-API-Crypto--DbEncryptionKey | 32 Byte String | Used for column encryption on certain tables | 53,2,59,171,31,193,231,222,12,172,72,163,61,220,196,244,182,244,81,190,56,153,158,236,60,28,228,155,108,229,254,153 |
| NOTIFICATIONS-API-GovNotifySetting--ConnectAPIKey | Modified GUID | Authentication details for the Connect service to access the government Notify service. This is in the format {name}-{guid1}{guid2} where the first GUID is the account id and the second is the specific key | 2024ts11-ab860773-c38e-4fea-bdf6-528ea17761ab-fa509756-a5a3-416d-8f8f-983417d0b1ce
| NOTIFICATIONS-API-GovNotifySetting--ManageAPIKey | Modified GUID | Authentication details for the Manage service to access the government Notify service. This is in the format {name}-{guid1}{guid2} where the first GUID is the account id and the second is the specific key | 2024ts11-9f3a2c18-7b14-4d6e-b21f-0c98a1b5d7e2-6d4f3e21-98ab-4c07-91cd-273f8b2e5601 |
| NOTIFICATIONS-API-GovUkOidcConfiguration--BearerTokenSigningKey | Random 64-char Hex String | Used to authenticate endpoint requests from the UIs | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| OPEN-REFERRAL-FUNC-ApiConnection | URL | The Open Referral Azure Function connects to this as its mock API to get OR-compliant data | https://s181t01-as-fh-open-referral-mock-api.azurewebsites.net |
| OPEN-REFERRAL-FUNC-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for the Open Referral function app |  |
| OPEN-REFERRAL-FUNC-ServiceDirectoryConnection | DB Connection String | Where the DEDS and DEDSMETA schema are stored for the Open Referral function | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-service-directory-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| PIPELINE-IDAM-DB-ConnectionString | DB Connection String | Used by the GitHub Actions Workflows to manipulate the IDAM Database | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-idam-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| PIPELINE-MOCK-HSDA-DB-ConnectionString | DB Connection String | Used by the GitHub Actions Workflows to manipulate the Mock HSDA Database | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-open-referral-mock-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| PIPELINE-NOTIFICATIONS-DB-ConnectionString | DB Connection String | Used by the GitHub Actions Workflows to manipulate the Notification Database | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-notification-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| PIPELINE-REFERRAL-DB-ConnectionString | DB Connection String | Used by the GitHub Actions Workflows to manipulate the Referral Database | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-referral-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| PIPELINE-REPORT-DB-ConnectionString | DB Connection String | Used by the GitHub Actions Workflows to manipulate the Report Database | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-report-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| PIPELINE-REPORT-STAGING-DB-ConnectionString | DB Connection String | Used by the GitHub Actions Workflows to manipulate the ADF Staging Database | Server=tcp:s181t01-as-fh-sql-server-reporting.database.windows.net,1433;Initial Catalog=s181t01-fh-report-staging-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| PIPELINE-SD-DB-ConnectionString | DB Connection String | Used by the GitHub Actions Workflows to manipulate the Service Directory Database | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-service-directory-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| REFERRAL-API-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for Connect API app service |  |
| REFERRAL-API-ConnectionStrings--ReferralConnection | DB Connection String | Database connection for the Referral API | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-referral-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| REFERRAL-API-Crypto--DbEncryptionIVKey | 16 Byte String | Used for column encryption on certain tables | 233,111,138,177,134,42,180,7,131,207,253,154,27,229,102,82 |
| REFERRAL-API-Crypto--DbEncryptionKey | 32 Byte String | Used for column encryption on certain tables | 53,2,59,171,31,193,231,222,12,172,72,163,61,220,196,244,182,244,81,190,56,153,158,236,60,28,228,155,108,229,254,153 |
| REFERRAL-API-GovUkOidcConfiguration--BearerTokenSigningKey | Random 64-char Hex String | Used to authenticate endpoint requests from other UIs and APIs | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| REFERRAL-API-ServiceDirectoryApiBaseUrl | URL | Used to retrieve service and organisation data when creating referrals | https://s181t01-as-fh-sd-api.azurewebsites.net/ |
| REPORT-API-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for Report API app service |  |
| REPORT-API-ConnectionStrings--ReportConnection | DB Connection String | Database connection for the reporting API | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-report-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| REPORT-API-GovUkOidcConfiguration--BearerTokenSigningKey | Random 64-char Hex String | Used to authenticate incoming requests to the endpoints from other UIs or APIs | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| SD-API-AZURE-WEBAPP-PUBLISH-PROFILE | Azure App Service Publish Profile String | Publish profile for Find API app service |  |
| SD-API-ConnectionStrings--ServiceDirectoryConnection | DB Connection String | Database connection for the Service Directory API | Server=tcp:s181t01-as-fh-sql-server.database.windows.net,1433;Initial Catalog=s181t01-fh-service-directory-db;Persist Security Info=False;User ID=\*\*\*;Password=\*\*\*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; |
| SD-API-GovUkOidcConfiguration--BearerTokenSigningKey | Random 64-char Hex String | Used to authenticate incoming requests to the endpoints from other UIs and APIs | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| TEST-REFERRAL-API-BaseUrl | URL | Used in the API acceptance tests GitHub Actions workflow. Passed in via the Deployment workflow | https://s181t01-as-fh-referral-api.azurewebsites.net/ |
| TEST-REFERRAL-API-BearerTokenSigningKey | Random 64-char Hex String | Used in the API acceptance tests GitHub Actions workflow. Passed in via the Deployment workflow | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| TEST-REPORT-API-BaseUrl | URL | Used in the API acceptance tests GitHub Actions workflow. Passed in via the Deployment workflow | https://s181t01-as-fh-report-api.azurewebsites.net/ |
| TEST-REPORT-API-BearerTokenSigningKey | Random 64-char Hex String | Used in the API acceptance tests GitHub Actions workflow. Passed in via the Deployment workflow | 99B3E7E9784F950E5EFE299FBFC69071FDB3DC55F8D8DD5D8A5CD6FF23E6F9B8 |
| TEST-SERVICE-DIRECTORY-API-BaseUrl | URL | Used in the API acceptance tests GitHub Actions workflow. Passed in via the Deployment workflow | https://s181t01-as-fh-sd-api.azurewebsites.net/ |
