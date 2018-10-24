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
using System.Windows.Shapes;
using TetrisMVC.Controller;

namespace TetrisMVC
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        #region ATTRIBUTE

        public int id = -1;
        public string fullname = "Guest";
        public int indexW;

        #endregion

        #region METHOD

        public MenuWindow()
        {
            InitializeComponent();
        }
        public MenuWindow(int id, string fullname)
        {
            InitializeComponent();
            this.id = id;
            this.fullname = fullname;
            if (id == -1)
            {
                fullname = "Guest";
            }
            else
            {
                btnSignin.Visibility = Visibility.Collapsed;
                btnSignup.Visibility = Visibility.Collapsed;
                btnSignout.Visibility = Visibility.Visible;
            }
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            indexW = 1;
            MainMenuController mainMenuController = new MainMenuController(this);
            if (Equal != null)
            {
                Equal(this, new EventArgs());
            }
        }
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            indexW = 2;
            MainMenuController mainMenuController = new MainMenuController(this);
            Equal?.Invoke(this, new EventArgs());
        }

        private void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            indexW = 3;
            MainMenuController mainMenuController = new MainMenuController(this);
            Equal?.Invoke(this, new EventArgs());
        }

        private void btnSignout_Click(object sender, RoutedEventArgs e)
        {
            id = -1;
            fullname = "Guest";
            btnSignin.Visibility = btnSignup.Visibility = Visibility.Visible;
            btnSignout.Visibility = Visibility.Collapsed;
        }

        private void btnHighScore_Click(object sender, RoutedEventArgs e)
        {
            indexW = 5;
            MainMenuController mainMenuController = new MainMenuController(this);
            Equal?.Invoke(this, new EventArgs());
        }

        #endregion

        public delegate void EqualHandler(object sender, EventArgs e);
        public event EqualHandler Equal;
    }
}
