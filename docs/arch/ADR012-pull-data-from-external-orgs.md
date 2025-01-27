# ADR012 - Pull directory data from external organisations

- **Status**: Adopted
- **Date**: 2024-08-06
- **Author**: Unknown

## Decision

Pull directory data from external organisations which have open referral
interfaces, in opposed to having the data pushed to family hubs.

## Options considered

1. (SELECTED) Pull directory data from external organisations
2. Receive data pushed from external organisations

## Consequences

### Option 1 - Pull directory data from external organisations

- Allows the family hubs service to ingest data without requiring changes at the
  external organisation.
- May need to evolve the database schema, due to a lack of 'created by' User IDs 
  when gathering data.
