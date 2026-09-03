using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http.Headers;

//using System.Numerics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D line;

    List<Triangle> Mesh;

    float fNear;
    float fFar;
    float fFov;
    float fAspectRatio;
    float fFovRad;

    matrix_4x4 proj_mat = new matrix_4x4();

    int w_width;
    int w_height;

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

        line = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        line.SetData(new[] {Color.White});

        w_width = GraphicsDevice.Viewport.Width;
        w_height = GraphicsDevice.Viewport.Height;

        Mesh = new List<Triangle>
        {
            // South
            new(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), new Vector3(1.0f, 1.0f, 0.0f)),
            new(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f)),

            // East
            new(new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f)),
            new(new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 0.0f, 1.0f)),

            // North
            new(new Vector3(1.0f, 0.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.0f, 1.0f, 1.0f)),
            new(new Vector3(1.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f)),

            // West
            new(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f)),
            new(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f)),

            // Top
            new(new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f)),
            new(new Vector3(0.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 0.0f)),

            // Bottom
            new(new Vector3(1.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f)),
            new(new Vector3(1.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f)),
        };
        
        fNear = 0.1f;
        fFar = 1000; 
        fFov = 90.0f;
        fAspectRatio = w_height / w_width;
        fFovRad = 1.0f / MathF.Tan(fFov * 0.5f / 180.0f * 3.1415f);

        proj_mat[0, 0] = fAspectRatio * fFovRad;
        proj_mat[1, 1] = fFovRad;
        proj_mat[2, 2] = fFar / (fFar - fNear);
        proj_mat[3, 2] = (-fFar * fNear) / (fFar - fNear);
        proj_mat[2, 3] = 1.0f;
        proj_mat[3, 3] = 0.0f;



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

        // TODO: Add your update logic here
        for(int i = 0; Mesh.Count < 0; i++)
        {
            Console.WriteLine(Mesh[i]);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

       _spriteBatch.Begin();
       _spriteBatch.Draw(line, new Rectangle(w_width / 2, w_height / 2, 200, 2), null, Color.White, 0, new Vector2(0,0), SpriteEffects.None, 0.0f);
       _spriteBatch.Draw(line, new Rectangle(w_width / 2, w_height / 2, 200, 2), null, Color.White, -1.5708f, new Vector2(0,0), SpriteEffects.None, 0.0f);
       _spriteBatch.Draw(line, new Rectangle(w_width / 2 + 200, w_height / 2, 282, 2), null, Color.White, -2.35619f, new Vector2(0,0), SpriteEffects.None, 0.0f);
       _spriteBatch.End();

        base.Draw(gameTime);
    }
}