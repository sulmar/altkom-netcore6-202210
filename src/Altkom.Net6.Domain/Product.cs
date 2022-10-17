namespace Altkom.Net6.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

    }
}