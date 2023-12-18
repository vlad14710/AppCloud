using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace AppCloud
{
    public class FileModelTemporary
    {
        [DefaultValue("Defult")]
        public string? Name { get; set; }
        [DefaultValue("Defult")]
        public string Info { get; set; }

       
    }
}
