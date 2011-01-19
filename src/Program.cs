using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            var echo = new EchoCommand();
            var unknown = new UnknownCommand();
            do 
            {
                var input = Console.ReadLine();
                if (input.Trim() == "exit") break;

                if (echo.IsValid(input)) echo.Execute(input);
                else if (unknown.IsValid(input)) unknown.Execute(input);
            }
            while (true); ;
        }
    }
}
