using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

public class Button : UIElement
{
    protected Color shade = Color.White;
    private MouseState last_mouse_state;
    protected Vector2 local_position;
    public Color button_color = new Color(35, 35, 35);

    public Button(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible) : base(Texture, Position, Width, Height, Is_Visible)
    {
        local_position = Position;
    }

    public Action OnClick {get; set;}

    public override void Update()
    {
        MouseState ms = Mouse.GetState();
        Rectangle cursor = new(ms.Position.X, ms.Position.Y, 1, 1);

        if(cursor.Intersects(rect))
        {
            shade = Color.Gray;
            if(ms.LeftButton == ButtonState.Pressed && last_mouse_state.LeftButton == ButtonState.Released)
            {
                shade = Color.LightGray;
                OnClick?.Invoke();
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

public class HierarchyEntry : Button
{
    private Color entry_color = new Color(35, 35, 35);
    private Color entry_hover_color = new Color(50, 50, 50);
    private Color selection_color = new Color(49, 111, 177);
    private Color icon_shade = Color.Gray;
    private MouseState last_mouse_state;
    public SceneObject target_object;
    private string object_name;
    private SpriteFont font;
    private float padding;
    private Rectangle icon_rect;
    private float vertical_offset = 15f;
    public bool is_selected = false;
    
    private Texture2D object_icon; 
    
    public HierarchyEntry(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible, SpriteFont Font, Cube Target_Object, float Padding, string Object_name, Texture2D Object_icon) : base(Texture, Position, Width, Height, Is_Visible)
    {
        font = Font;
        this.local_position = Position;
        padding = Padding * 1.5f;
        object_name = "object";
        this.rect.Inflate(-padding, 0.0f);
        object_name = Object_name;
        shade = entry_color;
        object_icon = Object_icon;
        icon_rect = new Rectangle((int)(local_position.X + vertical_offset + 5), (int)local_position.Y + 5, (int)(rect.Height * 0.7), (int)(rect.Height * 0.7));
    }

    public override void Update()
    {
        MouseState ms = Mouse.GetState();
        Rectangle cursor = new(ms.Position.X, ms.Position.Y, 1, 1);

        if(cursor.Intersects(rect))
        {
            shade = entry_hover_color;
            if(ms.LeftButton == ButtonState.Pressed && last_mouse_state.LeftButton == ButtonState.Released)
            {
                icon_shade = Color.LightGray;
                shade = Color.LightGray;
                is_selected = true;
            }
        }
        else
        {
            shade = entry_color;
        }

        if(is_selected)
        {
            shade = selection_color;
            icon_shade = Color.White;
        }

        if(ms.LeftButton == ButtonState.Pressed && last_mouse_state.LeftButton == ButtonState.Released && !(cursor.Intersects(rect)))
        {
            is_selected = false;
            icon_shade = Color.Gray;
        }

        last_mouse_state = ms;
    }

    public override void Draw(SpriteBatch sb)
    {
        //sb.Draw(object_icon, rect, Color.White);
        sb.Draw(texture, rect, shade);
        sb.Draw(object_icon, icon_rect, icon_shade);
        sb.DrawString(font, object_name, new Vector2(local_position.X + padding + (int)(icon_rect.Height * 1.5), this.local_position.Y + 5), Color.LightGray); 
    }
}