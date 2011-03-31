using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public interface IConsoleFacade
    {
        void WriteLine(string format, params object[] arg);
        void Write(string format, params object[] args);
        ConsoleColor ForegroundColor { get; set; }
        void ResetColor();
    }

}
