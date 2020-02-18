using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Base
{
    public abstract class Entity<T>
    {
        T _Id;
        public virtual T Id
        {
            get => _Id;
            protected set => _Id = value;
        }
    }
}
