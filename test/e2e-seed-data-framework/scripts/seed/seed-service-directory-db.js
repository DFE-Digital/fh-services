import * as Database from "../../core/service-directory-db-context.js";

/**
 * This class contains the implementation of the seeding data for the Service Directory Db.
 *
 * To get started, see "example/seed-service-directory-db.js".
 */

export async function seed() {
  // Create an LA Organisation for our E2E tests
  await Database.addOrganisation({
    id: 1,
    organisationType: "LA",
    name: "Test LA",
    description: "Test LA based in Westminster",
    adminAreaCode: "E09000033",
  });

  // Create an VCFS Organisation for our Service
  await Database.addOrganisation({
    id: 2,
    associatedOrganisationId: 1,
    organisationType: "VCFS",
    name: "Test Organisation",
    description: "Test Organisation based in Westminster",
    adminAreaCode: "E09000030",
  });

  // Add a Location for the LA Service & VCFS Organisation
  await Database.addLocation({
    id: 1,
    locationTypeCategory: "NotSet",
    name: "Test Location",
    description: "Test Description",
    latitude: 51.517612,
    longitude: -0.056838,
    address1: "100 Test Street",
    city: "London",
    postcode: "SW1A 2AD",
    stateProvince: "City of London",
  });
  
  // First Service
  //#region
  await Database.addService({
    id: 1,
    serviceType: "FamilyExperience",
    name: "Test LA Service One",
    description: "Test LA Service Description",
    status: "Active",
    organisationId: 1,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 1,
    locationId: 1,
    id: 1,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 1,
    taxonomyId: 18,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 1,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 1,
  });

  await Database.addContact({
    id: 2,
    telephone: "",
    email: "email@test.co.uk",
    locationId: 1,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 1,
    maximumAge: 18,
    minimumAge: 16,
    serviceId: 1,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 1,
    name: "English",
    serviceId: 1,
    code: "en",
  });

  // Adding Schedules to the Service..

  // Online..
  await Database.addSchedule({
    id: 1,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 1,
    attendingType: "Online",
  });

  // Telephone..
  await Database.addSchedule({
    id: 2,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 1,
    attendingType: "Telephone",
  });

  // InPerson.. Notice that this uses ServiceAtLocationId instead of ServiceId, as InPerson is tied to the Location!
  await Database.addSchedule({
    id: 3,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceAtLocationId: 1,
    attendingType: "InPerson",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 1,
    name: "InPerson",
    serviceId: 1,
  });

  await Database.addServiceDelivery({
    id: 2,
    name: "Online",
    serviceId: 1,
  });

  await Database.addServiceDelivery({
    id: 3,
    name: "Telephone",
    serviceId: 1,
  });
  
  //#endregion
  
  // Second Service
  //#region
  // Then let's create the Service
  await Database.addService({
    id: 2,
    serviceType: "FamilyExperience",
    name: "Test LA Service Two",
    description: "Test LA Service Description",
    status: "Active",
    organisationId: 1,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 2,
    locationId: 1,
    id: 2,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 2,
    taxonomyId: 17,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 3,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 2,
  });
  
  // Adding an Eligibility..
  await Database.addEligibility({
    id: 2,
    maximumAge: 14,
    minimumAge: 10,
    serviceId: 2,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 2,
    name: "English",
    serviceId: 2,
    code: "en",
  });

  // Adding Schedules to the Service..
  // InPerson.. Notice that this uses ServiceAtLocationId instead of ServiceId, as InPerson is tied to the Location!
  await Database.addSchedule({
    id: 4,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceAtLocationId: 2,
    attendingType: "InPerson",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 4,
    name: "InPerson",
    serviceId: 2,
  });
  //#endregion

  // Third Service
  //#region
  // Then let's create the Service
  await Database.addService({
    id: 3,
    serviceType: "FamilyExperience",
    name: "Test LA Service Three",
    description: "Test LA Service Description",
    status: "Active",
    organisationId: 1,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 3,
    locationId: 1,
    id: 3,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 3,
    taxonomyId: 11,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 4,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 3,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 3,
    maximumAge: 14,
    minimumAge: 10,
    serviceId: 3,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 3,
    name: "English",
    serviceId: 3,
    code: "en",
  });

  // Adding Schedules to the Service..
  // InPerson.. Notice that this uses ServiceAtLocationId instead of ServiceId, as InPerson is tied to the Location!
  await Database.addSchedule({
    id: 5,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceAtLocationId: 3,
    attendingType: "InPerson",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 5,
    name: "InPerson",
    serviceId: 3,
  });
  //#endregion

  // Fourth Service
  //#region
  // Then let's create the Service
  await Database.addService({
    id: 4,
    serviceType: "FamilyExperience",
    name: "Test LA Service Four",
    description: "Test LA Service Description",
    status: "Active",
    organisationId: 1,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 4,
    locationId: 1,
    id: 4,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 4,
    taxonomyId: 10,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 5,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 4,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 4,
    maximumAge: 14,
    minimumAge: 10,
    serviceId: 4,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 4,
    name: "English",
    serviceId: 4,
    code: "en",
  });

  // Adding Schedules to the Service..
  // InPerson.. Notice that this uses ServiceAtLocationId instead of ServiceId, as InPerson is tied to the Location!
  await Database.addSchedule({
    id: 6,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceAtLocationId: 4,
    attendingType: "InPerson",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 6,
    name: "InPerson",
    serviceId: 4,
  });
  //#endregion

  // Fifth Service
  //#region
  // Then let's create the Service
  await Database.addService({
    id: 5,
    serviceType: "FamilyExperience",
    name: "Test LA Service Five",
    description: "Test LA Service Description",
    status: "Active",
    organisationId: 1,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 5,
    locationId: 1,
    id: 5,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 5,
    taxonomyId: 10,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 6,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 5,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 5,
    maximumAge: 7,
    minimumAge: 6,
    serviceId: 5,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 5,
    name: "English",
    serviceId: 5,
    code: "en",
  });

  // Adding Schedules to the Service..
  // InPerson.. Notice that this uses ServiceAtLocationId instead of ServiceId, as InPerson is tied to the Location!
  await Database.addSchedule({
    id: 7,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceAtLocationId: 5,
    attendingType: "InPerson",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 7,
    name: "InPerson",
    serviceId: 5,
  });
  //#endregion

  // Sixth Service
  //#region
  // Then let's create the Service
  await Database.addService({
    id: 6,
    serviceType: "FamilyExperience",
    name: "Test LA Service Five",
    description: "Test LA Service Description",
    status: "Active",
    organisationId: 1,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 6,
    locationId: 1,
    id: 6,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 6,
    taxonomyId: 9,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 7,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 6,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 6,
    maximumAge: 7,
    minimumAge: 6,
    serviceId: 6,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 6,
    name: "English",
    serviceId: 6,
    code: "en",
  });

  // Adding Schedules to the Service..
  // InPerson.. Notice that this uses ServiceAtLocationId instead of ServiceId, as InPerson is tied to the Location!
  await Database.addSchedule({
    id: 8,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceAtLocationId: 6,
    attendingType: "InPerson",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 8,
    name: "InPerson",
    serviceId: 6,
  });
  //#endregion
}
