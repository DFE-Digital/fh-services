import { By, PageElement, } from '@serenity-js/web';

export const startButton = () =>
    PageElement
        .located(By.xpath("//a[normalize-space()='Start now']"))
        .describedAs('start Now Button');

export const signInButton = () =>
    PageElement
        .located(By.id("sign-in-button"))
        .describedAs('sign-in-button');

        export const emailText = () =>
    PageElement
        .located(By.id("email"))
        .describedAs('username entry');

        export const passwordText = () =>
    PageElement
        .located(By.id("password"))
        .describedAs('password entry');

        export const continueButton = () =>
        PageElement
            .located(By.xpath("//button[normalize-space()='Continue']"))
            .describedAs('continue Button');
        