import {Task} from '@serenity-js/core';
import {Click} from '@serenity-js/web';
import {
    laServiceLink,
    vcfsServiceLink
} from '../find-index';

export const clickOnTheLaService = (): Task =>
    Task.where(
        `#actor clicks on the LA service`,
        Click.on(laServiceLink()),
    );

export const clickOnTheVcfsService = (): Task =>
    Task.where(
        `#actor clicks on the LA service`,
        Click.on(vcfsServiceLink()),
    );