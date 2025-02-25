# ADR028 - Simplify DEDS schema to store JSON documents

- **Status**: Draft
- **Date**: 2025-02-24
- **Author**: Stuart Maskell

## Decision

Replace the existing DEDS full entity graph schema (taken from International spec v3.0 HSDS schema), and replace it with a simpler schema where the incoming JSON data is stored as JSON instead.

## Context

The existing DEDS schema models the OR international v3.0 spec. This was likely done because we were attempting to understand OpenRerral ourselves, and the International spec is what we were most likely to ingest. Since then it's seeming more likely that we'll be ingesting various data types, such as OpenReferral UK (A subset of International) and file upload which does not conform to any OpenReferral specification. 

Trying to implement this became really complicated using the existing DEDS schema. The question arose as to whether we could do things differently with less complexity. 

## Options considered

1. Store the JSON data as a string/JSON field in the database with minimal other columns to represent data.
2. Do nothing and keep the current full entity graph schema.

## Consequences

### Option 1

- No complexity storing data as we simply store the JSON document.
- Keep the original document from the third party intact. Querying of data within the document does not need to change.
- Greatly reduced complexity in the database as the schema is small. A single table including serviceId, JSON document, version of the document and auditing columns.
- Updating data is simplified to an overwrite of the JSON field, rather than querying and updating a full normalized entity graph. This eliminates the complexity of field mapping in large models.
- Complexity is when retrieving data from the database, mapping of each version of JSON data is required to extract the information we need.
- Increased storage size, as storing JSON will require more space than a fully normalized SQL schema. Unlike a normalized entity graph that uses references, JSON storage can contain data duplication within it's own structure, this is the nature of JSON."
    - Number of Family Hubs = 431
    - Number of VCFS = average 1000 per LA
    - Average file size = 13.5kb (Minified JSON) taken from Mock HSDS in the repo and a sample of a Somerset service

### Option 2

- Complexity is when ingesting data, mapping of each version of data is required to create on the DB schema.
- Supporting future Open Referral versions will require us to update the DEDS schema to reflect schema changes in the DB
- Mapping third party fields types to our schema can be mismatched which could require more work to map with in both our code database. <-- i.e id could be int when our field is uuid -->
- More mental overhead for developers dealing with such a large entity model.
- All the data stored is kept in a single normalized SQL schema.

## Advice

- Aaron Yarnborough - Tech lead - 2025-02-20
    - Go through the reasons why JSON would be a less complex design in both Database schema, data flow, code logic and Developer productivity. 
    - Go through technical flow diagrams and database schemas, understand if they make sense and simple to understand. Update where needed.
    - Go through any performance concerns.
    - Understand how it can fit in with potential future work around service reviews (manual checking of a service in DEDS)
    - Understand how it will can be a drop in replacement for the existing schema.
    - Maintainability.

- Josh Taylor - Technical Architect - on 2025-02-20
    - Go through the reasons why JSON would be a less complex design in Database schema and data flow.
    - Understand how it can fit in with potential future work around service reviews (manual checking of a service in DEDS)
    - Understand how it will can be a drop in replacement for the existing schema.
    - Are there any further consideration from a tech arch perspective.
