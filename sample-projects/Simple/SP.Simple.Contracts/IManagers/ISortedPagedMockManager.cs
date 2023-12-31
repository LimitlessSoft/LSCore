﻿using LSCore.Contracts.Http;
using LSCore.Contracts.Responses;
using SP.Simple.Contracts.Dtos.SortedPagedMock;
using SP.Simple.Contracts.Requests.SortedPagedMock;

namespace SP.Simple.Contracts.IManagers
{
    public interface ISortedPagedMockManager
    {
        LSCoreSortedPagedResponse<GetSortedPagedMockDto> Get(GetSortedPagedMockRequest request);
        LSCoreListResponse<GetSortedPagedMockDto> GetSorted(GetSortedMockRequest request);
    }
}
