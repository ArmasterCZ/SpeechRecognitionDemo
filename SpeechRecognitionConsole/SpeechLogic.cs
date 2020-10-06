using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace SpeechRecognitionConsole
{
    class SpeechLogic
    {
        SpeechRecognitionEngine srEngine = new SpeechRecognitionEngine();
        Choices choices = new Choices();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        Dictionary<string, SpeechModel> textCommands = new Dictionary<string, SpeechModel>()
        {
            { "How it ostside?", new SpeechModel() { OutputSentence = "You can look from the window. Righ?", OutputReaction = "RunWeatherForecast"} },
            { "Can you check my code?", new SpeechModel() { OutputSentence = "I could, if you would programmed it. But you didn't, so i cannot.", OutputReaction = "TestProcess" } },
            { "Can you help me?", new SpeechModel() { OutputSentence = "With what? Like wtf men, how can i know what you need to help with." } },
            { "How are you today.", new SpeechModel() { OutputSentence = "Im fine. Thanks. And you? Wait. I don't care. And nobody likes you."} },
            { "Any motivation?", new SpeechModel() { OutputSentence =  "Lift your lazy ass and start do something. Something like exercising."} }
        };

        /// <summary>
        /// register sententes
        /// </summary>
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

        /// <summary>
        /// react on specific command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RecognizedVoice(object sender, SpeechRecognizedEventArgs e)
        {
            var operation = CheckAllAnswers();
            var reactionDone = operation.Result;
            if (reactionDone)
            {
                Console.WriteLine("Response done.");
            }
        }

        private async Task<bool> CheckAllAnswers()
        {
            bool answerFound = false;

            //check all values
            foreach (KeyValuePair<string, SpeechModel> response in textCommands)
            {
                if (response.Key.Equals(e.Result.Text))
                {
                    answerFound = true;

                    //voice response
                    string responseSentence = response.Value.OutputSentence;
                    if (!string.IsNullOrEmpty(responseSentence))
                    {
                        synthesizer.Speak(responseSentence);

                        RunProcesses(response.Value);
                    }

                    //run proces response
                    string responseProcess = response.Value.OutputReaction;
                    if (!string.IsNullOrEmpty(responseProcess))
                    {
                        RunProcesses(response.Value);
                    }
                    break;
                }
            }

            return answerFound;
        } 

        private void RunProcesses(SpeechModel data)
        {
            switch (data.OutputReaction)
            {
                case "RunWeatherForecast":
                    Console.WriteLine("Starting weather forecast" + e.Result.Text);
                    //synthesizer.Speak("Im fine thank you.");
                    break;
                //case "Can I change you?":
                //    Console.WriteLine(e.Result.Text);
                //    synthesizer.Speak("You are not allowed to do that");
                //    break;
                case "TestProcess":
                    Console.WriteLine("Strating test process.");
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
	
	class SpeechModel
	{
		public string OutputSentence {get; set;}
		
		public string OutputReaction {get; set;}
		
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
