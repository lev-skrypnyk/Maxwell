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

public class Game1 : Game
{

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    SpriteFont ui_font;
    
    private Color bg_color = new Color(25, 25, 25); //34
    private Color obj_color = new Color(160, 160, 160);
    private Color grid_Color = new Color(60, 60, 60);//87

    private Color xAxisColor = new Color(100, 130, 50); //green
    private Color yAxisColor = new Color(150, 60, 75);  //red
    private Color zAxisColor = new Color(50, 135, 235); //blue
    

    int w_width;
    int w_height;

    private Vector3 movement;
    private float cameraSpeed;

    private float aspect_ratio;

    private Matrix _view;
    private Matrix _world;
    private Matrix _projection;

    private BasicEffect _effect;

    private Vector3 cameraPosition;
    private Vector3 cameraTarget;
    private Vector3 cameraUp;

    private VertexPositionColor[] _axisVertices;

    private KeyboardState keyboardState;

    // mouse
    private MouseState mouseState;
    private int mouseX;
    private int mouseY;
    private int PreviousMouseX;
    private int PreviousMouseY;
    private int deltaX;
    private int deltaY;

    private float theta; //horizontal rotation
    private float phi;

    ButtonState previousMiddleButtonState = ButtonState.Released;

    private float mouseSensitivity;
    private float zoomMultiplier;

    private float radius; //distance from target object position (e.g. 0,0,0) to camera position (e.g. 3, 2.5, 3)

    private bool MMBstatus;

    Cube cube;
    PerspectiveGrid grid;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //_graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        w_width = GraphicsDevice.Viewport.Width;
        w_height = GraphicsDevice.Viewport.Height;
        aspect_ratio = (float)w_width / w_height;

        cameraPosition = new Vector3(4, 4, 4);
        cameraTarget = new Vector3(0.5f, 0.5f, 0.5f);
        cameraUp = new Vector3(0, 1, 0);

        movement = Vector3.Zero;
        cameraSpeed = 0.015f;

        zoomMultiplier = 1.0f;

        radius = Vector3.Distance(cameraTarget, cameraPosition);

        PreviousMouseX = 0;
        PreviousMouseY = 0;
        deltaX = 0;
        deltaY = 0;
        mouseSensitivity = 0.35f;

        MMBstatus = false;

        cube = new Cube(obj_color);
        grid = new PerspectiveGrid(grid_Color);

        //_perspectiveGridVertices = new VertexPositionColor[]

        _axisVertices = new VertexPositionColor[6]
        {
            new VertexPositionColor(new Vector3(-100, -0.01f, 0.5f),  xAxisColor),    // Green
            new VertexPositionColor(new Vector3(100, -0.01f, 0.5f),   xAxisColor),  
            new VertexPositionColor(new Vector3(0.5f, -0.01f, -100),   zAxisColor),    // Red
            new VertexPositionColor(new Vector3(0.5f, -0.01f, 100),    zAxisColor),
            new VertexPositionColor(new Vector3(0.5f, -100, 0.5f),   yAxisColor),    // Blue
            new VertexPositionColor(new Vector3(0.5f, 100, 0.5f),    yAxisColor) 
        };

        

        _view = Matrix.CreateLookAt
        (
        cameraPosition,
        cameraTarget,
        cameraUp
        );

        _projection = Matrix.CreatePerspectiveFieldOfView
        (
            (float)(45.0f * (Math.PI / 180)),
            aspect_ratio,
            0.1f,
            100.0f
        );

        _world = Matrix.Identity;

        _effect = new BasicEffect(GraphicsDevice);

        _effect.VertexColorEnabled = true;
        _effect.World = _world;
        _effect.View = _view;
        _effect.Projection = _projection;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ui_font = Content.Load<SpriteFont>("MainFont");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        movement = Vector3.Zero;
        MMBstatus = false;
        zoomMultiplier = 1.0f;

        keyboardState = Keyboard.GetState();
        mouseState = Mouse.GetState();

        mouseX = mouseState.X;
        mouseY = mouseState.Y;

        if(mouseState.MiddleButton == ButtonState.Pressed)
        {
            MMBstatus = true;
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

            //spherical coordinates maths converion
            cameraPosition.X = (float)(radius * Math.Cos(phi * (Math.PI / 180)) * Math.Sin(theta * (Math.PI / 180)));
            cameraPosition.Y = (float)(radius * Math.Sin(phi * (Math.PI / 180)));
            cameraPosition.Z = (float)(radius * Math.Cos(phi * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180)));

            PreviousMouseX = mouseX;
            PreviousMouseY = mouseY;
        }
        previousMiddleButtonState = mouseState.MiddleButton;

        cameraPosition += movement;
        cameraTarget += movement;

        _view = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUp);
        _effect.View = _view;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(bg_color);

        _spriteBatch.Begin();

        // X, Y, Z
        _spriteBatch.DrawString(ui_font, $"X", new Vector2(10, 10), xAxisColor);
        _spriteBatch.DrawString(ui_font, $"Y", new Vector2(25, 10), yAxisColor);
        _spriteBatch.DrawString(ui_font, $"Z", new Vector2(40, 10), zAxisColor);


        _spriteBatch.DrawString(ui_font, $"Camera Position ({Math.Round(cameraPosition.X, 2)}, {Math.Round(cameraPosition.Y, 2)}, {Math.Round(cameraPosition.Z, 2)})", new Vector2(10, 30), Color.White);
        _spriteBatch.DrawString(ui_font, $"Cursor Position ({mouseX}, {mouseY})", new Vector2(10, 50), Color.White);
        _spriteBatch.DrawString(ui_font, $"Delta (X:{deltaX}, Y:{deltaY})", new Vector2(10, 70), Color.White);
        _spriteBatch.DrawString(ui_font, $"MMB held: {MMBstatus}", new Vector2(10, 90), Color.White);
        _spriteBatch.DrawString(ui_font, $"phi value: {phi}", new Vector2(10, 110), Color.White);
        _spriteBatch.DrawString(ui_font, $"theta value: {theta}", new Vector2(10, 130), Color.White);
        _spriteBatch.End();

        //relativeMousePos = Vector2.transform(MousePos, Matrix.Invert(transformMatrix));

        GraphicsDevice.DepthStencilState = DepthStencilState.Default; //fixes problem where axis-lines get seen through the cube because of _spriteBatch.Begin and _spriteBatch.Begin.

        foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserIndexedPrimitives
            (
                PrimitiveType.TriangleList,
                cube._vertices,
                0,
                cube._vertices.Length,
                cube._indices,
                0,
                cube._indices.Length / 3
            );

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _axisVertices, 0, 2); //change 2->3 if I want y-axis to be displayed.

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, grid._gridVertices, 0, 64);
        }

        base.Draw(gameTime);
    }
}