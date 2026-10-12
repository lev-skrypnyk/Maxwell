using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class PerspectiveGrid
{
    public VertexPositionColor[] _gridVertices = new VertexPositionColor[128];
    int index = 0;
    float lengthOfGrid = 15.0f;
    float verticalOffset = -0.02f;

    public PerspectiveGrid(Color grid_color)
    {
        for(float diff = -lengthOfGrid; diff <= lengthOfGrid; diff += 1.0f)
        {
            // y-axis
            _gridVertices[index] = new VertexPositionColor(new Vector3(-lengthOfGrid, -verticalOffset, diff),  grid_color);
            index++;
            _gridVertices[index] = new VertexPositionColor(new Vector3(lengthOfGrid, -verticalOffset, diff),  grid_color);
            index++;

            // x-axis
            _gridVertices[index] = new VertexPositionColor(new Vector3(diff, -verticalOffset, -lengthOfGrid),  grid_color);
            index++;
            _gridVertices[index] = new VertexPositionColor(new Vector3(diff, -verticalOffset, lengthOfGrid),  grid_color);
            index++;
        }
    
        
    }
}
