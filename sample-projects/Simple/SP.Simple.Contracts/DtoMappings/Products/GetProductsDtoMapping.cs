using LSCore.Contracts.Interfaces;
using Omu.ValueInjecter;
using SP.Simple.Contracts.Dtos.Products;
using SP.Simple.Contracts.Entities;

namespace SP.Simple.Contracts.DtoMappings.Products
{
    public class GetProductsDtoMapping : ILSCoreDtoMapper<GetProductsDto, ProductEntity>
    {
        public GetProductsDto ToDto(ProductEntity sender)
        {
            var dto = new GetProductsDto();
            dto.InjectFrom(sender);
            return dto;
        }
    }
}
