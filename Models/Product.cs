using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF

{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string Provider { get; set; }

        public void PrintInfor() => Console.WriteLine($"{ProductId} - {ProductName} - {Provider}");
    }
    
}