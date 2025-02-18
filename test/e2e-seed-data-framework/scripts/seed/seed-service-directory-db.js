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
    postcode: "E1 2EN",
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

  // First VCFS Service
  //#region
  await Database.addService({
    id: 7,
    serviceType: "InformationSharing",
    name: "Test VCFS Service One",
    description: "Test VCFS Service Description",
    status: "Active",
    organisationId: 2,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 7,
    locationId: 1,
    id: 7,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 7,
    taxonomyId: 18,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 8,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 7,
  });

  await Database.addContact({
    id: 9,
    telephone: "",
    email: "email@test.co.uk",
    locationId: 1,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 7,
    maximumAge: 18,
    minimumAge: 16,
    serviceId: 7,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 7,
    name: "English",
    serviceId: 7,
    code: "en",
  });

  // Adding Schedules to the Service..

  // Online..
  await Database.addSchedule({
    id: 9,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 7,
    attendingType: "Online",
  });

  // Telephone..
  await Database.addSchedule({
    id: 10,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 7,
    attendingType: "Telephone",
  });

  // InPerson.. Notice that this uses ServiceAtLocationId instead of ServiceId, as InPerson is tied to the Location!
  await Database.addSchedule({
    id: 11,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceAtLocationId: 7,
    attendingType: "InPerson",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 9,
    name: "InPerson",
    serviceId: 7,
  });

  await Database.addServiceDelivery({
    id: 10,
    name: "Online",
    serviceId: 7,
  });

  await Database.addServiceDelivery({
    id: 11,
    name: "Telephone",
    serviceId: 7,
  });

  //#endregion

  // Second VCFS Service
  //#region
  await Database.addService({
    id: 8,
    serviceType: "InformationSharing",
    name: "Test VCFS Service Two",
    description: "Test VCFS Service Description",
    status: "Active",
    organisationId: 2,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 8,
    locationId: 1,
    id: 8,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 9,
    taxonomyId: 18,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 9,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 8,
  });

  await Database.addContact({
    id: 10,
    telephone: "",
    email: "email@test.co.uk",
    locationId: 1,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 8,
    maximumAge: 8,
    minimumAge: 4,
    serviceId: 8,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 8,
    name: "English",
    serviceId: 8,
    code: "en",
  });

  // Adding Schedules to the Service..

  // Online..
  await Database.addSchedule({
    id: 12,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 8,
    attendingType: "Online",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 12,
    name: "InPerson",
    serviceId: 8,
  });
  //#endregion

  // Third VCFS Service
  //#region
  await Database.addService({
    id: 9,
    serviceType: "InformationSharing",
    name: "Test VCFS Service Three",
    description: "Test VCFS Service Description",
    status: "Active",
    organisationId: 2,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 9,
    locationId: 1,
    id: 9,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 9,
    taxonomyId: 14,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 10,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 9,
  });

  await Database.addContact({
    id: 11,
    telephone: "",
    email: "email@test.co.uk",
    locationId: 1,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 9,
    maximumAge: 8,
    minimumAge: 4,
    serviceId: 9,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 9,
    name: "English",
    serviceId: 9,
    code: "en",
  });

  // Adding Schedules to the Service..

  // Online..
  await Database.addSchedule({
    id: 13,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 9,
    attendingType: "Online",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 13,
    name: "InPerson",
    serviceId: 9,
  });
  //#endregion

  // Fourth VCFS Service
  //#region
  await Database.addService({
    id: 10,
    serviceType: "InformationSharing",
    name: "Test VCFS Service Four",
    description: "Test VCFS Service Description",
    status: "Active",
    organisationId: 2,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 10,
    locationId: 1,
    id: 10,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 10,
    taxonomyId: 15,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 11,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 10,
  });

  await Database.addContact({
    id: 12,
    telephone: "",
    email: "email@test.co.uk",
    locationId: 1,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 10,
    maximumAge: 9,
    minimumAge: 3,
    serviceId: 10,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 10,
    name: "English",
    serviceId: 10,
    code: "en",
  });

  // Adding Schedules to the Service..

  // Online..
  await Database.addSchedule({
    id: 14,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 10,
    attendingType: "Online",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 14,
    name: "InPerson",
    serviceId: 10,
  });
  //#endregion

  // Fifth VCFS Service
  //#region
  await Database.addService({
    id: 11,
    serviceType: "InformationSharing",
    name: "Test VCFS Service Five",
    description: "Test VCFS Service Description",
    status: "Active",
    organisationId: 2,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 11,
    locationId: 1,
    id: 11,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 11,
    taxonomyId: 16,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 12,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 11,
  });

  await Database.addContact({
    id: 13,
    telephone: "",
    email: "email@test.co.uk",
    locationId: 1,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 11,
    maximumAge: 9,
    minimumAge: 3,
    serviceId: 11,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 11,
    name: "English",
    serviceId: 11,
    code: "en",
  });

  // Adding Schedules to the Service..

  // Online..
  await Database.addSchedule({
    id: 15,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 11,
    attendingType: "Online",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 15,
    name: "InPerson",
    serviceId: 11,
  });
  //#endregion

  // Sixth VCFS Service
  //#region
  await Database.addService({
    id: 12,
    serviceType: "InformationSharing",
    name: "Test VCFS Service Six",
    description: "Test VCFS Service Description",
    status: "Active",
    organisationId: 2,
    summary: "Test Summary",
  });

  // To link them together, we need to create an entry in ServiceAtLocations
  await Database.addServiceAtLocation({
    serviceId: 12,
    locationId: 1,
    id: 12,
  });

  /**
   * A Service also needs at least one Taxonomy (i.e., Category)
   *
   * Since the Taxonomy table is pre-seeded, just select one to link up to your Service.
   *
   * In this case I chose ID 18, which is "Money, benefits and housing", which is automatically under category "Family support" as-is.
   */
  await Database.addServiceTaxonomy({
    serviceId: 12,
    taxonomyId: 17,
  });

  // Let's add two Contacts, one for the Service and one for its Location
  await Database.addContact({
    id: 13,
    telephone: "01234567890",
    url: "https://www.example.com/",
    email: "test@email.co.uk",
    serviceId: 12,
  });

  await Database.addContact({
    id: 14,
    telephone: "",
    email: "email@test.co.uk",
    locationId: 1,
  });

  // Adding an Eligibility..
  await Database.addEligibility({
    id: 12,
    maximumAge: 9,
    minimumAge: 3,
    serviceId: 12,
  });

  // Adding a Language..
  await Database.addLanguage({
    id: 12,
    name: "English",
    serviceId: 12,
    code: "en",
  });

  // Adding Schedules to the Service..

  // Online..
  await Database.addSchedule({
    id: 16,
    frequency: "WEEKLY",
    byDay: "MO,WE,FR",
    description: "Test Description",
    serviceId: 12,
    attendingType: "Online",
  });

  // For some reason, you need to make ServiceDelivery entries that match your Schedules..
  await Database.addServiceDelivery({
    id: 16,
    name: "InPerson",
    serviceId: 12,
  });
  //#endregion
}
