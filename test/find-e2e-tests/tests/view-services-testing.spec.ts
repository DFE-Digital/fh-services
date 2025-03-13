import {describe, it, test} from '@serenity-js/playwright-test';
import * as findIndex from './serenity-tools/find-index';
import {GENERAL_PUBLIC_USER} from './actors-user-roles';
const POSTCODE: string = "W1D 2JT"
describe('View Services Tests', () => {

    test.use({
        defaultActorName: `${GENERAL_PUBLIC_USER}`
    })

    test.beforeEach('Setup', async ({actor}) => {
        await actor.attemptsTo(
            findIndex.navigateToFind(),
            findIndex.clickOnTheStartButton(),
            findIndex.enterPostcode(POSTCODE),
            findIndex.clickOnPostcodeSearchButton()
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able to search for services`, async () => {
        const postcodeArray = POSTCODE.toLowerCase().split(" ");
        findIndex.isSearchResultsPageDisplayed(postcodeArray[0], postcodeArray[1])
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able verify LA Service information`, async ({actor}) => {
        await actor.attemptsTo(
            findIndex.doesTheLAServiceInformationInTheListOfServicesPageContain('Test LA'),
            findIndex.doesTheLAServiceInformationInTheListOfServicesPageContain('Category'),
            findIndex. doesTheLAServiceInformationInTheListOfServicesPageContain('Age range'),
            findIndex.doesTheLAServiceInformationInTheListOfServicesPageContain('Where'),
            findIndex.doesTheLAServiceInformationInTheListOfServicesPageContain('Cost'),
            findIndex.clickOnTheLaService(),
            findIndex.doesTheServiceDetailsPageContentContainField('Service details'),
            findIndex.doesTheServiceDetailsPageContentContainField('[E2E] Test Summary'),
            findIndex.doesTheServiceDetailsPageContentContainField('Location'),
            findIndex.doesTheServiceDetailsPageContentContainField('More details'),
            findIndex.doesTheServiceDetailsPageContentContainField('Contact details')
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able verify VCFS Service information`, async ({actor}) => {
        await actor.attemptsTo(
            findIndex.doesTheVCFSServiceInformationInTheListOfServicesPageContains('Test Organisation'),
            findIndex.doesTheVCFSServiceInformationInTheListOfServicesPageContains('Category'),
            findIndex.doesTheVCFSServiceInformationInTheListOfServicesPageContains('Age range'),
            findIndex.doesTheVCFSServiceInformationInTheListOfServicesPageContains('Where'),
            findIndex.doesTheVCFSServiceInformationInTheListOfServicesPageContains('Cost'),
            findIndex.clickOnTheVcfsService(),
            findIndex.doesTheServiceDetailsPageContentContainField('Service details'),
            findIndex.doesTheServiceDetailsPageContentContainField('[E2E] Test Summary'),
            findIndex.doesTheServiceDetailsPageContentContainField('Location'),
            findIndex.doesTheServiceDetailsPageContentContainField('More details'),
            findIndex.doesTheServiceDetailsPageContentContainField('Contact details')
        );
    });
});