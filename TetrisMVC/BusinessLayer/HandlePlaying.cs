using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TetrisMVC.Controller;
using TetrisMVC.DTO;
using TetrisMVC.TetrisService;

namespace TetrisMVC.BusinessLayer
{
    public class HandlePlaying
    {
        #region ATTRIBUTE
        private MainWindowController mainWindowController;

        private TetraminoMoving tetraminoMoving;

        public int rotation = 0;
        private int rowCount = 0;
        private int columnCount = 0;
        private bool nextShapeDrawed = false;
        private int nextShapeNumber;
        private bool bottomCollided = false;
        private bool leftCollided = false;
        private bool rightCollided = false;
        private int GAMESPEED = 700;        // millisecond


        private int currentTetrominoHeigth;
        private int currentTetrominoWidth;
        private int[,] currentTetromino = null;
        List<int> currentTetrominoRow = null;
        List<int> currentTetrominoColumn = null;
        Random shapeRandom;
        

        #endregion

        #region PROPERTIES
        public int GameSpeed { get; set; }
        public int GameLevel { get; set; } = 1;
        public DispatcherTimer Timer { get; set; }
        public bool GameActive { get; set; } = false;
        public int GameScore { get; set; } = 0;
        public double GameSpeedCounter { get; set; } = 0;
        public bool IsGameOver { get; set; } = false;
        public int LevelScale { get; set; } = 60;
        public int DownPos { get; set; } = 0;
        public int LeftPos { get; set; } = 0;
        public bool IsRotated { get; set; } = false;
        public int CurrentShapeNumber { get; set; }
        #endregion

        #region METHOD
        public HandlePlaying(MainWindowController mWC, TetraminoMoving tM)
        {
            mainWindowController = mWC;
            GameSpeed = GAMESPEED;
            shapeRandom = new Random();
            CurrentShapeNumber = shapeRandom.Next(1, 8);
            nextShapeNumber = shapeRandom.Next(1, 8);
            tetraminoMoving = tM;
        }

        public int getValueScore()
        {
            return GameScore;
        }

        public void setLevel(int level)
        {
            for (int i = 0; i < 12; i++)
            {
                if (i == level)
                {
                    GameLevel = i + 1;
                    break;
                }
                GameSpeed = GameSpeed - 50;
            }
        }
        //Kiểm tra va chạm của khối khi xoay
        public bool rotationCollided(int _rotation)
        {
            if (checkCollided(0, currentTetrominoWidth - 1)) { return true; }
            else if (checkCollided(0, -(currentTetrominoWidth - 1))) { return true; }
            else if (checkCollided(0, -1)) { return true; }
            else if (checkCollided(-1, currentTetrominoWidth - 1)) { return true; }
            else if (checkCollided(1, currentTetrominoWidth - 1)) { return true; }
            return false;
        }

        // Hàm xoay khối
        public void shapeRotationChecker(ref int rotation, TetraminoMoving te_moving)
        {
            if (rotationCollided(rotation))
            {
                rotation -= 90;
                return;
            }
            te_moving.shapeRotation(ref rotation, CurrentShapeNumber, ref currentTetromino, mainWindowController.Tetramino);
            IsRotated = true;
            addShape(CurrentShapeNumber, LeftPos, DownPos);
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            DownPos++;
            int result = moveShape(Timer, mainWindowController.Board);
            if (result == -1)
                gameOver();
            if (result == 0)
            {
                resetControl(IsGameOver);
                Timer.Start();
            }
            if (GameSpeedCounter >= LevelScale)
            {
                if (GameSpeed >= 50)
                {
                    GameSpeed -= 50;
                    GameLevel++;
                    mainWindowController.MainWindow.levelTxt.Text = "Level : " + GameLevel.ToString();
                }
                else { GameSpeed = 50; }
                Timer.Stop();
                Timer.Interval = new TimeSpan(0, 0, 0, 0, GameSpeed);
                Timer.Start();
                GameSpeedCounter = 0;
            }
            GameSpeedCounter += (GameSpeed / 1000f);
        }

        // Hàm di chuyển khối mới tạo vào grid
        public int moveShape(DispatcherTimer timer, Board board)
        {
            leftCollided = false;
            rightCollided = false;
            TetroCollided();
            if (LeftPos > (board.getTetrisGridColumn() - currentTetrominoWidth))
            {
                LeftPos = (board.getTetrisGridColumn() - currentTetrominoWidth);
            }
            else if (LeftPos < 0) { LeftPos = 0; }

            if (bottomCollided)
            {
                Timer.Stop();
                if (DownPos <= 2)
                {
                    return -1;
                }
                tetraminoMoving.shapeStoped(board.getMainGrid(), DownPos);
                checkComplete();
                return 0;
            }
            addShape(CurrentShapeNumber, LeftPos, DownPos);
            return 1;
        }

