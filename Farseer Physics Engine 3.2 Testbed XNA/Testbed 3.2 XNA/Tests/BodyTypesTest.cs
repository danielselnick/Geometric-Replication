/*
* Farseer Physics Engine based on Box2D.XNA port:
* Copyright (c) 2010 Ian Qvist
* 
* Box2D.XNA port of Box2D:
* Copyright (c) 2009 Brandon Furtwangler, Nathan Furtwangler
*
* Original source Box2D:
* Copyright (c) 2006-2009 Erin Catto http://www.gphysics.com 
* 
* This software is provided 'as-is', without any express or implied 
* warranty.  In no event will the authors be held liable for any damages 
* arising from the use of this software. 
* Permission is granted to anyone to use this software for any purpose, 
* including commercial applications, and to alter it and redistribute it 
* freely, subject to the following restrictions: 
* 1. The origin of this software must not be misrepresented; you must not 
* claim that you wrote the original software. If you use this software 
* in a product, an acknowledgment in the product documentation would be 
* appreciated but is not required. 
* 2. Altered source versions must be plainly marked as such, and must not be 
* misrepresented as being the original software. 
* 3. This notice may not be removed or altered from any source distribution. 
*/

using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FarseerPhysics.TestBed.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FarseerPhysics.TestBed.Tests
{
    public class BodyTypesTest : Test
    {
        private Body _attachment;
        private Body _platform;
        private float _speed;

        private BodyTypesTest()
        {
            //Ground
            FixtureFactory.CreateEdge(World, new Vector2(-40.0f, 0.0f), new Vector2(40.0f, 0.0f));

            // Define attachment
            {
                _attachment = BodyFactory.CreateBody(World);
                _attachment.BodyType = BodyType.Dynamic;
                _attachment.Position = new Vector2(0.0f, 3.0f);

                Vertices box = PolygonTools.CreateRectangle(0.5f, 2.0f);
                PolygonShape shape = new PolygonShape(box, 2);
                _attachment.CreateFixture(shape);
            }

            // Define platform
            {
                _platform = BodyFactory.CreateBody(World);
                _platform.BodyType = BodyType.Dynamic;
                _platform.Position = new Vector2(0.0f, 5.0f);

                Vertices box = PolygonTools.CreateRectangle(4.0f, 0.5f);
                PolygonShape shape = new PolygonShape(box, 2);

                Fixture fixture = _platform.CreateFixture(shape);
                fixture.Friction = 0.6f;

                RevoluteJoint rjd = new RevoluteJoint(_attachment, _platform,
                                                      _attachment.GetLocalPoint(_platform.Position),
                                                      Vector2.Zero);
                rjd.MaxMotorTorque = 50.0f;
                rjd.MotorEnabled = true;
                World.AddJoint(rjd);

                FixedPrismaticJoint pjd = new FixedPrismaticJoint(_platform, new Vector2(0.0f, 5.0f),
                                                                  new Vector2(1.0f, 0.0f));
                pjd.MaxMotorForce = 1000.0f;
                pjd.MotorEnabled = true;
                pjd.LowerLimit = -10.0f;
                pjd.UpperLimit = 10.0f;
                pjd.LimitEnabled = true;

                World.AddJoint(pjd);

                _speed = 3.0f;
            }

            // Create a payload
            {
                Body body = BodyFactory.CreateBody(World);
                body.BodyType = BodyType.Dynamic;
                body.Position = new Vector2(0.0f, 8.0f);

                Vertices box = PolygonTools.CreateRectangle(0.75f, 0.75f);
                PolygonShape shape = new PolygonShape(box, 2);

                Fixture fixture = body.CreateFixture(shape);
                fixture.Friction = 0.6f;
            }
        }

        public override void Keyboard(KeyboardManager keyboardManager)
        {
            if (keyboardManager.IsKeyDown(Keys.D))
            {
                _platform.BodyType = BodyType.Dynamic;
            }
            if (keyboardManager.IsKeyDown(Keys.S))
            {
                _platform.BodyType = BodyType.Static;
            }
            if (keyboardManager.IsKeyDown(Keys.K))
            {
                _platform.BodyType = BodyType.Kinematic;
                _platform.LinearVelocity = new Vector2(-_speed, 0.0f);
                _platform.AngularVelocity = 0.0f;
            }
        }

        public override void Update(GameSettings settings, GameTime gameTime)
        {
            // Drive the kinematic body.
            if (_platform.BodyType == BodyType.Kinematic)
            {
                Transform tf;
                _platform.GetTransform(out tf);
                Vector2 p = tf.Position;
                Vector2 v = _platform.LinearVelocity;

                if ((p.X < -10.0f && v.X < 0.0f) ||
                    (p.X > 10.0f && v.X > 0.0f))
                {
                    v.X = -v.X;
                    _platform.LinearVelocity = v;
                }
            }

            base.Update(settings, gameTime);
            DebugView.DrawString(50, TextLine, "Keys: (d) dynamic, (s) static, (k) kinematic");
            TextLine += 15;
        }


        internal static Test Create()
        {
            return new BodyTypesTest();
        }
    }
}