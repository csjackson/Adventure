using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class BonkCommand : BaseCommand, ICommand
    {
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "bonk");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            Console.WriteLine(String.Format("You bonk {0} on the head. Ouch!", output));
        }
    }
}
