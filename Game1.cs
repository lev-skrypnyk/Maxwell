using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D line;

    int w_width;
    int w_height;

    private float aspect_ratio;

    private Matrix _view;
    private Matrix _world;
    private Matrix _projection;

    private BasicEffect _effect;

    private Vector3 cameraPosition;
    private Vector3 cameraTarget;
    private Vector3 cameraUp;

    private VertexPositionColor[] _vertices;
    private short[] _indices;

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

        cameraPosition = new Vector3(2, 2, 2);
        cameraTarget = new Vector3(0.5f, 0.5f, 0.5f);
        cameraUp = new Vector3(0, 1, 0);

        aspect_ratio = (float)w_width / w_height;

        _vertices = new VertexPositionColor[8]
        {
            new VertexPositionColor(new Vector3(0,0,0), Color.White),
            new VertexPositionColor(new Vector3(0,1,0), Color.White),
            new VertexPositionColor(new Vector3(1,1,0), Color.White),
            new VertexPositionColor(new Vector3(1,0,0), Color.White),
            new VertexPositionColor(new Vector3(0,0,1), Color.White),
            new VertexPositionColor(new Vector3(0,1,1), Color.White),
            new VertexPositionColor(new Vector3(1,1,1), Color.White),
            new VertexPositionColor(new Vector3(1,0,1), Color.White)
        };

        _indices = new short[]
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

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            GraphicsDevice.DrawUserIndexedPrimitives
            (
                PrimitiveType.TriangleList,
                _vertices,
                0,
                _vertices.Length,
                _indices,
                0,
                _indices.Length / 3
            );
        }

        base.Draw(gameTime);
    }
}