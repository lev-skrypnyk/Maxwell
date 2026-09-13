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

        cameraPosition = new Vector3(2, 2, 0);
        cameraTarget = new Vector3(0, 0, 0);
        cameraUp = new Vector3(0, 1, 0);

        _vertices = new VertexPositionColor[8]
        {
            new VertexPositionColor(new Vector3(0,0,0), Color.Gray),
            new VertexPositionColor(new Vector3(0,1,0), Color.Gray),
            new VertexPositionColor(new Vector3(1,1,0), Color.Gray),
            new VertexPositionColor(new Vector3(1,0,0), Color.Gray),
            new VertexPositionColor(new Vector3(0,0,1), Color.Gray),
            new VertexPositionColor(new Vector3(0,1,1), Color.Gray),
            new VertexPositionColor(new Vector3(1,1,1), Color.Gray),
            new VertexPositionColor(new Vector3(1,0,1), Color.Gray)
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

        Matrix.CreateLookAt(
        cameraPosition,
        cameraTarget,
        cameraUp
        );

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

       _spriteBatch.Begin();

       _spriteBatch.End();

        base.Draw(gameTime);
    }
}