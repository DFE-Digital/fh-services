### Test Case: Verify DfE Admin can edit users

1. Given I am logged into manage as a DfE Admin
2. When click manage users
3. Then I am navigated to a list of users for my local authority<br/>
   https://fh-info-sharing-prototype.herokuapp.com
4. When I click edit on a user
5. Then I am navigated to the edit user page
6. When I edit the email for a user
7. Then the new email is saved
8. When I edit user permissions for a user
9. Then the user permissions are saved