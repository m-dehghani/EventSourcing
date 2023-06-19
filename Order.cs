using System.Collections.Generic;
using OrderingSystem.Events;
namespace OrderingSystem.Entities
{
    class Order : Entity
    {
        OrderId id;
        List<OrderItem> items;
        Money totalAmount;

        public void AddItem(OrderItem newItem)
        {
            if (!CanAddItem(item))
                throw new DomainException("Unable to add the item");

            var newTotal = totalAmount.Add(newItem.LineTotal).AsDouble;
            Apply(
              new ItemAdded
              {
                  OrderId = id,
                  Item = Map(newItem),
                  Total = newTotal
              }
              );

        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case ItemAdded e:
                    items.Add(OrderItem.FromEvent(e.Item));
                    totalAmount = e.Total;
                    break;
            }
        }


    }
}