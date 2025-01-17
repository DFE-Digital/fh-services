# ADR008 - Use Azure App Services for service hosting

- **Status**: Adopted
- **Date**: 2022-11-21
- **Author**: Unknown

## Decision

Applications in the family hub services will be directly deployed to Azure App
Services.

## Options considered

1. (SELECTED) Azure App Services
2. Container Apps

## Consequences

### Option 1 - Azure App Services

- More effort to migrate family hubs to another cloud provider.
- Brings the family hubs architecture more in line with the 'Apprenticeship
  service' which is considered the exemplar in the DfE.

### Option 2 - Container Apps

- Less effort to migrate family hubs to another cloud provider.
- Requires more effort to build and maintain hosting environments.