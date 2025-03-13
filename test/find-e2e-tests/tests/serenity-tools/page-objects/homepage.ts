import {By, PageElement} from '@serenity-js/web';

export const startButton = () =>
    PageElement
        .located(By.css("[data-testid='start-button']"))
        .describedAs('start Now Button');