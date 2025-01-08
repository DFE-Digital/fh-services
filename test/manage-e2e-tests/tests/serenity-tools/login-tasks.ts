import {Answerable, Masked, Task} from '@serenity-js/core';
import { Navigate, Click, Enter } from '@serenity-js/web';
import {startButton, continueButton, emailField, passwordField, signInButton} from "./gov-login-page-objects";

export const loginToTestEnvironment = (): Task =>
    Task.where( `#actor logs into test environment`,
        Navigate.to(`https://${Masked.valueOf(process.env.USER_NAME)}:${Masked.valueOf(process.env.PASSWORD)}@signin.integration.account.gov.uk/?prompt=login`)
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
        `#actor logs into manage with a gov login email and password`,
        Click.on(signInButton()),
        Enter.theValue(Masked.valueOf(process.env.DFE_ADMIN_USER)).into(emailField()),
        Click.on(continueButton()),
        Enter.theValue(Masked.valueOf(process.env.GOV_LOGIN_PASSWORD)).into(passwordField()),
        Click.on(continueButton()),
    );
