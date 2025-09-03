namespace Shared
{
	public class PaginatedResult<TEnity>
	{
		public PaginatedResult(int pageIndex, int pageSize, int totalFoundItems, IEnumerable<TEnity> data)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Data = data;
			TotalFoundItems = totalFoundItems;
			TotalPages = (int)Math.Ceiling(totalFoundItems / (double)pageSize);
		}
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int TotalFoundItems { get; set; }
		public IEnumerable<TEnity> Data { get; set; }
	}
}
