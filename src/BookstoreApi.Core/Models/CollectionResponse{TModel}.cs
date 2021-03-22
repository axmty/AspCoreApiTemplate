using System.Collections.Generic;

namespace BookstoreApi.Core.Models
{
    public class CollectionResponse<TModel>
    {
        public CollectionResponse(IEnumerable<TModel> data, int page, int total, int totalPages)
        {
            this.Data = data;
            this.Page = page;
            this.Total = total;
            this.TotalPages = totalPages;
        }

        public IEnumerable<TModel> Data { get; set; }

        public int Page { get; set; }

        public int Total { get; set; }

        public int TotalPages { get; set; }
    }
}
