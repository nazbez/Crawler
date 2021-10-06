
namespace Crawler.WebAPI.Models
{
    public class ResponseModel
    {
        public object Object { get; set; } = null;
        public bool IsSuccessful { get; set; } = true;
        public string Error { get; set; } = "";
    }
}
