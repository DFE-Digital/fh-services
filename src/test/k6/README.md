# Web Browser Performance Testing with K6

Here we have written performance tests for the three services in Family Hubs: Find, Connect, Manage.
# Prerequisites

- Install k6 locally
- Run data seed scripts
- Navigate to folder of service under test (i.e. find-tests, connect-tests, or manage-tests)

# How to run the tests
Set the environment variables in settings.json to define whether you are running the tests locally and which type of test you are running (load, stress, or soak)
k6 run main.js
k6 run --vus=10 --duration=30s main.js

To see the tests run we can run K6_BROWSER_HEADLESS=true k6 run main.js

# Helpful Links

See https://grafana.com/docs/k6/latest/using-k6-browser/running-browser-tests/ to learn more about using Browser API in your test scripts.



Stuff to investigate

Executor types documented here: https://grafana.com/docs/k6/latest/using-k6/scenarios/executors/

