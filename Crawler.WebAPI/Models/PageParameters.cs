using System.ComponentModel.DataAnnotations;

namespace Crawler.WebAPI.Models
{
    public class PageParameters
    {
		const int maxPageSize = 100;
		[Range(1, int.MaxValue)]
		public int PageNumber { get; set; } = 1;

		private int pageSize = 10;

		[Range(1, maxPageSize)]
		public int PageSize
		{
			get
			{
				return pageSize;
			}
			set
			{
				pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}
	}
}
