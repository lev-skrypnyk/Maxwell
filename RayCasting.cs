using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

public class RayCast
{

    public MouseState ms;
    public MouseState prev_ms;
    public int mouseX = 0;
    public int mouseY = 0;

    public Matrix world;
    public Matrix view;
    public Matrix projection;

    private Vector3 nearsource;
    private Vector3 farsource;

    private Vector3 nearPoint; 
    private Vector3 farPoint;

    Ray pickRay;


    public RayCast(Matrix World, Matrix View, Matrix Projection)
    {
        world = World;
        view = View;
        projection = Projection;
    }

    public void Update(MouseState MS, int MouseX, int MouseY)
    {
        ms = MS;
        mouseX = MouseX;
        mouseY = MouseY;

        nearPoint = new Vector3((float)mouseX, (float)mouseY, 0f);
        farPoint = new Vector3((float)mouseX, (float)mouseY, 1f);
        
        Vector3 direction = farPoint - nearPoint;
        direction.Normalize();
        
        pickRay = new Ray(nearPoint, direction);

        //float? distance = pickRay.Intersects()
        //if (distance.HasValue)
        {
            
        }
    }

    public void Draw(GraphicsDevice gd)
    {
        nearPoint = gd.Viewport.Unproject(nearsource, projection, view, world);
        farPoint = gd.Viewport.Unproject(farsource, projection, view, world);
    }

}