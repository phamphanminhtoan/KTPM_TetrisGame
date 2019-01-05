using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    class L_Tetromino : Tetramino
    {
        //---- Khối chữ L và các góc quay------------
        public int[,] L_Tetromino_0T = new int[2, 3] {{0,0,1},
                                                     {1,1,1}};

        public int[,] L_Tetromino_90T = new int[3, 2] {{1,0},
                                                      {1,0},
                                                      {1,1}};

        public int[,] L_Tetromino_180T = new int[2, 3] {{1,1,1},
                                                       {1,0,0}};

        public int[,] L_Tetromino_270T = new int[3, 2] {{1,1},
                                                       {0,1},
                                                       {0,1 }};
        public L_Tetromino()
        {
            TetrominoColor = Colors.Cyan;
            TetrominoName = "L_Tetromino_0";
        }
        //public override string[] getArrayTetrominosT()
        //{
        //    return null;
        //}

        //public override Color[] getShapecolorT()
        //{


        //    return null;
        //}

        //public override int[,] getVariableByStringT()
        //{

        //    return null;
        //}
    }
}
