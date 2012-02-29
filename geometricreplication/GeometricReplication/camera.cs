using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GeometricReplication
{
    class camera
    {
        public Vector2 position;
        public float rotation;
        public Vector2 origin;
        public float scale;
        public Vector2 screencenter;
        public static Matrix transform;
        public static Matrix translate;
        public float movespeed= .2f;
        public float maxscale= 1.5f;
        public float minscale=.0001f;
        public float scalespeed = .2f;
        private float viewportheight;
        private float viewportwidth;
        public int offset;

        public camera(Game game, int yoffset) 
        {
            viewportwidth = game.GraphicsDevice.Viewport.Width;
            viewportheight = game.GraphicsDevice.Viewport.Height - yoffset;
            screencenter = new Vector2(viewportwidth / 2, viewportheight / 2);
            scale = 1;
            movespeed = .25f;
        }
        
        public void update(GameTime gametime, Master master)
        {            
            //calculate the transform matrix
            transform = Matrix.Identity *
                //translate by negative position
                Matrix.CreateTranslation(-position.X, -position.Y, 0) *
                Matrix.CreateScale(scale) *
                //rotate about z axis
                //math.createrotationz(1.75f) *
                //translate by the origin amount
                Matrix.CreateTranslation(viewportwidth / 2, viewportheight / 2, 0);
            translate = Matrix.Identity *
                //translate by negative position
                Matrix.CreateTranslation(-position.X, -position.Y, 0) *
                //rotate about z axis
                //math.createrotationz(1.75f) *
                //translate by the origin amount
                Matrix.CreateTranslation(viewportwidth / 2, viewportheight / 2, 0);
            //delta so the movement seems smoooth
            float delta = (float)gametime.ElapsedGameTime.TotalSeconds;
            //get a rectangle which contains all the players
            Rectangle players = master.playerspace();
            //pad the rectangle
            players.X -= 200;
            players.Y -= 200;
            players.Width += 400;
            players.Height += 400;
            position = smoothstep(position, new Vector2(players.Center.X, players.Center.Y), movespeed); 
             
            //create a scale based on the the size of the player rectangle in respect to the original viewport size
            float xscale = viewportwidth / players.Width;
            float yscale = viewportheight/ players.Height;
            //use the largest scaling value, the smaller value scale will still include all the picture from the larger scale
            float newscale = (xscale < yscale) ? xscale : yscale;
            //clamp the scale to the min or max
            newscale = clamp(newscale, minscale, maxscale);
            //smooth the scaling
            scale = smoothstep(scale, newscale, scalespeed);
            //currently not using the rotation, but it is in there for future reference
        }

        public Vector2 smoothstep(Vector2 vector0, Vector2 vector1, float value)
        {
            //fancy clamp to make sure the value is between 0 and 1
            value = (value > 1f) ? 1f : ((value < 0f) ? 0f : value);
            //math!
            value = (value * value) * (3f - (2f * value));
            //step the values up to the desired value
            vector0.X = vector0.X + ((vector1.X - vector0.X) * value);
            vector0.Y = vector0.Y + ((vector1.Y - vector0.Y) * value);
            //return new vector
            return vector0;
        }

        float clamp(float value, float min, float max)
        {
            //if the value is above the max, set it to the max
            value = (value > max) ? max : value;
            //if the value is below the min, set it to the min
            value = (value < min) ? min : value;
            return value;
        }

        static float smoothstep(float value1, float value2, float amount)
        {
            float num = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            float value = (num * num) * (3f - (2f * num));
            float lerp = (value1 + ((value2 - value1) * amount));
            return lerp;
        }
    }
}
