Feature: User login
	I want to test the login featueres of my user system.

@mytag
Scenario: User is really admin and logs in succesfully
	Given that the userrole is Admin
	And the username is admin and the password is 1234
	Then the user succesfully logs in
	And the users role is Admin

Scenario: User logs in with wrong credentials
	Given that the users username is admin with the password 1234
	And he accidentially was drunk and enters the right username but the wrong password 1235
	Then the UserManager returns a user which is null