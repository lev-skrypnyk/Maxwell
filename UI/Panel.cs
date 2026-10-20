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

public class Panel : UIElement
{
    private Color panel_color;
    public Vector2 local_position;

    public Panel(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible, Color Panel_Color) : 
    base(Texture, Position, Width, Height, Is_Visible)
    {
        panel_color = Panel_Color;
        local_position = Position;
    }

    public override void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, rect, panel_color);
    }
}