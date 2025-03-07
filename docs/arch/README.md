# Architecture decision records (ADR)

All architectural decisions must be logged within this directory.

To create an ADR: 

1. Make a copy of [TEMPLATE.md](./TEMPLATE.md).
2. Complete the given sections in the template.
3. Run the `./Generate-AdrIndex` script to update the summary table below.
4. Submit a pull request to propose the ADR.
5. Work through any feedback until the ADR is accepted or rejected.

Please note that not all technical decisions are architectural decisions. An architectural decision relates to:

- changes at the module (such as dotnet namespace) or component level (structural changes)
- architectural characteristics (also known as  NFRs or CFRs)
- dependencies
- external interfaces
- construction techniques

## Summary of ADRs


| ID | Title |
| --- | --- |
| ADR001 | [Use HTTP for inter-service communications](./ADR001-use-http-for-inter-service-comms.md) |
| ADR002 | [Use a managed Azure database](./ADR002-use-a-managed-azure-database.md) |
| ADR004 | [Use ASP.NET Identity Authentication](./ADR004-use-aspdotnet-identity-auth.md) |
| ADR006 | [Use Redis for session storage](./ADR006-redis-for-session-storage.md) |
| ADR008 | [Use Azure App Services for service hosting](./ADR007-use-azure-app-services.md) |
| ADR008 | [Use GOV.UK One Login for authentication](./ADR008-use-govuk-one-login-auth.md) |
| ADR010 | [Use SQL Server caching for session storage](./ADR010-sql-server-caching.md) |
| ADR012 | [Pull directory data from external organisations](./ADR012-pull-data-from-external-orgs.md) |
| ADR013 | [Ingest service data into a 'staging schema'](./ADR013-ingest-into-staging-schema.md) |
| ADR020 | [Create an ASP.NET API to stub local authorities during development](./ADR020-create-mock-api-to-stub-las.md) |
| ADR025 | [Use SonarCloud in 'monitoring mode'](./ADR025-use-sonar-cloud.md) |
| ADR026 | [Merge 'Find' into 'Connect'](./ADR026-merge-find-into-connect.md) |
| ADR027 | [Build data warehouse for reporting metrics](./ADR027-build-data-warehouse.md) |
| ADR029 | [Merge all API services and databases](./ADR029.md) |
