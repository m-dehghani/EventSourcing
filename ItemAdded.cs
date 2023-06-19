namespace OrderingSystem.Events
{
    class ItemAdded
    {
        public string OrderId;
        public Shared.OrderItem Item;
        public double Total;
    }
}