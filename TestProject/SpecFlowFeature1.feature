Feature: User login
	I want to test the login featueres of my user system.

	Scenario: User logs in succesfully
	Given the user enter his username Finn_Luger and password hitler
	Then he logs in succesfully

	Scenario: User is really admin and logs in succesfully
	Given that the user logs in succesfully with username admin and password 1234
	Then the users role is Admin

	Scenario: User logs in with wrong credentials
	Given that the users username is admin with the password 1234
	And he accidentially was drunk and enters the right username but the wrong password 1235
	Then the UserManager returns a user which is null