import {describe, it, test} from '@serenity-js/playwright-test';

import {
    navigateToManage,
    clickOnTheStartButton,
    loginToManage,
    loginToTestEnvironment,
    acceptManageTermsAndConditions,
    acceptCookies,
    isTheManageHomepageDisplayed,
    clickAddUserLink,
    clickConfirmDetails,
    enterFullName,
    enterTestEmail,
    searchForUserByName,
    selectLocalAuthority,
    selectPermissionType,
    selectUserAction,
    isUserCreatedPageDisplayed,
    isUserFoundInUserList,
    getRandomEmail,
    getRandomFullName, clickContinue
} from './serenity-tools/manage-index';

describe('Manage Tests', () => {

    test.use({
        defaultActorName: 'DFE_ADMIN_user',
    })

    test.beforeEach('Setup', async ({ actor }) => {
        await actor.attemptsTo(
            loginToTestEnvironment(),
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage('DFE_ADMIN_USER'),
            acceptManageTermsAndConditions(),
            acceptCookies(),
            isTheManageHomepageDisplayed());
        
    });
    
    it('should check a DfE Admin User can create LA manager user', async ({ actor }) => {
        const emailAddress = getRandomEmail();
        const fullName = getRandomFullName();
        
        await actor.attemptsTo(
            clickAddUserLink(),
            selectPermissionType('la'),
            clickContinue(),
            selectUserAction('add services'),
            clickContinue(),
            selectLocalAuthority('Tower Hamlets'),
            clickContinue(),
            enterTestEmail(emailAddress),
            clickContinue(),
            enterFullName(fullName),
            clickContinue(),
            clickConfirmDetails(),
            isUserCreatedPageDisplayed(),
            searchForUserByName(fullName),
            isUserFoundInUserList());
    });
});