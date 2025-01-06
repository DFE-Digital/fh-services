import { By, PageElement, } from '@serenity-js/web';

export const startButton = () =>
    PageElement
        .located(By.css("#main-content > div > div > a"))
        .describedAs('Start Now Button');

export const signInButton = () =>
    PageElement
        .located(By.css("[id='sign-in-button']"))
        .describedAs('Sign In Button');

export const emailField = () =>
    PageElement
        .located(By.css("[id='email']"))
        .describedAs('Email Field');

export const passwordField = () =>
    PageElement
        .located(By.css("[id='password']"))
        .describedAs('Password Field');

export const continueButton = () =>
    PageElement
        .located(By.css("form > .govuk-button"))
        .describedAs('Continue Button');
