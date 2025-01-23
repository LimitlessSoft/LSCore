using ACR.Microservice.Client;
using ACR.Microservice.Contracts.Requests.Products;
using Microsoft.AspNetCore.Mvc;

namespace ACR.Api.Controllers;

public class ProductsController(ACRMicroserviceClient microserviceClient) : ControllerBase
{
    [HttpGet]
    [Route("/products")]
    public async Task<IActionResult> GetProducts() =>
        Ok(await microserviceClient.GetMultipleAsync(new GetMultipleRequest() { MockQueryParam = "mock"}));
}