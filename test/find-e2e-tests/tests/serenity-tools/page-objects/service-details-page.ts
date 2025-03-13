import {By, PageElement} from '@serenity-js/web';

export const serviceDetailsPage = () =>
    PageElement
        .located(By.id("main-content"))
        .describedAs('Service Details Page');

