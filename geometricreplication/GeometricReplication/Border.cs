using System;
using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GeometricReplication
{
    public class Border
    {
        private Body _anchor;
        private Body left;
        private Body right;
        private Body up;
        private Body down;
        private World _world;
        private Texture2D leftBorder;
        private Texture2D rightBorder;
        private Texture2D topBorder;
        private Texture2D bottomBorder;
        private readonly float _bouncyValue;
        private readonly float _frictionValue;

        public Border(World world, float width, float height, float borderWidth)
        {
            _bouncyValue = 1.0f;
            _frictionValue = 0.5f;
            _world = world;

            CreateBorder(width, height, borderWidth);
        }

        private void CreateBorder(float width, float height, float borderWidth)
        {
            width = Math.Abs(width);
            height = Math.Abs(height);

            _anchor = BodyFactory.CreateBody(_world);
            left = BodyFactory.CreateBody(_world);
            right = BodyFactory.CreateBody(_world);
            up = BodyFactory.CreateBody(_world);
            down = BodyFactory.CreateBody(_world);

            UserData userData = new UserData(-1, 3, true); 

            Vertices vert = new Vertices();

            // top
            // counterclockwise
            Vertices vup = new Vertices();
            vup.Add(new Vector2(0, 10));
            vup.Add(new Vector2(0, 0));
            vup.Add(new Vector2(1000, 0));
            vup.Add(new Vector2(1000, 10));
            up.CreateFixture(new PolygonShape(vup, 0.0f), userData);
            //Bottom          
            down.CreateFixture(new PolygonShape(vup, 0.0f), userData);
            down.Position = new Vector2(0, 990);
            //Left
            Vertices vl = new Vertices();
            vl.Add(new Vector2(0, 1000));
            vl.Add(new Vector2(0, 0));
            vl.Add(new Vector2(10, 0));
            vl.Add(new Vector2(10, 1000));
            left.CreateFixture(new PolygonShape(vl, 0.0f), userData);

            //Right
                     
            right.CreateFixture(new PolygonShape(vl, 0.0f), userData);
            right.Position = new Vector2(990, 0);
            

            foreach (Fixture t in _anchor.FixtureList)
            {
                t.CollisionFilter.CollisionCategories = Category.All;
                t.CollisionFilter.CollidesWith = Category.All;
                t.Friction = _frictionValue;
                t.Restitution = _bouncyValue;         
            }

            ContentManager content = Master.theMaster.game.Content;
            topBorder = content.Load<Texture2D>("images/BoarderHorizontal");
            bottomBorder = content.Load<Texture2D>("images/BoarderHorizontal");
            rightBorder = content.Load<Texture2D>("images/BoarderVertical");
            leftBorder = content.Load<Texture2D>("images/BoarderVertical");
        }

        public void ResetBorder(float width, float height, float borderWidth)
        {
            _world.RemoveBody(_anchor);
            _world.ProcessChanges();

            CreateBorder(width, height, borderWidth);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(topBorder, new Vector2(0, -67), Color.White);
            sb.Draw(bottomBorder, new Vector2(-67, 1000), Color.White);
            sb.Draw(rightBorder, new Vector2(1000, -67), Color.White);
            sb.Draw(leftBorder, new Vector2(-67, 0), Color.White);
        }
    }
}