using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SearchViewModel
    {
        [Required, Url]
        public string URL { get; set; }
    }
}
