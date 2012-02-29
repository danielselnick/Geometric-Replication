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

namespace GeometricReplication
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Master master;
        StartMenu gStart;
        GameEnd gEnd;
        Credits gCredits;
        private int creditsY;
        private double currentTime;
        private Texture2D creditsScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            master = new Master(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            creditsScreen = this.Content.Load<Texture2D>("images/Credits");
            // TODO: Add your initialization logic here
            Master.theMaster = this.master;
            master.InitializeSfx(this);
            master.Initialize();
            base.Initialize();

            gStart = new StartMenu(this);
            gEnd = new GameEnd(this);
            gCredits = new Credits(this);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            if (gStart.gameStart)
            {
                if (gEnd.gameReset && !gEnd.doOnce)
                {
                    gCredits.isDrawing = true;
                    gCredits.curState = true;
                    gCredits.prevState = true;
                    gEnd.doOnce = true;
                }
                else if (gEnd.gameReset && gCredits.gameReset)
                {
                    master.GameTimeLeft = 1;
                    master.returnScore[0] = 0;
                    master.returnScore[1] = 0;
                    gStart.gameStart = false;
                    gStart.curState = true;
                    gStart.prevState = true;
                    master.roundOver = false;
                    gEnd.gameReset = false;
                    gCredits.gameReset = false;
                    gEnd.doOnce = false;
                    gCredits.isDrawing = false;
                }
                if (!gCredits.isDrawing && !gEnd.gameReset)
                    master.Update(gameTime);
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

            if (!gStart.gameStart)
                gStart.drawMenu(this, gameTime);
            else if (master.roundOver && gStart.gameStart && !gCredits.isDrawing)
                gEnd.Draw(this, master);
            else if (master.roundOver && gStart.gameStart && gCredits.isDrawing)
            {
                gCredits.Draw(this);
            }
            else
                master.Draw(this, gameTime);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
