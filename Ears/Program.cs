using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Threading; 
namespace NumberFive.Ears
{
    class Program
    {
        // Variables.
        static string[] _Machine_Greeting;
        static string[] _ChoiceList;
        static Choices _Machine_Choice = new Choices();
        static GrammarBuilder _Machine_GB = new GrammarBuilder();
        static SpeechSynthesizer _Voice = new SpeechSynthesizer();
        static PromptBuilder _VoiceBuilder = new PromptBuilder();
        static string _MTime;        
        // Main function...
        static void Main(string[] args)
        {
            //AddCommands(8, GenerateCommandArray(8));
            AddCommands(36);
            CreateRecognitionSet(_Machine_Choice, _Machine_GB);
        }
        // Generic Windows Recognizer
        static void CreateRecognizer(string _CultureInfo)
        {

            using (SpeechRecognitionEngine _rec = new SpeechRecognitionEngine(new CultureInfo(_CultureInfo)))
            {
                Console.WriteLine("Listening...");
                _rec.LoadGrammar(new DictationGrammar());
                _rec.SetInputToDefaultAudioDevice();
                _rec.RecognizeAsync(RecognizeMode.Multiple);
                _rec.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_rec_SpeechRecognized);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
        // eSpak function.
        static void eSpeak(string _Input)
        {
            //espeak.exe -vmb-us3+m5 "Yes." -g 40 -s 850 -k80 -p 40

            using (Process _P = new Process())
            {
                ProcessStartInfo _PI = _P.StartInfo;
                _PI.FileName = "espeak.exe";
                _PI.Arguments = " -vmb-us1+f2 " + '"'+ _Input + '"' + " -g 140 -s 600 -k20 -p 120 " ;
                _PI.CreateNoWindow = false;
                _PI.UseShellExecute = false;

                _P.Start();
                _P.WaitForExit();
            }
        }
        // Emergency system shutdown function
        protected static void ShutdownMachine()
        {
            using (Process _P = new Process())
            {
                ProcessStartInfo _PI = _P.StartInfo;
                _PI.FileName = "cmd.exe";
                _PI.Arguments = " /C shutdown /s /t 0";
                _PI.CreateNoWindow = false;
                _PI.UseShellExecute = true;
                _P.Start();
            }
        }
        // Shutdown Ears.
        static void ShutdownEars(int _statusCode)
        {
            eSpeak("Closing...");
            Environment.Exit(_statusCode);
        }
        // Get Current Time
        static void GetCurrentTime()
        {
            _MTime = DateTime.Now.ToString("hh:mm tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (_MTime.Contains("AM"))
            {
                //_time.Replace("AM", "A. M.");
                Console.WriteLine(_MTime.Replace("AM", "A.M."));
                eSpeak("The time is" + _MTime.Replace("AM", "A. M..."));
            }
            if (_MTime.Contains(" PM"))
            {
                _MTime.Replace(" PM", "P. M.");
                Console.WriteLine(_MTime);
                eSpeak("The time is" + _MTime.Replace("PM", "P. M..."));
            }
        }
        // Generate the dynamic command array.
        static string[] GenerateCommandArray(int _num)
        {
            string[] _CommandArray1;
            _CommandArray1 = new string[_num];
            return _CommandArray1;
        }

        static void OpenWindowsApp(string _AppName, string _AppArguments, bool _useShellEx)
        {
            using (Process _P = new Process())
            {
                ProcessStartInfo _PI = _P.StartInfo;
                _PI.FileName = _AppName+".exe";
                _PI.Arguments = _AppArguments;
                _PI.CreateNoWindow = false;
                _PI.UseShellExecute = _useShellEx;
                _P.Start();
            }
        }
        // Add commands to the array and give them a position number.
        static void AddCommands(int _numberOfCommands)
        {
            // Create Greeting Recognition Array.
            _ChoiceList = new string[_numberOfCommands];
            // Create Greeting Recognition Set.
            // BY SPLITTING UP EACH CHOICE INTO CATAGORYS WE CAN BETTER ORGINIZE OUt
            // PRE DEFINED GREETINGS.
            CreateGreetingSet(1, "Hello Computer", _ChoiceList);
            CreateGreetingSet(5, "Good Morning Computer", _ChoiceList);
            CreateGreetingSet(6, "Good Afternoon Computer", _ChoiceList);
            CreateGreetingSet(7, "Good Evening Computer", _ChoiceList);
            
            // PRE DEFINED PERSONAL QUESTIONS
            CreateGreetingSet(9, "What is your name?", _ChoiceList);
            CreateGreetingSet(2, "Who are you?", _ChoiceList);

            // PRE DEFINED COMMANDS 
            CreateGreetingSet(0, "Voice Shutdown", _ChoiceList);
            CreateGreetingSet(4, "Emergency System Shutdown", _ChoiceList);
            // Time.. done
            CreateGreetingSet(3, "What time is it?", _ChoiceList);
            // Date .. to do
            CreateGreetingSet(10, "What day is it?", _ChoiceList);
            CreateGreetingSet(11, "What month is it?", _ChoiceList);
            CreateGreetingSet(12, "What year is it?", _ChoiceList);
            // Application Commands [WINDOWS]... to do
            CreateGreetingSet(13, "Computer, Open Microsoft Notepad", _ChoiceList);
            CreateGreetingSet(14, "Computer, Open Command 64", _ChoiceList);
            CreateGreetingSet(15, "Computer, Open Command 32", _ChoiceList);
            CreateGreetingSet(16, "Computer, Open Microsoft Windows Media Player", _ChoiceList);
            CreateGreetingSet(17, "Computer, Open Run Command", _ChoiceList);
            CreateGreetingSet(18, "Computer, Open Microsoft Internet Explorer", _ChoiceList);
            CreateGreetingSet(19, "Computer, Open Microsoft Edge", _ChoiceList);
            CreateGreetingSet(20, "Computer, Open Microsoft Paint", _ChoiceList);
            CreateGreetingSet(21, "Computer, Open Microsoft Word", _ChoiceList);
            CreateGreetingSet(22, "Computer, Open Wordpad", _ChoiceList);
            CreateGreetingSet(23, "Computer, Open Putty", _ChoiceList);
            // to do...
            CreateGreetingSet(24, "Computer, Open Component Services", _ChoiceList);
            CreateGreetingSet(25, "Computer, Open Computer Management", _ChoiceList);
            CreateGreetingSet(26, "Computer, Open Run Command", _ChoiceList);
            CreateGreetingSet(27, "Computer, Open Disk Defrag", _ChoiceList);
            CreateGreetingSet(28, "Computer, Open Disk Cleanup", _ChoiceList);
            CreateGreetingSet(29, "Computer, Open Event Viewer", _ChoiceList);
            CreateGreetingSet(30, "Computer, Open Run Command", _ChoiceList);
            // to do..
            CreateGreetingSet(31, "Computer, Open Adobe Audition", _ChoiceList);
            CreateGreetingSet(32, "Computer, Open Adobe Dreamweaver", _ChoiceList);
            CreateGreetingSet(33, "Computer, Open Adobe Photoshop", _ChoiceList);
            // Games to do...
            CreateGreetingSet(34, "Computer, Open Steam", _ChoiceList);
            CreateGreetingSet(35, "Computer, Open Origin", _ChoiceList);


            // SELF AWARE QUESTIONS...TODO.... FOR SELF AWARE AI....
            CreateGreetingSet(8, "How are you feeling today?", _ChoiceList);
            // CreateGreetingSet(10, "How old are you?", _ChoiceList);
            // CreateGreetingSet(11, "Where did you come from?", _ChoiceList);
            // CreateGreetingSet(12, "What is your purpose?", _ChoiceList);
        }
        // for counting...
        static int i;
        // Function to add into Array dynamically.
        static void CreateGreetingSet(int _ChoiceNumber, string _ChoiceString, string[] _ChoiceArray)
        {
            _ChoiceArray[_ChoiceNumber] = _ChoiceString;
            i++;
            Thread.Sleep(75);
            Console.WriteLine("Reading Voice Recognition Choice: " + i.ToString());
        }
        // Listen to the speach, and recognize the words spoken, and check if they match out grammer sets.
        static void CreateRecognitionSet(Choices _Choice, GrammarBuilder _GrammerBuilder)
        {
            _Choice.Add(_ChoiceList);
            _GrammerBuilder.Append(_Choice);
            Grammar _Grammer = new Grammar(_GrammerBuilder);
            using (SpeechRecognitionEngine _rec = new SpeechRecognitionEngine(new CultureInfo("en-US")))
            {
                _rec.LoadGrammarAsync(_Grammer);
                _rec.SetInputToDefaultAudioDevice();
                Console.WriteLine("Listening: ....");
                _rec.RecognizeAsync(RecognizeMode.Multiple);
                _rec.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_rec_SpeechRecognized);
                // Console.WriteLine("Thinking....");
                // Thread.Sleep(100);
                //Console.Clear();
                while (true)
                {
                    Console.ReadLine();
                }
            }

        } 

        // Function to read each greeting an provide a responce based on each greeting recieved or spoken to the machine.
        static void ProcessSpeech(string _IncomingSpeech)
        {
            #region
            if (_IncomingSpeech == "Voice Shutdown")
            {
                ShutdownEars(0);
            }
            if (_IncomingSpeech == "Hello Computer")
            {
                eSpeak("Hello.");
            }
            if (_IncomingSpeech == "Who are you?")
            {
                eSpeak("I am Johnny Number Five.");
            }
            if (_IncomingSpeech == "What time is it?")
            {
                GetCurrentTime();
            }
            if (_IncomingSpeech == "Emergency System Shutdown")
            {
                eSpeak("Emergency Shutdown Initiated...");
                eSpeak("Goodbye...");
                ShutdownMachine();
            }
            if (_IncomingSpeech == "How are you feeling today?")
            {
                eSpeak("I am operating at full capacity... how. are you feeling?");
            }
            if (_IncomingSpeech == "What is your name?")
            {
                eSpeak("NUMBER FIVE ");
            }
            if (_IncomingSpeech == "Good Afternoon Computer")
            {
                eSpeak("Good Afternoon.");
            }

            if (_IncomingSpeech == "Good Evening Computer")
            {
                eSpeak("Good Evening.");
            }

            if (_IncomingSpeech == "Good Morning Computer")
            {
                eSpeak("Good Morning.");
            }
            #endregion

        }


        static void _rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            ProcessSpeech(e.Result.Text);
        }

                //switch (e.Result.Text)
            //{
                    
            //    case "Hello Computer":
            //        Greeting(e.Result.Text);
            //        break;

             //   case "How are you?":
             //       Greeting(e.Result.Text);
             //       break;

               // case "Who are you?":
               //     eSpeak("Aye Am.. Number... Five.");
               //     break;

                //case "What time is it?":
                //    GetCurrentTime();
                //    break;

                //case "Voice Shutdown":
                //    
                 //   break;

               // case "Emergency System Shutdown":
               //     
               //     eSpeak("Goodbye..");
               //     ShutdownMachine();
               //     break;
    }
}
