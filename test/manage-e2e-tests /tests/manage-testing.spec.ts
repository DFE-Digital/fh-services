import {describe, it} from '@serenity-js/playwright-test';

import {
    navigateToManage,
    clickOnTheStartButton,
    loginToManage,
    loginToTestEnvironment, isTheManageHomepageDisplayed
} from './serenity-tools/manage-index';
import {isMap} from "node:util/types";

describe('Manage Tests', () => {

    it('should check a DfE Admin User is to log into Manage', async ({ actorCalled }) => {
        await actorCalled('DFE_ADMIN_USER').attemptsTo(
            loginToTestEnvironment(),
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage('DFE_ADMIN_USER'),
            isTheManageHomepageDisplayed()
        );
    });
});