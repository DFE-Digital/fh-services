import { Page } from '@serenity-js/web';
import { Ensure, equals } from '@serenity-js/assertions';

//This is for illustrative purposes and will be removed when appropriate questions can be created. 

export const isTheManageHomepageDisplayed = () =>
    Ensure.that(
        Page.current().title().describedAs('Manage Agree T&Cs'),
        equals('Agree to our terms of use - Manage family support services and accounts - GOV.UK'),
    )  
