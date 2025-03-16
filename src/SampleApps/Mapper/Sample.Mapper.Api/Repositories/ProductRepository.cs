using LSCore.Exceptions;
using Sample.Mapper.Api.Entities;
using Sample.Mapper.Api.Interfaces;

namespace Sample.Mapper.Api.Repositories;

public class ProductRepository : IProductRepository
{
	private static readonly List<ProductEntity> _products =
	[
		new()
		{
			Id = 1,
			Name = "Product 1",
			CreatedAt = DateTime.UtcNow,
			IsActive = true
		},
		new()
		{
			Id = 2,
			Name = "Product 2",
			CreatedAt = DateTime.UtcNow,
			IsActive = true
		},
		new()
		{
			Id = 3,
			Name = "Product 3",
			CreatedAt = DateTime.UtcNow,
			IsActive = false
		},
		new()
		{
			Id = 4,
			Name = "Product 4",
			CreatedAt = DateTime.UtcNow,
			IsActive = true
		}
	];

	public ProductEntity Get(int id)
	{
		var product = _products.FirstOrDefault(x => x.Id == id);

		if (product is null)
			throw new LSCoreNotFoundException();

		return product;
	}

	public List<ProductEntity> GetAll() => _products.Where(x => x.IsActive).ToList();
}
