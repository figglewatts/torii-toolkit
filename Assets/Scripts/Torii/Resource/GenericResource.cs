using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Resource
{
    public abstract class GenericResource
    {
        public ResourceLifespan Lifespan;

        protected GenericResource(ResourceLifespan lifespan)
        {
            Lifespan = lifespan;
        }
    }
}
