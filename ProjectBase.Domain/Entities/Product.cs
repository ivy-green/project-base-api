namespace ProjectBase.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Desc { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public int ProductTypeId { get; set; }
        public User ProductType { get; set; }
    }
}
