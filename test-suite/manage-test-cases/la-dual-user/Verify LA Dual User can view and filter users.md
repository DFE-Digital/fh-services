### Test Case: Verify LA Dual User can view and filter users

1. Given I am logged into manage as a LA Dual User
2. When click manage users
3. Then I am navigated to a list of users for my local authority<br/>
   https://fh-info-sharing-prototype.herokuapp.com
4. When I filter on the users<br/>
   **Name**<br/>
   **Email Address**<br/>
   **Organisation**<br/>
   **Type of User**
5. Then I get a filtered list of users back
6. When I clear filters
7. Then the original list of users is returned