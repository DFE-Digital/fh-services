import {describe, it} from '@serenity-js/playwright-test';
import {
    acceptCookies,
    acceptManageTermsAndConditions,
    clickOnTheStartButton,
    isTheManageHomepageDisplayed,
    loginToManage,
    navigateToManage
} from './serenity-tools/manage-index';

describe('Manage Tests', () => {

    it('should check a DfE Admin User is able to log into Manage', async ({actorCalled}) => {
        await actorCalled('DFE_ADMIN_USER').attemptsTo(
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage(),
            acceptManageTermsAndConditions(),
            acceptCookies(),
            isTheManageHomepageDisplayed());
    });
});