# ADR006 - Use Redis for session storage

- **Status**: Superceded by ADR010
- **Date**: 2022-10-26
- **Author**: Unknown

## Decision

A redis cache will be used for session storage.

## Context

The need for session storage came from needing to implement a 'stack frame' to
record pages and ensure proper 'back button' functionality in the Admin UI.