using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF

{
    [Table("Product")]
    public class Product
    {
        [Key]//primary key
        public int ProductId { get; set; }

        [Required]//not null
        [StringLength(50)]//Nvarchar(50)
        [Column("Tensanpham", TypeName = "ntext")]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? CategId { get; set; }

        //cách tạo ra foreign key
        [ForeignKey("CategId")]
        [Required]
        public Category Category { get; set; } //FK -> PK

        public void PrintInfor() => Console.WriteLine($"{ProductId} - {Name} - {Price}");
    }
    
}