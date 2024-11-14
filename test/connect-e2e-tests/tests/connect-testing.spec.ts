import { describe, it } from '@serenity-js/playwright-test';

import { navigateToConnect, clickOnTheStartButton,navigateToLoggedInConnect,loginAsUser  } from './serenity-tools/connect-index';

describe('Find Tests', () => {

    it('should check a DfE Find User is able to search for services ', async ({ actorCalled }) => {
        await actorCalled('DfE_Find_User').attemptsTo(
            navigateToConnect(),
            loginAsUser(),
        );
    });
    

    // it('should check referrals ', async ({ actorCalled }) => {
    //     await actorCalled('DfE_Find_User').attemptsTo(
    //         navigateToLoggedInConnect(),
    //         Wait.for(Duration.ofSeconds(5)), 
    //     );
    // });
});

