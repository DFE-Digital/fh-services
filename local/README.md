# Family Hubs Docker Compose
## Quick Start
Run `docker compose up -d` and wait for the containers to build and everything to be healthy.
Open the apps at:
- https://manage-ui.local.gd
- https://connect-ui.local.gd
- https://idam-ui.local.gd

Open the apis directly at:
- https://idam-api.local.gd
- https://sd-api.local.gd
- https://referral-api.local.gd
- https://report-api.local.gd
- https://notification-api.local.gd

You can connect to the local database on port 11433.
This is to avoid conflicting with any other mssql servers running locally.
- Username: `sa`
- Password: `qKoCBawgNrWqG62E`

Optionally seed the database with the e2e test data:
- `cd ../test/e2e-seed-data-framework`
- `npm run setup:docker`
