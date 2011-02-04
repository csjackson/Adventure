using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class EchoCommand : BaseCommand, Adventure.ICommand
    {
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "echo");
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            Console.WriteLine(output);
        }
                             }
}

