import {describe, it, test} from '@serenity-js/playwright-test';
import {
    navigateToFind,
    clickOnTheStartButton,
    enterPostcode,
    clickOnPostcodeSearchButton,
    doesTheLAServiceInformationInTheListOfServicesPageContain,
    doesTheVCFSServiceInformationInTheListOfServicesPageContains,
    clickOnTheLaService,
    doesTheServiceDetailsPageContentContain, 
    clickOnTheVcfsService, 
    isSearchResultsPageDisplayed
} from './serenity-tools/find-index';
import {GENERAL_PUBLIC_USER} from './actors-user-roles';
export const POSTCODE: string = "W1D 2JT"
describe('View Services Tests', () => {

    test.use({
        defaultActorName: `${GENERAL_PUBLIC_USER}`
    })

    test.beforeEach('Setup', async ({actor}) => {
        await actor.attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode(POSTCODE),
            clickOnPostcodeSearchButton()
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able to search for services`, async () => {
        const postcodeArray = POSTCODE.toLowerCase().split(" ");
        isSearchResultsPageDisplayed(postcodeArray[0], postcodeArray[1])
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able verify LA Service information`, async ({actor}) => {
        await actor.attemptsTo(
            doesTheLAServiceInformationInTheListOfServicesPageContain('Test LA'),
            doesTheLAServiceInformationInTheListOfServicesPageContain('Category'),
            doesTheLAServiceInformationInTheListOfServicesPageContain('Age range'),
            doesTheLAServiceInformationInTheListOfServicesPageContain('Where'),
            doesTheLAServiceInformationInTheListOfServicesPageContain('Cost'),
            clickOnTheLaService(),
            doesTheServiceDetailsPageContentContain('Service details'),
            doesTheServiceDetailsPageContentContain('[E2E] Test Summary'),
            doesTheServiceDetailsPageContentContain('Location'),
            doesTheServiceDetailsPageContentContain('More details'),
            doesTheServiceDetailsPageContentContain('Contact details')
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able verify VCFS Service information`, async ({actor}) => {
        await actor.attemptsTo(
            doesTheVCFSServiceInformationInTheListOfServicesPageContains('Test Organisation'),
            doesTheVCFSServiceInformationInTheListOfServicesPageContains('Category'),
            doesTheVCFSServiceInformationInTheListOfServicesPageContains('Age range'),
            doesTheVCFSServiceInformationInTheListOfServicesPageContains('Where'),
            doesTheVCFSServiceInformationInTheListOfServicesPageContains('Cost'),
            clickOnTheVcfsService(),
            doesTheServiceDetailsPageContentContain('Service details'),
            doesTheServiceDetailsPageContentContain('[E2E] Test Summary'),
            doesTheServiceDetailsPageContentContain('Location'),
            doesTheServiceDetailsPageContentContain('More details'),
            doesTheServiceDetailsPageContentContain('Contact details')
        );
    });
});