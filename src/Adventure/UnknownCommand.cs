using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class UnknownCommand : ICommand
    {

        #region ICommand Members

        public void Execute(string input)
        {
            Console.WriteLine("I don't understand.");
        }

        public bool IsValid(string input)
        {
            return true;
        }

        #endregion
    }
}
