import { Page } from '@serenity-js/web';
import {Ensure, equals, includes} from '@serenity-js/assertions';

//This is for illustrative purposes and will be removed when appropriate questions can be created. 

export const isTheFindPageDisplayed = () =>
    Ensure.that(Page.current().url().toString(), includes(process.env.BASE_URL));
