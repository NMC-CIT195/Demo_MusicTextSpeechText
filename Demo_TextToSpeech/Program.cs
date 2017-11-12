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
        static ManualResetEvent recongnitionComplete;
        static int rowPosition = 10;
        static int columnPosition = 30;
        static int ROW_INCREMENT = 1;
        static int COLUMN_INCREMENT = 1;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            SpeechRecognition();
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
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder("appear")));
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder("disappear")));
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
            Console.CursorVisible = false;

            switch (e.Result.Text)
            {
                case "appear":
                    Console.Clear();
                    Console.SetCursorPosition(columnPosition, rowPosition);
                    Console.Write("+");
                    break;

                case "disappear":
                    Console.Clear();
                    break;

                case "up":
                    Console.Clear();
                    rowPosition -= ROW_INCREMENT;
                    Console.SetCursorPosition(columnPosition, rowPosition);
                    Console.Write("+");
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Up!");
                    break;

                case "down":
                    Console.Clear();
                    rowPosition += ROW_INCREMENT;
                    Console.SetCursorPosition(columnPosition, rowPosition);
                    Console.Write("+");
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Down!");
                    break;

                case "right":
                    Console.Clear();
                    columnPosition += COLUMN_INCREMENT;
                    Console.SetCursorPosition(columnPosition, rowPosition);
                    Console.Write("+");
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Right!");
                    break;

                case "left":
                    Console.Clear();
                    columnPosition -= COLUMN_INCREMENT;
                    Console.SetCursorPosition(columnPosition, rowPosition);
                    Console.Write("+");
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Left!");
                    break;

                case "quit":
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Quit!");
                    //
                    // fix bug requiring two consecutive "quit" commands
                    //
                    recongnitionComplete.Set();
                    break;

                default:
                    break;
            }
        }

        static void MoveOnScreen()
        {

        }
    }
}
