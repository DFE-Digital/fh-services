import { browser } from 'k6/browser';
import { check, sleep } from 'k6';
export async function searchResultsTest(data) {
    const page = await browser.newPage();

    try {
        await page.goto(`${data.SETTINGS.baseUrl}`);
        const startButton = await page.locator('[data-testid="start-button"]');

        await Promise.all([startButton.click()]);
        await page.locator('[name="postcode"]').type('E1 2EN');

        const submitButton = await page.locator('[data-testid="search-button"]');

        await Promise.all([submitButton.click()]);

        const title = await page.locator('title').textContent();
        
        TODO: Update when data is seeded        
        await check(title, {
            title: title === 'Services, groups and activities in this area (page 1 of 45) - Find support for your family - GOV.UK',
        });
        
    } finally {
        await page.close();
    }
    sleep(1);
}

export async function filterSearchResultsTest(data) {
    const page = await browser.newPage();

    try {
        await page.goto(`${data.SETTINGS.baseUrl}`);
        const startButton = await page.locator('[data-testid="start-button"]');

        await Promise.all([startButton.click()]);
        await page.locator('[name="postcode"]').type('E1 2EN');

        const submitButton = await page.locator('[data-testid="search-button"]');

        await Promise.all([submitButton.click()]);

        const activitiesFilter = await page.locator('[id="activities-10"]');
        const freeFilter = await page.locator('[id="cost-free"]');
        const tenMileFilter = await page.locator('[id="search_within-1]');

        await Promise.all([activitiesFilter.click()], [freeFilter.click()], [tenMileFilter.click()]);

        sleep(1);

    } finally {
        await page.close();
    }
    sleep(1);
}

export async function serviceDetailsTest(data) {
    const page = await browser.newPage();

    try {
        await page.goto(`${data.SETTINGS.baseUrl}`);
        const startButton = await page.locator('[data-testid="start-button"]');

        await Promise.all([startButton.click()]);
        await page.locator('[name="postcode"]').type('E1 2EN');

        const submitButton = await page.locator('[data-testid="search-button"]');

        await Promise.all([submitButton.click()]);

        const firstSearchResult = await page.locator('//*[@id="results"]/div[1]/div');

        await Promise.all([firstSearchResult.click()]);

    } finally {
        await page.close();
    }
    sleep(1);
}

export async function verifyStatusCodeTest(data) {
    const page = await browser.newPage();
    try {
        const res = await page.goto(`${data.SETTINGS.baseUrl}/ServiceFilter?postcode=E1%202EN&adminarea=E09000030&latitude=51.517612&longitude=-0.056838&frompostcodesearch=True')`);

        check(res, {
            'status is 200': (r) => r.status === 200,
        });
    } finally {
        await page.close();
    }
    sleep(1);
}
