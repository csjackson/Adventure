using System;
using Adventure.Data;

namespace Adventure.Commands
{
    public class CreateCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<GameObject> repository;

        public CreateCommand(IConsoleFacade console, IRepository<GameObject> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "create");
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (repository)
            {
                repository.Add(new GameObject() 
                    { Name = output, Description="", Type ="Item"  });
            }
            console.WriteLine("Object '{0}' created.", output);
        }

    }
}
