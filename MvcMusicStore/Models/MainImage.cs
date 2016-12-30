using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models
{
    public class MainImage
    {
        public int MainImageId { get; set; }

        [StringLength(1024)]
        public string ImageUrl { get; set; }
    }
}