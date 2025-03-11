import {Page, Text} from '@serenity-js/web';
import {Ensure, includes} from '@serenity-js/assertions';
import {Answerable} from "@serenity-js/core";
import {laServiceInformation, vcfsServiceInformation} from '../find-index';

export const isSearchResultsPageDisplayed = (outwardCode: Answerable<string>, inwardCode: Answerable<string>) =>
    Ensure.that(Page.current().url().toString(), includes(process.env.BASE_URL + `ProfessionalReferral/LocalOfferResults?postcode=${outwardCode}}%20${inwardCode}&currentPage=1`)
    );

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