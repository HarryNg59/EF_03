
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryID { set; get; }

        [StringLength(100)]
        public string Name { set; get; }

        [Column(TypeName = "ntext")]
        public string Description { set; get; }

        //Collect Nagivation: điều hướng tập hợp -> ko tạo ra Foreign key
        public virtual List<Product> Products { set; get; }
    }
}
