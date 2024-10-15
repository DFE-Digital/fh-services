# Web Browser Performance Testing with K6

Here we have written performance tests for the three services in Family Hubs: Find, Connect, Manage.
# Prerequisites

- Install k6 locally
- Run data seed scripts
- Navigate to folder of service under test (i.e. find-tests, connect-tests, or manage-tests)

# How to run the tests

k6 run script.js
k6 run --vus=10 --duration=30s script.js

To see the tests run we can run K6_BROWSER_HEADLESS=true k6 run script.js

# Helpful Links

See https://grafana.com/docs/k6/latest/using-k6-browser/running-browser-tests/ to learn more about using Browser API in your test scripts.



Stuff to investigate

What does the executor shared-iterations mean? What other executor options do we have?

