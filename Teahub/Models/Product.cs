using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Teahub.Models
{
    [Table("Product")] 
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Abstract { get; set; }
        public string? Contents { get; set; }
        public string? Images { get; set; }
        public string? Link { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public int ProductOrder { get; set; }

        public int MenuID { get; set; }
        public int Status { get; set; }
        public int Category { get; set; }

    }
}
