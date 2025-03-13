import {Answerable, Task} from '@serenity-js/core';
import {Click, Enter} from '@serenity-js/web';
import {
    postcodeSearchBox,
    postcodeSearchButton,
} from '../find-index';

export const enterPostcode = (postcodeInputValue: Answerable<string>): Task =>
    Task.where(
        `#actor enters a postcode ${postcodeInputValue} and searches for LA services within that area`,
        Enter.theValue(postcodeInputValue).into(postcodeSearchBox()),
    );

export const clickOnPostcodeSearchButton = (): Task =>
    Task.where(
        `#actor clicks on the search button on the enter postcode Page`,
        Click.on(postcodeSearchButton()),
    );