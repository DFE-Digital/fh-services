/**
 * This script contains examples of how to seed each table in the Report Db.
 *
 * All of these will be the result of populating the ServiceSearches and ConnectionRequests in the Service Directory & Referral seeding scripts respectively.
 *
 * Essentially we are doing ADFs job :)
 *
 */

import {
  getDateKeyAndTimeKey,
  getHttpResponseTimeFromHttpRequestTime,
} from "../../../helpers.js";

import * as Database from "../../../core/report-db-context.js";

async function createConnectionRequestSentMetrics() {
  const dateTime = new Date();
  const [dateKey, timeKey] = getDateKeyAndTimeKey(dateTime);

  // Create an OrganisationDim for the VcsOrganisationKey in the Fact
  // This will be equivalent to the Organisations used in the Referral Metric..
  await Database.addOrganisationDim({
    organisationKey: 1,
    organisationTypeId: 4, // Always 4, as this is the code for a VCFS
    organisationTypeName: "VCFS",
    organisationId: 1,
    organisationName: "Test Service",
    createdBy: "",
    modifiedBy: "",
  });

  await Database.addOrganisationDim({
    organisationKey: 2,
    organisationTypeId: 2, // Always 2, as this is the code for an LA
    organisationTypeName: "LA",
    organisationId: 2,
    organisationName: "Test LA",
    createdBy: "",
    modifiedBy: "",
  });

  // This will have to mirror the ConnectionRequestSentMetrics you created when seeding the Referral Db
  await Database.addConnectionRequestsSentFact({
    dateKey: dateKey,
    timeKey: timeKey,
    organisationKey: 2,
    connectionRequestsSentMetricsId: 1,
    requestTimestamp: dateTime,
    responseTimestamp: getHttpResponseTimeFromHttpRequestTime(dateTime),
    httpResponseCode: 200,
    connectionRequestId: 1, // == ReferralId in ConnectionRequestSentMetric
    connectionRequestReferenceCode: "ABCDEF", // == ReferralReferenceCode in ConnectionRequestSentMetric
    createdBy: "",
    id: 1,
    vcsOrganisationKey: 1,
  });
}

async function createServiceSearch() {
  const dateTime = new Date();
  const [dateKey, timeKey] = getDateKeyAndTimeKey(dateTime);

  await Database.addServiceSearchesDim({
    serviceSearchesKey: 1,
    serviceSearchId: 1,
    serviceTypeId: 1,
    serviceTypeName: "InformationSharing",
    eventId: 1,
    eventName: "ServiceDirectoryInitialSearch",
    userId: 1,
    organisationId: 1,
    postcode: "E1 2EN",
    searchRadiusMiles: 20,
    httpRequestTimestamp: dateTime,
    httpResponseCode: 200,
    httpResponseTimestamp: getHttpResponseTimeFromHttpRequestTime(dateTime),
  });

  await Database.addServiceSearchFact({
    serviceSearchesKey: 1,
    dateKey: dateKey,
    timeKey: timeKey,
    serviceSearchId: 1,
    id: 1,
  });
}

export async function seed() {
  await createConnectionRequestSentMetrics();
  await createServiceSearch();
}
