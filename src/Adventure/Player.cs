using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure
{
    public interface IPlayer
    {
        int Id { get; set; }
    }
    public class Player : IPlayer
    {
        private IRepository<GameObject> repository;
        private IConsoleFacade console;
        private IMasterRoom master;

        public Player(IRepository<GameObject> repository, IConsoleFacade console, IMasterRoom master)
        {
            this.repository = repository;
            this.console = console;
            this.master = master;
        }
        public int FindPlayer(IRepository<GameObject> repository, IConsoleFacade console, IMasterRoom master)
        {
            using (repository)
            {
                var Player = repository.AsQueryable()
               .FirstOrDefault(qq => qq.Type == "Player");
                if (Player == null)
                {
                    console.WriteLine("What will you name your new player?");
                    string NewName = Console.ReadLine();

                    Player = new GameObject()
                    {
                        Name = NewName,
                        Description = "This is the default player description.",
                        Type = "Player",
                        Location = master.Id
                    };

                        repository.Add(Player);
                }
                int Id = Player.GameObjectId;
                return Id;
            }
        }
    }

}
