﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     This class will play a sound for the game.
    /// </summary>
    public class SoundPlayer
    {
        #region Methods

        [DllImport("winmm.dll")]
        private static extern int mciSendString(string command, StringBuilder buffer, int bufferSize,
            IntPtr hwndCallback);

        /// <summary>
        ///     Plays the specified path.
        ///     Precondition: path != null && name != null
        ///     Postcondition: Play a sound from the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        public void Play(string path, string name)
        {
            mciSendString($@"close {name}", null, 0, IntPtr.Zero);
            // Open
            mciSendString($@"open {path} type waveaudio alias {name}", null, 0, IntPtr.Zero);
            // Play
            mciSendString($@"play {name}", null, 0, IntPtr.Zero);
        }

        #endregion
    }
}