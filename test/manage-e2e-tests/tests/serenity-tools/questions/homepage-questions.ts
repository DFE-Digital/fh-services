import {Page} from '@serenity-js/web';
import {Ensure, equals} from '@serenity-js/assertions';
import {acceptCookiesButton} from "../page-objects/manage-page-objects";

export const isTheAgreeTermsConditionsPageDisplayed = () =>
    Ensure.that(
        Page.current().title().describedAs('Manage Homepage'),
        equals('Agree to our terms of use - Manage family support services and accounts - GOV.UK')
    )

export const isTheAgreeButtonDisplayed = () =>
    Ensure.that(
        acceptCookiesButton().isVisible(), equals(true)
    )

export const isTheManageHomepageDisplayed = () =>
    Ensure.that(
        Page.current().title().describedAs('Manage Homepage'),
        equals('Dfe Admin - Manage family support services and accounts - GOV.UK')
    )  
