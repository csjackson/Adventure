using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class PanicCommand : BaseCommand, ICommand
    {
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "panic");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            Console.WriteLine(String.Format("One look at {0} sends you into a panic.", output));

        }
    }
}
