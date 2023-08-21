Feature: LanguageFeature

As a user, I would like to add, delete and update a language, so that 
I can manage languages successfully.

Scenario Outline: Add a language with valid credentials
	Given I logged into the Mars portal successfully
	When I added language '<Language>' and level '<Level>'
	Then A language '<Language>' added success message should be displayed
	And A language '<Language>' and Level '<Level>' record should be added successfully
Examples:
	| Language | Level          |
	| Hindi    | Basic          |
	| English  | Fluent         |
	| Korean   | Basic          |
	| Japanese | Conversational |

Scenario Outline: Update an existing language record with valid credentials
	Given I logged into the Mars portal successfully
	When I update existing language '<ExistingLaguage>' and level '<ExistingLevel>' to new language '<Language>' and level '<Level>' of an existing record
	Then A language '<Language>' updated success message should be displayed
	And New language '<Language>' and level '<Level>' should get updated in existing language record successfully
Examples:
	| ExistingLaguage | ExistingLevel | Language | Level  |
	| English         | Fluent        | Spanish  | Fluent |

Scenario Outline: Delete an existing language record with valid credentials
	Given I logged into the Mars portal successfully
	When I delete a language '<Language>' and a level '<Level>'  record
	Then A language '<Language>' deleted success message should be displayed
	And A language '<Language>' and level '<Level>'record should not exist
Examples:
	| Language | Level |
	| Hindi    | Basic |

Scenario Outline: Adding a record with invalid language or/and no level
	Given I logged into the Mars portal successfully
	When I added language '<Language>' and level '<Level>'
	Then A language not added error message should be displayed
	And A record with invalid language '<Language>' or/and no level '<Level>' should not be added
Examples:
	| Language | Level                 |
	|          | Choose Language Level |
	| English  | Choose Language Level |
	|          | Fluent                |

Scenario Outline: Updating a record with invalid language or/and no level
	Given I logged into the Mars portal successfully
	When I update existing language '<ExistingLaguage>' and level '<ExistingLevel>' to new language '<Language>' and level '<Level>' of an existing record
	Then A language not updated error message should be displayed
	And Invalid language '<Language>' and level '<Level>' should not get updated in existing language record
Examples:
	| ExistingLaguage | ExistingLevel | Language | Level          |
	| English         | Fluent        |          | Language Level |
	| English         | Fluent        | Irish    | Language Level |
	| English         | Fluent        |          | Fluent         |

Scenario Outline: Add a duplicate language with valid credentials
	Given I logged into the Mars portal successfully
	When I added language '<Language>' and level '<Level>'
	And I added language '<Language>' and level '<Level>'
	Then A duplicate language added error message should be displayed
	And A duplicate language '<Language>' and Level record should not be added successfully
Examples:
	| Language | Level |
	| Tamil    | Basic |

Scenario Outline: Cancel adding language record with valid credentials
	Given I logged into the Mars portal successfully
	When I cancelled adding a language '<Language>' and level '<Level>' record
	Then A language '<Language>' record addition should be cancelled
Examples:
	| Language | Level          |
	| German   | Conversational |

Scenario Outline: Cancel updating a language record with valid credentials
	Given I logged into the Mars portal successfully
	When I cancelled updating existing language '<ExistingLaguage>' and level '<ExistingLevel>' to new language '<Language>' and level '<Level>' of an existing record
	Then A language '<Language>' and level '<Level>' record updation should be cancelled
Examples:
	| ExistingLaguage | ExistingLevel | Language | Level          |
	| English         | Fluent        | French   | Conversational |