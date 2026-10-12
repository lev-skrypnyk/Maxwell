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
    private Color grid_color = new Color(60, 60, 60);//87
    private Color panel_color = new Color(35, 35, 35);

    private Color xAxisColor = new Color(100, 130, 50); //green
    private Color yAxisColor = new Color(150, 60, 75);  //red
    private Color zAxisColor = new Color(50, 135, 235); //blue

    RasterizerState wireframeState;

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

    private Button b_view;
    private Button b_select;
    private Button b_add;

    private Panel p_properties_hierarchy;
    private Texture2D pixel;
    private Panel p_tool_bar;

    ButtonState previousMiddleButtonState = ButtonState.Released;

    private float mouseSensitivity;
    private float zoomMultiplier;

    private float radius; //distance from target object position (e.g. 0,0,0) to camera position (e.g. 3, 2.5, 3)

    private bool MMBstatus;

    PerspectiveGrid grid;

    SceneObject cube;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //_graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();

        wireframeState = new RasterizerState();
        wireframeState.FillMode = FillMode.WireFrame;

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

        grid = new PerspectiveGrid(grid_color);

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new[] {Color.White});

        // scene objects initialization
        cube = new SceneObject();
        cube.geometry.AddRange(new[]
        {
            new VertexPositionColor(new Vector3(0,0,0), obj_color),
            new VertexPositionColor(new Vector3(0,1,0), obj_color),
            new VertexPositionColor(new Vector3(1,1,0), obj_color),
            new VertexPositionColor(new Vector3(1,0,0), obj_color),
            new VertexPositionColor(new Vector3(0,0,1), obj_color),
            new VertexPositionColor(new Vector3(0,1,1), obj_color),
            new VertexPositionColor(new Vector3(1,1,1), obj_color),
            new VertexPositionColor(new Vector3(1,0,1), obj_color)
        });
        cube.indices.AddRange(new[]
        {
            0, 1, 2, // south 
            0, 2, 3,
            3, 2, 6, // east
            3, 6, 7,
            7, 6, 5, // north
            7, 5, 4,
            4, 5, 1, // west
            4, 1, 0,
            1, 5, 6, // top
            1, 6, 2,
            0, 7, 4, // bottom
            0, 3, 7
        });

        //_perspectiveGridVertices = new VertexPositionColor[]

        _axisVertices = new VertexPositionColor[6]
        {
            new VertexPositionColor(new Vector3(-15, -0.01f, 0.5f),  xAxisColor),    // Green
            new VertexPositionColor(new Vector3(15, -0.01f, 0.5f),   xAxisColor),  
            new VertexPositionColor(new Vector3(0.5f, -0.01f, -15),   zAxisColor),    // Red
            new VertexPositionColor(new Vector3(0.5f, -0.01f, 15),    zAxisColor),
            new VertexPositionColor(new Vector3(0.5f, -15, 0.5f),   yAxisColor),    // Blue
            new VertexPositionColor(new Vector3(0.5f, 15, 0.5f),    yAxisColor) 
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
        //0.825
        b_view = new(Content.Load<Texture2D>("C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_view.png"), new(70, 5), 46, 26, true); //559x319 D:/code/monogame/Maxwell/Content/ui_view.png
        b_select = new(Content.Load<Texture2D>("C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_select.png"), new(120, 5), 57, 26, true); //687x319 D:/code/monogame/Maxwell/Content/ui_select.png
        b_add = new(Content.Load<Texture2D>("C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_add.png"), new(182, 5), 41, 26, true); //495x319 D:/code/monogame/Maxwell/Content/ui_add.png

        p_properties_hierarchy = new(pixel, new(w_width - 325, 0), 325, w_height, true, panel_color);
        p_tool_bar = new(pixel, new(0,0), w_width - 325, 35, true, panel_color);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        b_view.Update();
        b_select.Update();
        b_add.Update();

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

        if(mouseState.LeftButton == ButtonState.Pressed)
            obj_color = new Color(120, 120, 120);

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


        //relativeMousePos = Vector2.transform(MousePos, Matrix.Invert(transformMatrix));
        GraphicsDevice.DepthStencilState = DepthStencilState.Default; //fixes problem where axis-lines get seen through the cube because of _spriteBatch.Begin and _spriteBatch.Begin.
        //GraphicsDevice.RasterizerState = wireframeState;

        foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserIndexedPrimitives
            (
                PrimitiveType.TriangleList,
                cube.geometry.ToArray(),
                0,
                cube.geometry.Count,
                cube.indices.ToArray(),
                0,
                cube.indices.Count / 3
            );

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _axisVertices, 0, 2); //change 2->3 if I want y-axis to be displayed.

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, grid._gridVertices, 0, 64);
        }


        _spriteBatch.Begin();
        
        p_properties_hierarchy.Draw(_spriteBatch);
        p_tool_bar.Draw(_spriteBatch);
        
        b_view.Draw(_spriteBatch);
        b_select.Draw(_spriteBatch);
        b_add.Draw(_spriteBatch);
        

        // X, Y, Z

        /*
        _spriteBatch.DrawString(ui_font, $"X", new Vector2(10, 12), xAxisColor);
        _spriteBatch.DrawString(ui_font, $"Y", new Vector2(25, 12), yAxisColor);
        _spriteBatch.DrawString(ui_font, $"Z", new Vector2(40, 12), zAxisColor);
        
        _spriteBatch.DrawString(ui_font, $"Camera Position ({Math.Round(cameraPosition.X, 2)}, {Math.Round(cameraPosition.Y, 2)}, {Math.Round(cameraPosition.Z, 2)})", new Vector2(10, 30), Color.White);
        _spriteBatch.DrawString(ui_font, $"Cursor Position ({mouseX}, {mouseY})", new Vector2(10, 50), Color.White);
        _spriteBatch.DrawString(ui_font, $"Delta (X:{deltaX}, Y:{deltaY})", new Vector2(10, 70), Color.White);
        _spriteBatch.DrawString(ui_font, $"MMB held: {MMBstatus}", new Vector2(10, 90), Color.White);
        _spriteBatch.DrawString(ui_font, $"phi value: {phi}", new Vector2(10, 110), Color.White);
        _spriteBatch.DrawString(ui_font, $"theta value: {theta}", new Vector2(10, 130), Color.White);
        */

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}