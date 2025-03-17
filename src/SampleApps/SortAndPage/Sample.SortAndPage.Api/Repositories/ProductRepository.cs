using Sample.SortAndPage.Api.Entities;
using Sample.SortAndPage.Api.Interfaces;

namespace Sample.SortAndPage.Api.Repositories;

public class ProductRepository : IProductRepository
{
	private static readonly List<ProductEntity> _products = [];

	static ProductRepository()
	{
		for (var i = 0; i < 100; i++)
			_products.Add(
				new ProductEntity()
				{
					CreatedAt = DateTime.UtcNow,
					Id = i + 1,
					IsActive = Random.Shared.Next(0, 2) == 1,
					Name = string.Join(
						"",
						Enumerable
							.Range(0, Random.Shared.Next(5, 11))
							.Select(_ =>
								"qwertyuiopasdfghjklzxcvbnm   "
									.ToCharArray()[Random.Shared.Next(0, 26)]
									.ToString()
							)
					)
				}
			);
	}

	public IQueryable<ProductEntity> GetAll() => _products.Where(x => x.IsActive).AsQueryable();
}
