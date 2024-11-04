// #region PARSE __ENV
let ENVIRONMENT = {};

// Parse environment variables
// Default to local execution
ENVIRONMENT.execution = "local";
if (__ENV.EXECUTION) {
    ENVIRONMENT.execution = __ENV.EXECUTION;
}
// Use default options
ENVIRONMENT.optionsSet = "load";
if  (__ENV.OPTIONS_SET) {
    ENVIRONMENT.optionsSet = `.${__ENV.OPTIONS_SET}`;
}
// #endregion

// #region LOAD TESTS
// Import tests
import { searchResultsTest } from './tests/find-tests.js';
import { verifyStatusCodeTest } from './tests/find-tests.js';

let TESTS = [ searchResultsTest, verifyStatusCodeTest ];
// #endregion

// #region k6 OPTIONS
// Load k6 Run Options
let optionsFile = `./env/${ENVIRONMENT.execution}/config.${ENVIRONMENT.optionsSet}.json`;
export let options = JSON.parse(open(optionsFile));
// #endregion

// #region DATA for TESTS/ENV
// Load test settings
let DATA = JSON.parse(open(`./env/${ENVIRONMENT.execution}/settings.json`));
DATA.ENVIRONMENT = ENVIRONMENT;
// #endregion

let TESTS_TO_RUN = [ ...TESTS ];

// Runs automatically before test run
export function setup() {
    return DATA;
}

export default function (DATA) {
    // VU code
    TESTS_TO_RUN.forEach(t => { t(DATA); });
}

// This function gets called automatically by K6 after a test run. Produces a test report in the location specified.
export function handleSummary(data) {
    console.log('Preparing the end-of-test summary...');
    return {
        "testResults.json": JSON.stringify(data)
    };
}

// TODO: data teardown step
//export function teardown(data) {
    // teardown code
//}

