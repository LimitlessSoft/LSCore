using Sample.SortAndPage.Api.Entities;

namespace Sample.SortAndPage.Api.Interfaces;

public interface IProductRepository
{
	IQueryable<ProductEntity> GetAll();
}
