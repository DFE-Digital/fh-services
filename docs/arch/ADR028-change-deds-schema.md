# ADR028 - Change deds schema

- **Status**: Draft
- **Date**: 2025-02-24
- **Author**: Stuart Maskell

## Decision

Replace the existing DEDS full entity graph schema (taken from Internation spec v3.0 HSDS schema), and replace it with a simpler schema where the incoming JSON data is stored as JSON instead.

## Context

There is significant complexity in creating a full entity graph schema based on the HSDS 3.0 Open referral internation spec. This leads to tight coupling to particular open referral spec, when our service will take on many open referral specs, and also potential file uploads which will contain far less data. This would then shoehorn other specification into this spec, and as a result, original data will then lose it's structure.

There is documentation in confluence which talks about using JSON. However, there is no known ADR or decision document which explains why the current was chosen over storing JSON. 

## Options considered

1. Store the JSON data as a string/json field in the database with minimal other columns to represent data.
2. Do nothing and keep the current full entity graph schema.

## Consequences

### Option 1

- No complexity storing data as we just store the JSON document.
- Keep the original document from the third party intact. Querying of data within the document does not need to change.
- Greatly reduced complexity in the database as the schema is small. A single table with serviceId, JSON document, version of the document and auditing columns.
- Updating data is simplified to an overwrite of the JSON field, rather than querying and updating a full normalized entity graph. This eliminates the complexity of field mapping in large models.
- Complexity of mapping is when retrieving data from the database, mapping of each version of data.
- Increased storage size, as storing JSON will require more space than a fully normalized SQL schema. Unlike a normalized entity graph that uses references, JSON storage can contain data duplication within it's own structure, this is the nature of JSON."
    - Number of Family Hubs = 431
    - Number of VCFS = average 1000 per LA
    - Average file size = 13.5kb (Minified JSON) taken from Mock HSDS in the repo and a sample of a Somerset service

### Option 2

- Complexity of mapping is when ingesting data, mapping of each version of data.
- Maintaining the full schema when changes are required could lead to time consuming or complex tasks with migrating data.
- Mapping third party fields types to our schema can be mismatched which could require more work to map with in both our code database. <-- i.e id could be int when our field is uuid -->
- More mental overhead for developers dealing with such a large entity model.
- All the data stored is kept in a single normalized SQL schema.

## Advice

- Aaron Yarnborough - Tech lead - 2025-02-20
    - Go through the reasons why JSON would be a less complex design in both Database schema, data flow, code logic and Developer productivity. 
    - Go through technical flow diagrams and database schemas, understand if they make sense and simple to understand. Update where needed.
    - Go through any performance concerns.
    - Understand how it can fit in with potential future work around service reviews (manual checking of a service in deds)
    - Understand how it will can be a drop in replacement for the existing schema.
    - Maintainability.

- Josh Taylor - Technical Architect - on 2025-02-20
    - Go through the reasons why JSON would be a less complex design in Database schema and data flow.
    - Understand how it can fit in with potential future work around service reviews (manual checking of a service in deds)
    - Understand how it will can be a drop in replacement for the existing schema.
    - Are there any further consideration from a tech arch perspective.
