
#region Using Statements
using System;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Particles;
#endregion

namespace ParticlesScreenSaver
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PScreenSaverGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static InputManager inputManager;        //this input manager is a game component that will exit the game
        //if the mouse or keyboard state change from their initial state

        Color [] colors;
        List<Attractor> attractors;
        List<ParticleSystem> fountains;
        ParticleSystemVisualizer psv;
        Timer timer;

        int width = 1440;
        int height = 900;

        float scale = 10f;
        Random random;

        public PScreenSaverGame()
        {
            graphics = new GraphicsDeviceManager(this);
            this.Content = new ResourceContentManager(Services, PSResources.ResourceManager);

            width = Screen.PrimaryScreen.Bounds.Width;
            height = Screen.PrimaryScreen.Bounds.Height;
            random = new Random();

            colors = new Color[3] { Color.Red, Color.Green, Color.Blue };

            ReGen();

            psv = new ParticleSystemVisualizer(null, Vector2.Zero, scale);
            timer = new Timer();
            timer.Interval = 10 * 1000;
            timer.TimeElapsed += new EventHandler(timer_TimeElapsed);

            IsFixedTimeStep = true;
        }

        void timer_TimeElapsed(object sender, EventArgs e) {
            ReGen();
        }

        private void ReGen() {
            attractors = CreateAttractors();
            fountains = CreateFountains();
            SetAttractors();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region Makes the graphics full screen and hides the mouse pointer

            //set the screen width and height to the screen width and height
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;

            //enable fullscreen mode
            graphics.IsFullScreen = true;

            //finally, apply the changes
            graphics.ApplyChanges();

            //hide the mouse pointer
            this.IsMouseVisible = false;

            #endregion

            inputManager = new InputManager(this);      //initialize the InputManager
            this.Components.Add(inputManager);          //and add it to the components

            base.Initialize();
        }


        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            psv.LoadContent(Content);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            for (int i = 0; i < fountains.Count; i++) {
                fountains[i].Update();
            }

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            for (int i = 0; i < fountains.Count; i++) {
                var ps = fountains[i];
                psv.Position = ps.Position.ToVector2() + new Vector2(width / 2, height / 2);
                psv.Color = colors[i];
                psv.Draw(spriteBatch, ps);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void SetAttractors() {
            foreach (var f in fountains) {
                f.Attractors.Clear();
                f.Attractors.AddRange(attractors);
            }
        }

        List<Attractor> CreateAttractors() {
            List<Attractor> ats = new List<Attractor>();
            var num = random.Next(2, 7);
            for (int i = 0; i < num; i++) {
                ats.Add(CreateAttractor());
            }
            return ats;
        }

        Attractor CreateAttractor() {
            return new Attractor(new PVector2((FInt)random.Next(-25, 25), (FInt)random.Next(-25, 25)));
        }

        private List<ParticleSystem> CreateFountains() {
            List<ParticleSystem> pts = new List<ParticleSystem>();
            var num = 3;
            for (int i = 0; i < num; i++) {
                pts.Add(CreateParticleSystem());
            }
            return pts;
        }

        ParticleSystem CreateParticleSystem() {
            return new ZeroFountain(random, new PVector2((FInt)random.Next(-30, 30), (FInt)random.Next(-30, 30)));
        }
    }
}