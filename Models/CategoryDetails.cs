namespace EF
{
    public class CategoryDetails
    {
        public int CategoryDetailID { set; get; }

        public int UserId { set; get; }

        public DateTime Created { set; get; }

        public DateTime Update { set; get; }

        public int CountProduct { set; get; }

        public Category category { set; get; }
    }
}