using System.ComponentModel.DataAnnotations;

namespace Crawler.Services.Models.RequestModels
{
    public class UserInputModel
    {
        [Url]
        public string Url { get; set; }
    }
}
