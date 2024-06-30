namespace ProjectBase.Domain.Entities
{
    public class ProductType : EntityBase
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public virtual List<Product> Products { get; set; } = [];
    }
}
