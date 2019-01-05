using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    class I_Tetromino : Tetramino
    {
        //---- Khối chữ I và các góc quay------------
        public int[,] I_Tetromino_0T = new int[2, 4] { { 1, 1, 1, 1 },
                                                      { 0, 0, 0, 0 } };

        public int[,] I_Tetromino_90T = new int[4, 2]  {{ 1,0 },
                                                       { 1,0 },
                                                       { 1,0 },
                                                       { 1,0 }};
        public I_Tetromino()
        {
            TetrominoColor = Colors.GreenYellow;
            TetrominoName = "I_Tetromino_0";
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
