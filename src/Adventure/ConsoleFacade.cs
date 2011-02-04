using System;

namespace Adventure
{
    public class ConsoleFacade : IConsoleFacade
    {

        #region IConsoleFacade Members

        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }

        #endregion
    }
}
