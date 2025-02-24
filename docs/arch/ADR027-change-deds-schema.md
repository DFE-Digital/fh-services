# ADR027 - Change deds schema

- **Status**: Draft
- **Date**: '2025-02-24
- **Author**: Stuart Maskell

## Decision

Replace the existing DEDS full entity graph schema (taken from Internation spec v3.0 HSDS schema), and replace it with a simpler schema where the incoming JSON data is stored as JSON instead.

## Context

There is significant complexity in creating a full entity graph schema based on the HSDS 3.0 Open referral internation spec. This leads to tight coupling to particular open referral spec, when the service will take on many open referral specs, and also potential file uploads which will contain far less data. This would then shoehorn other specification into this spec. Also the original data it will then lose it's original structure.

We also do not have any known and forseeable need to query specific data from the deds store, this means storing the full entitiy graph is adding additional complexity for no gain. Storing as JSON means overwriting is a simple case of replacing the JSON string, and reading is simply extracting the informatin we need from the JSON when we need it.

There is no known ADR or decision document which explains why the current was chosen over storing JSON.

## Options considered

1. Store the JSON data as a string/json field in the database with minimal other columns to represent data.
2. Do nothing and keep the current full entity graph schema.

## Consequences

### Option 1

- Reduced complexity in the database and fetching of data.
- Overwriting data when changes occur is a simple overwrite on the JSON field, as appose to pulling the full graph of data and updating.
- Increased storage size as storing JSON will require more space than using a full entity graph.
    - Number of Family Hubs = 431
    - Number of VCFS = average 1000 per LA
    - Average file size = 13.5kb (Minified JSON) taken from Mock HSDS in the repo and a sample of a Somerset service

### Option 2

- With each new version of the Open Referral specification in both Internation and UK, the complexity of mapping data becomes increasingly difficult.
- Maintaining the full schema when changes are required.

## Advice

Designs were drawn on a Lucid baord, including technical diagrams and Database schema tables. Discussions were around if it is a maintainable way, is it simple and effective, do the technical diagrams make sense now and for any forseeable future and does it fullfill all the known requirements.

These were then reviewed by:

- Aaron Yarnborough (Tech lead) on 2025-02-20
- Josh Taylor (Technical Architect) on 2025-02-20
