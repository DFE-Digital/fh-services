import {Ensure, equals} from "@serenity-js/assertions";
import {Text, Page} from "@serenity-js/web";
import {Answerable} from "@serenity-js/core";
import {userNameInUserList} from "../page-objects/accounts-page-objects";

export const isUserCreatedPageDisplayed = () =>
    Ensure.that(
        Page.current().title().describedAs('User created page'),
        equals('Account Confirmed - Manage family support services and accounts - GOV.UK'),
    )

export const isUserFoundInUserList = (fullName: Answerable<string>) =>
    Ensure.that(Text.of(userNameInUserList()), equals(fullName)
    ) 