using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GeometricReplication
{
    class GameBackground
    {
        Texture2D backgroundToDraw1;
        Texture2D [] backgroundToDraw = new Texture2D[8];
        double localTime = 0;
        int localTimeCalculator = 0;
        int lastBGPointer = 7;
        int currentBGPointer = 0;

        public GameBackground(Game1 cGame)
        {
            backgroundToDraw1 = cGame.Content.Load<Texture2D>("images/background");
            backgroundToDraw[0] = cGame.Content.Load<Texture2D>("images/BG/bg1");
            backgroundToDraw[1] = cGame.Content.Load<Texture2D>("images/BG/bg2");
            backgroundToDraw[2] = cGame.Content.Load<Texture2D>("images/BG/bg3");
            backgroundToDraw[3] = cGame.Content.Load<Texture2D>("images/BG/bg4");
            backgroundToDraw[4] = cGame.Content.Load<Texture2D>("images/BG/bg5");
            backgroundToDraw[5] = cGame.Content.Load<Texture2D>("images/BG/bg6");
            backgroundToDraw[6] = cGame.Content.Load<Texture2D>("images/BG/bg7");
            backgroundToDraw[7] = cGame.Content.Load<Texture2D>("images/BG/bg8");
        }

        public void Update(GameTime gameTime)
        {
            localTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (localTime > 0.1)
            {
                localTimeCalculator += 5;
                localTime -= 0.1;

                if (localTimeCalculator > 250)
                {
                    localTimeCalculator = 0;
                    lastBGPointer++;
                    currentBGPointer++;
                    if (lastBGPointer > 7)
                        lastBGPointer = 0;
                    if (currentBGPointer > 7)
                        currentBGPointer = 0;
                }
            }
        }

        public Texture2D returnBackgroundTexture
        {
            get { return backgroundToDraw[3]; }
        }

        public void drawThis(SpriteBatch sb)
        {
            //sb.Draw(backgroundToDraw1, new Vector2(0, 0), Color.Black);
            sb.Draw(backgroundToDraw[lastBGPointer], new Rectangle(-750, -750, 2500, 2500), new Color(255, 255, 255, (155 - localTimeCalculator)));
            sb.Draw(backgroundToDraw[currentBGPointer], new Rectangle(-750, -750, 2500, 2500), new Color(255, 255, 255, localTimeCalculator));
            
        }
    }
}
