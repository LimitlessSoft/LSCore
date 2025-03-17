using LSCore.Mapper.Contracts;
using Sample.Mapper.Api.Dtos;
using Sample.Mapper.Api.Entities;

namespace Sample.Mapper.Api.Mappers;

public class ProductDtoMapper : ILSCoreMapper<ProductEntity, ProductDto>
{
	public ProductDto ToMapped(ProductEntity source) =>
		new() { Id = source.Id, Name = source.Name, };
}
