using LSCore.SortAndPage.Contracts;
using Sample.SortAndPage.Api.SortColumnCodes;

namespace Sample.SortAndPage.Api.Requests;

public class ProductsGetAllRequest : LSCoreSortableAndPageableRequest<ProductsSortColumn>;
