using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public interface IConsoleFacade
    {
        void WriteLine(string format, params object[] arg);
        ConsoleColor ForegroundColor { get; set; }
        void ResetColor();
    }

}
