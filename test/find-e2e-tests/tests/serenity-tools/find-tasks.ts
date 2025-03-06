import {Answerable, Task } from '@serenity-js/core';
import {Navigate, Click, Enter, Page} from '@serenity-js/web';
import * as element from './find-page-objects';

export const navigateToFind = (): Task =>
    Task.where(
        `#actor navigates to the Find Website`,
        Navigate.to('/'),
    );

export const clickOnTheStartButton = (): Task =>
    Task.where(
        `#actor clicks on the start button on the Find Landing Page`,
        Click.on(element.startButton()),
    );

export const enterPostcode = (postcodeInputValue: Answerable<string>): Task =>
    Task.where(
        `#actor enters a postcode ${postcodeInputValue} and searches for LA services within that area`,
        Enter.theValue(postcodeInputValue).into(element.postcodeSearchBox()),
    );

export const clickOnPostcodeSearchButton = (): Task =>
    Task.where(
        `#actor clicks on the search button on the enter postcode Page`,
        Click.on(element.postcodeSearchButton()),
    );

/**
 * Family Support
 *  - Debt and Welfare Advice
 *  - Money, Benefits and Housing
 *  
 * Only show free services
 * 
 * 1 mile
 */
export const applyFirstRepresentativeSetOfFilters = (): Task => 
    Task.where("#actor applies a selection of filters",
        Click.on(element.filterCategoryFamilySupportAccordion()),
        Click.on(element.filterSubCategoryDebtAndWelfareAdviceCheckbox()),
        Click.on(element.filterSubCategoryMoneyBenefitsAndHousingCheckbox()),
        Click.on(element.filterOnlyShowFreeServicesCheckbox()),
        Click.on(element.filterSearchWithinRadioButton(5)),
        Click.on(element.filterApplyFiltersButton())
    );

export const applySecondRepresentativeSetOfFilters = (): Task => 
    Task.where("#actor applies a selection of filters",
        Click.on(element.filterDayAvailable("MO")),
        Click.on(element.filterDayAvailable("TU")),
        Click.on(element.filterDayAvailable("WE")),
        Click.on(element.filterDayAvailable("TH")),
        Click.on(element.filterDayAvailable("FR")),
        Click.on(element.filterAgeRange("3")),
        Click.on(element.filterAgeRange("4")),
        Click.on(element.filterLanguageList()),
        Click.on(element.filterLanguage("English"))
    );
