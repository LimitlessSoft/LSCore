using Sample.Minimal.Contracts.Dtos.Users;
using Sample.Minimal.Contracts.Entities;
using LSCore.Contracts.Interfaces;
using Omu.ValueInjecter;

namespace Sample.Minimal.Contracts.DtoMappings.Users;

public class UserDtoMapping : ILSCoreDtoMapper<UserEntity, UserDto>
{
    public UserDto ToDto(UserEntity sender)
    {
        var dto = new UserDto();
        dto.InjectFrom(sender);
        return dto;
    }
}