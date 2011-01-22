using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class YellCommand : BaseCommand, ICommand
    {

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "yell");
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input).ToUpper();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(output);
            Console.ResetColor();
        }

    }
}
 
