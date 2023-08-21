Feature: SkillFeature

As a user, I would like to add, delete and update a skill, so that 
I can manage skills successfully.

Scenario Outline: Add a skill with valid credentials
	Given I logged into the Mars portal successfully
	When I added '<Skill>' and '<Level>'
	Then A skill '<Skill>' added success message should be displayed
	And A '<Skill>' and '<Level>' record should be added successfully
Examples:
	| Skill  | Level        |
	| Java   | Beginner     |
	| Python | Expert       |
	| C++    | Intermediate |
	| c      | Expert       |

Scenario Outline: Update a existing skill record with valid credentials
	Given I logged into the Mars portal successfully
	When I update existing skill '<ExistingSkill>' and level '<ExistingLevel>' to new skill '<Skill>' and level '<Level>' of an existing record
	Then A skill '<Skill>' updated success message should be displayed
	And New skill '<Skill>' and level '<Level>' should get updated in existing skill record successfully
Examples:
	| ExistingSkill | ExistingLevel | Skill | Level  |
	| Python        | Expert        | C#    | Expert |

Scenario: Delete an exiisting skill record with valid credentials
	Given I logged into the Mars portal successfully
	When I delete a skill '<Skill>' and a level '<Level>'  record
	Then A skill '<Skill>' deleted success message should be displayed
	And A skill '<Skill>' and level '<Level>'record should not exist
Examples:
	| Skill | Level    |
	| Java  | Beginner |

Scenario Outline: Adding a record with invalid skill or/and no level
	Given I logged into the Mars portal successfully
	When I added '<Skill>' and '<Level>'
	Then A skill not added success message should be displayed
	And A record with invalid skill '<Skill>' or/and no level '<Level>' should not be added
Examples:
	| Skill | Level              |
	|       | Choose Skill Level |
	| JSON  | Choose Skill Level |
	|       | Beginner           |

Scenario Outline: Updating a record with empty skill or/and no level
	Given I logged into the Mars portal successfully
	When I update existing skill '<ExistingSkill>' and level '<ExistingLevel>' to new skill '<Skill>' and level '<Level>' of an existing record
	Then A skill not updated error message should be displayed
	And A record with empty skill '<Skill>' and level '<Level>' should not get updated in existing language record
Examples:
	| ExistingSkill | ExistingLevel | Skill | Level       |
	| Python        | Expert        |       | Skill Level |
	| Python        | Expert        | JSON  | Skill Level |
	| Python        | Expert        |       | Beginner    |

Scenario Outline: Add a duplicate skill with valid credentials
	Given I logged into the Mars portal successfully
	When I added '<Skill>' and '<Level>'
	And I added '<Skill>' and '<Level>'
	Then A duplicate skill added error message should be displayed
	And A duplicate skill '<Skill>' and Level record should not be added successfully
Examples:
	| Skill | Level  |
	| MySQL | Expert |
Scenario Outline: Cancel adding skill record with valid credentials
	Given I logged into the Mars portal successfully
	When I cancelled adding a skill '<Skill>' and  level '<Level>' reord
	Then A skill '<Skill>' record addition should be cancelled
Examples:
	| Skill   | Level    |
	| Postman | Beginner |

Scenario Outline: Cancel updating a skill record with valid credentials
	Given I logged into the Mars portal successfully
	When I cancelled updating existing skill '<ExistingSkill>' and level '<ExistingLevel>' to new skill '<Skill>' and level '<Level>' of an existing record
	Then A skill '<Skill>' and level '<Level>' record updation should be cancelled
Examples:
	| ExistingSkill | ExistingLevel | Skill | Level  |
	| Python        | Expert        | C#    | Expert |
