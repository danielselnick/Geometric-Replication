using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FarseerPhysics.DebugViews;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;

namespace GeometricReplication
{
    class CirclePlayer : Actor
    {
        private Texture2D convertedEnemyTexture;
        Particles collisionParticles = new Particles();
        public CirclePlayer()
        {
            moveSpeed = 50000.0f;
            width = 40f;
            height = 40f;
        }

        public void Init()
        {
            userData = new UserData(1, 1, true);
            userData.bIsPlayer = true;
            base.Init();
            myBody.BodyType = BodyType.Dynamic;
            myBody.Position = new Vector2(400, 400);
            myBody.LinearDamping = 0.0001f;
            myBody.FixtureList[0].Restitution = 1.0f;
           
            texture = theMaster.game.Content.Load<Texture2D>("images/RedCircleHero");
            convertedEnemyTexture = theMaster.game.Content.Load<Texture2D>("images/RedCircle");
            collisionParticles.setTexture(0, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle1"));
            collisionParticles.setTexture(1, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle2"));
            collisionParticles.setTexture(2, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle3"));
            collisionParticles.setTexture(3, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle4"));
        }

        public override void Update()
        {
            GamePadState gps = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            
            Vector2 moveDirection = Vector2.Zero;

            if (gps != lastGPS)
            {
                moveDirection = gps.ThumbSticks.Right;
                moveDirection.Y *= -1;
            }
            lastGPS = gps;
            // keyboard input
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                moveDirection.X -= 1.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                moveDirection.Y += 1.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                moveDirection.Y -= 1.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                moveDirection.X += 1.0f;
            }
            //Move(moveDirection);
            float factor = Vector2.Dot(myBody.LinearVelocity, moveDirection);
            Console.WriteLine(factor);
            float speedscale = (factor > 0 ? 1 : 6);
            myBody.ApplyForce(moveDirection * moveSpeed * theMaster.dt * speedscale);
            Console.WriteLine(myBody.LinearVelocity);
            lastKeyboardState = Keyboard.GetState(); 
  
        }

        public void drawCollisionParticles(SpriteBatch sb, GameTime gameTime)
        {
            collisionParticles.drawParticles(sb, gameTime);
        }

        public override void OnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            UserData data = (UserData)f2.UserData;
            // if we're colliding with an enemy that is currently not our own
            if (!data.bIsPlayer && (data.playerID != this.userData.playerID))
            {
                Enemy enemy = theMaster.GetEnemy(data.ID);
                enemy.texture = convertedEnemyTexture;
                data.playerID = 1;
                theMaster.returnGameSound.playSpecificSoundFx(0);
                collisionParticles.addParticle(new Vector2(Position.X - 40, Position.Y - 40), 50, 25, Color.Red);
            }
        }
    }
}
