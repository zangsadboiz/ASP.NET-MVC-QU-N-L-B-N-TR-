using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Teahub.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public long ReviewID { get; set; }
        public string? Title { get; set; }
        public string? Abstract { get; set; }
        public string? Contents { get; set; }
        public string? Images { get; set; }
        public string? Link { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public int ReviewOrder { get; set; }

        public int MenuID { get; set; }
        public int Category { get; set; }
        public int Status { get; set; }

    }
}
