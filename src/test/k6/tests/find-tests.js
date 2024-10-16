import http from 'k6/http';
import { browser } from 'k6/browser';
import { sleep } from 'k6';

export async function searchResultsTest(data) {
    const page = await browser.newPage();

    try {
        await page.goto(`${data.SETTINGS.baseUrl}`);

        const startButton = await page.locator('input[class="govuk-button govuk-button--start"]').click();

        await Promise.all([page.waitForNavigation(), startButton.click()]);
        
        await page.locator('input[name="postcode"]').type('E1 2EN');

        const submitButton = await page.locator('input[type="button"]');

        await Promise.all([page.waitForNavigation(), submitButton.click()]);

        const headerText = await page.locator('h1').textContent();
        check(headerText, {
            header: headerText === 'Your local family hubs, services and activities',
        }); //title attribute on the head rather than content body.
        
    } finally {
        await page.close();
    }
    sleep(1);
}

export function verifyStatusCodeTest(data) {
    const res = http.get(`${data.SETTINGS.baseUrl}/ServiceFilter?postcode=E1%202EN&adminarea=E09000030&latitude=51.517612&longitude=-0.056838&frompostcodesearch=True')`);

    check(res, {
        'status is 200': (r) => r.status === 200,
    });
}
