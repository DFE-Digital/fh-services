import * as Database from "../../core/service-directory-db-context.js";

/**
 * This class contains the implementation of the seeding data for the Service Directory Db.
 *
 * To get started, see "example/seed-service-directory-db.js".
 */

export async function seed() {
    // Create an Organisation for our E2E tests
    await Database.addOrganisation({
        id: 1,
        organisationType: "LA",
        name: "Test LA",
        description: "Test LA based in Westminster",
        adminAreaCode: "E09000033",
    });
}
