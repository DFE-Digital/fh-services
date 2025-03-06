import {beforeEach, describe, it} from '@serenity-js/playwright-test';

import {
    navigateToFind,
    clickOnTheStartButton,
    enterPostcode,
    clickOnPostcodeSearchButton,
    applyFirstRepresentativeSetOfFilters, applySecondRepresentativeSetOfFilters,
} from './serenity-tools/find-index';
import {By, isVisible, PageElement, Scroll} from "@serenity-js/web";
import {Ensure, isPresent} from "@serenity-js/assertions";
describe('Find Tests', () => {
    /**
     * TODO:
     *  - AC.1   - Apply Filters
     *  - AC.2.1 - Clearing Individual Filter
     *  - AC.2.2 - Clearing all filters
     *  - AC.3   - Services with multiple locations are displayed (should be in another ticket)
     *  - AC.4   - Pagination (should also probably be in a different ticket)
     */

    beforeEach("Should navigate to the search results page", async ({ actorCalled }) => {
        await actorCalled('DfE_Find_User')
            .attemptsTo(
                navigateToFind(),
                clickOnTheStartButton(),
                enterPostcode('SW1A 0RS'),
                clickOnPostcodeSearchButton(),
            );
    });
    
    // it("Should apply the first representative set of filters", async ({ actorCalled }) => {
    //     const firstService = PageElement.located(By.css("[data-testid='[e2e]testlaserviceone']"));
    //     const secondService = PageElement.located(By.css("[data-testid='[e2e]testvcfsserviceone']"));
    //     const thirdService = PageElement.located(By.css("[data-testid='[e2e]testvcfsservicetwo']"));
    //     const fourthService = PageElement.located(By.css("[data-testid='[e2e]testvcfsservicefour']"));
    //     await actorCalled('DfE_Find_User')
    //         .attemptsTo(
    //             applyFirstRepresentativeSetOfFilters(),
    //             Scroll.to(firstService),
    //             Ensure.that(firstService, isVisible()),
    //             Scroll.to(secondService),
    //             Ensure.that(secondService, isVisible()),
    //             Scroll.to(thirdService),
    //             Ensure.that(thirdService, isVisible()),
    //             Scroll.to(fourthService),
    //             Ensure.that(fourthService, isVisible()),
    //         );
    // });
    
    it("Should apply the second representative set of filters", async ({ actorCalled }) => {
        await actorCalled('DfE_Find_User')
            .attemptsTo(
              applySecondRepresentativeSetOfFilters(),
            );
    });
});