import {describe, it} from '@serenity-js/playwright-test';
import {
    acceptCookies,
    acceptManageTermsAndConditions,
    clickOnTheStartButton,
    isTheManageHomepageDisplayed,
    loginToManage,
<<<<<<<< HEAD:test/manage-e2e-tests/tests/manage-testing.spec.ts
    loginToTestEnvironment, 
    isTheManageHomepageDisplayed
========
    loginToTestEnvironment,
    navigateToManage,
>>>>>>>> 4c17d897747570a75640c867245d6ce74652859d:test/manage-e2e-tests/tests/manage-login.spec.ts
} from './serenity-tools/manage-index';

describe('Manage Tests', () => {

    it('should check a DfE Admin User is able to log into Manage', async ({actorCalled}) => {
        await actorCalled('DFE_ADMIN_USER').attemptsTo(
            loginToTestEnvironment(),
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage(),
            acceptManageTermsAndConditions(),
            acceptCookies(),
            isTheManageHomepageDisplayed());
    });
});