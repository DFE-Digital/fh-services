import * as Database from "../../core/referral-db-context.js";

/**
 * This class contains the implementation of the seeding data for the Referral Db.
 *
 * To get started, see "example/seed-referral-db.js".
 */

export async function seed() {
    // Create an Organisation for our E2E tests
    await Database.addOrganisation({
        id: 1,
        name: "Test LA",
        description: "Test LA based in Westminster",
        createdBy: "",
    });
}
