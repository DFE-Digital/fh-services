### Test Case: Verify VCS Dual User can decline connection request

1. Given I log into connect as a VCS Dual User in the environment under test
2. And I have connection requests pending in my requests list
3. When I view my request lists
4. And I decline a pending request with reason
5. Then the request is declined and no longer shows on my request list