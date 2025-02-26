# ADR025 - Use SonarCloud in 'monitoring mode'

- **Status**: Adopted
- **Date**: 2024-12-10
- **Author**: Unknown

## Decision

SonarCloud scanning tools will be used to detect issues in code quality. The
'monitoring mode' will be set, causing a new scan to start on every push to the
main or release branches.

## Context

An initial scan of SonarCloud on the family hubs service repository branches
revealed that they did not meet the thresholds for any quality checks. 

## Options considered

1. (SELECTED) Use SonarCloud in 'monitoring mode'

## Consequences

### Option 1 - Use SonarCloud in 'monitoring mode'

- Would apply DfE-wide rules on code quality to the family hubs service.