using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure 
{
    class ClapCommand : BaseCommand, ICommand
    {
           public bool IsValid(string input)
        {
            return IsFirstWord(input, "clap");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            Console.WriteLine(String.Format("You applaud at {0}. Bravo!", output));
         }
}
}
