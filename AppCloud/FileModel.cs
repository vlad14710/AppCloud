using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace AppCloud
{
    public class FileModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [ForeignKey("AppUser")]
        public string? User { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
