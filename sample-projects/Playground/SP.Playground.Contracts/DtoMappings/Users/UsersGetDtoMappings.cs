using SP.Playground.Contracts.Dtos.Users;
using SP.Playground.Contracts.Entities;
using LSCore.Contracts.Interfaces;

namespace SP.Playground.Contracts.DtoMappings.Users
{
    public class UsersGetDtoMappings : ILSCoreDtoMapper<UsersGetDto, UserEntity>
    {
        public UsersGetDto ToDto(UserEntity sender) =>
            new UsersGetDto()
            {
                Id = sender.Id,
                Name = sender.Name,
                Address = sender.City == null || sender.City.Streets == null ? "UNDEFINED" : sender.City.Name + " - " + sender.City.Streets[0].Name
            };
    }
}
