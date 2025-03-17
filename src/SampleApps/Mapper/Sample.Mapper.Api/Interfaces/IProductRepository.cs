using Sample.Mapper.Api.Entities;

namespace Sample.Mapper.Api.Interfaces;

public interface IProductRepository
{
	ProductEntity Get(int id);
	List<ProductEntity> GetAll();
}
