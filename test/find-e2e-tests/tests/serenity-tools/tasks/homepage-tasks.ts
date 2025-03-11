import {Task} from '@serenity-js/core';
import {Navigate, Click} from '@serenity-js/web';
import {startButton} from '../find-index';

export const navigateToFind = (): Task =>
    Task.where(
        `#actor navigates to the Find Website`,
        Navigate.to('/'),
    );

export const clickOnTheStartButton = (): Task =>
    Task.where(
        `#actor clicks on the start button on the Find Landing Page`,
        Click.on(startButton()),
    );