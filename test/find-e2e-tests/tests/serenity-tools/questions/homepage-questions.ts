import {Page} from '@serenity-js/web';
import {Ensure, includes} from '@serenity-js/assertions';

export const isTheFindPageDisplayed = () =>
    Ensure.that(Page.current().url().toString(), includes(process.env.BASE_URL));