        public void gameOver()
        {
            mainWindowController.saveScore(mainWindowController.MainWindow);
            IsGameOver = true;
            resetControl(IsGameOver);
            mainWindowController.MainWindow.startStopBtn.Content = "Start";
            mainWindowController.MainWindow.restartStopBtn.Visibility = Visibility.Collapsed;
            GameSpeedCounter = 0;
            mainWindowController.MainWindow.txtfinish.Visibility = Visibility.Visible;
            GameSpeed = GAMESPEED;
            GameLevel = 1;
            GameActive = false;
            Timer.Interval = new TimeSpan(0, 0, 0, 0, GameSpeed);
        }

        public void resetControl(bool isGameOver)
        {
            DownPos = 0;
            LeftPos = 3;
            IsRotated = false;
            CurrentShapeNumber = nextShapeNumber;
            if (!isGameOver) { addShape(CurrentShapeNumber, LeftPos); }
            nextShapeDrawed = false;
            shapeRandom = new Random();
            nextShapeNumber = shapeRandom.Next(1, 8);
            bottomCollided = false;
            leftCollided = false;
            rightCollided = false;
            rotation = 0;
        }

        public void TetroCollided()
        {
            bottomCollided = checkCollided(0, 1);
            leftCollided = checkCollided(-1, 0);
            rightCollided = checkCollided(1, 0);
        }

        //  Hàm kiểm tra va chạm
        public bool checkCollided(int _leftRightOffset, int _bottomOffset )
        {
            Rectangle movingSquare;
            int squareRow = 0;
            int squareColumn = 0;
            for (int i = 0; i <= 3; i++)
            {
                squareRow = currentTetrominoRow[i];
                squareColumn = currentTetrominoColumn[i];
                try
                {
                    movingSquare = (Rectangle)mainWindowController.Board.getMainGrid().Children
                    .Cast<UIElement>()
                    .FirstOrDefault(e => Grid.GetRow(e) == squareRow + _bottomOffset && Grid.GetColumn(e) == squareColumn + _leftRightOffset);
                    if (movingSquare != null)
                    {
                        if (movingSquare.Name.IndexOf("arrived") == 0)
                        {
                            return true;
                        }
                    }
                }
                catch { }
            }
            if (DownPos > (mainWindowController.Board.getTetrisGridRow() - currentTetrominoHeigth)) { return true; }
            return false;
        }

        public void setKeyright()
        {
            TetroCollided();
            if (!rightCollided) { LeftPos++; }
            rightCollided = false;
        }

        public void setKeyleft()
        {
            TetroCollided();
            if (!leftCollided) { LeftPos--; }
            leftCollided = false;
        }

        // Hàm kiểm tra full dòng
        private void checkComplete()
        {
            int gridRow = mainWindowController.Board.getMainGrid().RowDefinitions.Count;
            int gridColumn = mainWindowController.Board.getMainGrid().ColumnDefinitions.Count;
            int squareCount = 0;
            for (int row = gridRow; row >= 0; row--)
            {
                squareCount = 0;
                for (int column = gridColumn; column >= 0; column--)
                {
                    Rectangle square;
                    square = (Rectangle)mainWindowController.Board.getMainGrid().Children
                   .Cast<UIElement>()
                   .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                    if (square != null)
                    {
                        if (square.Name.IndexOf("arrived") == 0)
                        {
                            squareCount++;
                        }
                    }
                }

                // If squareCount == gridColumn this means tha the line is completed and must to be delete
                if (squareCount == gridColumn)
                {
                    deleteLine(row);
                    mainWindowController.Board.getScoreTxt().Text = "Your Score: " + getScore().ToString();
                    checkComplete();
                }
            }
        }

        private int getScore()
        {
            GameScore += 10;
            return GameScore;
        }

        // Hàm xoá dòng đã hoàn thành trên grid
        private void deleteLine(int row)
        {
            for (int i = 0; i < mainWindowController.Board.getMainGrid().ColumnDefinitions.Count; i++)
            {
                Rectangle square;
                try
                {
                    square = (Rectangle)mainWindowController.Board.getMainGrid().Children
                   .Cast<UIElement>()
                   .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == i);
                    mainWindowController.Board.getMainGrid().Children.Remove(square);
                }
                catch { }
            }
            foreach (UIElement element in mainWindowController.Board.getMainGrid().Children)
            {
                Rectangle square = (Rectangle)element;
                if (square.Name.IndexOf("arrived") == 0 && Grid.GetRow(square) <= row)
                {
                    Grid.SetRow(square, Grid.GetRow(square) + 1);
                }
            }
        }

