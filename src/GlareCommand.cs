using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class GlareCommand : BaseCommand, ICommand
    {
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "glare");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            Console.WriteLine(String.Format("You fix {0} with a baleful glare.", output));

        }
    }
}
