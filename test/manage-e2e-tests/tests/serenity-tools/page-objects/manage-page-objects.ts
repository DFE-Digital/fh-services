import { By, PageElement, } from '@serenity-js/web';

export const agreeButton = () =>
    PageElement
        .located(By.css("[data-testid='continue-button']"))
        .describedAs('Agree T&Cs button');

export const acceptCookiesButton = () =>
    PageElement
        .located(By.css("[value='accept']"))
        .describedAs('Accept cookies button');