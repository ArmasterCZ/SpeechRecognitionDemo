using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecognitionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            SpeechLogic logic = new SpeechLogic();
            logic.Start();
            Console.WriteLine("Waiting.");
            Console.ReadLine();
        }
    }
}
