using System.ComponentModel.DataAnnotations;

namespace Crawler.WebApplication.Models
{
    public class UserInputModel
    {
        [Url]
        public string Url { get; set; }
    }
}
