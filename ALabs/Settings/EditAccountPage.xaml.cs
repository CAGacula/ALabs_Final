using ALabs.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ALabs
{
    /// <summary>
    /// Interaction logic for LeaderboardsPage.xaml
    /// </summary>
    public partial class EditAccountPage : Page
    {
        private readonly MainWindow mainWindow;
        private User authenticatedUser;
        private UserDataContext dbContext;

        public EditAccountPage(MainWindow mainWindow, User authenticatedUser)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.authenticatedUser = authenticatedUser;

            // Initialize the database context
            dbContext = new UserDataContext();


            // Retrieve and display leaderboard data
            
        }

        private void ChangeNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangeNamePlaceholder.Visibility = Visibility.Collapsed;
        }

        private void ChangeNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ChangeNameTextBox.Text))
            {
                ChangeNamePlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void ChangePasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangePasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void ChangePasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ChangePasswordBox.Password))
            {
                ChangePasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void ChangePasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ChangePasswordBox.Password))
            {
                ChangePasswordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void ConfirmChangePasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangePasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void ConfirmChangePasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ChangePasswordBox.Password))
            {
                ConfirmChangePasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void ConfirmChangePasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ChangePasswordBox.Password))
            {
                ConfirmChangePasswordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                ConfirmChangePasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ChangeNameTextBox.Text) && string.IsNullOrEmpty(ChangePasswordBox.Password) && string.IsNullOrEmpty(ConfirmChangePasswordBox.Password))
            {
                MessageBox.Show("You cannot save changes if all fields are empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Exit the method if all fields are empty
            }

            if (string.IsNullOrEmpty(ChangePasswordBox.Password) && string.IsNullOrEmpty(ConfirmChangePasswordBox.Password))
            {
                // Only the name is being updated, proceed with saving changes
                SaveChangesToDatabase();
            }
            else
            {
                // Check passwords if they match
                if (ChangePasswordBox.Password != ConfirmChangePasswordBox.Password)
                {
                    MessageBox.Show("Passwords do not match. Please make sure the passwords match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Exit the method if passwords don't match
                }

                // Check if new password is the same as the current password
                if (ChangePasswordBox.Password == authenticatedUser.Password)
                {
                    MessageBox.Show("New password must be different from the current password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Exit the method if new password is the same as the current password
                }

                // All conditions passed, proceed with saving changes
                SaveChangesToDatabase();
            }
        }

        private void SaveChangesToDatabase()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Detach the user from the context
                dbContext.Entry(authenticatedUser).State = EntityState.Detached;

                // Attach the user to the context
                dbContext.Users.Attach(authenticatedUser);

                // Update the user's name in the local context
                if (string.IsNullOrEmpty(ChangeNameTextBox.Text))
                {
                    authenticatedUser.Password = ChangePasswordBox.Password;

                    // Update the user in the database
                    dbContext.SaveChanges();
                }
                else
                {
                    authenticatedUser.Name = ChangeNameTextBox.Text;

                    authenticatedUser.Password = ChangePasswordBox.Password;

                    // Update the user in the database
                    dbContext.SaveChanges();
                    
                }

                MessageBox.Show("Changes saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // If the result is No, do nothing (user canceled the operation)
        }



        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new SettingsPage(mainWindow, authenticatedUser));
        }
    }
}
