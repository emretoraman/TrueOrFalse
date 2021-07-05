Feature: Playing
	In order to get some fun
	As a gamer
	I want to play in a "TrueOrFalse" game

	Background: 
		Given I have five statements

Scenario: Play a game
	Given I added five statements
	When I start game
		And I give five answers right
	Then I win the game