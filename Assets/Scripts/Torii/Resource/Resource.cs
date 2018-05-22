using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Resource
{
    public class Resource<T> : GenericResource
    {
        public T Data;
        public Resource(T data, ResourceLifespan lifespan = ResourceLifespan.Global) : base(lifespan)
        {
            Data = data;
        }

        public Resource(ResourceLifespan lifespan = ResourceLifespan.Global) : base(lifespan)
        {
            // intentionally empty
        }
    }
}
