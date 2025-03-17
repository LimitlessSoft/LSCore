using LSCore.Mapper.Domain;
using LSCore.SortAndPage.Contracts;
using LSCore.SortAndPage.Domain;
using Sample.SortAndPage.Api.Dtos;
using Sample.SortAndPage.Api.Entities;
using Sample.SortAndPage.Api.Interfaces;
using Sample.SortAndPage.Api.Requests;
using Sample.SortAndPage.Api.SortColumnCodes;

namespace Sample.SortAndPage.Api.Managers;

public class ProductManager(IProductRepository productRepository) : IProductManager
{
	public LSCoreSortedAndPagedResponse<ProductDto> GetAll(ProductsGetAllRequest request) =>
		productRepository
			.GetAll()
			.ToSortedAndPagedResponse<ProductEntity, ProductsSortColumn, ProductDto>(
				request,
				SortColumnRules.ProductsSortColumnCodesRules,
				x => x.ToMapped<ProductEntity, ProductDto>()
			);
}
