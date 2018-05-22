using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Resource
{
    public interface IResourceHandler
    {
        Type GetResourceType();

        void Load(string path, ResourceLifespan span);
    }
}
