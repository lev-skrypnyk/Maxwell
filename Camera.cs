using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

public class Camera
{
    protected MouseState mouseState;
    protected MouseState previousMouseState;
    protected int mouseX;
    protected int mouseY;
    private int PreviousMouseX = 0;
    private int PreviousMouseY = 0;
    private int deltaX;
    private int deltaY;
    private float mouseSensitivity = 0.35f;
    private float radius = 6.0f;
    //radius = Vector3.Distance(cameraTarget, cameraPosition);

    private float theta = 0;
    private float phi = 0;

    protected int mouseScrollDelta;
    private ButtonState previousMiddleButtonState = ButtonState.Released;

    public Camera(){}

    public void Update(MouseState mouseState, int mouseX, int mouseY)
    {
        this.mouseState = mouseState;
        this.mouseX = mouseX;
        this.mouseY = mouseY;
    }

    public Vector3 Orbiting(Vector3 cameraPosition)
    {

        if(mouseState.MiddleButton == ButtonState.Pressed)
        {
            if(previousMiddleButtonState == ButtonState.Released)
            {
                PreviousMouseX = mouseX;
                PreviousMouseY = mouseY;
            }
            else
            {
                deltaX = (mouseX - PreviousMouseX) * -1;
                deltaY = mouseY - PreviousMouseY; // as mouse moves up, the value of y increases
            
                theta += deltaX * mouseSensitivity;
                phi += deltaY * mouseSensitivity;

            }

            cameraPosition.X = (float)(radius * Math.Cos(phi * (Math.PI / 180)) * Math.Sin(theta * (Math.PI / 180)));
            cameraPosition.Y = (float)(radius * Math.Sin(phi * (Math.PI / 180)));
            cameraPosition.Z = (float)(radius * Math.Cos(phi * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180)));

            PreviousMouseX = mouseX;
            PreviousMouseY = mouseY;
        }

        previousMouseState = mouseState;
        previousMiddleButtonState = mouseState.MiddleButton;

        return new Vector3(cameraPosition.X, cameraPosition.Y, cameraPosition.Z);
    }

    public float Zoom(float FOV)
    {
        int mouse_scroll_delta = mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;

        if(mouse_scroll_delta < 0)
            FOV += 5f;       
        else if(mouse_scroll_delta > 0)
            FOV -= 5f;

        previousMouseState = mouseState;

        return FOV;
    }
}