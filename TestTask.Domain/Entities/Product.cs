using TestTask.Domain.Abstractions;

namespace TestTask.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
