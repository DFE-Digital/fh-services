import {By, PageElement} from '@serenity-js/web';

export const laServiceInformation = () =>
    PageElement
        .located(By.css("[data-testid='service-area-[e2e]testlaserviceone']"))       
        .describedAs('Test LA Service One Detail Area');


export const laServiceLink = () =>
    PageElement
        .located(By.css("[data-testid='[e2e]testlaserviceone']"))
        .describedAs('the Test LA Service One link');


export const vcfsServiceInformation = () =>
    PageElement
        .located(By.css("[data-testid='service-area-[e2e]testvcfsserviceone']"))   
        .describedAs('Test VCFS Service One Detail Area');


export const vcfsServiceLink = () =>
    PageElement
        .located(By.css("[data-testid='[e2e]testvcfsserviceone']"))
        .describedAs('the Test VCFS Service One link');

