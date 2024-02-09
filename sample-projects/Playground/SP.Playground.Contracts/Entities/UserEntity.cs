using LSCore.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Playground.Contracts.Entities
{
    public class UserEntity : LSCoreEntity
    {
        public string Name { get; set; }
        public int? CityId { get; set; }

        [NotMapped]
        public CityEntity? City { get; set; }
    }
}
