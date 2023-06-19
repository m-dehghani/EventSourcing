using System.Collections.Generic;

namespace OrderingSystem.Abstract
{
    abstract class Entity
    {
        public List<object> Changes;

        public void Apply(object @event)
        {
            When(@event);
            Changes.Add(@event);
        }

        protected abstract void When(object @event);
    }
}