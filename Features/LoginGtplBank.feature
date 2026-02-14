Feature: Login GTPL Bank

Scenario: Valid login
  Given user navigates to GTPL login page
  When user logs in with username "mngr654305" and password "nYvAnEn"
  Then user should be logged in successfully
