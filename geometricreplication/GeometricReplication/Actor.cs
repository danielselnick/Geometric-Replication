using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.DebugViews;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework.Graphics;

namespace GeometricReplication
{
    public enum ShapeType
    {
        Circle = 0, Square
    }
    abstract class Actor
    {
        // shortcut so you don't have to do Master.theMaster
        protected Master theMaster;
        protected DebugViewXNA theRenderman;
        protected Body myBody;
        public UserData userData;
        protected float moveSpeed;
        protected float radius;
        protected float density;
        protected float width;
        protected float height;        

        protected KeyboardState lastKeyboardState;
        protected GamePadState lastGPS;        

        public Texture2D texture;

        
        
        public Actor()
        {
            theMaster = Master.theMaster;
            theRenderman = theMaster.Renderer;
            if (moveSpeed == 0)
            moveSpeed = radius = density = width = height = 10f;            
        }

        public virtual void Init()
        {
            myBody = new Body(theMaster.theWorld, userData);
            myBody.BodyType = BodyType.Dynamic;            
            Vertices verts = PolygonTools.CreateRectangle(width / 2, height / 2);
            myBody.CreateFixture(new PolygonShape(verts, 0.0f), userData);
            myBody.FixtureList[0].AfterCollision += OnCollision;
            myBody.IsBullet = true;
        }

        public abstract void Update();

        public abstract void OnCollision(Fixture f1, Fixture f2, Contact contact);

        public void Move(Vector2 direction)
        {
            //myBody.LinearVelocity = (direction * moveSpeed);
        }

        public bool isKeyPressed(Keys key)
        {
            if (Keyboard.GetState() != lastKeyboardState)
            {
                if (Keyboard.GetState().IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isKeyReleased(Keys key)
        {
            if (lastKeyboardState.IsKeyDown(key) && Keyboard.GetState().IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public Vector2 Position
        {
            get
            {
                return myBody.WorldCenter;
            }
            set
            {
                myBody.Position = value;
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            Rectangle rect1 = new Rectangle();
            Point point;
            point.X = (int)(Position.X - (width / 2));
            point.Y = (int)(Position.Y - (width / 2));
            rect1.Location = point;
            rect1.Width = (int)width;
            rect1.Height = (int)height;
            sb.Draw(texture, rect1, Color.White);            
        }
    }
}
