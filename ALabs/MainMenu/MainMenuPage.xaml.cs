using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ALabs.Database;

namespace ALabs
{
    public partial class MainPage : Page
    {
        private readonly MainWindow mainWindow;
        private readonly User authenticatedUser;
        private int totalPoints = 0;
        private string userName;
        private UserDataContext dbContext;

        public MainPage(MainWindow mainWindow, User authenticatedUser)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.authenticatedUser = authenticatedUser;
            UserName = authenticatedUser.Name;
            UserNameText = "Welcome " + UserName + "!";
            // Assuming UserDataContext is your DbContext class
            dbContext = new UserDataContext();

            // Update authenticatedUser with the latest data
            if (dbContext != null)
            {
                dbContext.Entry(authenticatedUser).Reload();

                totalPoints = authenticatedUser.userScore;
                TotalPointsText = "Total Points: " + totalPoints.ToString();
               // MessageBox.Show(TotalPointsText);

                DataContext = this;
            }
            else
            {
                // Handle the case where dbContext is null
                MessageBox.Show("Error: dbContext is null");
            }


            DataContext = this;
        }

        private string _userNameText;
        private string _totalPointsText;

        public string TotalPointsText
        {
            get { return _totalPointsText; }
            set
            {
                _totalPointsText = value;
                OnPropertyChanged(nameof(TotalPointsText));
            }
        }

        public string UserNameText
        {
            get { return _userNameText; }
            set
            {
                _userNameText = value;
                OnPropertyChanged(nameof(UserNameText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            mainWindow.Close();
        }

        private void LeaderboardsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new LeaderboardsPage(mainWindow, authenticatedUser));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new SettingsPage(mainWindow, authenticatedUser));
        }

        private void LessonsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new LessonsPage(mainWindow, authenticatedUser));
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new PlayPage(mainWindow, authenticatedUser));
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new UserLogin(mainWindow, authenticatedUser));
        }
    }
}
