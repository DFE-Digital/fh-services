import { By, PageElement, } from '@serenity-js/web';

export const addUserLink = () =>
    PageElement
        .located(By.css("[id='add-permission']"))
        .describedAs('Add User Link');

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
        .located(By.css("#main-content > div > div > form > button"))
        .describedAs('Continue Button');

export const confirmDetailsButton = () =>
    PageElement
.located(By.css("#main-content > div > div > fieldset > form > button"))
.describedAs('Confirm details button')