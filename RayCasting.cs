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

    private Vector3 nearsource;
    private Vector3 farsource;

    public RayCast(Matrix World, Matrix View)
    {
        world = World;
        view = View;
    }

    public void Update(MouseState MS, int MouseX, int MouseY)
    {
        ms = MS;
        mouseX = MouseX;
        mouseY = MouseY;
    }
}