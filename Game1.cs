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
    private Color window_color = new Color(45, 45, 45);
    private Color selection_color = new Color(232, 125, 13);

    private Color xAxisColor = new Color(100, 130, 50); //green
    private Color yAxisColor = new Color(150, 60, 75);  //red
    private Color zAxisColor = new Color(50, 135, 235); //blue

    RasterizerState wireframeState;
    RasterizerState normalState;
    private bool is_wireframe_enabled;

    int w_width;
    int w_height;

    private float padding;

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
    private MouseState previousMouseState;

    private Button button_view;
    private Button button_select;
    private Button button_add;

    private float FOV;

    private Panel panel_properties_hierarchy;
    private Texture2D pixel;
    private Panel panel_tool_bar;

    ButtonState previousMiddleButtonState = ButtonState.Released;

    private float mouseSensitivity;
    private float zoomMultiplier;

    private float radius; //distance from target object position (e.g. 0,0,0) to camera position (e.g. 3, 2.5, 3)

    private bool MMBstatus;

    PerspectiveGrid grid;

    Cube cube;

    Window window_hierarchy;
    Window window_transform;
    HierarchyWindow entries_hierarchy;
    
    Camera camera;


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

        wireframeState = new RasterizerState
        {
            FillMode = FillMode.WireFrame,
            CullMode = CullMode.None
        };

        normalState = new RasterizerState
        {
            FillMode = FillMode.Solid,
            CullMode = CullMode.CullCounterClockwiseFace
        };

        is_wireframe_enabled = false;

        w_width = GraphicsDevice.Viewport.Width;
        w_height = GraphicsDevice.Viewport.Height;
        aspect_ratio = (float)w_width / w_height;

        cameraPosition = new Vector3(4, 4, 4);
        cameraTarget = new Vector3(0.5f, 0.5f, 0.5f);
        cameraUp = new Vector3(0, 1, 0);

        FOV = 45.0f;
        padding = 10.0f;

        movement = Vector3.Zero;
        cameraSpeed = 0.015f;

        zoomMultiplier = 1.0f;

        radius = Vector3.Distance(cameraTarget, cameraPosition);

        previousMouseState = Mouse.GetState();
        mouseSensitivity = 0.35f;

        MMBstatus = false;

        grid = new PerspectiveGrid(grid_color);

        camera = new Camera();

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new[] {Color.White});

        // scene objects initialization
        cube = new Cube(obj_color);
        //cube.geometry.AddRange()


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

        //Matrix.CreateOrthographic(w_width, w_height, 0f, 1000f);
        _projection = Matrix.CreatePerspectiveFieldOfView((float)(FOV * (Math.PI / 180)), aspect_ratio, 0.1f, 100.0f);

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
        button_view = new(Content.Load<Texture2D>("C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_view.png"), new(70, 5), 46, 26, true); //559x319  C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_view.png
        button_select = new(Content.Load<Texture2D>("C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_select.png"), new(120, 5), 57, 26, true); //687x319  C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_select.png
        button_add = new(Content.Load<Texture2D>("C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_add.png"), new(182, 5), 41, 26, true); //495x319  C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_add.png
        
        //C:/Users/lev/Documents/GitHub/Maxwell/Content/ui_view.png
        //D:/code/monogame/Maxwell/Content/ui_add.png

        // UI COMPONENT LAYOUT POSITION FORMULA -> WorldPosition = Parent.WorldPosition + LocalPosition

        panel_properties_hierarchy = new(pixel, new(1275, 0), 325, w_height, true, panel_color);
        panel_tool_bar = new(pixel, new(0,0), 1275, 35, true, panel_color);

        window_hierarchy = new(pixel, new(panel_properties_hierarchy.local_position.X, panel_properties_hierarchy.local_position.Y), panel_properties_hierarchy.width, 250, true, window_color, padding, "Hierarchy", ui_font);
        entries_hierarchy = new(pixel, new(panel_properties_hierarchy.local_position.X, panel_properties_hierarchy.local_position.Y), panel_properties_hierarchy.width, 250, true, window_color, padding, "Hierarchy", ui_font);
        window_transform = new(pixel, new(panel_properties_hierarchy.local_position.X, panel_properties_hierarchy.local_position.Y + (entries_hierarchy.height - padding)), panel_properties_hierarchy.width, 200, true, window_color, padding, "Transform", ui_font);

        entries_hierarchy.AddEntry(cube);
        entries_hierarchy.AddEntry(cube);
        entries_hierarchy.AddEntry(cube);

        HierarchyEntry.Update();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        keyboardState = Keyboard.GetState();
        mouseState = Mouse.GetState();

        button_view.Update();
        button_select.Update();
        button_add.Update();

        camera.Update(mouseState, mouseState.X, mouseState.Y);
        FOV = camera.Zoom(FOV);
        cameraPosition = camera.Orbiting(cameraPosition);

        //if(mouseState.LeftButton == ButtonState.Pressed)
            //cube.ChangeColor(selection_color);

        is_wireframe_enabled = keyboardState.IsKeyDown(Keys.Tab);
        
        _view = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUp);
        _effect.View = _view;

        _projection = Matrix.CreatePerspectiveFieldOfView((float)(FOV * (Math.PI / 180)), aspect_ratio, 0.1f, 100.0f);
        _effect.Projection = _projection;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(bg_color);

        GraphicsDevice.DepthStencilState = DepthStencilState.Default; //fixes problem where axis-lines get seen through the cube because of _spriteBatch.Begin and _spriteBatch.End.        
        
        GraphicsDevice.RasterizerState = is_wireframe_enabled ? wireframeState : normalState;
        

        foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _axisVertices, 0, 2); //change 2->3 if I want y-axis to be displayed.
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, grid._gridVertices, 0, 64);

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

            /*
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
            */

            
        }

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        panel_properties_hierarchy.Draw(_spriteBatch);
        panel_tool_bar.Draw(_spriteBatch);
        window_hierarchy.Draw(_spriteBatch);
        entries_hierarchy.Draw(_spriteBatch);
        window_transform.Draw(_spriteBatch);
        
        button_view.Draw(_spriteBatch);
        button_select.Draw(_spriteBatch);
        button_add.Draw(_spriteBatch);
        

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
        */

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}