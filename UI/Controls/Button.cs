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

public class Button : UIElement
{
    private Color shade = Color.White;
    private MouseState last_mouse_state;
    protected Vector2 local_position;

    public Button(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible) : base(Texture, Position, Width, Height, Is_Visible)
    {
        local_position = Position;
    }

    public override void Update()
    {
        MouseState ms = Mouse.GetState();
        Rectangle cursor = new(ms.Position.X, ms.Position.Y, 1, 1);

        if(cursor.Intersects(rect))
        {
            shade = Color.Aqua;
            if(ms.LeftButton == ButtonState.Pressed && last_mouse_state.LeftButton == ButtonState.Released)
            {
                
            }
        }
        else
        {
            shade = Color.White;
        }

        last_mouse_state = ms;
    }   

    public override void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, rect, shade);
    }
}