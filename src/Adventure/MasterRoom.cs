using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure
{
    public interface IMasterRoom
    {
        int Id { get; }
    }
    public class MasterRoom : IMasterRoom
    {
        public int Id { get; set; }
    }
    public interface IMasterRoomFactory
    {
        IMasterRoom FindRoom();
    }
    public class MasterRoomFactory : IMasterRoomFactory 
    {
        private IRepository<GameObject> repository;

        public MasterRoomFactory(IRepository<GameObject> repository)
        {
        	this.repository = repository;
        }
        public IMasterRoom FindRoom()
            {
                using (repository)
                {
                    var Master = repository.AsQueryable()
                   .FirstOrDefault(qq => qq.Name == "Master Room");
                    if (Master == null)
                    {
                        Master = new GameObject()
                        {
                            Name = "Master Room",
                            Description = "This is the Master Room.",
                            Type = "Room"
                        };
                        repository.Add(Master);
                        repository.UnitOfWork.Save();
                    }
                    return new MasterRoom() { Id = Master.GameObjectId };
                }
            }
    }
 
}
