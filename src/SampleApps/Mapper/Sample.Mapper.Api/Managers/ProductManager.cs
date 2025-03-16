using LSCore.Mapper.Domain;
using Sample.Mapper.Api.Dtos;
using Sample.Mapper.Api.Entities;
using Sample.Mapper.Api.Interfaces;

namespace Sample.Mapper.Api.Managers;

public class ProductManager(IProductRepository productRepository) : IProductManager
{
	public ProductDto Get(int id) =>
		productRepository.Get(id).ToMapped<ProductEntity, ProductDto>();

	public List<ProductDto> GetAll() =>
		productRepository.GetAll().ToMappedList<ProductEntity, ProductDto>();
}
