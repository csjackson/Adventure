using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure
{
    public interface IMasterRoom
    {
        int Id { get; set; }
    }
    public class MasterRoom : IMasterRoom
    {
        public int Id { get; set; } 
    }
    public class MasterRoomFactory
    {
        private IRepository<GameObject> repository;

        public MasterRoomFactory(IRepository<GameObject> repository)
        {
            this.repository = repository;
            
        }
        public IRepository<GameObject> CreateMasterRoom()
            {
                GameObject MasterRoom;

                using (repository)
                {
                    MasterRoom = repository.AsQueryable().FirstOrDefault(qq => qq.Name == "MasterRoom" && qq.Type == "Room");
                    if (MasterRoom == null)
                    {
                        MasterRoom = new GameObject()
                        {
                            Name = "Master Room",
                            Description = "This is the Master Room. It is everwhere and nowhere.",
                            Type = "Room"
                        };
                        repository.Add(MasterRoom);
                    }
                    return new MasterRoom() { Id = MasterRoom.GameObjectId };
                }
            }
    }
}
