# ADR001 - Use HTTP for inter-service communications

- **Status**: Adopted
- **Date**: 2022-10-04
- **Author**: Unknown

## Decision

HTTP will be used for inter-service communication. No message queue (RabbitMQ or
otherwise) shall be used.

## Options considered

1. (SELECTED) 'Simple HTTP'
2. A message queue

## Consequences

### Option 1 - Simple HTTP

- If an API call fails, we potentially lose the data in the call.