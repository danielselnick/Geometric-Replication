using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace GeometricReplication
{
    class SoundFx
    {
        float fxVolume = 10f;
        List<SoundEffect> fxSounds = new List<SoundEffect>();

        public SoundFx(Game1 cGame)
        {
            fxSounds.Add(cGame.Content.Load<SoundEffect>("songsAndSounds/hit1"));
            fxSounds.Add(cGame.Content.Load<SoundEffect>("songsAndSounds/hit2"));
            fxSounds.Add(cGame.Content.Load<SoundEffect>("songsAndSounds/hit3"));
            fxSounds.Add(cGame.Content.Load<SoundEffect>("songsAndSounds/menu_select"));
        }

        public void playSoundFx(SoundEffect inputSound)
        {
            inputSound.Play(fxVolume / 100f, 0f, 0f);
        }

        public void playSpecificSoundFx(int index)
        {
            fxSounds[index].Play(fxVolume / 100f, 0f, 0f);
        }
    }
}
