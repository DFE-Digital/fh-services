# ADR004 - Use ASP.NET Identity Authentication

- **Status**: Superceded by ADR008
- **Date**: 2022-10-04
- **Author**: Unknown

## Decision

ASP.NET Identity will be used to authenticate users of the family hubs services.

## Options considered

1. (SELECTED) ASP.NET Identity
2. IdentityServer
3. Azure AD B2C
4. GOV.UK Sign-in
5. DfE Sign-in

## Consequences

### Option 1 - ASP.NET Identity

- Can be customised because the source code is available.
- The team has an added responsibility for the security of stored user accounts and passwords.

### Option 2 - IdentityServer

- Would require a paid licence.

### Option 3 - Azure AD B2C

- A seaparte Azure AD tenant would be required for each LA and VCF organisation. This would be onerous for the support desk.

### Option 4 - GOV.UK Sign-in

- Does not provide features for login organisations.

### Option 5 - DfE Sign-in

- Requires users to have an education.gov.uk account, which VCF users would not have.