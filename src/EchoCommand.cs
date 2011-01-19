using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class EchoCommand : Adventure.ICommand
    {
        public bool IsValid(string input)
        {
            return input.Trim().ToLower().Split(' ')[0] == "echo";
        }

        public void Execute(string input)
        {
            var output = string.Join(" ", input.Trim().Split(' ').Skip(1));
            Console.WriteLine(output);
        }
    }
}
