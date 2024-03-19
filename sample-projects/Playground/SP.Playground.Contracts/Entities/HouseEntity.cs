using LSCore.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Playground.Contracts.Entities
{
    public class HouseEntity : LSCoreEntity
    {
        public int Number { get; set; }
        public int StreetId { get; set; }

        [NotMapped]
        public StreetEntity Street { get; set; }
    }
}
