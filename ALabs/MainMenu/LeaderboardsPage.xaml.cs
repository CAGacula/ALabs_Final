using ALabs.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ALabs
{
    /// <summary>
    /// Interaction logic for LeaderboardsPage.xaml
    /// </summary>
    public partial class LeaderboardsPage : Page
    {
        private readonly MainWindow mainWindow;
        private User authenticatedUser;
        private UserDataContext dbContext;

        public LeaderboardsPage(MainWindow mainWindow, User authenticatedUser)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.authenticatedUser = authenticatedUser;

            // Initialize the database context
            dbContext = new UserDataContext();


            // Retrieve and display leaderboard data
            DisplayLeaderboard();
        }

        private void DisplayLeaderboard()
        {
            // Retrieve user data from the database and order by userScore
            List<User> users = dbContext.Users.OrderByDescending(u => u.userScore).ToList();

            // Display user data in UI
            for (int i = 0; i < users.Count; i++)
            {
                // Create dynamic labels for ranking, username, and user score
                Label rankLabel = new Label { Content = (i + 1).ToString(), Margin = new Thickness(0, 0, 0, 5) };
                stackPanelRanking.Children.Add(rankLabel);

                Label userNameLabel = new Label { Margin = new Thickness(0, 0, 0, 5) };
                Label userScoreLabel = new Label { Margin = new Thickness(0, 0, 0, 5) };

                // Check if the current user is in the leaderboard
                if (users[i].Username == authenticatedUser.Username)
                {
                    // If it's the authenticated user, display "You"
                    rankLabel.Foreground = System.Windows.Media.Brushes.Red;
                    rankLabel.FontWeight = FontWeights.Bold;
                    userNameLabel.Content = "You";
                    userNameLabel.Foreground = System.Windows.Media.Brushes.Red;
                    userNameLabel.FontWeight = FontWeights.Bold;
                    userScoreLabel.Content = users[i].userScore.ToString();
                    userScoreLabel.Foreground = System.Windows.Media.Brushes.Red;
                    userScoreLabel.FontWeight = FontWeights.Bold;
                }
                else
                {
                    // If it's not the authenticated user, display the actual username
                    userNameLabel.Content = users[i].Username;
                    userScoreLabel.Content = users[i].userScore.ToString();
                }

                stackPanelUserName.Children.Add(userNameLabel);
                stackPanelTotalPoints.Children.Add(userScoreLabel);

              //  Debug.WriteLine($"Added userScoreLabel for user {users[i].Username}, score: {users[i].userScore}");
            }
        }



        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the main menu or perform other actions
             mainWindow.mainFrame.Navigate(new MainPage(mainWindow, authenticatedUser));
        }
    }
}
