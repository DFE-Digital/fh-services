import {Check, Masked, Task} from '@serenity-js/core';
import {Click, Enter, Navigate} from '@serenity-js/web';
import {
    continueButton,
    emailField,
    passwordField,
    signInButton,
    startButton
} from "../page-objects/gov-login-page-objects";
import {acceptCookiesButton, agreeButton} from "../page-objects/manage-page-objects";
import {equals} from "@serenity-js/assertions";
export const navigateToManage = (): Task =>
    Task.where(
        `#actor navigates to the Manage Website`,
        Navigate.to(process.env.BASE_URL)
    );
export const clickOnTheStartButton = (): Task =>
    Task.where(
        `#actor clicks on the start button on the Manage Landing Page`,
        Click.on(startButton())
    );

//TODO: Make this is a reusable method that takes userType and enters different email addresses from .env based on userType. 
export const loginToManage = (): Task =>
    Task.where(
        `#actor logs into manage with a gov login email and password`,
        Click.on(signInButton()),
        Enter.theValue(Masked.valueOf(process.env.DFE_ADMIN_USER)).into(emailField()),
        Click.on(continueButton()),
        Enter.theValue(Masked.valueOf(process.env.GOV_LOGIN_PASSWORD)).into(passwordField()),
        Click.on(continueButton())
    );

export const acceptCookies = (): Task =>
    Task.where(
        `#actor accepts cookies`,
        Check.whether(acceptCookiesButton().isVisible(), equals(true))
            .andIfSo(
                Click.on(acceptCookiesButton())
            )
    );

export const acceptManageTermsAndConditions = (): Task =>
    Task.where(
        `#actor accepts terms and conditions`,
        Check.whether(agreeButton().isVisible(), equals(true))
            .andIfSo(
                Click.on(agreeButton()),
            ),
    );
