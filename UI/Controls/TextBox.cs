using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

public class TextBox : UIElement
{
    private Color textbox_color = new Color(35, 35, 35);
    private Color textbox_hover_color = new Color(50, 50, 50);
    private Color color;
    private Vector2 position;
    private string text;
    private float value = 1.0f;
    private SpriteFont font;
    private MouseState previous_mouse_state;
    private Vector2 text_pos;
    private Texture2D axis_icon;
    private Rectangle axis_icon_rect;
    private float axis_icon_width = 14.5f;

    private int mouse_x;
    private int previous_mouse_x;
    private float deltaX;
    
    public TextBox(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible, Color Color, SpriteFont Font, Texture2D Axis_Icon) : 
    base(Texture, Position, Width, Height, Is_Visible)
    {
        color = Color;
        position = Position;
        rect = new((int)(position.X + axis_icon_width), (int)position.Y, width, height);
        font = Font;
        text_pos = new Vector2(
        rect.X + 5, 
        rect.Y + (rect.Height - font.MeasureString(value.ToString()).Y) / 2f
        );
        axis_icon = Axis_Icon;
        axis_icon_rect = new((int)position.X, (int)position.Y, (int)axis_icon_width, height);
    }

    public override void Update()
    {
        //base.Update();
        MouseState ms = Mouse.GetState();
        mouse_x = ms.X;

        Rectangle cursor = new(ms.Position.X, ms.Position.Y, 1, 1);    
        
        if(cursor.Intersects(rect))
        {
            color = textbox_hover_color;
            if(ms.LeftButton == ButtonState.Pressed)
            {
                deltaX = (mouse_x - previous_mouse_x) / 10.0f;
                value += deltaX;
                value = (float)Math.Round(value, 2);
            }

            if(ms.RightButton == ButtonState.Pressed)
                value = 1.0f;
        }
        else
        {
            color = textbox_color;
        }
    
        previous_mouse_state = ms;
        previous_mouse_x = ms.X;
    }

    public override void Draw(SpriteBatch sb)
    {
        sb.Draw(axis_icon, axis_icon_rect, Color.White);
        sb.Draw(texture, rect, color);
        sb.DrawString(font, value.ToString(), text_pos, Color.White);
    }
}