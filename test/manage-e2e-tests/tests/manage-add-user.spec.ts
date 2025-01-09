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
    loginToTestEnvironment,
    navigateToManage,
    searchForUserByName,
    selectLocalAuthority,
    selectPermissionType,
    selectUserAction
} from './serenity-tools/manage-index';

describe('Add a User - Manage Tests', () => {

    test.use({
        defaultActorName: 'DFE_ADMIN_user',
    })

    test.beforeEach('Setup', async ({actor}) => {
        await actor.attemptsTo(
            loginToTestEnvironment(),
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
            selectUserAction('add services'),
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
});