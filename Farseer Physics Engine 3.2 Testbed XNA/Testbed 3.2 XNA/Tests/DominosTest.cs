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

namespace FarseerPhysics.TestBed.Tests
{
    public class DominosTest : Test
    {
        private DominosTest()
        {
            //Ground
            FixtureFactory.CreateEdge(World, new Vector2(-40.0f, 0.0f), new Vector2(40.0f, 0.0f));

            {
                Vertices box = PolygonTools.CreateRectangle(6.0f, 0.25f);
                PolygonShape shape = new PolygonShape(box, 0);

                Body ground = BodyFactory.CreateBody(World);
                ground.Position = new Vector2(-1.5f, 10.0f);

                ground.CreateFixture(shape);
            }

            {
                Vertices box = PolygonTools.CreateRectangle(0.1f, 1.0f);
                PolygonShape shape = new PolygonShape(box, 20);

                for (int i = 0; i < 10; ++i)
                {
                    Body body = BodyFactory.CreateBody(World);
                    body.BodyType = BodyType.Dynamic;
                    body.Position = new Vector2(-6.0f + 1.0f*i, 11.25f);

                    Fixture fixture = body.CreateFixture(shape);
                    fixture.Friction = 0.1f;
                }
            }

            {
                Vertices box = PolygonTools.CreateRectangle(7.0f, 0.25f, Vector2.Zero, 0.3f);
                PolygonShape shape = new PolygonShape(box, 0);

                Body ground = BodyFactory.CreateBody(World);
                ground.Position = new Vector2(1.0f, 6.0f);

                ground.CreateFixture(shape);
            }

            Body b2;
            {
                Vertices box = PolygonTools.CreateRectangle(0.25f, 1.5f);
                PolygonShape shape = new PolygonShape(box, 0);

                b2 = BodyFactory.CreateBody(World);
                b2.Position = new Vector2(-7.0f, 4.0f);

                b2.CreateFixture(shape);
            }

            Body b3;
            {
                Vertices box = PolygonTools.CreateRectangle(6.0f, 0.125f);
                PolygonShape shape = new PolygonShape(box, 10);

                b3 = BodyFactory.CreateBody(World);
                b3.BodyType = BodyType.Dynamic;
                b3.Position = new Vector2(-0.9f, 1.0f);
                b3.Rotation = -0.15f;

                b3.CreateFixture(shape);
            }

            Vector2 anchor = new Vector2(-2.0f, 1.0f);
            FixedRevoluteJoint jd = new FixedRevoluteJoint(b3, b3.GetLocalPoint(anchor), anchor);
            jd.CollideConnected = true;
            World.AddJoint(jd);

            Body b4;
            {
                Vertices box = PolygonTools.CreateRectangle(0.25f, 0.25f);
                PolygonShape shape = new PolygonShape(box, 10);

                b4 = BodyFactory.CreateBody(World);
                b4.BodyType = BodyType.Dynamic;
                b4.Position = new Vector2(-10.0f, 15.0f);

                b4.CreateFixture(shape);
            }

            anchor = new Vector2(-7.0f, 15.0f);
            FixedRevoluteJoint jd2 = new FixedRevoluteJoint(b4, b4.GetLocalPoint(anchor), anchor);
            World.AddJoint(jd2);

            Body b5;
            {
                b5 = BodyFactory.CreateBody(World);
                b5.BodyType = BodyType.Dynamic;
                b5.Position = new Vector2(6.5f, 3.0f);

                Vertices vertices = PolygonTools.CreateRectangle(1.0f, 0.1f, new Vector2(0.0f, -0.9f), 0.0f);
                PolygonShape shape = new PolygonShape(vertices, 10);

                Fixture fix = b5.CreateFixture(shape);
                fix.Friction = 0.1f;

                vertices = PolygonTools.CreateRectangle(0.1f, 1.0f, new Vector2(-0.9f, 0.0f), 0.0f);

                shape.Set(vertices);
                fix = b5.CreateFixture(shape);
                fix.Friction = 0.1f;

                vertices = PolygonTools.CreateRectangle(0.1f, 1.0f, new Vector2(0.9f, 0.0f), 0.0f);

                shape.Set(vertices);
                fix = b5.CreateFixture(shape);
                fix.Friction = 0.1f;
            }

            anchor = new Vector2(6.0f, 2.0f);
            FixedRevoluteJoint jd3 = new FixedRevoluteJoint(b5, b5.GetLocalPoint(anchor), anchor);
            World.AddJoint(jd3);

            Body b6;
            {
                Vertices box = PolygonTools.CreateRectangle(1.0f, 0.1f);
                PolygonShape shape = new PolygonShape(box, 30);

                b6 = BodyFactory.CreateBody(World);
                b6.BodyType = BodyType.Dynamic;
                b6.Position = new Vector2(6.5f, 4.1f);

                b6.CreateFixture(shape);
            }

            anchor = new Vector2(1.0f, -0.1f);
            RevoluteJoint jd4 = new RevoluteJoint(b5, b6, b5.GetLocalPoint(b6.GetWorldPoint(anchor)), anchor);
            jd4.CollideConnected = true;
            World.AddJoint(jd4);

            Body b7;
            {
                Vertices box = PolygonTools.CreateRectangle(0.1f, 1.0f);
                PolygonShape shape = new PolygonShape(box, 10);

                b7 = BodyFactory.CreateBody(World);
                b7.BodyType = BodyType.Dynamic;
                b7.Position = new Vector2(7.4f, 1.0f);

                b7.CreateFixture(shape);
            }

            DistanceJoint djd = new DistanceJoint(b3, b7, new Vector2(6.0f, 0.0f), new Vector2(0.0f, -1.0f));
            Vector2 d = djd.BodyB.GetWorldPoint(djd.LocalAnchorB) - djd.BodyA.GetWorldPoint(djd.LocalAnchorA);
            djd.Length = d.Length();
            World.AddJoint(djd);

            {
                const float radius = 0.2f;

                CircleShape shape = new CircleShape(radius, 10);

                for (int i = 0; i < 4; ++i)
                {
                    Body body = BodyFactory.CreateBody(World);
                    body.BodyType = BodyType.Dynamic;
                    body.Position = new Vector2(5.9f + 2.0f*radius*i, 2.4f);

                    body.CreateFixture(shape);
                }
            }
        }

        internal static Test Create()
        {
            return new DominosTest();
        }
    }
}