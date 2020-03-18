using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NumberFive.Voice
{

    /// <summary>
    ///  Number five.VOICE
    ///  
    /// Iconically named after the robot from short circuit.
    /// This is my attempt to recreate number fives voice,
    /// using the processing power of windows architecture;
    /// and the inguinuity of the eSpeak synthasizer.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            //eSpeak("This is the voice application.");

            
                CopyArguments(args);
            
        }

        static void CopyArguments(string[] xArg)
        {
            // copy the string arguments to prevent an exception being thrown at the start.


        }
            

        static void eSpeak(string _Input)
        {
            //espeak.exe -vmb-us3+m5 "Yes." -g 40 -s 850 -k80 -p 40

            using (Process _P = new Process())
            {
                ProcessStartInfo _PI = _P.StartInfo;
                _PI.FileName = "espeak.exe";
                _PI.Arguments = " -vmb-us1+m1 " + '"' + _Input + '"' + " -g 80 -s 850 -k40 -p 20 ";
                _PI.CreateNoWindow = false;
                _PI.UseShellExecute = false;

                _P.Start();
                _P.WaitForExit();
            }
        }
    }
}
