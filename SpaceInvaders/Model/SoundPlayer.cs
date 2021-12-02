using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    class SoundPlayer
    {
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        public void Play(string path, string name)
        {

            mciSendString($@"close {name}", null, 0, IntPtr.Zero);
            // Open
            mciSendString($@"open {path} type waveaudio alias {name}", null, 0, IntPtr.Zero);
            // Play
            mciSendString($@"play {name}", null, 0, IntPtr.Zero);

            
        }
    }
}
