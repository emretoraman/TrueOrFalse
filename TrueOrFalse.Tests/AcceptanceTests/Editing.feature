Feature: Editing
	In order to provide the ability to play in a "TrueOrFalse" game
	As an editor
	I want to keep track of game statements

	Background: 
		Given I have five statements

Scenario: Add a statement
	Given I add one statement
	Then it gets saved and I can get back to it

Scenario: Edit and save a statement
	Given I added one statement
		And current statement is not empty
	When I edit both text and statement's flag
		And I save the editings
	Then it gets saved

Scenario: Remove a statement
	Given I added two statements
	When I remove one of them
	Then only one statement remains in the list

Scenario: Cut the statement text
	Given I added one statement
		And current statement is not empty
	When I cut the statement's text
	Then it gets removed from the UI and saved into clipboard