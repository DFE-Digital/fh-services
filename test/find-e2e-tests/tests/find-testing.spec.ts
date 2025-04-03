import { describe, it } from '@serenity-js/playwright-test';

import { navigateToFind, clickOnTheStartButton, enterPostcode, clickOnPostcodeSearchButton } from './serenity-tools/find-index';

describe('Find Tests', () => {

    it('should check a DfE Find User is able to search for services ', async ({ actorCalled }) => {
        await actorCalled('DfE_Find_User').attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode('E1 2EN'),
            clickOnPostcodeSearchButton(),
        );
    });
});