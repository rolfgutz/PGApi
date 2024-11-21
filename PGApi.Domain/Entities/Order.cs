namespace PGApi.PGApi.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Order(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
            CreatedAt = DateTime.UtcNow;
        }

        // Método de atualização
        public void Update(int quantity)
        {
            Quantity = quantity;
        }
    }
}
