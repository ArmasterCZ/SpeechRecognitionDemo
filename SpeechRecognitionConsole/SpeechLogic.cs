using System.Collections.Generic;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace SpeechRecognitionConsole
{
    class SpeechLogic
    {
        SpeechRecognitionEngine srEngine = new SpeechRecognitionEngine();
        Choices choices = new Choices();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        Dictionary<string, string> textCommands = new Dictionary<string, string>()
        {
            { "How it ostside?", "You can look from the window. Righ?" },
            { "Can you check my code?", "I could, if you would programmed it. But you didn't, so i cannot." },
            { "Can you help me?", "With what? Like wtf men, how can i know what you need to help with." },
            { "How are you today.", "Im fine. Thanks. And you? Wait. I don't care. And nobody likes you." },
            { "Any motivation?", "Lift your lazy ass and start do something. Something like exercising." }
        };

        public void Start()
        {
            List<string> commands = new List<string>();
            foreach (var item in textCommands)
            {
                commands.Add(item.Key);
            }
            choices.Add(commands.ToArray());
            GrammarBuilder builder = new GrammarBuilder();
            builder.Append(choices);
            Grammar grammar = new Grammar(builder);
            srEngine.LoadGrammarAsync(grammar);
            srEngine.SetInputToDefaultAudioDevice();
            srEngine.SpeechRecognized += RecognizedVoice;
            srEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void RecognizedVoice(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (var item in textCommands)
            {
                if (item.Key.Equals(e.Result.Text))
                {
                    synthesizer.Speak(item.Value);
                    break;
                }
            }
        }
    }
}

        //public void Start()
        //{
        //    choices = new Choices();
        //    choices.Add(new string[] { "How are you", "Can I change you?" });
        //    GrammarBuilder builder = new GrammarBuilder();
        //    builder.Append(choices);
        //    Grammar grammar = new Grammar(builder);
        //    srEngine.LoadGrammarAsync(grammar);
        //    srEngine.SetInputToDefaultAudioDevice();
        //    srEngine.SpeechRecognized += recognizedVoice;
        //    srEngine.RecognizeAsync(RecognizeMode.Multiple);
        //}

        //private void recognizedVoice(object sender, SpeechRecognizedEventArgs e)
        //{
        //    switch (e.Result.Text)
        //    {
        //        case "How are you":
        //            Console.WriteLine(e.Result.Text);
        //            synthesizer.Speak("Im fine thank you.");
        //            break;
        //        case "Can I change you?":
        //            Console.WriteLine(e.Result.Text);
        //            synthesizer.Speak("You are not allowed to do that");
        //            break;
        //        default:
        //            Console.WriteLine("Unknown command.");
        //            break;
        //    }
        //}
