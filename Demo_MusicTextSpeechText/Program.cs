using System;
using System.Media;
using System.Speech;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Text;
using System.Threading;

namespace Demo_MusicTextSpeechText
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayMusic();
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

            //
            // manually dispose of SoundPlayer object
            //
            scaryMusic.Dispose();

            Console.WriteLine("Hello NMC!");
            Console.ReadKey();
        }        
    }
}
