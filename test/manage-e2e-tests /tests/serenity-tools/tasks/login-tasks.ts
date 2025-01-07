import {Answerable, Check, Task} from '@serenity-js/core';
import {Navigate, Click, Enter, isVisible} from '@serenity-js/web';
import {startButton, continueButton, emailField, passwordField, signInButton} from "../page-objects/gov-login-page-objects";
import {acceptCookiesButton, agreeButton} from "../page-objects/manage-page-objects";

export const loginToTestEnvironment = (): Task =>
    Task.where( `#actor logs into test environment with ${process.env.USER_NAME} and ${process.env.USER_NAME}`,
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
        `#actor logs into manage with a gov login email and password`,
        Click.on(signInButton()),
        Enter.theValue(process.env.DFE_ADMIN_USER).into(emailField()),
        Click.on(continueButton()),
        Enter.theValue(process.env.GOV_LOGIN_PASSWORD).into(passwordField()),
        Click.on(continueButton()),
    );

export const acceptManageTermsAndConditions = (): Task =>
    Task.where(
        `#actor accepts terms and conditions`,
        Check.whether(agreeButton(), isVisible())
            .andIfSo(
                Click.on(agreeButton()),
            )
    );

export const acceptCookies = (): Task =>
    Task.where(
        `#actor accepts cookies`,
        Check.whether(acceptCookiesButton(), isVisible())
            .andIfSo(
                Click.on(acceptCookiesButton()),
            )
    );

