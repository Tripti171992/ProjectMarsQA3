Feature: SignInAndRegistrationFeature

Scenario: Register to Mars with valid credentials
	Given I registered into the Mars portal
	Then A user should be registered successfully

Scenario: Sign in to Mars with valid credentials
	Given I logged into the Mars portal successfully
	Then A user should be taken to home page successfully

Scenario Outline: Log in to Mars with invalid credentials
	Given I logged into the Mars portal with invalid credentials '<Email>''<Password>'
	Then A user should not be taken to home page successfully
Examples:
	| Email            | Password |
	| test13@gmail.com | abcd123  |
	| abcygygt767      | test@123 |
	| abcygygt767      | abcd123  |
	|                  | test@123 |
	| test13@gmail.com |          |
	|                  |          |
	|                  | abcdef   |
	| test13@gmail.com |          |

