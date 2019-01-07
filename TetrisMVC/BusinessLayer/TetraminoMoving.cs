using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using TetrisMVC.Controller;
using TetrisMVC.DTO;
using TetrisMVC.DTO.Tetramino;

namespace TetrisMVC.BusinessLayer
{
    public class TetraminoMoving
    {
        #region METHOD

        public TetraminoMoving()
        {
            
        }


        // Hàm xoay khối 
        public void shapeRotation(ref int _rotation, ref int[,] currentTetromino,  Tetramino teBlock) 
        {
            
            if (teBlock.TetrominoName.Substring(0, 1) == "O")
            {
                return;
            }
            currentTetromino = teBlock.getVariableByString(teBlock.TetrominoName.Substring(0, 12) + _rotation);

        }

        

        // Hàm dừng khối
        public void shapeStoped(Grid MainGrid, int downPos)
        {
            int index = 0;
            while (index < MainGrid.Children.Count)
            {
                UIElement element = MainGrid.Children[index];
                if (element is Rectangle)
                {
                    Rectangle square = (Rectangle)element;
                    if (square.Name.IndexOf("moving_") == 0)
                    {
                        string newName = square.Name.Replace("moving_", "arrived_");
                        square.Name = newName;
                    }
                }
                index++;
            }

        }

        #endregion

    }
}
