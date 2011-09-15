using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Primitiver1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Primitiver : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        BasicEffect effect;

        VertexPositionColor[] vertices;
        VertexPositionColor[] linelist;
        VertexPositionColor[] linelist2;

        VertexPositionColor[] linestrip;
        VertexPositionColor[] trianglestrip;

        Matrix world;
        Matrix projection;
        Matrix view;

        Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 5.0f);
        Vector3 cameraTarget = Vector3.Zero;
        Vector3 cameraUpVector = new Vector3(0.0f, 1.0f, 0.0f);

        public Primitiver()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = new ContentManager(this.Services);

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            InitDevice();
            InitCamera();
            InitVertices();
            InitLineList();
            InitLineStrip();
            InitTriangleStrip();
        }

        private void InitDevice()
        {
            device = graphics.GraphicsDevice;

            //Setter størrelse på framebuffer:
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Window.Title = "HD-READY";

            //Initialiserer Effect-objektet:
            effect = new BasicEffect(graphics.GraphicsDevice);
            effect.VertexColorEnabled = true;
        }

        private void InitCamera()
        {
            //Projeksjon:
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            //Oppretter view-matrisa:
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);

            //Oppretter projeksjonsmatrisa:
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.01f, 1000.0f, out projection);

            //Gir matrisene til shader:
            effect.Projection = projection;
            effect.View = view;
        }

        private void InitVertices()
        {
            vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0.0f);
            vertices[1].Position = new Vector3(0.0f, 0.5f, 0.0f);
            vertices[2].Position = new Vector3(0.5f, -0.5f, 0.0f);

            vertices[0].Color = Color.Red;
            vertices[1].Color = Color.Green;
            vertices[2].Color = Color.Blue;
        }

        private void InitLineList()
        {
            linelist = new VertexPositionColor[2];
            //Første
            linelist[0].Position = new Vector3(0.0f, 0.0f, 0.0f);
            linelist[1].Position = new Vector3(3.0f, 0.0f, 0.0f);
            linelist[0].Color = Color.Green;
            linelist[1].Color = Color.Green;

            //Andre
            linelist2 = new VertexPositionColor[2];
            linelist2[0].Position = new Vector3(0.0f, 0.0f, 0.0f);
            linelist2[1].Position = new Vector3(2.0f, -1.0f, 0.0f);
            linelist2[0].Color = Color.Yellow;
            linelist2[1].Color = Color.Yellow;

        }

        private void InitLineStrip()
        {
            linestrip = new VertexPositionColor[4];
            //Første
            linestrip[0].Position = new Vector3(-1.0f, 0.0f, 0.0f);
            linestrip[1].Position = new Vector3(2.0f, -1.0f, 0.0f);
            linestrip[0].Color = Color.Blue;
            linestrip[1].Color = Color.Blue;
            //Andre
            linestrip[2].Position = new Vector3(0.0f, 1.4f, 0.0f);
            linestrip[2].Color = Color.DarkBlue;
            //Tredje
            linestrip[3].Position = new Vector3(-2.0f, -2.0f, 0.0f);
            linestrip[3].Color = Color.LightBlue;
        }

        private void InitTriangleStrip()
        {
            trianglestrip = new VertexPositionColor[5];
            //Første
            trianglestrip[0].Position = new Vector3(-1.0f, -1.0f, 0.0f);
            trianglestrip[0].Color = Color.LightPink;
            trianglestrip[1].Position = new Vector3(0.0f, 1.0f, 0.0f);
            trianglestrip[1].Color = Color.LightPink;
            trianglestrip[2].Position = new Vector3(0.0f, -1.0f, 0.0f);
            trianglestrip[2].Color = Color.LightPink;
            //Andre
            trianglestrip[3].Position = new Vector3(1.0f, 1.0f, 0.0f);
            trianglestrip[3].Color = Color.Pink;
            //Tredje
            trianglestrip[4].Position = new Vector3(1.0f, -1.0f, 0.0f);
            trianglestrip[4].Color = Color.HotPink;
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            device.Clear(Color.Black);
            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            rasterizerState1.FillMode = FillMode.WireFrame;
            device.RasterizerState = rasterizerState1;


            //Setter world=I:
            world = Matrix.Identity;
            // Setter world-matrisa på effect-objektet (verteks-shaderen):
            effect.World = world;

            //Starter tegning - må bruke effect-objektet:
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall 
                // primitiver (her 1 siden verteksene beskriver en trekant):
                //Trekant fra Trekant1
                device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1, VertexPositionColor.VertexDeclaration);
                //2 enkle linjer
                device.DrawUserPrimitives(PrimitiveType.LineList, linelist, 0, 1);
                device.DrawUserPrimitives(PrimitiveType.LineList, linelist2, 0, 1);
                //3 sammenhengende linjer
                device.DrawUserPrimitives(PrimitiveType.LineStrip, linestrip, 0, 3);
                //Trekant
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, trianglestrip, 0, 3);
            }

            base.Draw(gameTime);
        }
    }
}
