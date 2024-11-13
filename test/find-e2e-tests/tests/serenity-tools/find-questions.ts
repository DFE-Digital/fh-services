import { Page } from '@serenity-js/web';
import { Ensure, equals } from '@serenity-js/assertions';

//This is for illustrative purposes and will be removed when appropriate questions can be created. 

export const isTheFindPageDisplayed = () =>
    Ensure.that(
        Page.current().title().describedAs('Find Page Title'),
        equals('Find support for your family - Find support for your family - GOV.UK'),
    )  
