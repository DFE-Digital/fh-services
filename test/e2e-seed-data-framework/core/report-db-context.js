import { testId, encrypt, eventName } from "../helpers.js";
import * as Report from "../models/report-models.js";
import crypto from "crypto";

/**
 * Add an Organisation Dim
 *
 * @param organisationKey - The ID of the Organisation Dim
 * @param organisationTypeId - Either 2 (LA) or 4 (VCFS)
 * @param organisationTypeName - Either LA or VCFS
 * @param organisationId - The ID of the Organisation that this Dim represents, from the Service Directory Db
 * @param organisationName - The name of the Organisation that this Dim represents, from the Service Directory Db
 * @param createdBy - Who created this Organisation Dim
 * @param modifiedBy - Who modified this Organisation Dim
 */
export async function addOrganisationDim({
  organisationKey,
  organisationTypeId,
  organisationTypeName,
  organisationId,
  organisationName,
  createdBy,
  modifiedBy,
}) {
  await Report.OrganisationDim.create({
    OrganisationKey: testId(organisationKey),
    OrganisationTypeId: organisationTypeId,
    OrganisationTypeName: organisationTypeName,
    OrganisationId: testId(organisationId),
    OrganisationName: organisationName,
    Created: new Date(),
    CreatedBy: encrypt(createdBy),
    Modified: new Date(),
    ModifiedBy: encrypt(modifiedBy),
  });
}

/**
 * Add a Service Searches Dim
 *
 * @param serviceSearchesKey - The ID of this Service Searches Dim
 * @param serviceSearchId - The ID of the Service Search that this Dim represents, from the Service Directory Db
 * @param serviceTypeId - Either 1 (InformationSharing, Connect) or 2 (FamilyExperience, Find) - matches the Service Search that this Dim represents, from the Service Directory Db
 * @param serviceTypeName - Either InformationSharing (Connect) or FamilyExperience (Find) - determined from the ServiceTypeId
 * @param eventId - Either 1 (ServiceDirectoryInitialSearch) or 2 (ServiceDirectorySearchFilter) - matches the Service Search that this Dim represents, from the Service Directory Db
 * @param userId - The ID of the User who performed the service search, if applicable
 * @param organisationId - If ServiceTypeId == 1 (Connect), this is the organisation of the user. If ServiceTypeId == 2 (Find), this is the LA organisation that matches the postcode entered
 * @param postcode - The postcode that was entered for the search
 * @param searchRadiusMiles - The search radius in miles that was used
 * @param httpRequestTimestamp - When the search initiated
 * @param httpRequestCorrelationId - Telemetry data, can be made up
 * @param httpResponseCode - The response code of the search
 * @param httpResponseTimestamp - When the search returned a result
 */
export async function addServiceSearchesDim({
  serviceSearchesKey,
  serviceSearchId,
  serviceTypeId,
  serviceTypeName,
  eventId,
  userId,
  organisationId,
  postcode,
  searchRadiusMiles,
  httpRequestTimestamp,
  httpResponseCode,
  httpResponseTimestamp,
}) {
  await Report.ServiceSearchesDim.create({
    ServiceSearchesKey: testId(serviceSearchesKey),
    ServiceSearchId: testId(serviceSearchId),
    ServiceTypeId: serviceTypeId,
    ServiceTypeName: serviceTypeName,
    EventId: eventId,
    EventName: eventName(eventId),
    UserId: userId,
    OrganisationId: testId(organisationId),
    PostCode: postcode,
    SearchRadiusMiles: searchRadiusMiles,
    HttpRequestTimestamp: httpRequestTimestamp,
    HttpRequestCorrelationId: crypto.randomUUID(),
    HttpResponseCode: httpResponseCode,
    HttpResponseTimestamp: httpResponseTimestamp,
    Created: new Date(),
    Modified: new Date(),
  });
}

/**
 * Add a Service Search Fact
 *
 * @param serviceSearchesKey - The ID of the ServiceSearchesDim entry that this Service Search Fact is linked to
 * @param dateKey - The ID of the DateDim entry that this Service Search Fact is linked to
 * @param timeKey - The ID iof the TimeDim entry that this Service Search Fact is linked to
 * @param serviceSearchId - The ID of the Service Search that this Fact represents, from the Service Directory Db, also the same as the one linked to the servicesSearchesKey in the Service Search Dim table
 * @param id - The ID of this Service Search Fact
 */
export async function addServiceSearchFact({
  serviceSearchesKey,
  dateKey,
  timeKey,
  serviceSearchId,
  id,
}) {
  await Report.ServiceSearchFacts.create({
    ServiceSearchesKey: testId(serviceSearchesKey),
    DateKey: dateKey,
    TimeKey: timeKey,
    Created: new Date(),
    Modified: new Date(),
    ServiceSearchId: testId(serviceSearchId),
    Id: testId(id),
  });
}

/**
 * Add a Connection Request Sent Fact
 *
 * @param dateKey - The ID of the DateDim entry that this Con. Req. Sent Fact is linked to
 * @param timeKey - The ID of the TimeDim entry that this Con. Req. Sent Fact is linked to
 * @param organisationKey - The LA that this Fact is connected to, from the OrganisationDim table
 * @param connectionRequestsSentMetricsId - The ID of the connection requests sent metrics ID is linked to, from the Service Directory Db
 * @param requestTimestamp - When this connection request was initiated
 * @param requestCorrelationId - Telemetry data, can be made up
 * @param responseTimestamp - When the connection request is saved by the server
 * @param httpResponseCode - The response code
 * @param connectionRequestId - The ID of the Referral from the Referral Db
 * @param connectionRequestReferenceCode - The reference code also from the Referral Db
 * @param createdBy - Which user created this Fact
 * @param modifiedBy - Which user modified this Fact
 * @param id - The ID of this Fact
 * @param vcsOrganisationKey - The VCFS Organisation that this Fact is connected to, from the OrganisationDim table
 */
export async function addConnectionRequestsSentFact({
  dateKey,
  timeKey,
  organisationKey,
  connectionRequestsSentMetricsId,
  requestTimestamp,
  responseTimestamp,
  httpResponseCode,
  connectionRequestId,
  connectionRequestReferenceCode,
  createdBy,
  modifiedBy,
  id,
  vcsOrganisationKey,
}) {
  await Report.ConnectionRequestsSentFacts.create({
    DateKey: dateKey,
    TimeKey: timeKey,
    OrganisationKey: testId(organisationKey),
    ConnectionRequestsSentMetricsId: testId(connectionRequestsSentMetricsId),
    RequestTimestamp: requestTimestamp,
    RequestCorrelationId: crypto.randomUUID(),
    ResponseTimestamp: responseTimestamp,
    HttpResponseCode: httpResponseCode,
    ConnectionRequestId: testId(connectionRequestId),
    ConnectionRequestReferenceCode: connectionRequestReferenceCode,
    Created: new Date(),
    CreatedBy: encrypt(createdBy),
    Modified: new Date(),
    ModifiedBy: encrypt(modifiedBy),
    Id: testId(id),
    VcsOrganisationKey: testId(vcsOrganisationKey),
  });
}
