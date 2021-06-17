using System;
using System.Threading;

namespace TabloidCLI { 
    public class Music
    {
        public void Mario()
        {
            Console.Beep(659, 150); Console.Beep(659, 150); Thread.Sleep(150);
            Console.Beep(659, 150); Thread.Sleep(167); Console.Beep(523, 150);
            Console.Beep(659, 150); Thread.Sleep(150); Console.Beep(784, 150);
            Thread.Sleep(375); Console.Beep(392, 150);
        }
    }
}