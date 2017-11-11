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
        static ManualResetEvent recongnitionComplete = null;

        static void Main(string[] args)
        {
            //PlayMusic();

            //DisplayInstallVoices();
            //TextToSpeech();

            SpeechRecognition();
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

        /// <summary>
        /// demo text to speech
        /// </summary>
        static void TextToSpeech()
        {
            SpeechSynthesizer textSpeaker = new SpeechSynthesizer();

            textSpeaker.SelectVoice("Microsoft David Desktop"); // "Microsoft Zira Desktop"
            textSpeaker.Rate = 0; // -10 <-> 10
            textSpeaker.Volume = 100; // 0 <-> 100

            //
            // implementing a using block to manage and dispose of the SpeechSynthesizer object
            //
            using (textSpeaker)
            {
                textSpeaker.Speak("Hello NMC!");
                textSpeaker.Speak(GetTextToSpeak());
            }
        }

        /// <summary>
        /// recognize speech
        /// adapted from the following CodeProject article
        /// https://www.codeproject.com/Articles/483347/Speech-recognition-speech-to-text-text-to-speech-a#speechrecognitionincsharp
        /// </summary>
        static void SpeechRecognition()
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            recongnitionComplete = new ManualResetEvent(false);

            //
            // load grammar elements
            //
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder("up")));
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder("down")));
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder("right")));
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder("left")));
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder("quit")));

            //
            // add event handler to manage recognized speech
            //
            recognizer.SpeechRecognized += RecognizerSpeechRecognized;

            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            recongnitionComplete.WaitOne();
            recognizer.Dispose();
        }

        /// <summary>
        /// event handler to manage recognized speech
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void RecognizerSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "up":
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Up!");
                    break;
                case "down":
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Down!");
                    break;
                case "right":
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Right!");
                    break;
                case "left":
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Left!");
                    break;
                case "quit":
                    Console.Clear();
                    Console.Write("Quit!");
                    recongnitionComplete.Set();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// display all install voices
        /// Source: MSDN website
        /// </summary>
        static void DisplayInstallVoices()
        {

            // Initialize a new instance of the SpeechSynthesizer.
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                // Output information about all of the installed voices. 
                Console.WriteLine("Installed voices -");
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    string AudioFormats = "";
                    foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
                    {
                        AudioFormats += String.Format("{0}\n",
                        fmt.EncodingFormat.ToString());
                    }

                    Console.WriteLine(" Name:          " + info.Name);
                    Console.WriteLine(" Culture:       " + info.Culture);
                    Console.WriteLine(" Age:           " + info.Age);
                    Console.WriteLine(" Gender:        " + info.Gender);
                    Console.WriteLine(" Description:   " + info.Description);
                    Console.WriteLine(" ID:            " + info.Id);
                    Console.WriteLine(" Enabled:       " + voice.Enabled);
                    if (info.SupportedAudioFormats.Count != 0)
                    {
                        Console.WriteLine(" Audio formats: " + AudioFormats);
                    }
                    else
                    {
                        Console.WriteLine(" No supported audio formats found");
                    }

                    string AdditionalInfo = "";
                    foreach (string key in info.AdditionalInfo.Keys)
                    {
                        AdditionalInfo += String.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
                    }

                    Console.WriteLine(" Additional Info - " + AdditionalInfo);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// get or generate text for speech
        /// </summary>
        /// <returns>text to be spoken</returns>
        static string GetTextToSpeak()
        {
            StringBuilder textToSpeak = new StringBuilder();

            textToSpeak.Clear();
            textToSpeak.Append("Have I told you about the butcher who backed into his meat grinder?");
            textToSpeak.Append("Yep! He got a little behind in his work.");

            return textToSpeak.ToString();
        }
    }
}
