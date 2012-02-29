using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GeometricReplication
{
    class Particles
    {
        const int MAX_PARTICLES = 300;

        private Texture2D[] particleTextures = new Texture2D[4];
        private List<Vector2> particlePos = new List<Vector2>();
        private List<float> secondsToLive = new List<float>();
        private List<bool> isAlive = new List<bool>();
        private List<Color> color = new List<Color>();
        Random dRandom = new Random();

        double timeCheck = 0;

        public void setTexture(int index, Texture2D Texture)
        {
            particleTextures[index] = Texture;
        }

        public void addParticle(Vector2 sPos, int nAmount, float sToLive, Color colorParticle)
        {
            Vector2 basePos = sPos;
            for (int i = 0; i < nAmount; i++)
            {
                if (particlePos.Count < MAX_PARTICLES)
                {
                    sPos = new Vector2(basePos.X + dRandom.Next(-15, 15), basePos.Y + dRandom.Next(-15, 15));
                    particlePos.Add(sPos);
                    secondsToLive.Add(sToLive);
                    isAlive.Add(true);
                    color.Add(colorParticle);
                }
            }
        }

        private void tickSeconds(GameTime gameTime)
        {
            if (timeCheck + 1 < gameTime.TotalGameTime.Milliseconds)
            {
                timeCheck = gameTime.TotalGameTime.Seconds;
                if (secondsToLive.Count > 0)
                {
                    for (int i = 0; i < secondsToLive.Count; i++)
                    {
                        if (secondsToLive[i] <= 0)
                        {
                            isAlive.Remove(isAlive[i]);
                            secondsToLive.Remove(secondsToLive[i]);
                            particlePos.Remove(particlePos[i]);
                            color.Remove(color[i]);
                        }
                        else
                            secondsToLive[i] -= 1;
                    }
                }
            }
        }

        public void drawParticles(SpriteBatch sb, GameTime gameTime)
        {
            tickSeconds(gameTime);
            if (isAlive.Count > 0)
            {
                for (int i = 0; i < isAlive.Count; i++)
                {
                    if (isAlive[i])
                        sb.Draw(particleTextures[dRandom.Next(0, 3)], new Rectangle((int)particlePos[i].X, (int)particlePos[i].Y, 80, 80), color[i]);
                }
            }
        }
    }
}
