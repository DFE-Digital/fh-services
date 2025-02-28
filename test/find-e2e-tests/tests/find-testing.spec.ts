import {describe, it} from '@serenity-js/playwright-test';
import {
    navigateToFind,
    clickOnTheStartButton,
    enterPostcode,
    clickOnPostcodeSearchButton,
    verifyLAServiceInformationInTheListOfServicesPageContains,
    verifyVCFSServiceInformationInTheListOfServicesPageContains,
    clickOnTheLaService,
    verifyTheServiceDetailsPageContent, clickOnTheVcfsService
} from './serenity-tools/find-index';

describe('Find Tests', () => {

    it('should check a DfE Find User is able to search for services ', async ({actorCalled}) => {
        await actorCalled('DfE_Find_User').attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode('W1D 2JT'),
            clickOnPostcodeSearchButton(),
        );
    });


    it('Verify LA Service information', async ({actorCalled}) => {
        await actorCalled('DfE_Find_User').attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode('W1D 2JT'),
            clickOnPostcodeSearchButton(),
            verifyLAServiceInformationInTheListOfServicesPageContains('Test LA'),
            verifyLAServiceInformationInTheListOfServicesPageContains('Category'),
            verifyLAServiceInformationInTheListOfServicesPageContains('Age range'),
            verifyLAServiceInformationInTheListOfServicesPageContains('Where'),
            verifyLAServiceInformationInTheListOfServicesPageContains('Cost'),
            clickOnTheLaService(),
            verifyTheServiceDetailsPageContent('Service details'),
            verifyTheServiceDetailsPageContent('Location'),
            verifyTheServiceDetailsPageContent('More details'),
            verifyTheServiceDetailsPageContent('Contact details')
        );
    });

    it('Verify VCFS Service information', async ({actorCalled}) => {
        await actorCalled('DfE_Find_User').attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode(' W1D 2JT'),
            clickOnPostcodeSearchButton(),
            verifyVCFSServiceInformationInTheListOfServicesPageContains('Test Organisation'),
            verifyVCFSServiceInformationInTheListOfServicesPageContains('Category'),
            verifyVCFSServiceInformationInTheListOfServicesPageContains('Age range'),
            verifyVCFSServiceInformationInTheListOfServicesPageContains('Where'),
            verifyVCFSServiceInformationInTheListOfServicesPageContains('Cost'),
            clickOnTheVcfsService(),
            verifyTheServiceDetailsPageContent('Service details'),
            verifyTheServiceDetailsPageContent('Location'),
            verifyTheServiceDetailsPageContent('More details'),
            verifyTheServiceDetailsPageContent('Contact details')
        );
    });
});