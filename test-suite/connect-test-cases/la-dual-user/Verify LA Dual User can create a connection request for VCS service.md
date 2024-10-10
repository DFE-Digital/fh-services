### Test Case: Verify LA Dual User can create a connection request for VCS service

1. Given I log into Connect as an LA Dual User in the environment under test
2. And I conduct a valid postcode search
3. And I select a service from the search results page
4. When I click Request a connection
5. Then I am redirected to the connection request flow
6. When I enter valid details for each stage of the flow
7. And I click submit on the final page
8. Then the connection request is submitted successfully
9. And can be viewed on the my requests page