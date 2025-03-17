using LSCore.SortAndPage.Contracts;
using Sample.SortAndPage.Api.Dtos;
using Sample.SortAndPage.Api.Requests;

namespace Sample.SortAndPage.Api.Interfaces;

public interface IProductManager
{
	LSCoreSortedAndPagedResponse<ProductDto> GetAll(ProductsGetAllRequest request);
}
