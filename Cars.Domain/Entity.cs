using System;

namespace Cars.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            if (CreateDate == DateTime.MinValue)
            {
                CreateDate = DateTime.Now;
            }
        }

        public int Id { get; protected set; }
        public DateTime CreateDate { get; protected set; }
    }
}
