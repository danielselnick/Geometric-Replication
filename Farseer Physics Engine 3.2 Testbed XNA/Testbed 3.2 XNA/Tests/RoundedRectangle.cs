﻿using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.TestBed.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FarseerPhysics.TestBed.Tests
{
    public class RoundedRectangle : Test
    {
        private int _segments = 3;

        private RoundedRectangle()
        {
            //Ground
            FixtureFactory.CreateEdge(World, new Vector2(-40.0f, 0.0f), new Vector2(40.0f, 0.0f));

            Create(0);
        }

        public override void Keyboard(KeyboardManager keyboardManager)
        {
            if (keyboardManager.IsNewKeyPress(Keys.A))
                _segments++;

            if (keyboardManager.IsNewKeyPress(Keys.S) && _segments > 0)
                _segments--;

            if (keyboardManager.IsNewKeyPress(Keys.D))
                Create(0);
            if (keyboardManager.IsNewKeyPress(Keys.F))
                Create(1);
        }

        private void Create(int type)
        {
            Vector2 position = new Vector2(0, 30);

            switch (type)
            {
                default:
                    List<Fixture> rounded = FixtureFactory.CreateRoundedRectangle(World, 10, 10, 2.5F, 2.5F, _segments,
                                                                                  10, position);
                    rounded[0].Body.BodyType = BodyType.Dynamic;
                    break;
                case 1:
                    List<Fixture> capsule = FixtureFactory.CreateCapsule(World, 10, 2,
                                                                         (int) MathHelper.Max(_segments, 1), 3,
                                                                         (int) MathHelper.Max(_segments, 1), 10,
                                                                         position);
                    capsule[0].Body.BodyType = BodyType.Dynamic;
                    break;
            }
        }

        public override void Update(GameSettings settings, GameTime gameTime)
        {
            base.Update(settings, gameTime);
            DebugView.DrawString(50, TextLine,
                                 "Segments: " + _segments +
                                 "\nPress: 'A' to increase segments, 'S' decrease segments\n'D' to create rectangle. 'F' to create capsule.");
            TextLine += 15;
        }

        internal static Test Create()
        {
            return new RoundedRectangle();
        }
    }
}