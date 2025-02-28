import {Page, Text} from '@serenity-js/web';
import {Ensure, equals, includes} from '@serenity-js/assertions';
import {Answerable} from "@serenity-js/core";
import {laServiceInformation, serviceDetailsPage, vcfsServiceInformation} from './find-page-objects';

//This is for illustrative purposes and will be removed when appropriate questions can be created. 

export const isTheFindPageDisplayed = () =>
    Ensure.that(
        Page.current().title().describedAs('Find Page Title'),
        equals('Find support for your family - Find support for your family - GOV.UK'),
    )

export const verifyLAServiceInformationInTheListOfServicesPageContains = (categoryName: Answerable<string>) =>
    Ensure.that(
        Text.of(laServiceInformation()),
        includes(categoryName)
    );


export const verifyVCFSServiceInformationInTheListOfServicesPageContains = (categoryName: Answerable<string>) =>
    Ensure.that(
        Text.of(vcfsServiceInformation()),
        includes(categoryName)
    );

export const verifyTheServiceDetailsPageContent = (categoryName: Answerable<string>) =>
    Ensure.that(
        Text.of(serviceDetailsPage()),
        includes(categoryName)
    );     