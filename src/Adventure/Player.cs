using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure
{


    public interface IPlayer
    {
        int FindPlayer(IRepository<GameObject> repository, IMasterRoom master);
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
        public int FindPlayer(IRepository<GameObject> repository, IMasterRoom master)
        {
            using (repository)
            {
                var Player = repository.AsQueryable()
               .FirstOrDefault(qq => qq.Type == "Player");
                if (Player == null)
                {
                    Player = new GameObject()
                    {
                        Name = "DefaultPlayer",
                        Description = "This is the default player description.",
                        Type = "Player",
                        Location = master.FindRoom(repository)
                    };

                        repository.Add(Player);
                }
                int Id = Player.GameObjectId;
                return Id;
            }
        }
    }

}
