using System;

namespace Adventure
{
    public class ConsoleFacade : IConsoleFacade
    {
        public void Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
        }

        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }
        public ConsoleColor ForegroundColor
        {
            get
            {
                return Console.ForegroundColor;
            }
            set
            {
                Console.ForegroundColor = value;
            }
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }
    }
}
