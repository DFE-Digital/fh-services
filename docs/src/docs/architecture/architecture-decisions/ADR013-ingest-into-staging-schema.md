# ADR013 - Ingest service data into a 'staging schema'

- **Status**: Adopted
- **Date**: 2024-08-06
- **Author**: Unknown

## Decision

Data ingested from external sources will be loaded into a separate 'staging' schema
within the existing service directory database. This is in opposition to maintaining
separate databases for this purpose.

## Context

New guidelines were given recently with regard to the number of organisation and
users that reduces the scaling requirements of the Family Hubs products. An opportunity was
seen to simplify the architecture by reducing the need for multiple databases.

## Options considered

1. (SELECTED) Ingest data into a 'staging schema' in the service directory database
2. Do nothing, keeping the existing multi-database structure

## Consequences

### Option 1 - Staging schema

- Would simplify the architecture and be a step towards a 'modular monolith'
  architecture style.
- Reduces the scale capability of the services to 25 local authorities with 100
  VCFS organisations.

### Option 2 - Do nothing

- Would maintain the current high scale capability of 152 local authorities with
  1000 VCFS organisations.