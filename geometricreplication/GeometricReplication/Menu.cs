using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
namespace GeometricReplication
{
    class Menu
    {
         //wherever your declarations are
        SpriteFont Arial;
        Color fontColor = Color.Red;
        string Start;
        string Quit;

        // in LoadContent()
        public Menu(Game1 cGame)
        {
            Arial = cGame.Content.Load<SpriteFont>("SpriteFont1");
        }

        public void drawLeaderboard(Game1 cGame, Master cScore)
        {
            Start = "Start Game";
            Quit = "Exit Game";

            cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            cGame.spriteBatch.DrawString(Arial, Start, new Vector2(400.0f, 200.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            cGame.spriteBatch.DrawString(Arial, Quit, new Vector2(400.0f, 400.0f), new Color((byte)fontColor.R, (byte)fontColor.G, (byte)fontColor.B));
            cGame.spriteBatch.End();
    }
}
