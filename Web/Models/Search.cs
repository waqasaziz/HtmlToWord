using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Search
    {
        [Required, Url]
        public string URL { get; set; }
    }
}
