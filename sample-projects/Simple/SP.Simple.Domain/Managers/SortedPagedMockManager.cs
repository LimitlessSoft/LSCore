using LSCore.Contracts.Extensions;
using LSCore.Contracts.Http;
using LSCore.Contracts.Responses;
using SP.Simple.Contracts.Dtos.SortedPagedMock;
using SP.Simple.Contracts.Enums.SortColumnCodes;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Requests.SortedPagedMock;

namespace SP.Simple.Domain.Managers
{
    public class SortedPagedMockManager : ISortedPagedMockManager
    {
        public LSCoreSortedPagedResponse<GetSortedPagedMockDto> Get(GetSortedPagedMockRequest request)
        {
            #region Initialize mock data
            var mockListData = new List<GetSortedPagedMockDto>();
            for(int i = 0; i < 100; i++)
                mockListData.Add(new GetSortedPagedMockDto()
                {
                    Name = $"{ Random.Shared.Next(Int32.MaxValue) } [{i}]",
                    Description = $"{Random.Shared.Next(Int32.MaxValue)} [{i}]"
                });
            #endregion

            return mockListData.AsQueryable()
                .ToSortedAndPagedResponse(request, SortedPagedMockSortColumnCodes.SortedPagedMockSortRules);
        }

        public LSCoreListResponse<GetSortedPagedMockDto> GetSorted(GetSortedMockRequest request)
        {
            #region Initialize mock data
            var mockListData = new List<GetSortedPagedMockDto>();
            for (int i = 0; i < 100; i++)
                mockListData.Add(new GetSortedPagedMockDto()
                {
                    Name = $"{Random.Shared.Next(Int32.MaxValue)} [{i}]",
                    Description = $"{Random.Shared.Next(Int32.MaxValue)} [{i}]"
                });
            #endregion

            return new LSCoreListResponse<GetSortedPagedMockDto>(mockListData.AsQueryable()
                .SortQuery(request, SortedPagedMockSortColumnCodes.SortedPagedMockSortRules)
                .ToList());
        }
    }
}
