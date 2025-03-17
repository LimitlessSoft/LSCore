using LSCore.Mapper.Contracts;
using Sample.SortAndPage.Api.Dtos;
using Sample.SortAndPage.Api.Entities;

namespace Sample.SortAndPage.Api.Mappers;

public class ProductDtoMapper : ILSCoreMapper<ProductEntity, ProductDto>
{
	public ProductDto ToMapped(ProductEntity source) =>
		new() { Id = source.Id, Name = source.Name };
}
