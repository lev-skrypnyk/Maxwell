using System;
using System.Collections.Generic;
//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

class SceneObject
{
    private Vector3 position;
    private Vector3 rotation;
    private Vector3 scale;

    private List<Vector3> vertices = new List<Vector3>();

    protected string object_name;
    protected Color object_color;

    public SceneObject(Color object_color)
    {
        position = Vector3.Zero;
        rotation = Vector3.Zero;
        scale = Vector3.One;

        this.object_color = object_color;
    }

    protected void SetPosition(Vector3 object_pos)
    {   
        position.X = object_pos.X;
        position.Y = object_pos.X;
        position.Z = object_pos.X;
    }

    protected void ChangeRotation(Vector3 object_rot)
    {
        rotation.X = object_rot.X;
        rotation.Y = object_rot.Y;
        rotation.Z = object_rot.Z;
    }

    protected void ChangeScale(Vector3 object_scl)
    {
        scale.X = object_scl.X;
        scale.Y = object_scl.Y;
        scale.Z = object_scl.Z;
    }
}   

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