using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    class Z_Tetromino : Tetramino
    {
        //---- Khối chữ Z và các góc quay------------
        public int[,] Z_Tetromino_0T = new int[2, 3] {{1,1,0},
                                                     {0,1,1}};

        public int[,] Z_Tetromino_90T = new int[3, 2] {{0,1},
                                                      {1,1},
                                                      {1,0}};
        public Z_Tetromino()
        {
            TetrominoColor = Colors.Cyan;
            TetrominoName = "Z_Tetromino_0";
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
