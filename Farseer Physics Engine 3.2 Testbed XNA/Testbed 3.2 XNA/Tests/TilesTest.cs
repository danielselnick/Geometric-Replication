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

using System;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.TestBed.Framework;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.TestBed.Tests
{
    /// <summary>
    /// This stress tests the dynamic tree broad-phase. This also shows that tile
    /// based collision is _not_ smooth due to Box2D not knowing about adjacency.
    /// </summary>
    public class TilesTest : Test
    {
        private const int Count = 20;

        private TilesTest()
        {
            {
                const float a = 0.5f;
                Body ground = BodyFactory.CreateBody(World, new Vector2(0, -a));
                const int n = 200;
                const int m = 10;
                Vector2 position = Vector2.Zero;
                position.Y = 0.0f;
                for (int j = 0; j < m; ++j)
                {
                    position.X = -n*a;
                    for (int i = 0; i < n; ++i)
                    {
                        PolygonShape shape = new PolygonShape(0);
                        shape.SetAsBox(a, a, position, 0.0f);
                        ground.CreateFixture(shape);
                        position.X += 2.0f*a;
                    }
                    position.Y -= 2.0f*a;
                }
            }

            {
                const float a = 0.5f;
                Vertices box = PolygonTools.CreateRectangle(a, a);
                PolygonShape shape = new PolygonShape(box, 5);

                Vector2 x = new Vector2(-7.0f, 0.75f);
                Vector2 deltaX = new Vector2(0.5625f, 1.25f);
                Vector2 deltaY = new Vector2(1.125f, 0.0f);

                for (int i = 0; i < Count; ++i)
                {
                    Vector2 y = x;

                    for (int j = i; j < Count; ++j)
                    {
                        Body body = BodyFactory.CreateBody(World);
                        body.BodyType = BodyType.Dynamic;
                        body.Position = y;
                        body.CreateFixture(shape);

                        y += deltaY;
                    }

                    x += deltaX;
                }
            }
        }

        public override void Update(GameSettings settings, GameTime gameTime)
        {
            ContactManager cm = World.ContactManager;
            int height = cm.BroadPhase.ComputeHeight();
            int leafCount = cm.BroadPhase.ProxyCount;
            int minimumNodeCount = 2*leafCount - 1;
            float minimumHeight = (float) Math.Ceiling(Math.Log(minimumNodeCount)/Math.Log(2.0f));
            DebugView.DrawString(50, TextLine, "Test of dynamic tree performance in worse case scenario.", height,
                                 minimumHeight);
            TextLine += 15;
            DebugView.DrawString(50, TextLine, "I know this is slow. I hope to address this in a future update.", height,
                                 minimumHeight);
            TextLine += 15;
            DebugView.DrawString(50, TextLine, "Dynamic tree height = {0}, min = {1}", height, minimumHeight);
            TextLine += 15;

            base.Update(settings, gameTime);
        }

        internal static Test Create()
        {
            return new TilesTest();
        }
    }
}