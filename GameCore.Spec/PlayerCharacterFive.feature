Feature: PlayerCharacterFive
  In order to play the game
  As a human player
  I want my character attributes to be correctly represented

	Background:
    Given I'm a new player

@mytag
# Given My character class is set to Healer 
# And Cast a healing spell      
Scenario: Healers restore all health
	Given My character class is set to Healer  
	When I take 40 damage  
	   And Cast a healing spell               
	Then My health should now be 100


 #Given I have the following magical items
 #Then My total magical power should be 700
Scenario: Total magical power
   Given I have the following magical items
   | name   | value | power |
   | Ring   | 200   | 100   |
   | Amulet | 400   | 200   |
   | Gloves | 100   | 400   |
   Then My total magical power should be 700


#Given I last slept 3 days ago
#And I read a restore health scroll
Scenario: Reading a restore health scroll when over tired has no effect
Given I last slept 3 days ago
When  I take 40 damage
   And I read a restore health scroll
  Then My health should now be 60


#Given I have the following weapons
#Then My weapons should be worth 100
Scenario: Weapons are worth money
  Given I have the following weapons
         | name  | value |
         | Sword | 50    |
         | Pick  | 40    |
         | knife | 10    |

   Then My weapons should be worth 100

#And I havae an Amulet with a power of 200
#When  I use a magical Amulet
#Then  The Amulet power should not be reduced
Scenario: Elf race characters don't lose magical item power
Given I'm an Elf
  And I have an Amulet with a power of 200
When  I use a magical Amulet
Then  The Amulet power should not be reduced