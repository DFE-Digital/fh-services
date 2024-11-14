import { Answerable, Task, Interaction } from '@serenity-js/core';
import { Navigate, Click, Enter, isVisible } from '@serenity-js/web';
import { startButton, signInButton, passwordText, emailText, continueButton } from './connect-page-objects';

// let page:Page;

export const navigateToConnect = (): Task =>
    Task.where(
        `#actor navigates to the Find Website`,
        Navigate.to('/'),
        
    );


export const loginAsUser = () =>
    Task.where(`#actor logs in with email and password`,
        Click.on(startButton()),
        Click.on(signInButton()),
        enterUsername('familyhubs.lapro@education.gov.uk'),
        Click.on(continueButton()),
        enterPassword('43@$mellyC4t3'),
        Click.on(continueButton()),
    );




export const navigateToLoggedInConnect = (): Task =>
    Task.where(
        `#actor navigates to the Find Website`,
        Navigate.to('/referrals/La/Dashboard'),
    );

export const enterUsername = (usernameInputValue: Answerable<string>): Task =>
    Task.where(
        `#actor enters a postcode ${usernameInputValue} and searches for LA services within that area`,
        Enter.theValue(usernameInputValue).into(emailText()),
    );

export const enterPassword = (usernamePasswordValue: Answerable<string>): Task =>
    Task.where(
        `#actor enters a postcode ${usernamePasswordValue} and searches for LA services within that area`,
        Enter.theValue(usernamePasswordValue).into(passwordText()),
    );

export const clickOnTheStartButton = (): Task =>
    Task.where(
        `#actor clicks on the start button on the Find Landing Page`,
        Click.on(startButton()),
    );

export const clickOnTheContinueButton = (): Task =>
    Task.where(
        `#actor clicks on the start button on the Find Landing Page`,
        Click.on(continueButton()),
    );
