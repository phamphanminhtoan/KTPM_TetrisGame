using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using TetrisMVC.DTO;
using TetrisMVC.BusinessLayer;
using System.Windows.Threading;
using System.Windows.Media;

namespace TetrisMVC.Controller
{
    class MainMenuController
    {

        #region ATTRIBUTE



        #endregion

        #region METHOD

        //Xu ly hien thi cho MainMenu
        public MainMenuController(MenuWindow menuWindow)
        {
            switch (menuWindow.indexW)
            {
                case 1:
                    menuWindow.Equal += (object sender, EventArgs e) =>
                    {
                        ShowMainWindow(menuWindow);
                    };
                    break;
                case 2:
                    menuWindow.Equal += (object sender, EventArgs e) =>
                    {
                        ShowSignUp(menuWindow);
                    };
                    break;
                case 3:
                    menuWindow.Equal += (object sender, EventArgs e) =>
                    {
                        ShowSignIn(menuWindow);
                    };
                    break;
                case 5:
                    menuWindow.Equal += (object sender, EventArgs e) =>
                    {
                        ShowHighScore(menuWindow);
                    };
                    break;
                default:
                    break;
            };
        }

        public void ShowMainWindow(MenuWindow menuWindow)
        {
            MainWindow mainWindow = new MainWindow(menuWindow.comboBox.SelectedIndex,
                menuWindow.comboBox_shapeColor.SelectedIndex, menuWindow.id, menuWindow.fullname);
            menuWindow.Close();
            mainWindow.Show();
        }
        public void ShowSignIn(MenuWindow menuWindow)
        {
            signIn signin = new signIn();
            menuWindow.Close();
            signin.Show();
        }
        public void ShowSignUp(MenuWindow menuWindow)
        {
            signUp signup = new signUp();
            menuWindow.Close();
            signup.Show();
        }

        public void ShowHighScore(MenuWindow menuWindow)
        {
            highScore bxh = new highScore(menuWindow.id, menuWindow.fullname);
            menuWindow.Close();
            bxh.Show();
        }

        #endregion

    }
}

