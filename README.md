# A-Labs Final Update

Added New Feature Leaderboards:
-	Implemented in the LeaderboardsPage.xaml file.
- Retrieved user data from the database, ordered by userScore in descending order.
- Displayed the leaderboard in the UI using a stack panel.
-	Users can view their total earned points as well as the total earned points of other users.

Introduction of Scoring System in Challenge Regex:
- Implemented a scoring system in the Challenge Regex section of the application.
- Added scoring logic to evaluate user performance in regex challenges.
- Incorporated a time-based bonus points system.
- Stopwatch (stopwatch) tracks the time taken by the user to complete a challenge.
- Bonus points are calculated based on the elapsed time, with a maximum bonus limit.
- Correct answers earn users both regular and bonus points.
- The overall user score is updated and displayed to the user.
- Added a "Reset Progress" button (btnReset) to reset the user's challenge progress level.
- Users are prompted for confirmation before the reset.
- Adjusted button visibility based on the user's challenge progress.
- Implemented navigation back to the main play page (PlayPage) via the "Back" button.
- The "Delete Account" button triggers a confirmation prompt to ensure the user's intention.
- If the user confirms, the DeleteAccount method is invoked.
- The DeleteAccount method interacts with the database through Entity Framework.
- Retrieves the user from the database based on their ID.
- Removes the user from the database and saves changes.
- Displays a success message and navigates to a specified page (e.g., UserLogin) after successful account deletion.

Added New Feature Edit and Delete Account in Settings:
- Implemented an EditAccountPage.xaml file allowing users to edit their account details.
- Designed the page with input fields for changing the user's name and password.
- Incorporated placeholders to guide users when the fields are empty.
- Validated input fields before attempting to save changes.
- Displayed appropriate error messages for different scenarios (e.g., empty fields, password mismatch, etc.).
- Utilized a confirmation dialog before saving changes to ensure user intention.
- Separated the logic to save changes to the database into the SaveChangesToDatabase method.
- Detached and reattached the user entity from the context to handle updates.
- Updated the user's name and password based on the provided input.
- Included a "Back" button to return to the SettingsPage after editing.

Added New Feature Dynamic Display of User Name and Total Points in Main Menu:
- Updated the MainMenu page to dynamically display the user's name and total points.
- Incorporated the LessonsPage constructor to fetch and display dynamic user information.
- Extracted the user's name and total points from the authenticated user data.

Bug Fixed in Login Functionality:
- The previous code had a bug where the conditional statements for checking empty username and password were placed after attempting to 
  authenticate the user.
- The code has been updated to first check for empty username or password before attempting to authenticate the user.
- Moved the check for empty username or password to the beginning of the LoginButton_Click method.
- Introduced a new boolean variable userExist to check if the user with the given username or password exists in the database.

Bug Fixed in Challenge Regex Progress Update:
- The bug was related to incorrect progress updates for challenge levels when a user completes a level and returns to the main menu. The 
  user's progress was not updated correctly when replaying the challenge, resulting in the user being placed back at the wrong level.
- The code has been updated to ensure that the user's challenge progress is correctly updated based on the completed level.
- Adjusted the conditional check in the case 3 block to compare against the correct progress level (authenticatedUser.challengeprogress < 
  4 instead of authenticatedUser.challengeprogress < 3)

