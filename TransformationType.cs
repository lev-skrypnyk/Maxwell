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

public class TransformationType
{
    public TextBox x_axis_textbox;
    public TextBox y_axis_textbox;
    public TextBox z_axis_textbox;
    private SpriteFont font;
    private string header;
    private Vector2 text_pos;
    private float vertical_gap;

    public TransformationType(TextBox X_Axis_Textbox, TextBox Y_Axis_Textbox, TextBox Z_Axis_Textbox, string Header, SpriteFont Font, float Vertical_Gap)
    {
        vertical_gap = Vertical_Gap;
        x_axis_textbox = X_Axis_Textbox;
        y_axis_textbox = Y_Axis_Textbox;
        z_axis_textbox = Z_Axis_Textbox;
        //position = Position;
        header = Header;
        font = Font;
        text_pos = new Vector2(
            x_axis_textbox.position.X, 
            x_axis_textbox.position.Y - 25.0f + vertical_gap
        );

        x_axis_textbox.axis_icon_rect.Y += (int)vertical_gap;
        y_axis_textbox.axis_icon_rect.Y += (int)vertical_gap;
        z_axis_textbox.axis_icon_rect.Y += (int)vertical_gap;

        x_axis_textbox.rect.Y += (int)vertical_gap;
        y_axis_textbox.rect.Y += (int)vertical_gap;
        z_axis_textbox.rect.Y += (int)vertical_gap;
    }

    /*
    public void ChangePosition(Cube object_ref)
    {
        object_ref.ChangePosition(x_axis_textbox.value, y_axis_textbox.value, z_axis_textbox.value);
    }
    */

    public virtual void Update()
    {
        x_axis_textbox.Update();
        y_axis_textbox.Update();
        z_axis_textbox.Update();
    }

    public virtual void Draw(SpriteBatch sb)
    {
        sb.DrawString(font, header, text_pos, Color.LightGray);
        x_axis_textbox.Draw(sb);
        y_axis_textbox.Draw(sb);
        z_axis_textbox.Draw(sb);
    }
}