﻿/*
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

using System.Collections.Generic;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.TestBed.Framework;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.TestBed.Tests
{
    public class ContinuousTest : Test
    {
        private float _angularVelocity;
        private Fixture _box;

        private ContinuousTest()
        {
            List<Vertices> list = new List<Vertices>();
            list.Add(PolygonTools.CreateEdge(new Vector2(-10.0f, 0.0f), new Vector2(10.0f, 0.0f)));
            list.Add(PolygonTools.CreateRectangle(0.2f, 1.0f, new Vector2(0.5f, 1.0f), 0));

            FixtureFactory.CreateCompoundPolygon(World, list, 0);

            _box = FixtureFactory.CreateRectangle(World, 4, 0.2f, 1);
            _box.Body.Position = new Vector2(0, 20);
            _box.Body.BodyType = BodyType.Dynamic;
            //_box.Body.Rotation = 0.1f;

            //_angularVelocity = 46.661274f;
            _angularVelocity = Rand.RandomFloat(-50.0f, 50.0f);
            _box.Body.LinearVelocity = new Vector2(0.0f, -100.0f);
            _box.Body.AngularVelocity = _angularVelocity;
        }

        private void Launch()
        {
            _box.Body.SetTransform(new Vector2(0.0f, 20.0f), 0.0f);
            _angularVelocity = Rand.RandomFloat(-50.0f, 50.0f);
            _box.Body.LinearVelocity = new Vector2(0.0f, -100.0f);
            _box.Body.AngularVelocity = _angularVelocity;
        }

        public override void Update(GameSettings settings, GameTime gameTime)
        {
            if (StepCount == 12)
            {
                StepCount += 0;
            }

            base.Update(settings, gameTime);

            if (Distance.GJKCalls > 0)
            {
                DebugView.DrawString(50, TextLine, "GJK calls = {0:n}, Ave GJK iters = {1:n}, Max GJK iters = {2:n}",
                                     Distance.GJKCalls, Distance.GJKIters/(float) Distance.GJKCalls,
                                     Distance.GJKMaxIters);
                TextLine += 15;
            }

            if (TimeOfImpact.TOICalls > 0)
            {
                DebugView.DrawString(50, TextLine, "TOI calls = {0:n}, Ave TOI iters = {1:n}, Max TOI iters = {2:n}",
                                     TimeOfImpact.TOICalls, TimeOfImpact.TOIIters/(float) TimeOfImpact.TOICalls,
                                     TimeOfImpact.TOIMaxRootIters);
                TextLine += 15;

                DebugView.DrawString(50, TextLine, "Ave TOI root iters = {0:n}, Max TOI root iters = {1:n}",
                                     TimeOfImpact.TOIRootIters/(float) TimeOfImpact.TOICalls,
                                     TimeOfImpact.TOIMaxRootIters);
                TextLine += 15;
            }

            if (StepCount%60 == 0)
            {
                //Launch();
            }
        }

        internal static Test Create()
        {
            return new ContinuousTest();
        }
    }
}