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
    /// <summary>
    /// A motor driven slider crank with joint friction.
    /// </summary>
    public class SliderCrankTest : Test
    {
        private RevoluteJoint _joint1;
        private FixedPrismaticJoint _joint2;

        private SliderCrankTest()
        {
            Body ground;
            {
                ground = BodyFactory.CreateBody(World);

                PolygonShape shape = new PolygonShape(0);
                shape.SetAsEdge(new Vector2(-40.0f, 0.0f), new Vector2(40.0f, 0.0f));
                ground.CreateFixture(shape);
            }

            {
                Body prevBody = ground;

                // Define crank.
                {
                    PolygonShape shape = new PolygonShape(2);
                    shape.SetAsBox(0.5f, 2.0f);

                    Body body = BodyFactory.CreateBody(World);
                    body.BodyType = BodyType.Dynamic;
                    body.Position = new Vector2(0.0f, 7.0f);

                    body.CreateFixture(shape);

                    Vector2 anchor = new Vector2(0.0f, -2.0f);
                    _joint1 = new RevoluteJoint(prevBody, body, prevBody.GetLocalPoint(body.GetWorldPoint(anchor)),
                                                anchor);
                    _joint1.MotorSpeed = 1.0f*Settings.Pi;
                    _joint1.MaxMotorTorque = 10000.0f;
                    _joint1.MotorEnabled = true;
                    World.AddJoint(_joint1);

                    prevBody = body;
                }

                // Define follower.
                {
                    PolygonShape shape = new PolygonShape(2);
                    shape.SetAsBox(0.5f, 4.0f);

                    Body body = BodyFactory.CreateBody(World);
                    body.BodyType = BodyType.Dynamic;
                    body.Position = new Vector2(0.0f, 13.0f);

                    body.CreateFixture(shape);

                    Vector2 anchor = new Vector2(0.0f, -4.0f);
                    RevoluteJoint rjd3 = new RevoluteJoint(prevBody, body,
                                                           prevBody.GetLocalPoint(body.GetWorldPoint(anchor)), anchor);
                    rjd3.MotorEnabled = false;
                    World.AddJoint(rjd3);

                    prevBody = body;
                }

                // Define piston
                {
                    PolygonShape shape = new PolygonShape(2);
                    shape.SetAsBox(1.5f, 1.5f);

                    Body body = BodyFactory.CreateBody(World);
                    body.BodyType = BodyType.Dynamic;
                    body.Position = new Vector2(0.0f, 17.0f);

                    body.CreateFixture(shape);

                    Vector2 anchor = Vector2.Zero;
                    RevoluteJoint rjd2 = new RevoluteJoint(prevBody, body,
                                                           prevBody.GetLocalPoint(body.GetWorldPoint(anchor)), anchor);
                    World.AddJoint(rjd2);

                    _joint2 = new FixedPrismaticJoint(body, new Vector2(0.0f, 17.0f), new Vector2(0.0f, 1.0f));
                    _joint2.MaxMotorForce = 1000.0f;
                    _joint2.MotorEnabled = true;

                    World.AddJoint(_joint2);
                }

                // Create a payload
                {
                    PolygonShape shape = new PolygonShape(2);
                    shape.SetAsBox(1.5f, 1.5f);

                    Body body = BodyFactory.CreateBody(World);
                    body.BodyType = BodyType.Dynamic;
                    body.Position = new Vector2(0.0f, 23.0f);

                    body.CreateFixture(shape);
                }
            }
        }

        public override void Keyboard(KeyboardManager keyboardManager)
        {
            if (keyboardManager.IsNewKeyPress(Keys.F))
            {
                _joint2.MotorEnabled = !_joint2.MotorEnabled;
                _joint2.BodyB.Awake = true;
            }
            if (keyboardManager.IsNewKeyPress(Keys.M))
            {
                _joint1.MotorEnabled = !_joint1.MotorEnabled;
                _joint1.BodyB.Awake = true;
            }
        }

        public override void Update(GameSettings settings, GameTime gameTime)
        {
            base.Update(settings, gameTime);
            DebugView.DrawString(50, TextLine, "Keys: (f) toggle friction, (m) toggle motor");
            TextLine += 15;
            float torque = _joint1.MotorTorque;
            DebugView.DrawString(50, TextLine, "Motor Torque = {0:n}", torque);
            TextLine += 15;
        }

        internal static Test Create()
        {
            return new SliderCrankTest();
        }
    }
}