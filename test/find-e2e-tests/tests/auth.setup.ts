import { test as setup } from '@playwright/test';
import {isTheFindPageDisplayed} from "./serenity-tools/find-questions";

setup('Perform Basic Auth', async ({ page }) => {
    await page.goto(process.env.BASE_URL);
    isTheFindPageDisplayed()
});