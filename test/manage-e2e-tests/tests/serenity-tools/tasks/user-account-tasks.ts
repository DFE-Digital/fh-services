import {Answerable, Check, Task} from '@serenity-js/core';
import {Click, Enter, isVisible} from '@serenity-js/web';
import {
    addServicesUserActivity,
    addUserLink,
    applyFilterButton,
    confirmDetailsButton,
    continueButton,
    emailAddressInputBox,
    fullNameInputBox,
    laOrganisationInputBox,
    laOrganisationInputSuggestion,
    localAuthorityPermissions,
    manageUsersLink,
    secondContinueButton,
    showFiltersButton,
    userNameFilterInputBox
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
            )
    );

export const selectUserAction = (actionType: Answerable<string>): Task =>
    Task.where(`#actor assigns available actions to user`,
        Check.whether(actionType, equals('add services'))
            .andIfSo(
                Click.on(addServicesUserActivity())
            )
    );

export const selectLocalAuthority = (laName: Answerable<string>): Task =>
    Task.where(`#actor assigns ${laName} local authority to user`,
        Enter.theValue(laName).into(laOrganisationInputBox()),
        Click.on(laOrganisationInputSuggestion())
    );

export const enterTestEmail = (emailAddress: Answerable<string>): Task =>
    Task.where(`#actor assigns ${emailAddress} email address to user`,
        Enter.theValue(emailAddress).into(emailAddressInputBox()),
    );

export const enterFullName = (fullName: Answerable<string>): Task =>
    Task.where(`#actor assigns ${fullName} full name address to user`,
        Enter.theValue(fullName).into(fullNameInputBox()),
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
        Enter.theValue(fullName).into(userNameFilterInputBox()),
        Click.on(applyFilterButton())
    );