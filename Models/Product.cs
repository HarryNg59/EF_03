using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF

{
    // [Table("Product")]
    public class Product
    {
        // [Key] = HasKey(p => p.ProductId)//primary key
        public int ProductId { get; set; }

        [Required]//not null
        [StringLength(50)]//Nvarchar(50)
        [Column("Tensanpham", TypeName = "ntext")]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int CategId { get; set; } //có ? sau int thì là ON DELETE CASCADE
        //Reference Nagivation -> tham chiếu từ model này sang model khác Foreign key (quan hệ 1-nhiều)
        //cách tạo ra foreign key
        // [ForeignKey("CategId")]//dùng fluent api nên ko cần cái này nữa
        // [Required]
        public virtual Category Category { get; set; } //FK -> PK

        public int? CategId2 { get; set; }
        //Reference Nagivation -> tham chiếu từ model này sang model khác Foreign key (quan hệ 1-nhiều)
        //cách tạo ra foreign key
        // [ForeignKey("CategId2")]
        // [InverseProperty("Products")]
        // [Required]
        public virtual Category Category2 { get; set; } //FK -> PK

        public void PrintInfor() => Console.WriteLine($"{ProductId} - {Name} - {Price} - {CategId}");
    }
    
}