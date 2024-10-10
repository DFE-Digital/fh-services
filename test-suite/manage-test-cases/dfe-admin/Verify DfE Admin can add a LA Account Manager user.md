### Test Case: Verify DfE Admin can add a LA Account Manager user

1. Given I am logged into manage as a DfE Admin
2. When click add a user
3. Then I am navigated to the add a user flow<br/>
   https://fh-info-sharing-prototype.herokuapp.com
4. When I proceed through the flow entering valid details for the following:<br/>
   **What type of user are you adding?** LA user<br/>
   **What do they need to do?** Add/Manage services<br/>
   **Which Local Authority do they work for?** Choose an LA<br/>
   **What's their email address?** Add a test email<br/>
   **What's their full name?** Add test name
5. And I verify the details I input on the confirm details page
6. And I click submit
7. Then the user is created successfully