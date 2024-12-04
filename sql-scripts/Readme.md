# SQL Scripts

This folder contains scripts we need to use to add permissions or perform one off changes to the database.

## Entra

The _Entra_ folder contains the scripts that add the _data reader) and _data writer_ permissions for each application on their respective databases.

These are run once, when we build a new environment (or rebuild an existing one) so each app has its permissions before first deployment.