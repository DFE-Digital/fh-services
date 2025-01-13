import {Answerable, Check, Task} from '@serenity-js/core';
import {Click, Enter, isVisible, Scroll} from '@serenity-js/web';
import {
    addUserLink,
    applyFilterButton,
    confirmDetailsButton,
    continueButton,
    emailAddressInputBox,
    fullNameInputBox,
    laManagerUserActivity,
    laOrganisationInputBox,
    laOrganisationInputSuggestion,
    laPractitionerUserActivity,
    localAuthorityPermissions,
    manageUsersLink,
    secondContinueButton,
    showFiltersButton,
    userNameFilterInputBox,
    vcfsManagerUserActivity, vcfsOrganisationInputBox,
    vcfsOrganisationInputSuggestion,
    vcfsPermissions,
    vcfsPracitionerUserActivity
} from "../page-objects/accounts-page-objects";
import {equals} from "@serenity-js/assertions";
import {homeButton} from "../page-objects/manage-page-objects";

export const clickAddUserLink = (): Task =>
    Task.where(`#actor clicks add a user link on homepage`,
        Click.on(addUserLink())
    );

export const selectPermissionType = (permissionType: Answerable<string>): Task =>
    Task.where(`#actor assigns permission type to user`,
        Check.whether(permissionType, equals('la'))
            .andIfSo(
                Click.on(localAuthorityPermissions())
            ),
        Check.whether(permissionType, equals('la pracitioner'))
            .andIfSo(
                Click.on(vcfsPermissions())
            )
    );

export const selectUserAction = (actionType: Answerable<string>): Task =>
    Task.where(`#actor assigns available actions to user`,
        Check.whether(actionType, equals('la manager'))
            .andIfSo(
                Click.on(laManagerUserActivity())
            ),
        Check.whether(actionType, equals('la practitioner'))
            .andIfSo(
                Click.on(laPractitionerUserActivity())
            ),
        Check.whether(actionType, equals('vcfs manager'))
            .andIfSo(
                Click.on(vcfsManagerUserActivity())
            ),
        Check.whether(actionType, equals('vcfs practitioner'))
            .andIfSo(
                Click.on(vcfsPracitionerUserActivity())
            )
    );

export const selectLocalAuthority = (laName: Answerable<string>): Task =>
    Task.where(`#actor assigns ${laName} local authority to user`,
        Scroll.to(laOrganisationInputBox()),
        Enter.theValue(laName).into(laOrganisationInputBox()),
        Click.on(laOrganisationInputSuggestion())
    );

export const selectOrganisation = (organisationName: Answerable<string>): Task =>
    Task.where(`#actor assigns ${organisationName} organisation to user`,
        Scroll.to(vcfsOrganisationInputBox()),
        Enter.theValue(organisationName).into(vcfsOrganisationInputBox()),
        Click.on(vcfsOrganisationInputSuggestion())
    );

export const enterTestEmail = (emailAddress: Answerable<string>): Task =>
    Task.where(`#actor assigns ${emailAddress} email address to user`,
        Scroll.to(emailAddressInputBox()),
        Enter.theValue(emailAddress).into(emailAddressInputBox())
    );

export const enterFullName = (fullName: Answerable<string>): Task =>
    Task.where(`#actor assigns ${fullName} full name address to user`,
        Scroll.to(fullNameInputBox()),
        Enter.theValue(fullName).into(fullNameInputBox())
    );

export const clickContinue = (): Task =>
    Task.where(`#actor clicks continue`,
        Click.on(continueButton())
    );

export const clickSecondContinue = (): Task =>
    Task.where(`#actor clicks continue`,
        Click.on(secondContinueButton())
    );

export const clickConfirmDetails = (): Task =>
    Task.where(`#actor clicks confirm on final page of user journey`,
        Click.on(confirmDetailsButton())
    );

export const searchForUserByName = (fullName: Answerable<string>): Task =>
    Task.where(`#actor searches for user in manage`,
        Click.on(homeButton()),
        Click.on(manageUsersLink()),
        Check.whether(showFiltersButton(), isVisible())
            .andIfSo(
                Click.on(showFiltersButton())
            ),
        Scroll.to(userNameFilterInputBox()),
        Enter.theValue(fullName).into(userNameFilterInputBox()),
        Click.on(applyFilterButton())
    );