using LSCore.Contracts.Requests;
using LSCore.Domain.Extensions;
using RB.Contracts.Dtos.Products;
using RB.Contracts.Entities;
using RB.Contracts.Interfaces.IManagers;
using RB.Contracts.Interfaces.IRepositories;

namespace RB.Domain.Managers;

public class ProductManager(IProductRepository productRepository) : IProductManager
{
    public ProductDto Get(LSCoreIdRequest request) =>
        productRepository.Get(request.Id).ToDto<ProductEntity, ProductDto>();

    public List<ProductDto> GetMultiple() =>
        productRepository.GetMultiple().ToDtoList<ProductEntity, ProductDto>();

    public void Initialize()
    {
        var products = new List<ProductEntity>();
        for (var i = 0; i < 100; i++)
            products.Add(new ProductEntity
            {
                Name = $"Product {i}",
                IsActive = true
            });
        
        productRepository.Create(products);
    }
}