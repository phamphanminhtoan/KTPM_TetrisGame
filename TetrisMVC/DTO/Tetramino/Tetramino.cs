using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TetrisMVC.DTO.Tetramino
{
    public abstract class Tetramino
    {
        #region ATTRIBUTE

        private Color tetrominoColor ;
        private string tetrominoName;

        public string TetrominoName { get => tetrominoName; set => tetrominoName = value; }
        public Color TetrominoColor { get => tetrominoColor; set => tetrominoColor = value; }

        #endregion

        #region METHOD


        //Hàm vẽ 1 đơn vị ô vuông
        public Rectangle getBasicSquare(Color rectColor)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = 30;
            rectangle.Height = 30;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.White;
            rectangle.Fill = getGradientColor(rectColor);
            return rectangle;
        }

        // Hàm đặt màu sắc cho 1 đơn vị ô vuông
        private LinearGradientBrush getGradientColor(Color clr)
        {
            LinearGradientBrush gradientColor = new LinearGradientBrush();
            gradientColor.StartPoint = new Point(0, 0);
            gradientColor.EndPoint = new Point(1, 1.5);
            GradientStop black = new GradientStop();
            black.Color = Colors.Black;
            black.Offset = -1.5;
            gradientColor.GradientStops.Add(black);
            GradientStop other = new GradientStop();
            other.Color = clr;
            other.Offset = 0.70;
            gradientColor.GradientStops.Add(other);
            return gradientColor;
        }
     
        public int[,] getVariableByString(string variable)
        {
            return (int[,])GetType().GetField(variable).GetValue(this);
        }

        public abstract string getArrayTetrominos();

        public abstract Color getShapecolor();


        #endregion
    }
}
