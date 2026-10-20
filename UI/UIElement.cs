using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Maxwell;

public class UIElement
{
    protected Texture2D texture;
    protected Rectangle rect;
    private Vector2 position;
    private bool is_visible;
    public int width;
    public int height;
    //rect.Inflate(-padding,-padding);

    public UIElement(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible)
    {
        texture = Texture;
        position = Position;
        width = Width;
        height = Height;
        rect = new((int)position.X, (int)position.Y, width, height);
        is_visible = Is_Visible;
    }

    public virtual void Update()
    {
        
    }

    public virtual void Draw(SpriteBatch sb)
    {
        
    }
}