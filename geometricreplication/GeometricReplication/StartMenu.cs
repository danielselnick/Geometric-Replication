using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GeometricReplication
{
    class StartMenu
    {
        int curSelection = 0;
        public int menuSection = 0;
        public bool curState = false;
        public bool prevState = true;
        public bool gameStart = false;

        Texture2D backgroundImg;
        Texture2D backgroundImg2;
        Texture2D HowToPlay;
        Texture2D Start;
        Texture2D Credits;
        Texture2D Exit;
        Texture2D htpScreen;
        
        Color[] itemColor = new Color[4];

        SpriteFont Arial;
        Color fontColor = Color.White;
        private Texture2D creditsScreen;
        private double currentTime;
        private int creditsY;

        public StartMenu(Game1 cGame)
        {
            creditsScreen = cGame.Content.Load<Texture2D>("images/Credits");
            backgroundImg = cGame.Content.Load<Texture2D>("images/Main_Title");
            HowToPlay = cGame.Content.Load<Texture2D>("images/how_to_play");
            Start = cGame.Content.Load<Texture2D>("images/Start");
            Credits = cGame.Content.Load<Texture2D>("images/Credits_menu");
            Exit = cGame.Content.Load<Texture2D>("images/Exit");
            htpScreen = cGame.Content.Load<Texture2D>("images/HowToPlay");
            backgroundImg2 = cGame.Content.Load<Texture2D>("images/background");
            Arial = cGame.Content.Load<SpriteFont>("SpriteFont1");
        }

        private void update(Game1 cGame, GameTime gameTime)
        {
            if (menuSection == 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                {
                    if (curState != prevState)
                    {
                        curState = true;
                        curSelection++;
                    }
                    prevState = curState;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                {
                    if (curState != prevState)
                    {
                        curState = true;
                        curSelection--;
                    }
                    prevState = curState;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
                {
                    if (curSelection == 1)
                    {
                        if (curState != prevState)
                        {
                            curState = true;
                            menuSection = 1;
                        }
                        prevState = curState;
                    }
                    else if (curSelection == 2)
                    {
                        if (curState != prevState)
                        {
                            curState = true;
                            gameStart = true;
                        }
                        prevState = curState;
                    }
                    else if (curSelection == 3)
                    {
                        if (curState != prevState)
                        {
                            curState = true;
                            menuSection = 2;
                        }
                        prevState = curState;
                    }
                    else if (curSelection == 4)
                    {
                        if (curState != prevState)
                        {
                            curState = true;
                            cGame.Exit();
                        }
                        prevState = curState;
                    }
                }
                else
                    curState = false;

                if (curSelection > 4)
                    curSelection = 0;
                else if (curSelection < 0)
                    curSelection = 4;

                switch (curSelection)
                {
                    case 0:
                        itemColor[0] = Color.White;
                        itemColor[1] = Color.White;
                        itemColor[2] = Color.White;
                        itemColor[3] = Color.White;
                        break;
                    case 1:
                        itemColor[0] = Color.Turquoise;
                        itemColor[1] = Color.White;
                        itemColor[2] = Color.White;
                        itemColor[3] = Color.White;
                        break;
                    case 2:
                        itemColor[0] = Color.White;
                        itemColor[1] = Color.Turquoise;
                        itemColor[2] = Color.White;
                        itemColor[3] = Color.White;
                        break;
                    case 3:
                        itemColor[0] = Color.White;
                        itemColor[1] = Color.White;
                        itemColor[2] = Color.Turquoise;
                        itemColor[3] = Color.White;
                        break;
                    case 4:
                        itemColor[0] = Color.White;
                        itemColor[1] = Color.White;
                        itemColor[2] = Color.White;
                        itemColor[3] = Color.Turquoise;
                        break;
                }
            }
            else if (menuSection == 1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (curState != prevState)
                    {
                        curState = true;
                        menuSection = 0;
                    }
                    prevState = curState;
                }
                else
                    curState = false;
            }
            else if (menuSection == 2)
            {
                currentTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (currentTime > 0.01)
                {
                    creditsY++;
                    currentTime -= 0.01;
                    if (creditsY > 1300)
                    {
                        curState = true;
                        menuSection = 0;
                        creditsY = 0;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (curState != prevState)
                    {
                        curState = true;
                        menuSection = 0;
                        creditsY = 0;
                    }
                    prevState = curState;
                }
                else
                    curState = false;
            }
        }

        public void drawMenu(Game1 cGame, GameTime gameTime)
        {
            update(cGame, gameTime);


            cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            cGame.spriteBatch.Draw(backgroundImg, new Vector2(0, 0), Color.White);
            cGame.spriteBatch.End();
            cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
            if (menuSection == 0)
            {
                cGame.spriteBatch.Draw(HowToPlay, new Vector2(225, 325), itemColor[0]);
                cGame.spriteBatch.Draw(Start, new Vector2(325, 385), itemColor[1]);
                cGame.spriteBatch.Draw(Credits, new Vector2(325, 445), itemColor[2]);
                cGame.spriteBatch.Draw(Exit, new Vector2(325, 505), itemColor[3]);
                cGame.spriteBatch.End();
            }
            else if (menuSection == 1)
            {
                cGame.spriteBatch.Draw(htpScreen, new Vector2(0, 0), Color.White);
                cGame.spriteBatch.End();
            }
            else if (menuSection == 2)
            {
                cGame.spriteBatch.End();
                cGame.spriteBatch.Begin();
                cGame.spriteBatch.Draw(creditsScreen, new Rectangle(0, 0, 800, 600), new Rectangle(0, creditsY, 800, 600), Color.White);
                cGame.spriteBatch.End();
                //cGame.spriteBatch.End();
                //cGame.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);
                //cGame.spriteBatch.Draw(backgroundImg2, new Vector2(0, 0), Color.White);
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
}
