Feature: BrowseGithub
	I want to be able to browse github users and repositories


Scenario: Search user
	Given I am on the "Search" page
	When I have entered "rhaly" to search view	
	Then I should see "rhaly" in results list

Scenario: Browse User Repositories
	Given I am on the "Search" page
	When I have entered "rhaly" to search view	    
    And I see "rhaly" in results list 
	And I tap "rhaly"
    Then I should be navigated to repository page
    And I should see "Repository" page
