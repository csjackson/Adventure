using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class DanceCommand : BaseCommand, ICommand
    {
   
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "dance");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            Console.WriteLine(String.Format("You gracefully dance the {0}.", output));
                
        }
    }
   
}
