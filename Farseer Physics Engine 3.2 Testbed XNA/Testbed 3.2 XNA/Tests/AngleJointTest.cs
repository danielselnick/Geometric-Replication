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
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using FarseerPhysics.TestBed.Framework;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.TestBed.Tests
{
    public class AngleJointTest : Test
    {
        private AngleJointTest()
        {
            FixtureFactory.CreateEdge(World, new Vector2(-40, 0), new Vector2(40, 0));

            Fixture fA = FixtureFactory.CreateRectangle(World, 4, 4, 1, new Vector2(-5, 4));
            fA.Body.BodyType = BodyType.Dynamic;

            Fixture fB = FixtureFactory.CreateRectangle(World, 4, 4, 1, new Vector2(5, 4));
            fB.Body.BodyType = BodyType.Dynamic;

            AngleJoint joint = new AngleJoint(fA.Body, fB.Body);
            joint.TargetAngle = (float) Math.PI/2;
            World.AddJoint(joint);

            Fixture fC = FixtureFactory.CreateRectangle(World, 4, 4, 1, new Vector2(10, 4));
            fC.Body.BodyType = BodyType.Dynamic;

            FixedAngleJoint fixedJoint = new FixedAngleJoint(fC.Body);
            fixedJoint.TargetAngle = (float) Math.PI/3;
            World.AddJoint(fixedJoint);
        }

        internal static Test Create()
        {
            return new AngleJointTest();
        }
    }
}