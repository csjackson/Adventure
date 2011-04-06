using System;
using Adventure.Data;

namespace Adventure.Commands
{
    public interface IFormatter
    {
        void Output(GameObject obj);
    }
    public class Formatter : IFormatter
    {
        private IConsoleFacade console;
        private IRepository<GameObject> repository;

        public Formatter(IConsoleFacade console, IRepository<GameObject> repository)
        {
            this.console = console;
            this.repository = repository;
        }
        public void Output(GameObject obj)
        {

            console.WriteLine(obj.Description);
        }

    }
}
