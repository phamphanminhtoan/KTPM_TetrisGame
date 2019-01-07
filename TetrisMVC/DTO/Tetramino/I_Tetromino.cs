using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    class I_Tetromino : test
    {
        //---- Khối chữ I và các góc quay------------
        public int[,] I_Tetromino_0 = new int[2, 4] { { 1, 1, 1, 1 },
                                                      { 0, 0, 0, 0 } };

        public int[,] I_Tetromino_90 = new int[4, 2]  {{ 1,0 },
                                                       { 1,0 },
                                                       { 1,0 },
                                                       { 1,0 }};
        public I_Tetromino()
        {
            TetrominoColor = Colors.Red;
            TetrominoName = "I_Tetromino_0";
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
