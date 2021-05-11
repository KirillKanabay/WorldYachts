using System;

namespace WorldYachts.Helpers
{
    public static class EntityContainer
    {
        private static object Entity { get; set; }
        public static bool IsEmpty => Entity == null;

        public static void Push<TEntity>(TEntity entity) where TEntity : class
        {
            Entity = entity;
        }

        public static TEntity Pop<TEntity>() where TEntity : class
        {
            if (!(Entity is TEntity tempEntity))
            {
                throw new InvalidCastException("Wrong entity type.");
            }
                
            Entity = null;
            return tempEntity;
        }
    }
}
