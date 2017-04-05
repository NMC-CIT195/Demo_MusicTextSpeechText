using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MusicTextSpeechText
{
    class Program
    {
        //static Dictionary<>
        static void Main(string[] args)
        {
            bool running = true;

            PlayMusic();

            while (running)
            {
                
            }

        }

        static void PlayMusic()
        {
            using (SoundPlayer scaryMusic = new SoundPlayer(@"Media\scary-suspense.wav"))
            {
                scaryMusic.PlaySync();
            }
        }
    }
}
