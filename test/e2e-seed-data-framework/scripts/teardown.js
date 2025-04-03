import { Op } from "sequelize";
import { checkConnections, closeConnections } from "../connections.js";
import * as ServiceDirectory from "../models/service-directory-models.js";
import * as Referral from "../models/referral-models.js";
import * as Report from "../models/report-models.js";

await checkConnections();

const baseId = parseInt(process.env.IDS_START_FROM);

try {
  await teardown();
} catch (error) {
  console.error("Unable to run teardown:", error);
} finally {
  await closeConnections();
}

async function teardown() {
  console.log("Tearing down Databases...");

  console.log("Tearing down ServiceDirectory..");
  await deleteRecords([
    ServiceDirectory.ServiceSearchResults,
    ServiceDirectory.ServiceSearches,
    ServiceDirectory.ServiceDeliveries,
    ServiceDirectory.Eligibilities,
    ServiceDirectory.Languages,
    ServiceDirectory.CostOptions,
    ServiceDirectory.Schedules,
    ServiceDirectory.Services,
    ServiceDirectory.Locations,
    ServiceDirectory.Organisations,
    ServiceDirectory.ServiceAtLocations,
    ServiceDirectory.Contacts,
    ServiceDirectory.ServiceTaxonomies,
  ]);

  console.log("Tearing Down Referral..");
  await deleteRecords([
    Referral.Organisations,
    Referral.ConnectionRequestsSentMetric,
    Referral.Recipients,
    Referral.ReferralServices,
    Referral.UserAccounts,
    Referral.Referrals,
    Referral.UserAccountRoles,
  ]);

  console.log("Tearing Down Report..");
  await deleteRecords([
    Report.ServiceSearchFacts,
    Report.ConnectionRequestsSentFacts,
    Report.OrganisationDim,
    Report.ServiceSearchesDim,
    Report.UserAccountDim,
  ]);

  console.log("Databases Torn Down!");
}

async function deleteRecords(models) {
  const messages = [];

  for (const model of models) {
    const primaryKey = model.primaryKeyAttributes[0];
    const deletedCount = await model.destroy({
      where: { [primaryKey]: { [Op.gte]: baseId } },
    });
    messages.push(`Deleted ${deletedCount} ${model.name}`);
  }

  messages.forEach((message) => {
    console.log(message);
  });
}
