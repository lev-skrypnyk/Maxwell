using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

class matrix_4x4
{
    float[,] m = new float[4, 4];

    public float this[int row, int col]
    {
        get => m[row, col];
        set => m[row, col] = value;
    }
}