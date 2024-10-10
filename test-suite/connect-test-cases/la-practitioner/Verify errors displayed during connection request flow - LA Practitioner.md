### Test Case: Verify errors displayed during connection request flow - LA Practitioner

1. Given I log into Connect as an LA practitioner in the environment under test
2. And I conduct a valid postcode search
3. And I select a service from the search results page
4. When I click Request a connection
5. Then I am redirected to the connection request flow
6. When I enter invalid details for each stage of the flow
7. Then appropriate errors are displayed until valid information is entered<br/>
   https://fh-info-sharing-prototype.herokuapp.com/