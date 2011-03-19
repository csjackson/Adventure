using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class UnknownInputHandler : IUnknownInputHandler 
    {
          private IConsoleFacade storage;

        public UnknownInputHandler(IConsoleFacade console)
        {
            this.storage = console;
        }

        public void HandleUnknownInput(string input)
        {
            storage.WriteLine("I don't understand.");
        }


    }
}
