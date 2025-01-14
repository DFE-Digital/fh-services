# ADR008 - Use GOV.UK One Login for authentication

- **Status**: Adopted
- **Date**: 2023-04-04
- **Author**: Unknown

## Decision

Use GOV.UK One Login to authenticate users to the family hubs services.

## Context

After adopting ASP.NET Identity in ADR004, it was found to not conform to the 
OpenId Connect (OIDC) protocol.

## Options considered

1. (SELECTED) Adopt GOV.UK One Login
2. Identity Server 4
3. Identity Server 6

## Consequences

### Option 1 - Adopt GOV.UK One Login

-  Would conform to the OIDC protocol.
-  Transfers some security risk to GOV.UK One Login.
-  Requires reworking the user flow for the sign-up process.
