using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GeometricReplication
{
    class Credits
    {
        public bool isDrawing = false;
        public bool doOnce = false;
        public bool gameReset = false;
        public bool curState = false;
        public bool prevState = false;
        Texture2D backgroundImg, creditsScreen;
        SpriteFont Arial;
        Color fontColor = Color.White;

        private int creditsY;
        private double currentTime;

        public Credits(Game1 cGame)
        {
            creditsScreen = cGame.Content.Load<Texture2D>("images/Credits");
            backgroundImg = cGame.Content.Load<Texture2D>("images/background");
            Arial = cGame.Content.Load<SpriteFont>("SpriteFont1");
        }

        private void update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentTime > 0.2)
            {
                creditsY++;
                currentTime -= 0.2;
                //if (creditsY > 1300)
                //{
                //    gCredits.isDrawing = false;
                //}
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                if (curState != prevState)
                {
                    curState = true;
                    gameReset = true;
                    
                }
                prevState = curState;
            }
            curState = false;
        }

        public void Draw(Game1 cGame)
        {
            cGame.spriteBatch.Begin();
            cGame.spriteBatch.Draw(creditsScreen, new Rectangle(0, 0, 800, 600), new Rectangle(0, creditsY, 800, 600), Color.White);
            cGame.spriteBatch.End();
            

            //cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            ///************/
            
            
            //cGame.spriteBatch.Draw(backgroundImg, new Vector2(0, 0), Color.White);
            //cGame.spriteBatch.End();

            //cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //cGame.spriteBatch.DrawString(Arial, "Credits ", new Vector2(325.0f, 100.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            //cGame.spriteBatch.DrawString(Arial, "Programmers: ", new Vector2(125.0f, 225.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            //cGame.spriteBatch.DrawString(Arial, "Brett Faulds", new Vector2(125.0f, 245.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            //cGame.spriteBatch.DrawString(Arial, "Daniel Selnick", new Vector2(125.0f, 265.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            //cGame.spriteBatch.DrawString(Arial, "Even Cheng", new Vector2(125.0f, 285.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            //cGame.spriteBatch.DrawString(Arial, "Rj McKeenHan", new Vector2(125.0f, 305.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));

            //cGame.spriteBatch.DrawString(Arial, "Artist: ", new Vector2(325.0f, 225.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            //cGame.spriteBatch.DrawString(Arial, "Even Cheng", new Vector2(325.0f, 245.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            //cGame.spriteBatch.DrawString(Arial, "Brian Maralis", new Vector2(325.0f, 265.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            
            //cGame.spriteBatch.End();
        }
    }
}
