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

        /// <summary>
        /// demonstration of SoundPlayer class Play methods
        /// </summary>
        static void PlayMusic()
        {
            SoundPlayer scaryMusic = new SoundPlayer(@"Media\scary-suspense.wav");

            scaryMusic.Play(); // new thread created
            //scaryMusic.PlaySync(); // runs in existing thread, app waits for music to finish
            //scaryMusic.PlayLooping(); // new thread created

            Console.WriteLine("Hello NMC!");
            Console.ReadKey();
        }
    }
}
