import {describe, it} from '@serenity-js/playwright-test';

import {
    navigateToManage,
    clickOnTheStartButton,
    loginToManage,
    isTheManageHomepageDisplayed
} from './serenity-tools/manage-index';

describe('Manage Tests', () => {
    it('should check a DfE Admin User is able to log into Manage', async ({ actorCalled }) => {
        await actorCalled('DFE_ADMIN_USER').attemptsTo(
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage('DFE_ADMIN_USER'),
            isTheManageHomepageDisplayed()
        );
    });
});