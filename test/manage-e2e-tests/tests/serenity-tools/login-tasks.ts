import { Answerable, Task } from '@serenity-js/core';
import { Navigate, Click, Enter } from '@serenity-js/web';
import {startButton, continueButton, emailField, passwordField, signInButton} from "./gov-login-page-objects";

export const loginToTestEnvironment = (): Task =>
    Task.where( `#actor logs into test environment`,
        Navigate.to(`https://${process.env.USER_NAME}:${process.env.PASSWORD}@signin.integration.account.gov.uk/?prompt=login`)
    );
export const navigateToManage= (): Task =>
    Task.where(
        `#actor navigates to the Manage Website`,
        Navigate.to(process.env.BASE_URL),
    );
export const clickOnTheStartButton = (): Task =>
    Task.where(
        `#actor clicks on the start button on the Manage Landing Page`,
        Click.on(startButton()),
    );
export const loginToManage = (userType: Answerable<string>): Task =>
    Task.where(
<<<<<<< HEAD
        `#actor logs into manage with a gov login email ${userType}`,
=======
        `#actor logs into manage with a gov login email and password`,
>>>>>>> 6e483acf9e926f07d2d1247282b11f218f3e52c2
    Task.where( `#actor logs into test environment`,
        Click.on(signInButton()),
        Enter.theValue(process.env.DFE_ADMIN_USER).into(emailField()),
        Click.on(continueButton()),
        Enter.theValue(process.env.GOV_LOGIN_PASSWORD).into(passwordField()),
        Click.on(continueButton()),
    );

