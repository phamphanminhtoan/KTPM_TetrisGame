using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    public class O_Tetromino : test
    {
        //---- Khối chữ O và các góc quay------------
        public int[,] O_Tetromino_0 = new int[2, 2] { { 1, 1 },
                                                    { 1, 1 }};
        public O_Tetromino()
        {
            TetrominoColor = Colors.GreenYellow;
            TetrominoName = "O_Tetromino_0";
        }

        public override string getArrayTetrominos()
        {
            return TetrominoName;
        }

        public override Color getShapecolor()
        {
            return TetrominoColor;
        }
    }
}
