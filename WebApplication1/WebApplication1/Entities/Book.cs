namespace WebApplication1.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double CostPrice { get; set; }
        public int CatagoryId { get; set; }
        public Category Catagory { get; set; }
    }
}
