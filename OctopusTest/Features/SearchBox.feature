Feature: SearchBox
	In order to reduce the list of employees
	As a website visitor
	I want to use search box to look for employees of interest

	Background: this is a background
	Given I launch chrome
	And I load employees table data
	And I navigate to Octopus home page
	And I maximise browser window
	And I navigate to Our People page


@mytag
Scenario: Search by typing full name
	Given I type a person name in the searchbox
	Then person with such name appears on the screen


@mytag
Scenario: Search for invalid name
	Given I type a name that does not exist in Employees Table
	Then No Results text is displayed