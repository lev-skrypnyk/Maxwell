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

public class Window : UIElement
{
    private Color window_color;
    private Vector2 local_position;
    private string title;
    private SpriteFont font;
    private float padding;
    //_spriteBatch.DrawString(ui_font, $"X", new Vector2(10, 12), xAxisColor);

    public Window(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible, Color Panel_Color, float Padding, string Title, SpriteFont Font) : 
    base(Texture, Position, Width, Height, Is_Visible)
    {
        window_color = Panel_Color;
        local_position = Position;
        title = Title;
        font = Font;
        padding = Padding;
        this.rect.Inflate(-padding, -padding);
    }
    

    public override void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, rect, window_color);
        sb.DrawString(font, title, new Vector2(local_position.X + padding * 1.5f, local_position.Y + padding * 1.5f), Color.White);
    }
}