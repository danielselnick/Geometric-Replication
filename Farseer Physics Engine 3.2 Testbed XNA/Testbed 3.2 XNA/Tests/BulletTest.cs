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

using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.TestBed.Framework;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.TestBed.Tests
{
    public class BulletTest : Test
    {
        private Body _body;
        private Body _bullet;
        private float _x;

        private BulletTest()
        {
            FixtureFactory.CreateEdge(World, new Vector2(-10, 0), new Vector2(10, 0));
            FixtureFactory.CreateRectangle(World, 0.4f, 2f, 0, new Vector2(0.5f, 1.0f));

            //Bar
            _body = FixtureFactory.CreateRectangle(World, 4f, 0.2f, 1, new Vector2(0.5f, 1.0f)).Body;
            _body.Position = new Vector2(0, 4);
            _body.BodyType = BodyType.Dynamic;

            //Bullet
            _bullet = FixtureFactory.CreateRectangle(World, 0.5f, 0.5f, 100).Body;
            _bullet.IsBullet = true;
            _bullet.BodyType = BodyType.Dynamic;
            _x = 0.20352793f;
            _bullet.Position = new Vector2(_x, 10);

            _bullet.LinearVelocity = new Vector2(0, -50);
        }

        public override void Update(GameSettings settings, GameTime gameTime)
        {
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
                Launch();
            }
        }

        private void Launch()
        {
            _body.SetTransform(new Vector2(0.0f, 4.0f), 0.0f);
            _body.LinearVelocity = Vector2.Zero;
            _body.AngularVelocity = 0;

            _x = Rand.RandomFloat(-1.0f, 1.0f);
            _bullet.SetTransform(new Vector2(_x, 10.0f), 0.0f);
            _bullet.LinearVelocity = new Vector2(0.0f, -50.0f);
            _bullet.AngularVelocity = 0;

            Distance.GJKCalls = 0;
            Distance.GJKIters = 0;
            Distance.GJKMaxIters = 0;

            TimeOfImpact.TOICalls = 0;
            TimeOfImpact.TOIIters = 0;
            TimeOfImpact.TOIMaxIters = 0;
            TimeOfImpact.TOIRootIters = 0;
            TimeOfImpact.TOIMaxRootIters = 0;
        }

        internal static Test Create()
        {
            return new BulletTest();
        }
    }
}