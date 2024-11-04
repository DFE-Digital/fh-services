# Web Browser Performance Testing with K6

Here we have written performance tests for the three services in Family Hubs: Find, Connect, Manage.
# Prerequisites

- Install k6 locally
- Run data seed scripts
- Navigate to folder of service under test (i.e. find-tests, connect-tests, or manage-tests)
NOTE: Playwright is not required as we use K6 with playwright under the hood.

# How to run the tests locally
Locally here means against the test env but on your local machine/desktop and not within a pipeline. 
We should set these tests to run within a pipeline in the future as they can make your computer freeze when running with a large number of users locally.

Set the environment variables in settings.json to define whether you are running the tests locally and which type of test you are running (load, stress, or soak)

Load: how the services behave with a constant load (number of max users expected using the services at with a ramp up period and duration of 5mins).

Stress: how the services behave with a peak load (number of max users expected multiplied by 5 using the services all at once).

Soak: how the services behave with a constant load (number of max users expected using the services over an extended period of time (4 hours)).


Virtual Users (VUs): Number of threads/webdrivers/users.
Iterations: Number of times a VU will execute a test.
Executor: Determines how the iterations are shared across users.
Duration: Length of test run.

k6 run main.js --out json=loadResults.json
k6 run --vus=10 --duration=30s main.js --out json=loadResults.json

To see the tests run we can run K6_BROWSER_HEADLESS=true k6 run main.js

The --out command will output the results into a file.

# Helpful Links

See https://grafana.com/docs/k6/latest/using-k6-browser/running-browser-tests/ to learn more about using Browser API in your test scripts.



Stuff to investigate

Executor types documented here: https://grafana.com/docs/k6/latest/using-k6/scenarios/executors/

