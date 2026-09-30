using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

class Cube
{

    public VertexPositionColor[] _vertices;
    public short[] _indices;

    public Cube(Color obj_color)
    {
        _vertices = new VertexPositionColor[]
        {
            new VertexPositionColor(new Vector3(0,0,0), obj_color),
            new VertexPositionColor(new Vector3(0,1,0), obj_color),
            new VertexPositionColor(new Vector3(1,1,0), obj_color),
            new VertexPositionColor(new Vector3(1,0,0), obj_color),
            new VertexPositionColor(new Vector3(0,0,1), obj_color),
            new VertexPositionColor(new Vector3(0,1,1), obj_color),
            new VertexPositionColor(new Vector3(1,1,1), obj_color),
            new VertexPositionColor(new Vector3(1,0,1), obj_color)
        };

        _indices = new short[]
        {
            0, 1, 2, // south
            0, 2, 3,
            3, 2, 6, // east
            3, 6, 7,
            7, 6, 5, // north
            7, 5, 4,
            4, 5, 1, // west
            4, 1, 0,
            1, 5, 6, // top
            1, 6, 2,
            0, 7, 4, // bottom
            0, 3, 7
        };
    }

    
}