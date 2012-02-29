using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GeometricReplication
{
    class GameEnd
    {
        public bool doOnce = false;
        public bool gameReset = false;
        public bool curState = false;
        public bool prevState = false;
        string player1Score;
        string player2Score;
        Texture2D backgroundImg;
        SpriteFont Arial;
        Color fontColor = Color.Black;

        public GameEnd(Game1 cGame)
        {
            backgroundImg = cGame.Content.Load<Texture2D>("images/round_over");
            Arial = cGame.Content.Load<SpriteFont>("SpriteFont1");
        }

        private void update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                curState = true;
                if (curState != prevState)
                {
                    gameReset = true;
                }
                prevState = curState;
            }
            else
                curState = false;
        }

        public void Draw(Game1 cGame, Master cScore)
        {
            update();

            cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            cGame.spriteBatch.Draw(backgroundImg, new Vector2(0, 0), Color.White);
            cGame.spriteBatch.End();

            player1Score = "Player 1 Score: " + cScore.returnScore[0];
            player2Score = "Player 2 Score: " + cScore.returnScore[1];
            cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            cGame.spriteBatch.DrawString(Arial, player1Score, new Vector2(325.0f, 300.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            cGame.spriteBatch.DrawString(Arial, player2Score, new Vector2(325.0f, 320.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            cGame.spriteBatch.End();
        }
    }
}
