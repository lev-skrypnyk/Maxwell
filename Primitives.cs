using System;
using System.Collections.Generic;
//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

public class SceneObject
{
    private Vector3 position;
    private Vector3 rotation;
    private Vector3 scale;

    public List<VertexPositionColor> geometry = new List<VertexPositionColor>();
    public List<int> indices = new List<int>();

    protected string object_name;
    protected Color object_color;

    public SceneObject()
    {
        position = Vector3.Zero;
        rotation = Vector3.Zero;
        scale = Vector3.One;
        object_color = new Color(160, 160, 160);
    }

    public void SetPosition(Vector3 object_pos)
    {   
        position.X = object_pos.X;
        position.Y = object_pos.X;
        position.Z = object_pos.X;
    }

    public void ChangeRotation(Vector3 object_rot)
    {
        rotation.X = object_rot.X;
        rotation.Y = object_rot.Y;
        rotation.Z = object_rot.Z;
    }

    public void ChangeScale(Vector3 object_scl)
    {
        scale.X = object_scl.X;
        scale.Y = object_scl.Y;
        scale.Z = object_scl.Z;
    }

    public void ChangeColor(Color new_color)
    {
        for(int i = 0; i < geometry.Count; i++)
        {
            VertexPositionColor vertex = geometry[i];
            vertex.Color = new_color;
            geometry[i] = vertex;
        }
    }
}   
/*
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
*/