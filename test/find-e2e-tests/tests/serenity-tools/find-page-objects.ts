import {By, PageElement, Text} from '@serenity-js/web';

export const startButton = () =>
    PageElement
        .located(By.css(".govuk-button.govuk-button--start"))
        .describedAs('start Now Button');

export const postcodeSearchBox = () =>
    PageElement
        .located(By.id("postcode"))
        .describedAs('the Postcode searchbox');


export const postcodeSearchButton = () =>
    PageElement
        .located(By.css("button[type='submit']"))
        .describedAs('the search button');


export const laServiceInformation = () =>
    PageElement
        .located(By.css("body > div:nth-child(6) > main:nth-child(3) > form:nth-child(1) > div:nth-child(4) > div:nth-child(2) > ul:nth-child(1) > li:nth-child(1)"))
        .describedAs('Test LA Service One Detail Area');


export const laServiceLink = () =>
    PageElement
        .located(By.css(".govuk-link[data-testid='[e2e]testlaserviceone']"))
        .describedAs('the Test LA Service One link');


export const vcfsServiceInformation = () =>
    PageElement
        .located(By.css("body > div:nth-child(6) > main:nth-child(3) > form:nth-child(1) > div:nth-child(4) > div:nth-child(2) > ul:nth-child(1) > li:nth-child(7)"))
        .describedAs('Test VCFS Service One Detail Area');


export const vcfsServiceLink = () =>
    PageElement
        .located(By.css(".govuk-link[data-testid='[e2e]testvcfsserviceone']"))
        .describedAs('the Test VCFS Service One link');


export const serviceDetailsPage = () =>
    PageElement
        .located(By.css("main[id='main-content'] div[class='govuk-grid-column-two-thirds']"))
        .describedAs('Service Details Page');

