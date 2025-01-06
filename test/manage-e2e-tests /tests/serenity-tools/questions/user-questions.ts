import {Ensure, equals} from "@serenity-js/assertions";
import {Page} from "@serenity-js/web";

export const isUserCreatedPageDisplayed = () =>
    Ensure.that(
        Page.current().title().describedAs('User created page'),
        equals(''),
    )

export const isUserFoundInUserList = () =>
    Ensure.that(
        Page.current().title().describedAs('Manage Homepage'),
        equals('Dfe Admin - Manage family support services and accounts - GOV.UK'),
    ) 