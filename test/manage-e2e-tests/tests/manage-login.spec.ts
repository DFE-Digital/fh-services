import {describe, it} from '@serenity-js/playwright-test';

import {
    navigateToManage,
    clickOnTheStartButton,
    loginToManage,
<<<<<<<< HEAD:test/manage-e2e-tests/tests/manage-login.spec.ts
    loginToTestEnvironment, isTheManageHomepageDisplayed, acceptManageTermsAndConditions
========
    loginToTestEnvironment, 
    isTheManageHomepageDisplayed
>>>>>>>> 6dae1e0bcb035755a719facdac7a199fff84040a:test/manage-e2e-tests/tests/manage-testing.spec.ts
} from './serenity-tools/manage-index';

describe('Manage Tests', () => {

    it('should check a DfE Admin User is able to log into Manage', async ({ actorCalled }) => {
        await actorCalled('DFE_ADMIN_USER').attemptsTo(
            loginToTestEnvironment(),
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage('DFE_ADMIN_USER'),
            acceptManageTermsAndConditions(),
            isTheManageHomepageDisplayed()
        );
    });
});