using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class PerspectiveGrid
{
    public VertexPositionColor[] _gridVertices = new VertexPositionColor[128];
    int index = 0;

    public PerspectiveGrid(Color grid_color)
    {
        for(float diff = -15.0f; diff <= 15.0f; diff += 1.0f)
        {
            // y-axis
            _gridVertices[index] = new VertexPositionColor(new Vector3(-15, -0.02f, diff),  grid_color);
            index++;
            _gridVertices[index] = new VertexPositionColor(new Vector3(15, -0.02f, diff),  grid_color);
            index++;

            // x-axis
            _gridVertices[index] = new VertexPositionColor(new Vector3(diff, -0.02f, -15),  grid_color);
            index++;
            _gridVertices[index] = new VertexPositionColor(new Vector3(diff, -0.02f, 15),  grid_color);
            index++;
        }
    
        
    }
}
