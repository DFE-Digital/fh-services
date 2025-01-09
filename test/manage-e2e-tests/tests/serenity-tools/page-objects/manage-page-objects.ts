import {By, PageElement,} from '@serenity-js/web';

export const agreeButton = () =>
    PageElement
        .located(By.css("[data-testid='continue-button']"))
        .describedAs('Agree T&Cs button');

export const acceptCookiesButton = () =>
    PageElement
        .located(By.css("body > div.govuk-cookie-banner > div.govuk-cookie-banner__message.govuk-width-container.js-cookie-banner-message > div.govuk-button-group > button.govuk-button.js-cookie-banner-accept"))
        .describedAs('Accept cookies button');

export const homeButton = () =>
    PageElement
        .located(By.css("body > header > div > div.dfe-header__logo > a > img.dfe-logo"))
        .describedAs('Home button');