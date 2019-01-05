using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    class S_Tetromino : Tetramino
    {
        //---- Khối chữ S và các góc quay------------
        public int[,] S_Tetromino_0T = new int[2, 3] {{0,1,1},
                                                     {1,1,0}};

        public int[,] S_Tetromino_90T = new int[3, 2] {{1,0},
                                                      {1,1},
                                                      {0,1}};
        public S_Tetromino()
        {
            TetrominoColor = Colors.Violet;
            TetrominoName = "S_Tetromino_0";
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
