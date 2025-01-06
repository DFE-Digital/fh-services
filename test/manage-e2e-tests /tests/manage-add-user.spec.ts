import {describe, it, test} from '@serenity-js/playwright-test';

import {
    navigateToManage,
    clickOnTheStartButton,
    loginToManage,
    loginToTestEnvironment, isTheManageHomepageDisplayed
} from './serenity-tools/manage-index';


describe('Manage Tests', () => {

    test.use({
        defaultActorName: 'DFE_ADMIN_user',
    })

    test.beforeAll('Setup', async ({ actor }) => {
        await actor.attemptsTo(
            loginToTestEnvironment(),
            navigateToManage(),
            clickOnTheStartButton(),
            loginToManage('DFE_ADMIN_USER'),
            isTheManageHomepageDisplayed());
    });
    
    it('should check a DfE Admin User can create LA manager user', async ({ actor }) => {
        
    });
});