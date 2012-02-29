using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GeometricReplication
{
    class Leaderboard
    {
        //wherever your declarations are
        SpriteFont Arial;
        Color fontColor = Color.White;
        string player1Score;
        string player2Score;
        string gameTimer;

        double gTimeSeconds = 0;
        double gTimeMinutes = 0;

        public int countDownSecs = 0;
        public int countDownMins = 0;

        public bool doOnce = false;
        Texture2D CircleBar, SquareBar, NeutralBar;

        // in LoadContent()
        public Leaderboard(Game1 cGame)
        {
            Arial = cGame.Content.Load<SpriteFont>("SpriteFont1");
            CircleBar = cGame.Content.Load<Texture2D>("images/Bar/CircleBar");
            SquareBar = cGame.Content.Load<Texture2D>("images/Bar/SquareBar");
            NeutralBar = cGame.Content.Load<Texture2D>("images/Bar/NeutralBar");
            countDownSecs = 5;
            countDownMins = 5;
        }

        public void drawLeaderboard(Game1 cGame, GameTime gameTime, Master cScore, int CircleCount, int NeutralCount, int SquareCount, int maxEnemyNumber)
        {
            if (!doOnce)
            {
                gTimeSeconds = gameTime.TotalGameTime.TotalSeconds;
                gTimeMinutes = gameTime.TotalGameTime.TotalMinutes;
                countDownSecs = 60;
                countDownMins = cScore.GameTimeLeft - 1;
                doOnce = true;
            }
            if (gTimeSeconds + 1 < gameTime.TotalGameTime.TotalSeconds)
            {
                if (countDownSecs <= 0 && countDownMins >= 0)
                    countDownSecs = 60;
                else if (countDownSecs > 0)
                    countDownSecs--;
                gTimeSeconds = gameTime.TotalGameTime.TotalSeconds;
            }
            if (gTimeMinutes + 1 < gameTime.TotalGameTime.TotalMinutes)
            {
                countDownMins--;
                gTimeMinutes = gameTime.TotalGameTime.TotalMinutes;
            }
            cScore.GameTimeLeft = countDownMins;

            if (countDownMins < 10 && countDownSecs < 10)
                gameTimer = "Time Left: 0" + countDownMins + ":0" + countDownSecs;
            else if (countDownMins < 10)
                gameTimer = "Time Left: 0" + countDownMins + ":" + countDownSecs;
            else if (countDownSecs < 10)
                gameTimer = "Time Left: " + countDownMins + ":0" + countDownSecs;
            else
                gameTimer = "Time Left: " + countDownMins + ":" + countDownSecs;
            
            player1Score = "Square Score: " + cScore.returnScore[0];
            player2Score = "Circle Score: " + cScore.returnScore[1];
            //20 apart on y-axis
            //fuxk x axis
            cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            cGame.spriteBatch.DrawString(Arial, player1Score, new Vector2(5.0f, 5.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            cGame.spriteBatch.DrawString(Arial, player2Score, new Vector2(600.0f, 5.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            cGame.spriteBatch.DrawString(Arial, gameTimer, new Vector2(300.0f, 5.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));

            cGame.spriteBatch.Draw(CircleBar, new Rectangle(0, 550, 800 * CircleCount / maxEnemyNumber, 50), Color.White);
            cGame.spriteBatch.Draw(NeutralBar, new Rectangle(800 * CircleCount / maxEnemyNumber, 550, 800 * NeutralCount / maxEnemyNumber, 50), Color.White);
            cGame.spriteBatch.Draw(SquareBar, new Rectangle(800 * (CircleCount + NeutralCount) / maxEnemyNumber, 550, 800 * SquareCount / maxEnemyNumber, 50), Color.White);
            cGame.spriteBatch.End();
        }
    }
}
