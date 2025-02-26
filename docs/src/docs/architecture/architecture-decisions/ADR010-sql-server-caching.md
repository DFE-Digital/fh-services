# ADR010 - Use SQL Server caching for session storage

- **Status**: Adopted 
- **Date**: 2023-06-06
- **Author**: Unknown

## Decision

Use SQL Server caching for session storage.

## Context

We use a distributed cache (separate to the server a website is hosted on) to 
save the information collected on a given page in a User Journey between 
request post-backs (e.g. the User Journey for adding a service, the User 
Journey for a Professional referral and the User Journey for IdAMs) and 
encountered a number of issue with the Azure Managed Redis cache that resulted 
in the cache being unresponsive and returning an error. 

After quite a lengthy investigation the issue was determined to be outside of
our control most likely in the client library we were using so it was decided to
investigate an alternate mechanism for the distributed cache.

## Options considered

1. (SELECTED) Use SQL Server caching
2. Stick with Redis

## Consequences

### Option 1 - Use SQL Server Caching

- Simple to implement
- May result in a slight performance reduction
- Could increase database load
- Saves costs, since SQL Server is already being used as the primary database

### Option 2 - Stick with Redis

- Continuing errors and unresponsiveness on page loads