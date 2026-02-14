Feature: Login with Excel

Scenario: Login using Excel credentials
  Given user reads credentials from Excel
  When user logs in using Excel data
  Then user should be logged in successfully with data from Excel
