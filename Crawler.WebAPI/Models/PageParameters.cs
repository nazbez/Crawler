using System.ComponentModel.DataAnnotations;

namespace Crawler.WebAPI.Models
{
    public class PageParameters
    {
	    private const int MaxPageSize = 100;

		private int pageSize = 10;

		[Range(1, int.MaxValue)]
		public int PageNumber { get; set; } = 1;

		[Range(1, MaxPageSize)]
		public int PageSize
		{
			get
			{
				return pageSize;
			}
			set
			{
				pageSize = (value > MaxPageSize) ? MaxPageSize : value;
			}
		}
	}
}
