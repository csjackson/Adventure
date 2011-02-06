using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class LolCommand : BaseCommand, ICommand
    {
        private IConsoleFacade tempVal;
        public LolCommand(IConsoleFacade console)
        {
            this.tempVal = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "lol");
        }

        public void Execute(string input)
        { 
            if (input == "lol") tempVal.WriteLine("You laugh. Tee Hee!");
            else tempVal.WriteLine("You laugh at {0}. Tee Hee!", GetAllButFirstWord(input));
        }
    }
}