        // Xoá khối 
        private void removeShape()
        {

            //error board null
            int index = 0;
            while (index < mainWindowController.Board.getMainGrid().Children.Count)
            {
                UIElement element = mainWindowController.Board.getMainGrid().Children[index];
                if (element is Rectangle)
                {
                    Rectangle square = (Rectangle)element;
                    if (square.Name.IndexOf("moving_") == 0)
                    {
                        mainWindowController.Board.getMainGrid().Children.Remove(element);
                        index = -1;
                    }
                }
                index++;
            }

        }

        // Hàm tạo 1 khối mới
        public void addShape(int shapeNumber, int _left = 0, int _down = 0)
        {
            removeShape();
            currentTetrominoRow = new List<int>();
            currentTetrominoColumn = new List<int>();
            Rectangle square = null;
            if (!IsRotated)
            {
                currentTetromino = null;
                currentTetromino = mainWindowController.Tetramino.getVariableByString(mainWindowController.Tetramino.getArrayTetrominos()[shapeNumber].ToString());
            }
            int firstDim = currentTetromino.GetLength(0);
            int secondDim = currentTetromino.GetLength(1);
            currentTetrominoWidth = secondDim;
            currentTetrominoHeigth = firstDim;
            if (currentTetromino == mainWindowController.Tetramino.I_Tetromino_90)
            {
                currentTetrominoWidth = 1;
            }
            else if (currentTetromino == mainWindowController.Tetramino.I_Tetromino_0) { currentTetrominoHeigth = 1; }
            for (int row = 0; row < firstDim; row++)
            {
                for (int column = 0; column < secondDim; column++)
                {
                    int bit = currentTetromino[row, column];
                    if (bit == 1)
                    {
                        square = mainWindowController.Tetramino.getBasicSquare(handleColor(mainWindowController.Board.getColor(), shapeNumber));
                        mainWindowController.Board.getMainGrid().Children.Add(square);
                        square.Name = "moving_" + Grid.GetRow(square) + "_" + Grid.GetColumn(square);
                        if (_down >= mainWindowController.Board.getMainGrid().RowDefinitions.Count - currentTetrominoHeigth)
                        {
                            _down = mainWindowController.Board.getMainGrid().RowDefinitions.Count - currentTetrominoHeigth;
                        }
                        Grid.SetRow(square, rowCount + _down);
                        Grid.SetColumn(square, columnCount + _left);
                        currentTetrominoRow.Add(rowCount + _down);
                        currentTetrominoColumn.Add(columnCount + _left);

                    }
                    columnCount++;
                }
                columnCount = 0;
                rowCount++;
            }
            columnCount = 0;
            rowCount = 0;
            if (!nextShapeDrawed)
            {
                drawNextShape(nextShapeNumber);
            }
        }

        public Color handleColor(int color, int shapeNumber)
        {
            if (color == 1)
                return Colors.GreenYellow;
            if (color == 2)
                return Colors.Red;
            if (color == 3)
                return Colors.Gold;
            if (color == 4)
                return Colors.Violet;
            if (color == 5)
                return Colors.DeepSkyBlue;
            if (color == 6)
                return Colors.Cyan;
            if (color == 7)
                return Colors.LightSeaGreen;
            return mainWindowController.Tetramino.getShapecolor()[shapeNumber - 1];

        }

        // Hàm tạo một khối tiếp theo
        private void drawNextShape(int shapeNumber)
        {
            mainWindowController.Board.getNextShapeCanvas().Children.Clear();
            int[,] nextShapeTetromino = null;
            nextShapeTetromino = mainWindowController.Tetramino.getVariableByString(mainWindowController.Tetramino.getArrayTetrominos()[shapeNumber]);
            int firstDim = nextShapeTetromino.GetLength(0);
            int secondDim = nextShapeTetromino.GetLength(1);
            int x = 0;
            int y = 0;
            Rectangle square;
            for (int row = 0; row < firstDim; row++)
            {
                for (int column = 0; column < secondDim; column++)
                {
                    int bit = nextShapeTetromino[row, column];
                    if (bit == 1)
                    {
                        square = mainWindowController.Tetramino.getBasicSquare(handleColor(mainWindowController.Board.getColor(), shapeNumber));
                        mainWindowController.Board.getNextShapeCanvas().Children.Add(square);
                        Canvas.SetLeft(square, x);
                        Canvas.SetTop(square, y);
                    }
                    x += 30;
                }
                x = 0;
                y += 30;
            }
            nextShapeDrawed = true;
        }
    }

    #endregion
}
