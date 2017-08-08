Feature: Checkboxes
	In order to see employees only from a particular team(s)
	As a website user
	I want to be able to use team filtering function


	Background: this is a background
	Given I launch chrome
	#And I load employees table data
	And I load employee teams data
	| Name                  | Team                        |
	| Anna Pollins          | Strategic partnerships team |
	| Charlotte Fairhurst   | Strategic partnerships team |
	| Matt Johnson          | Strategic partnerships team |
	| Nick Maidment         | Strategic partnerships team |
	| Steve Skelding        | Strategic partnerships team |
	| Emily James           | Sales support team          |
	| Georgina Clark        | Sales support team          |
	| Harriet Morton-Liddle | Sales support team          |
	| Louise Barelli        | Sales support team          |
	| Oliver Wallin         | Multi manager team          |
	
	And I navigate to Octopus home page
	And I maximise browser window
	And I navigate to Our People page

@mytag
Scenario: Verify that when ticking a team and searching for an employee who is not in that team, his/her name will not be returned
	Given I have ticked a team checkbox
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
