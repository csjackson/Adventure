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
        private IRepository<GameObject> repository;

        public MasterRoom(IRepository<GameObject> repository)
        {
        	this.repository = repository;
        }
        public int PlaceToStand(IRepository<GameObject> repository)
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
                    }
                    int Id = Master.GameObjectId;
                    return Id;
                }
            }
    }
 
}
