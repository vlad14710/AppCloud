using System.ComponentModel.DataAnnotations.Schema;

namespace AppCloud
{
    public class FileModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        [ForeignKey("AppUser")]
        public string? User { get; set; }

        public AppUser? AppUser { get; set; }

    }
}
