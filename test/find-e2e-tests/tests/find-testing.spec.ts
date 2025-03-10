import {describe, it} from '@serenity-js/playwright-test';
import {
    navigateToFind,
    clickOnTheStartButton,
    enterPostcode,
    clickOnPostcodeSearchButton,
    doesTheLAServiceInformationInTheListOfServicesPageContain,
    doesTheVCFSServiceInformationInTheListOfServicesPageContains,
    clickOnTheLaService,
    doesTheServiceDetailsPageContentContain, clickOnTheVcfsService
} from './serenity-tools/find-index';
import {GENERAL_PUBLIC_USER} from './actors-user-roles';

describe('Single Directory Tests', () => {

    it(`should check a ${GENERAL_PUBLIC_USER} is able to search for services`, async ({actorCalled}) => {
        await actorCalled(GENERAL_PUBLIC_USER).attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode('W1D 2JT'),
            clickOnPostcodeSearchButton(),
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able verify LA Service information`, async ({actorCalled}) => {
        await actorCalled(GENERAL_PUBLIC_USER).attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode('W1D 2JT'),
            clickOnPostcodeSearchButton(),
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

    it(`should check a ${GENERAL_PUBLIC_USER} is able verify VCFS Service information`, async ({actorCalled}) => {
        await actorCalled(GENERAL_PUBLIC_USER).attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode('W1D 2JT'),
            clickOnPostcodeSearchButton(),
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