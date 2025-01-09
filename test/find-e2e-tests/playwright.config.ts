import {defineConfig, devices} from '@playwright/test';
import type { SerenityOptions } from '@serenity-js/playwright-test';
import dotenv from 'dotenv';

dotenv.config();

export default defineConfig<SerenityOptions>({
    testDir: './tests',
    /* Maximum time one test can run for, measured in milliseconds. */
    timeout: 30_000,
    expect: {
        /**
         * The maximum time, in milliseconds, that expect() should wait for a condition to be met.
         * For example in `await expect(locator).toHaveText();`
         */
        timeout: 5000,
    },
    /* Run tests in files in parallel */
    fullyParallel: true,
    /* Fail the build on CI if you accidentally left test.only in the source code. */
    forbidOnly: !!process.env.CI,
    /* Retry on CI only */
    retries: process.env.CI ? 2 : 0,
    /* Specifies the reporter to use. For more information, see https://playwright.dev/docs/test-reporters */
    reporter: [
        ['line'],
        ['html', { open: 'never' }],
        ['@serenity-js/playwright-test', {
            crew: [
                '@serenity-js/console-reporter',
                ['@serenity-js/serenity-bdd', {
                    specDirectory: './tests',
                    reporter: {
                        includeAbilityDetails: true,
                    },
                }],
                ['@serenity-js/core:ArtifactArchiver', { outputDirectory: 'target/site/serenity' }],
                // '@serenity-js/core:StreamReporter',  // uncomment to enable debugging output
            ],
        }],
    ],
    /* Shared settings for all the projects below. See https://playwright.dev/docs/api/class-testoptions. */
    use: {
        /* Base URL via the environment variable to use in actions like `await page.goto('/')`. */
        baseURL: process.env.BASE_URL,

        /* Set headless: false to see the browser window */
        headless: true,

        /* Maximum time each action such as `click()` can take. Defaults to 0 (no limit). */
        actionTimeout: 0,

        /* Collect trace when retrying the failed test. See https://playwright.dev/docs/trace-viewer */
        trace: 'on-first-retry',

        // Capture screenshot only on failure
        screenshot: 'only-on-failure'
    },

    /* Configure projects for major browsers */
    projects: [
        // {
        //     name: 'firefox',
        //     use: {
        //         ...devices['Desktop Firefox'],
        //     },
        // },
        // {
        //     name: 'webkit',
        //     use: { ...devices['Desktop Safari'] },
        // },
        {
            name: 'Microsoft Edge',
            use: {
                channel: 'msedge',
            },
        },
        {
            name: 'Google Chrome',
            use: {
                channel: 'chrome',
            },
        },
        /* Test against mobile viewports. */
        {
            name: 'Mobile Chrome',
            use: {
                ...devices['Pixel 5'],
            },
        },
        {
            name: 'Mobile Safari',
            use: {
                ...devices['iPhone 12'],
            },
        },
    ],

    /* Folder for test artifacts such as screenshots, videos, traces, etc. */
    outputDir: 'test-results/',
});