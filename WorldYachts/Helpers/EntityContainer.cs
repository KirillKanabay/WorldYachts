using System;

namespace WorldYachts.Helpers
{
    public class EntityContainer
    {
        private object Entity { get; set; }
        public bool IsEmpty => Entity == null;

        public void Push<TEntity>(TEntity entity) where TEntity : class
        {
            if (!IsEmpty)
            {
                throw new Exception("Container isn't empty");
            }
            Entity = entity;
        }

        public TEntity Pop<TEntity>() where TEntity : class
        {
            if (IsEmpty)
            {
                throw new NullReferenceException("Container is empty.");
            }

            if (!(Entity is TEntity tempEntity))
            {
                throw new InvalidCastException("Wrong entity type.");
            }
                
            Entity = null;
            return tempEntity;
        }
    }
}
