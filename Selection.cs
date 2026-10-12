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

public class SelectionManager
{
    //Vector3 near_source = new Vector3((float)mouse_x, (float)mouse_y, 0f);
    //Vector3 far_source = new Vector3((float)mouse_x, (float)mouse_y, 10f);
    public SelectionManager() {}

    public bool WasObjectSelected(MouseState ms, MouseState prev_ms, int mouse_x, int mouse_y, Matrix world_matrix)
    {
        return false;
    }
}