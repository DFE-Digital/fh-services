import {Page, Text} from '@serenity-js/web';
import {Ensure, includes} from '@serenity-js/assertions';
import {Answerable} from "@serenity-js/core";
import {laServiceInformation, serviceDetailsPage, vcfsServiceInformation} from './find-page-objects';

export const isTheFindPageDisplayed = () =>
    Ensure.that(Page.current().url().toString(), includes(process.env.BASE_URL));

export const doesTheLAServiceInformationInTheListOfServicesPageContain = (serviceDetailHeader: Answerable<string>) =>
    Ensure.that(
        Text.of(laServiceInformation()),
        includes(serviceDetailHeader)
    );

export const doesTheVCFSServiceInformationInTheListOfServicesPageContains = (serviceDetailHeader: Answerable<string>) =>
    Ensure.that(
        Text.of(vcfsServiceInformation()),
        includes(serviceDetailHeader)
    );

export const doesTheServiceDetailsPageContentContain = (categoryName: Answerable<string>) =>
    Ensure.that(
        Text.of(serviceDetailsPage()),
        includes(categoryName)
    );     