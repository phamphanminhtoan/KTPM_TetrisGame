using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisMVC.DTO.Tetramino
{
    public class O_Tetromino : Tetramino
    {
        //---- Khối chữ O và các góc quay------------
        public int[,] O_TetrominoT = new int[2, 2] { { 1, 1 },
                                                    { 1, 1 }};
        public O_Tetromino()
        {
            TetrominoColor = Colors.GreenYellow;
            TetrominoName = "O_Tetromino";
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
