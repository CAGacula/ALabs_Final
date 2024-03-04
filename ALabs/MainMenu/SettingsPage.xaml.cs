using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ALabs.Database;

namespace ALabs
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private readonly MainWindow mainWindow;
        private User authenticatedUser;

        public SettingsPage(MainWindow mainWindow, User authenticatedUser)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.authenticatedUser = authenticatedUser;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Go back to MainPage
            mainWindow.mainFrame.Navigate(new MainPage(mainWindow, authenticatedUser));
        }

        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new EditAccountPage(mainWindow, authenticatedUser));
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete your account? This action cannot be undone.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // User confirmed, proceed with account deletion
                DeleteAccount();
            }
            // If the result is No, do nothing (user canceled the operation)
        }

        private void DeleteAccount()
        {
            // Add your logic here to delete the account from the database or perform any other actions

            // For example:
            using (UserDataContext context = new UserDataContext())
            {
                // Retrieve the user from the database
                User userToDelete = context.Users.FirstOrDefault(user => user.Id == authenticatedUser.Id);

                if (userToDelete != null)
                {
                    // Remove the user from the database
                    context.Users.Remove(userToDelete);
                    context.SaveChanges();

                    // Optionally, perform additional cleanup or actions

                    MessageBox.Show("Your account has been deleted.", "Account Deleted", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Navigate to a different page or perform other actions after account deletion
                    mainWindow.mainFrame.Navigate(new UserLogin(mainWindow, authenticatedUser));
                }
                else
                {
                    MessageBox.Show("Error deleting account. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
