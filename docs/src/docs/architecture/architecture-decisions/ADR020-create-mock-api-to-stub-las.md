# ADR020 - Create an ASP.NET API to stub local authorities during development

- **Status**: Adopted
- **Date**: 2024-08-06
- **Author**: Unknown

## Decision

A stub API will be created in ASP.NET to send requests against while developing
open referral integration capabilities.

## Context

Open referral (OR) ingestion capabilities need to be developed before the
OR-compliant API has been created by Somerset Council.

An approach was needed to unblock developers from this dependency.

## Options considered

1. (SELECTED) Create a mock API as an ASP.NET API

## Consequences

### Option 1 - Create a mock API as an ASP.NET API

- Allows controlling the data returned from the API, giving flexibility to
  simulate many test scenarios.
