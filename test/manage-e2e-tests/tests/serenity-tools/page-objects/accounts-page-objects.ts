import {By, PageElement,} from '@serenity-js/web';

export const addUserLink = () =>
    PageElement
        .located(By.css("[id='add-permission']"))
        .describedAs('Add User Link');

export const manageUsersLink = () =>
    PageElement
        .located(By.css("#main-content > div > div > div:nth-child(5) > div:nth-child(2) > h3 > a"))
        .describedAs('Manage Users Link');

export const localAuthorityPermissions = () =>
    PageElement
        .located(By.css("[id='radio-LA']"))
        .describedAs('Radio Button for Local Authority Permissions');

export const addServicesUserActivity = () =>
    PageElement
        .located(By.css("[id='checkbox-LaManager']"))
        .describedAs('Checkbox for Add and Manage Services Activity');

export const laOrganisationInputBox = () =>
    PageElement
        .located(By.css("[id='LaOrganisationName']"))
        .describedAs('LA Organisation Input Box');

export const laOrganisationInputSuggestion = () =>
    PageElement
        .located(By.css("[id='LaOrganisationName__option--0']"))
        .describedAs('LA Organisation Input Box');

export const emailAddressInputBox = () =>
    PageElement
        .located(By.css("[id='emailAddress']"))
        .describedAs('Email Address Input Box');

export const fullNameInputBox = () =>
    PageElement
        .located(By.css("[id='fullName']"))
        .describedAs('Full Name Input Box');

export const continueButton = () =>
    PageElement
        .located(By.css("[data-testid='buttonContinue']"))
        .describedAs('Continue Button');

//TODO: Change the continue button on local authority page to have a data test id = buttonContinue and then delete this method.
export const secondContinueButton = () =>
    PageElement
        .located(By.css("[id='buttonContinue']"))
        .describedAs('Continue Button');

export const confirmDetailsButton = () =>
    PageElement
        .located(By.css("[data-testid='confirm-button']"))
        .describedAs('Confirm details button');

export const userNameFilterInputBox = () =>
    PageElement
        .located(By.css("[name='Name']"))
        .describedAs('User Name Filter');

export const applyFilterButton = () =>
    PageElement
        .located(By.css("#filters-component > button"))
        .describedAs('Apply Filter Button');

export const showFiltersButton = () =>
    PageElement
        .located(By.css("#main-content > form > div > div.govuk-grid-column-one-third > button"))
        .describedAs('Show Filters Button - Only available in Mobile View');

export const userNameInUserList = () =>
    PageElement
        .located(By.css("#main-content > form > div > div.govuk-grid-column-two-thirds > table > tbody > tr:nth-child(1) > td:nth-child(1)"))
        .describedAs('Name of user in user list');