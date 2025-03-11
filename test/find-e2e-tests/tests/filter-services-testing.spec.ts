/*
import {describe, it, test} from '@serenity-js/playwright-test';
import {
    navigateToFind,
    clickOnTheStartButton,
    enterPostcode,
    clickOnPostcodeSearchButton
} from './serenity-tools/find-index';
import {GENERAL_PUBLIC_USER} from './actors-user-roles';
export const POSTCODE: string = "W1D 2JT"

describe('Single Directory Tests', () => {

    test.use({
        defaultActorName: `${GENERAL_PUBLIC_USER}`
    })

    test.beforeEach('Setup', async ({actor}) => {
        await actor.attemptsTo(
            navigateToFind(),
            clickOnTheStartButton(),
            enterPostcode(POSTCODE),
            clickOnPostcodeSearchButton()
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able to filter on search results`, async ({actor}) => {
        await actor.attemptsTo(
            applyAFilter(),
            isFilterApplied(),
            doesEachSearchResultContainFilterOption()
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able to clear filters`, async ({actorCalled}) => {
        await actorCalled(GENERAL_PUBLIC_USER).attemptsTo(
            applyAFilter(),
            clearAFilter(),
            areTheFilterCleared(),
            applyFilter(),
            applyFilter(),
            clearAllFilters(),
            areTheFilterCleared(),
            areAllSearchResultsReturned()
        );
    });

    it(`should check a ${GENERAL_PUBLIC_USER} is able to use pagination on search results page`, async ({actorCalled}) => {
        await actorCalled(GENERAL_PUBLIC_USER).attemptsTo(
            isPaginationDisplayed(),
            clickNextPage(),
            isUserOnExpectedPageOfResults('2'),
            clickPaginationNumber('3'),
            isUserOnExpectedPageOfResults('3'),
            clickPreviousPage(),
            isUserOnExpectedPageOfResults('2')
        );
    });
});
*/
