using LSCore.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Playground.Contracts.Entities
{
    public class CityEntity : LSCoreEntity
    {
        public string Name { get; set; }

        [NotMapped]
        public List<UserEntity>? Users { get; set; }
        [NotMapped]
        public List<StreetEntity>? Streets { get; set; }
    }
}
