using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;


namespace Adventure.Commands
{
    public class LookCommand : BaseCommand, ICommand
    {
        
        private IConsoleFacade console;
        private IRepository<GameObject> repository;

        public LookCommand(IConsoleFacade console,  IRepository<GameObject> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "look"); 
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (repository)
            {
                var LookedAt = repository.AsQueryable()
                    .FirstOrDefault(qq => qq.Name == output);
                if (LookedAt == null)
                {
                    console.WriteLine("I don't see that here.");
                    return;
                }
                console.WriteLine(LookedAt.Description);
            }
        }
    }
}
