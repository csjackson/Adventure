using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class AngryCommand : BaseCommand, ICommand
    {
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "angry");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            Console.WriteLine(String.Format("You shake your fist at {0} in anger.", output));
        }
    }
}
