import { test as setup } from '@playwright/test';
import path from 'path';
import {isManageLandingPageDisplayed} from "./serenity-tools/questions/login-questions";

const authFile = path.join(__dirname, '../playwright/.auth/user.json');

setup('Login to Manage', async ({ page }) => {

    await page.goto(process.env.BASE_URL);
    await page.getByText('Start Now').click();
    await page.locator('#sign-in-button').click();
    await page.locator('#email').fill(process.env.DFE_ADMIN_USER);
    await page.getByText('Continue').click();
    await page.locator('#password').fill(process.env.GOV_LOGIN_PASSWORD);
    await page.getByText('Continue').click();
    isManageLandingPageDisplayed()
    
    await page.context().storageState({ path: authFile });
});