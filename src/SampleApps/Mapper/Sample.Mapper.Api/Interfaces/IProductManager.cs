using Sample.Mapper.Api.Dtos;

namespace Sample.Mapper.Api.Interfaces;

public interface IProductManager
{
	ProductDto Get(int id);
	List<ProductDto> GetAll();
}
