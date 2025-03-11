import {By, PageElement} from '@serenity-js/web';

export const postcodeSearchBox = () =>
    PageElement
        .located(By.id("postcode"))
        .describedAs('the Postcode searchbox');

export const postcodeSearchButton = () =>
    PageElement
        .located(By.css("[data-testid='button-search']"))
        .describedAs('the search button');
