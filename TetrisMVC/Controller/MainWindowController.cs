using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TetrisMVC.BusinessLayer;
using TetrisMVC.DTO;
using TetrisMVC.TetrisService;

namespace TetrisMVC.Controller
{
    public class MainWindowController
    {
        #region ATTRIBUTE
        TetraminoMoving te_moving;
        HandlePlaying handlePlaying;

        public Board Board { get; set; }
        public Tetramino Tetramino { get; set; }

        public MainWindow MainWindow { get; set; }
        #endregion

        #region METHOD
        public MainWindowController(MainWindow mainWindow)
        {
            mainWindow.Equal += (object sender, EventArgs e) =>
            {
                ShowMainMenu(mainWindow);
            };
        }

        public MainWindowController(Board BOARD, MainWindow mw, int level)
        {
            Board = BOARD;
            MainWindow = mw;

            Tetramino = new Tetramino();
            te_moving = new TetraminoMoving(Tetramino);
            handlePlaying = new HandlePlaying(this, te_moving);
            
            
            
            handlePlaying.setLevel(level);
            handlePlaying.Timer = new DispatcherTimer();
            handlePlaying.Timer.Interval = new TimeSpan(0, 0, 0, 0, handlePlaying.GameSpeed);
            handlePlaying.Timer.Tick += handlePlaying.Timer_Tick;

            MainWindow.txtfinish.Visibility = Visibility.Collapsed;
            MainWindow.scoreTxt.Visibility = MainWindow.nextTxt.Visibility = MainWindow.restartStopBtn.Visibility = MainWindow.levelTxt.Visibility = Visibility.Hidden;
            startGame(MainWindow);
        }

        public void HandleKeyPress(string keyString)
        {
            if (!handlePlaying.Timer.IsEnabled) { return; }

            switch (keyString)
            {
                case "Up":
                    handlePlaying.rotation += 90;
                    if (handlePlaying.rotation > 270) { handlePlaying.rotation = 0; }
                    handlePlaying.shapeRotationChecker(ref handlePlaying.rotation, te_moving);
                    break;
                case "Down":
                    handlePlaying.DownPos++;
                    break;
                case "Right":
                    handlePlaying.setKeyright();
                    break;
                case "Left":
                    handlePlaying.setKeyleft();
                    break;
                default:
                    break;
            }

            int result = handlePlaying.moveShape(handlePlaying.Timer, Board);
            if (result == -1)
                handlePlaying.gameOver();
            if (result == 0)
            {
                handlePlaying.resetControl(handlePlaying.IsGameOver);
                handlePlaying.Timer.Start();
            }
        }

        

        public void saveScore(MainWindow mainWindow)
        {
            if (mainWindow.id == -1)
                return;
            else
            {
                UserServiceSoapClient reader = new UserServiceSoapClient();
                int userScore = reader.getScore(mainWindow.id);
                int currentScore = handlePlaying.getValueScore();
                if (currentScore > userScore)
                    reader.updateScore(mainWindow.id, currentScore);
            }
        }

        public void startGame(MainWindow mainWindow)
        {
            if (!handlePlaying.GameActive)
            {
                mainWindow.scoreTxt.Text = "0";
                handlePlaying.LeftPos = 3;
                handlePlaying.addShape(handlePlaying.CurrentShapeNumber, handlePlaying.LeftPos);
            }
            mainWindow.levelTxt.Text = "Level : " + handlePlaying.GameLevel.ToString();
            handlePlaying.Timer.Start();
            mainWindow.startStopBtn.Content = "Pause";
            mainWindow.restartStopBtn.Visibility = Visibility.Visible;
            mainWindow.scoreTxt.Visibility = mainWindow.nextTxt.Visibility = mainWindow.restartStopBtn.Visibility = mainWindow.levelTxt.Visibility = Visibility.Visible;
            handlePlaying.GameActive = true;
        }

        public void ShowMainMenu(MainWindow mainWindow)
        {
            MenuWindow win1 = new MenuWindow(mainWindow.id, mainWindow.fullname);
            mainWindow.Close();
            win1.Show();
        }

        public void ShowRestart(MainWindow mainWindow)
        {
            handlePlaying.resetControl(handlePlaying.IsGameOver);
        }

        public void HandleStartPause(MainWindow mainWindow)
        {
            if (handlePlaying.IsGameOver)
            {
                mainWindow.MainGrid.Children.Clear();
                mainWindow.nextShapeCanvas.Children.Clear();
                mainWindow.txtfinish.Visibility = Visibility.Collapsed;
                handlePlaying.IsGameOver = false;
            }
            if (!handlePlaying.Timer.IsEnabled)
            {
                if (!handlePlaying.GameActive)
                {
                    mainWindow.scoreTxt.Text = "0";
                    handlePlaying.LeftPos = 3;
                    handlePlaying.addShape(handlePlaying.CurrentShapeNumber, handlePlaying.LeftPos);
                }
                mainWindow.levelTxt.Text = "Level : " + handlePlaying.GameLevel.ToString();
                handlePlaying.Timer.Start();
                mainWindow.startStopBtn.Content = "Pause";
                mainWindow.restartStopBtn.Visibility = Visibility.Visible;
                mainWindow.scoreTxt.Visibility = mainWindow.nextTxt.Visibility = mainWindow.restartStopBtn.Visibility = mainWindow.levelTxt.Visibility = Visibility.Visible;
                handlePlaying.GameActive = true;
            }
            else
            {
                handlePlaying.Timer.Stop();
                mainWindow.startStopBtn.Content = "Start";
                mainWindow.restartStopBtn.Visibility = Visibility.Collapsed;
            }
        }
        
        public void setLevel(int level)
        {
            for (int i = 0; i < 12; i++)
            {
                if (i == level)
                {
                    handlePlaying.GameLevel = i + 1;
                    break;
                }
                handlePlaying.GameSpeed -= 50;
            }
        }
   
        public void reStart()
        {
            handlePlaying.GameScore = 0;
            Board.getScoreTxt().Text = "Your Score : 0";
        }
        
        #endregion
    }
}
