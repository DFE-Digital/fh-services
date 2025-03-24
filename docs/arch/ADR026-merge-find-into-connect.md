# ADR026 - Merge 'Find' into 'Connect'

- **Status**: Adopted
- **Date**: 2025-02-05
- **Author**: Joshua Taylor

## Decision

'Connect' will be transformed into the new single directory service. 'Find' will
be removed and its traffic redirected to 'Connect'.

## Context

An objective has been set for the FH programme, to be completed by March 2025,
to consolidate the 'Find' and 'Connect' services into a single service. 

The existing structure has 'Connect' showing local authority (LA) services and
voluntary, community and faith services (VCFS); whereas 'Find' only shows LA
services. 'Connect' is additionally only available for logged in users who have
been granted access.

The new 'single directory' service will show both LA services and VCFS in a
public directory. It will also contain the 'Request for Support' features behind
a login, although initially these will be disabled for users. The existing
services would then be retired or cut down as necessary. 

An approach for transitioning 'Find' and 'Connect' to this single service needed
to be decided.

## Options considered

1. (SELECTED) Transform 'Connect' into the new single directory and remove
   'Find'.
2. Build a new component then remove 'Find' and 'Connect'.
3. Transform 'Find' into the new single directory and remove 'Connect'.

## Consequences

### Option 1 - Transform 'Connect' into the new single directory and remove 'Find'

- 'Connect' already has service directory and support request features, so
  would be less work to change it to deliver the new service. 

- 'Connect' is generally easier to maintain, so would be less costly to change
  to accommodate being the single service.

- The existing technical debt and design of 'Connect' would remain to add
  difficulty to future maintenance.

### Option 2 - Build a new component then remove 'Find' and remove 'Connect'

- The new single directory component would not inherit any existing technical
  debt in either 'Find' or 'Connect'.

- The overall cost of development would be far higher than the other options.

### Option 3 - Transform 'Find' into the new single directory and remove 'Connect'

- The existing technical debt and design of 'Find' would remain to add
  difficulty to future maintenance.

- 'Find' does not contains support request or login functionality. Adding the
  functionality would be an additional cost of this approach. 

## Advice

- Aaron Yarborough:
  - It was always the plan to consolidate 'Find' and 'Connect' into a single
    component with all capabilities.
    
  - 'Connect' is the most feature rich and has easier code to maintain; so general
    effort would be lower.

  - Making a new component would give the opportunity to remove some tech debt
    and leave a better designed component. Although it could have also
    introduced some more tech debt.
    
  - A new component would take more time to complete.

  - Transforming 'Find' would be harder since it does not contain request for
    support features.

- Zac King:
  - The easiest to maintain approach would be to start a new component, due to the
    difficulty of working with the existing services.

  - 'Connect' is still pretty hard to maintain, but it's marginally better
    compared to 'Find'.

  - A new component was not as attractive more due to risk than development time.

  - It could have been a waste to build a new component, since we are hoping to
    do a larger piece of work on design improvements.

  - 'Find' has better accessibility properties compared to 'Connect', so if
    'Find' were to be the merge base there would be less accessibility work to
    complete. 