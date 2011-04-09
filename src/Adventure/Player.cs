using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure
{
    public interface IPlayer
    {
        int Id { get; }
    }
    public class Player : IPlayer
    {
        public int Id { get; set; }
    }

    public interface IPlayerFactory
    {
        IPlayer FindPlayer();
    }
    public class PlayerFactory : IPlayerFactory
    {
        private IRepository<GameObject> repository;
        private IConsoleFacade console;
        private IMasterRoom master;

        public PlayerFactory(IRepository<GameObject> repository, IConsoleFacade console, IMasterRoom master)
        {
            this.repository = repository;
            this.console = console;
            this.master = master;
        }
        public IPlayer FindPlayer()
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
                        Location_Id = master.Id
                    };

                    repository.Add(Player);
                    repository.UnitOfWork.Save();
                }
                return new Player() { Id = Player.GameObjectId };
            }
        }
    }

}
