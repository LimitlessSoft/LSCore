using LSCore.Contracts.Http;
using LSCore.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using SP.Simple.Contracts.Dtos.SortedPagedMock;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Requests.SortedPagedMock;

namespace SP.Simple.Api.Controllers
{
    public class SortedPagedMocksController : ControllerBase
    {
        private readonly ISortedPagedMockManager _sortedPagedMockManager;

        public SortedPagedMocksController(ISortedPagedMockManager sortedPagedMockManager)
        {
            _sortedPagedMockManager = sortedPagedMockManager;
        }

        [HttpGet]
        [Route("/sorted-paged-mocks")]
        public LSCoreSortedPagedResponse<GetSortedPagedMockDto> Get([FromQuery] GetSortedPagedMockRequest request) =>
            _sortedPagedMockManager.Get(request);

        [HttpGet]
        [Route("/sorted-mocks")]
        public LSCoreListResponse<GetSortedPagedMockDto> GetSorted([FromQuery] GetSortedMockRequest request) =>
            _sortedPagedMockManager.GetSorted(request);
    }
}
