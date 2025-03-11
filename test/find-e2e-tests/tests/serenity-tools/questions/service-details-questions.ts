import {Text} from '@serenity-js/web';
import {Ensure, includes} from '@serenity-js/assertions';
import {Answerable} from "@serenity-js/core";
import {serviceDetailsPage} from '../find-index';

export const doesTheServiceDetailsPageContentContain = (categoryName: Answerable<string>) =>
    Ensure.that(
        Text.of(serviceDetailsPage()),
        includes(categoryName)
    );     