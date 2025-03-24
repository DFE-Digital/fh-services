# ADR027 - Build data warehouse for reporting metrics

<!-- 
Editor's note: This is a retroactive ADR made to reflect a decision that had 
               already been made as of writing. 
-->

- **Status**: Adopted
- **Date**: 2025-02-19
- **Author**: Joshua Taylor MBCS

## Decision

We will generate reporting metrics by building a data warehouse, structured with
a star schema. The warehouse will be populated using an Azure data factory (ADF)
pipeline using the existing operational databases as data sources. The data 
warehouse will be accessed from the Manage UI through a new 'Reporting API' 
service.

![](./img/ADR027-etl-approach.png)

## Context

In July 2024, the product manager made a feature request to provide end-user metric
and usage reports for the family hubs services for consumption by LA admins,
VCFS admins and DfE admins.

These reports would need to contain, for example:

- the number of organisations recorded
- the number of services stored
- how many searches have been queried

These reports needed be accessible through the 'Manage' service. An
approach for how the reports could be generated and how their underlying data
could be collected needed to be decided.

The approach needed to satisfy the high scalability requirements of the 'Find'
and 'Connect' services. Accessing the reports needed to also not exceed
acceptance performance ranges.

## Options considered

1. (SELECTED) Azure data factory ETL pipeline to generate service-fronted data
   warehouse