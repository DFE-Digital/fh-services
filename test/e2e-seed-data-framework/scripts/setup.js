import { closeConnections } from '../connections.js'
import { testId, testPrefix } from '../helpers.js'
import * as ServiceDirectory from '../models/service-directory-models.js'

try {
  await setup()
} catch (error) {
  console.error('Unable to run setup:', error)
} finally {
  await closeConnections()
}

/**
 * Runs the test data setup scripts.
 */
async function setup () {
    console.log('Executing setup...')

  const orgOne = await ServiceDirectory.Organisations.create({
    Id: testId(1),
    OrganisationType: 'LA',
    Name: testPrefix("Aaron's test organisation"),
    Description: testPrefix('test desc'),
    AdminAreaCode: 'E08000031',
    Created: new Date()
  })

  const serviceOne = await ServiceDirectory.Services.create({
    Id: testId(1),
    ServiceType: 'FamilyExperience',
    Name: testPrefix("Aaron's test service"),
    Description: testPrefix("Aaron's test description"),
    CanFamilyChooseDeliveryLocation: false,
    Status: 'Active',
    DeliverableType: 'NotSet',
    Created: new Date(),
    CreatedBy: 2,
    OrganisationId: orgOne.Id,
    Summary: testPrefix('asdf')
  })

  await ServiceDirectory.Eligibilities.create({
    Id: 78645,
    ServiceId: serviceOne.Id,
    MaximumAge: 16,
    MinimumAge: 7,
    Created: new Date(),
    CreatedBy: 2
  })

  console.log('Successfully executed setup...')
}
