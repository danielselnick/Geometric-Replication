using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Contacts;

namespace GeometricReplication
{
    class Enemy : Actor
    {
        bool waitTillNext = false;
        double timeCheck;
        Vector2 targetedPosition;
        public Enemy()
        {
            moveSpeed = radius = density = width = height = 10.0f;
            width = 25f;
            height = 25f;
        }

        public void Init(int id, Texture2D yourtexture)
        {
            texture = yourtexture;
            userData = new UserData(id, -1, false);
            base.Init();            
            myBody.FixtureList[0].Restitution = 1;
        }

        public override void Update()
        {
            
        }

        public void movementUpdate(GameTime gameTime)
        {
            Random rand = new Random();
            if (!waitTillNext)
            {
                timeCheck = gameTime.TotalGameTime.TotalSeconds;
                targetedPosition = new Vector2(rand.Next(0, 1000), rand.Next(0, 1000));
                Move(targetedPosition);
                waitTillNext = true;
            }
            else
            {
                if (timeCheck + 59 < gameTime.TotalGameTime.TotalSeconds)
                {
                    waitTillNext = false;
                }
            }
        }

        public Vector2 returnTargetPos
        {
            get { return targetedPosition; }
        }

        public override void OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            
        }


        public void ShootInDirection(Vector2 direction)
        {
            targetedPosition = -direction;
            myBody.ApplyForce(targetedPosition * -10000);
        }
    }
}
