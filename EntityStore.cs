using OrderingSystem.Abstract;
using System.Linq;
using System.Collections.Generic;
namespace OrderingSystem.EntityStore
{
    class EntityStore
    {
        EventDatabase db;
        Serializer serializer;

        public void Save<T>(T entity) where T : Entity
        {
            var changes = entity.Changes;
            if (!changes.Any()) return;

            var dbEvents = new List<DbEvent>();
            foreach(var @event in changes){
              var serializedEvent = serializer.Serialize(@event);
              dbEvents.Add(
                data: new DbEvent(serializedEvent),
                type: entity.GetType()
              );
            }
            var streamName = EntityStreamName.For(entity);
            db.AppendEvents(streamName, dbEvents);
      
        }

        public T Load<T>(string id) where T : Entity, new()
        {
            var streamName = EntityStreamName.For(entity);
            var dbEvents = db.ReadEvents(streamName);
            if( dbEvents.Any() ) return default(T);

            var entity = new T();
            foreach (var @event in dbEvents)
            {
                entity.When( @event );
            }
            return entity;
        }
    }
}