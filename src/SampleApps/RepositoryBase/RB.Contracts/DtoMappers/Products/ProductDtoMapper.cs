using LSCore.Contracts.Interfaces;
using RB.Contracts.Dtos.Products;
using RB.Contracts.Entities;

namespace RB.Contracts.DtoMappers.Products;

public class ProductDtoMapper : ILSCoreDtoMapper<ProductEntity, ProductDto>
{
    public ProductDto ToDto(ProductEntity sender)
    {
        return new ProductDto
        {
            Id = sender.Id,
            Name = sender.Name,
        };
    }
}