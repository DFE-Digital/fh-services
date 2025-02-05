import {Ensure, includes} from "@serenity-js/assertions";
import {Page} from "@serenity-js/web";

export const isManageLandingPageDisplayed = () => (
        Ensure.that(Page.current().url().toString(), includes(process.env.BASE_URL + 'Welcome'))
    )  