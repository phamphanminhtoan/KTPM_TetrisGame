using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    class T_Tetromino : Tetramino
    {
        //---- Khối chữ T và các góc quay------------
        public int[,] T_Tetromino_0 = new int[2, 3] {{0,1,0},
                                                     {1,1,1}};

        public int[,] T_Tetromino_90 = new int[3, 2] {{1,0},
                                                      {1,1},
                                                      {1,0}};

        public int[,] T_Tetromino_180 = new int[2, 3] {{1,1,1},
                                                       {0,1,0}};

        public int[,] T_Tetromino_270 = new int[3, 2] {{0,1},
                                                       {1,1},
                                                       {0,1}};
        public T_Tetromino()
        {
            TetrominoColor = Colors.Gold;
            TetrominoName = "T_Tetromino_0";
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
