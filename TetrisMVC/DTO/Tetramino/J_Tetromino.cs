﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    class J_Tetromino : test
    {
        //---- Khối chữ J và các góc quay------------
        public int[,] J_Tetromino_0 = new int[2, 3] {{1,0,0},
                                                     {1,1,1}};

        public int[,] J_Tetromino_90 = new int[3, 2] {{1,1},
                                                      {1,0},
                                                      {1,0}};

        public int[,] J_Tetromino_180 = new int[2, 3] {{1,1,1},
                                                       {0,0,1}};

        public int[,] J_Tetromino_270 = new int[3, 2] {{0,1},
                                                       {0,1},
                                                       {1,1 }};
        public J_Tetromino()
        {
            TetrominoColor = Colors.Cyan;
            TetrominoName = "J_Tetromino_0";
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