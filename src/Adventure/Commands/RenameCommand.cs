using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class RenameCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<GameObject> repository;

        public RenameCommand(IConsoleFacade console, IRepository<GameObject> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return (IsFirstWord(input, "rename"));
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            string name = output.Split('=')[0].Trim();
            var NewName = output.Split('=')[1].Trim();

            using (repository)
            {
                var item = repository.AsQueryable()
                    .FirstOrDefault(qq => qq.Name == name);
                if (item == null)
                {
                    console.WriteLine("I do not recognize \"{0}\".", item);
                    return;
                }
                console.WriteLine("Name of {0} changed to {1}.",
                    item.Name, NewName);

                item.Name = NewName;
            }

        }

    }
}
