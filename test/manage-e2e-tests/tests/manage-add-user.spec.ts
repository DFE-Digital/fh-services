import {describe, it, test} from '@serenity-js/playwright-test';
import {
    acceptCookies,
    acceptManageTermsAndConditions,
    clickAddUserLink,
    clickConfirmDetails,
    clickContinue,
    clickOnTheStartButton,
    clickSecondContinue,
    enterFullName,
    enterTestEmail,
    getRandomEmail,
    getRandomFullName,
    isTheManageHomepageDisplayed,
    isUserCreatedPageDisplayed,
    isUserFoundInUserList,
    loginToManage,
    navigateToManage,
    searchForUserByName,
    selectLocalAuthority, selectOrganisation,
    selectPermissionType,
    selectUserAction
} from './serenity-tools/manage-index';

describe('Add a User - Manage Tests', () => {

    test.use({
        defaultActorName: 'DFE_ADMIN_user'
    })

    test.beforeEach('Setup', async ({actor}) => {
        await actor.attemptsTo(
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage(),
            acceptManageTermsAndConditions(),
            acceptCookies(),
            isTheManageHomepageDisplayed());
    });

    it('should check a DfE Admin User can create LA manager user', async ({actor}) => {
        const emailAddress = getRandomEmail();
        const fullName = getRandomFullName();

        await actor.attemptsTo(
            clickAddUserLink(),
            selectPermissionType('la'),
            clickContinue(),
            selectUserAction('la manager'),
            clickContinue(),
            selectLocalAuthority('Tower Hamlets'),
            clickSecondContinue(),
            enterTestEmail(emailAddress),
            clickSecondContinue(),
            enterFullName(fullName),
            clickSecondContinue(),
            clickConfirmDetails(),
            isUserCreatedPageDisplayed(),
            searchForUserByName(fullName),
            isUserFoundInUserList(fullName));
    });

    it('should check a DfE Admin User can create VCFS manager user', async ({actor}) => {
        const emailAddress = getRandomEmail();
        const fullName = getRandomFullName();

        await actor.attemptsTo(
            clickAddUserLink(),
            selectPermissionType('vcfs'),
            clickContinue(),
            selectUserAction('vcfs manager'),
            clickContinue(),
            selectLocalAuthority('Test LA'),
            clickSecondContinue(),
            selectOrganisation('Test Organisation'),
            clickSecondContinue(),
            enterTestEmail(emailAddress),
            clickSecondContinue(),
            enterFullName(fullName),
            clickSecondContinue(),
            clickConfirmDetails(),
            isUserCreatedPageDisplayed(),
            searchForUserByName(fullName),
            isUserFoundInUserList(fullName));
    });

    it('should check a DfE Admin User can create LA Practitioner user', async ({actor}) => {
        const emailAddress = getRandomEmail();
        const fullName = getRandomFullName();

        await actor.attemptsTo(
            clickAddUserLink(),
            selectPermissionType('la'),
            clickContinue(),
            selectUserAction('la practitioner'),
            clickContinue(),
            selectLocalAuthority('Test LA'),
            clickSecondContinue(),
            enterTestEmail(emailAddress),
            clickSecondContinue(),
            enterFullName(fullName),
            clickSecondContinue(),
            clickConfirmDetails(),
            isUserCreatedPageDisplayed(),
            searchForUserByName(fullName),
            isUserFoundInUserList(fullName));
    });

    it('should check a DfE Admin User can create VCFS Practitioner user', async ({actor}) => {
        const emailAddress = getRandomEmail();
        const fullName = getRandomFullName();

        await actor.attemptsTo(
            clickAddUserLink(),
            selectPermissionType('vcfs'),
            clickContinue(),
            selectUserAction('vcfs practitioner'),
            clickContinue(),
            selectLocalAuthority('Test LA'),
            clickSecondContinue(),
            selectOrganisation('Test Organisation'),
            clickSecondContinue(),
            enterTestEmail(emailAddress),
            clickSecondContinue(),
            enterFullName(fullName),
            clickSecondContinue(),
            clickConfirmDetails(),
            isUserCreatedPageDisplayed(),
            searchForUserByName(fullName),
            isUserFoundInUserList(fullName));
    });

    it('should check a DfE Admin User can create LA Dual user', async ({actor}) => {
        const emailAddress = getRandomEmail();
        const fullName = getRandomFullName();

        await actor.attemptsTo(
            clickAddUserLink(),
            selectPermissionType('la'),
            clickContinue(),
            selectUserAction('la manager'),
            selectUserAction('la practitioner'),
            clickContinue(),
            selectLocalAuthority('Test LA'),
            clickSecondContinue(),
            enterTestEmail(emailAddress),
            clickSecondContinue(),
            enterFullName(fullName),
            clickSecondContinue(),
            clickConfirmDetails(),
            isUserCreatedPageDisplayed(),
            searchForUserByName(fullName),
            isUserFoundInUserList(fullName));
    });

    it('should check a DfE Admin User can create VCFS Dual user', async ({actor}) => {
        const emailAddress = getRandomEmail();
        const fullName = getRandomFullName();

        await actor.attemptsTo(
            clickAddUserLink(),
            selectPermissionType('vcfs'),
            clickContinue(),
            selectUserAction('vcfs manager'),
            selectUserAction('vcfs practitioner'),
            clickContinue(),
            selectLocalAuthority('Test LA'),
            clickSecondContinue(),
            selectOrganisation('Test Organisation'),
            clickSecondContinue(),
            enterTestEmail(emailAddress),
            clickSecondContinue(),
            enterFullName(fullName),
            clickSecondContinue(),
            clickConfirmDetails(),
            isUserCreatedPageDisplayed(),
            searchForUserByName(fullName),
            isUserFoundInUserList(fullName));
    });
});