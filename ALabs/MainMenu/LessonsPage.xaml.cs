using ALabs.LessonIntro;
using ALabs.LessonNFA;
using ALabs.LessonRegEx;
using ALabs.Database;
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
using System.ComponentModel;

namespace ALabs
{
    /// <summary>
    /// Interaction logic for LessonsPage.xaml
    /// </summary>
    public partial class LessonsPage : Page
    {
        private readonly MainWindow mainWindow;
        private User authenticatedUser;
        private int totalPoints = 0;
        private string UserName;

        public LessonsPage(MainWindow mainWindow, User authenticatedUser)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.authenticatedUser = authenticatedUser;
            UserName = authenticatedUser.Name;
            UserNameText = "Welcome " + UserName + "!";
            totalPoints = authenticatedUser.userScore;
            TotalPointsText = "Total Points: " + totalPoints.ToString();
            // MessageBox.Show(TotalPointsText);

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
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Go back to MainPage
            mainWindow.mainFrame.Navigate(new MainPage(mainWindow, authenticatedUser));
        }
        private void Lesson1_Click(object sender, RoutedEventArgs e)
        {
            // Go to Intro Lesson
            mainWindow.mainFrame.Navigate(new Lesson1Page(mainWindow, authenticatedUser));
        }
        private void Lesson2_Click(object sender, RoutedEventArgs e)
        {
            // Go to RegEx Lesson
            mainWindow.mainFrame.Navigate(new Lesson2Page(mainWindow, authenticatedUser));
        }
        private void Lesson3_Click(object sender, RoutedEventArgs e)
        {
            // Go to NFA Lesson
            mainWindow.mainFrame.Navigate(new Lesson3Page(mainWindow, authenticatedUser));
        }
    }
}
