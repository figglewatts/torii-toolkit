using System;
using System.Collections;
using System.Collections.Generic;

namespace Torii.Exceptions
{
    public class ResourceLoadException : Exception
    {
        public Type ResourceType { get; private set; }

        public ResourceLoadException(Type resourceType)
        {
            ResourceType = resourceType;
        }

        public ResourceLoadException(string message, Type resourceType) : base(message)
        {
            ResourceType = resourceType;
        }

        public ResourceLoadException(string message, Exception innerException, Type resourceType) : base(message, innerException)
        {
            ResourceType = resourceType;
        }
    }
}