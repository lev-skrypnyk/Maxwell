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

public class TextBox : UIElement
{
    public TextBox(Texture2D Texture, Vector2 Position, int Width, int Height, bool Is_Visible, Color TextBoxPasive, Color TextBoxActive) : 
    base(Texture, Position, Width, Height, Is_Visible) {}

    
}