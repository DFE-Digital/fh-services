import { sleep } from 'k6';

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

export default function () {
    // VU code
    TESTS_TO_RUN.forEach(t => { t(); sleep(1); });
    sleep(1);
}

/*
// data teardown step
export function teardown(data) {
    // teardown code
}*/
