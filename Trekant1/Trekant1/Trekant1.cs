using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Trekant1
{
    /// <summary>
    /// Et XNA klasse som tegne opp en enkel trekant.
    /// </summary>
    public class Trekant1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;      //Representerer tegneflata.

        private BasicEffect effect;
        private VertexDeclaration mVertPosColor;

        //Liste med vertekser:
        private VertexPositionColor[] vertices, vertices2;

        //WVP-matrisene:
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        //Kameraposisjon:
        private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 5.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = new Vector3(0.0f, 1.0f, 0.0f);

        SpriteBatch spriteBatch;

        /// <summary>
        /// Konstruktør. Henter ut et graphics-objekt.
        /// </summary>
        public Trekant1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(this.Services);
            //Gjør at musepekeren er synlig over vinduet:
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Her legger man initialiseringskode.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            InitDevice();
            InitCamera();
            InitVertices();
        }

        /// <summary>
        /// Diverse initilaliseringer. 
        /// Henter ut device-objektet.
        /// </summary>
        private void InitDevice()
        {
            device = graphics.GraphicsDevice;

            //Setter størrelse på framebuffer:
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Window.Title = "En enkel trekant... HERP DERP";

            //Initialiserer Effect-objektet:
            effect = new BasicEffect(graphics.GraphicsDevice, null);

            //Setter verteksformat - forteller grafikkortet hvilken type vertekser som brukes:
            mVertPosColor = new VertexDeclaration(graphics.GraphicsDevice,
                VertexPositionColor.VertexElements);

            effect.VertexColorEnabled = true;
        }

        /// <summary>
        /// Stiller inn kameraet.
        /// </summary>
        private void InitCamera()
        {

            //Projeksjon:
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            //Oppretter view-matrisa:
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);

            //Oppretter projeksjonsmatrisa:
            Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(10.0f), aspectRatio, 1.0f, 1000.0f, out projection);

            //Gir matrisene til shader:
            effect.Projection = projection;
            effect.View = view;
        }

        /// <summary>
        /// Vertekser for trekanten.
        /// </summary>
        private void InitVertices()
        {
            vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0.0f);
            vertices[1].Position = new Vector3(0.0f, 0.5f, 0.0f);
            vertices[2].Position = new Vector3(0.5f, -0.5f, 0.0f);

            vertices[0].Color = Color.Red;
            vertices[1].Color = Color.Green;
            vertices[2].Color = Color.Blue;

            vertices2 = new VertexPositionColor[3];
            vertices2[0].Position = new Vector3(1.0f, -0.5F, 0.0F);
            vertices2[1].Position = new Vector3(1.5f, 0.5F, 0.0F);
            vertices2[2].Position = new Vector3(2.0f, -0.5F, 0.0F);

            vertices2[0].Color = Color.Blue;
            vertices2[1].Color = Color.Green;
            vertices2[2].Color = Color.Red;

        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            device.RenderState.FillMode = FillMode.Solid;

            device.Clear(Color.Black);

            //Setter world=I:
            world = Matrix.Identity;

            // Setter world-matrisa på effect-objektet (verteks-shaderen):
            effect.World = world;

            //Starter tegning - må bruke effect-objektet:
            effect.Begin();
            effect.Techniques[0].Passes[0].Begin();
            // Setter vertekstype:
            device.VertexDeclaration = mVertPosColor;
            // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall primitiver (her 1 siden verteksene beskriver en trekant):
            device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
            device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices2, 0, 1);
            effect.Techniques[0].Passes[0].End();
            effect.End();

            base.Draw(gameTime);
        }
    }
}
