import { By, PageElement, } from '@serenity-js/web';

export const agreeButton = () =>
    PageElement
        .located(By.css("#main-content > div > div > a"))
        .describedAs('Start Now Button');