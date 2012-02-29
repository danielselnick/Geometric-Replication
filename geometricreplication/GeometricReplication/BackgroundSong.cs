using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace GeometricReplication
{
    class BackgroundSong
    {
        private const int MAX_SONGS = 3;
        private List<Song> bgSong = new List<Song>();
        private float sVolume = 5f;
        private int cSong = 0;
        private int prevSong = 0;
        private bool songBegin = false;

        private Random soundPick = new Random();

        public BackgroundSong(Game1 cGame)
        {
            // VVV This is example how to add a song VVV
            bgSong.Add(cGame.Content.Load<Song>("songsAndSounds/GnG"));
            bgSong.Add(cGame.Content.Load<Song>("songsAndSounds/cTwn"));
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = sVolume / 100;
        }

        public void playSong()
        {
            songCheck();
            if (!songBegin)
            {
                MediaPlayer.Play(bgSong[cSong]);
                prevSong = cSong;
                songBegin = true;
            }
        }

        private void songCheck()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                cSong = soundPick.Next(0, (MAX_SONGS - 1));
                songBegin = false;
            }
        }

        public void playNext()
        {
            MediaPlayer.Stop();
            while (cSong == prevSong)
            {
                cSong = soundPick.Next(0, (MAX_SONGS - 1));
                if (cSong >= MAX_SONGS)
                    cSong = MAX_SONGS - 1;
            }
            prevSong = cSong;
            songBegin = false;
        }

        public void stopSong()
        {
            if (songBegin)
            {
                MediaPlayer.Stop();
                songBegin = false;
            }
        }
    }
}
