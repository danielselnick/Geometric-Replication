using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.DebugViews;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Collision.Shapes;

namespace GeometricReplication
{
    class SquarePlayer : Actor
    {
        private Texture2D convertedEnemyTexture;
        Particles collisionParticles = new Particles();
        public SquarePlayer()
        {
            moveSpeed = 50000.0f;
            width = 40.0f;
            height = 40.0f;
        }

        public void Init()
        {
            userData = new UserData(0, 0, true);
            userData.bIsPlayer = true;
            base.Init();
            myBody.Position = new Vector2(200, 200);
            myBody.BodyType = BodyType.Dynamic;
            myBody.FixtureList[0].Restitution = 1.0f;
            myBody.LinearDamping = 0.0001f;
            texture = theMaster.game.Content.Load<Texture2D>("images/BlueSquareHero");
            convertedEnemyTexture = theMaster.game.Content.Load<Texture2D>("images/BlueSquare");
            collisionParticles.setTexture(0, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle1"));
            collisionParticles.setTexture(1, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle2"));
            collisionParticles.setTexture(2, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle3"));
            collisionParticles.setTexture(3, theMaster.game.Content.Load<Texture2D>("images/Particles/Particle4"));
        }

        public override void Update()
        {
            GamePadState gps = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
            Vector2 moveDirection = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();

            if (gps != lastGPS)
            {
                moveDirection = gps.ThumbSticks.Left;
                moveDirection.Y *= -1;
            }
            lastGPS = gps;
            // keyboard input
            if (keyboardState.IsKeyDown(Keys.A))
            {
                moveDirection.X -= 1.0f;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                moveDirection.Y += 1.0f;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                moveDirection.Y -= 1.0f;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                moveDirection.X += 1.0f;
            }
            
            float factor = Vector2.Dot(myBody.LinearVelocity, moveDirection);
            Console.WriteLine(factor);
            float speedscale = (factor > 0 ? 1 : 6);
            myBody.ApplyForce(moveDirection * moveSpeed * theMaster.dt * speedscale);
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
                data.playerID = 0;
                theMaster.returnGameSound.playSpecificSoundFx(0);
                collisionParticles.addParticle(new Vector2(Position.X - 40, Position.Y - 40), 50, 25, new Color(26, 121, 194));
            }   
        }
    }
}
