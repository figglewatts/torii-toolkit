using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using Torii.Exceptions;
using Torii.Resource.Handlers;
using Torii.UI;
using Torii.Util;
using UnityEngine;

namespace Torii.Resource
{
    public static class ResourceManager
    {
        private static readonly Dictionary<string, GenericResource> _resources;
        private static readonly Dictionary<Type, IResourceHandler> _handlers;

        static ResourceManager()
        {
            _resources = new Dictionary<string, GenericResource>();
            _handlers = new Dictionary<Type, IResourceHandler>();
        }

        public static void Initialize()
        {
            RegisterHandler(new SpriteHandler());
            RegisterHandler(new Texture2DHandler());
        }

        // TODO: user configurable resource lifespans?

        public static void ClearLifespan(ResourceLifespan span)
        {
            List<string> toRemove = new List<string>();
            foreach (var res in _resources)
            {
                if (res.Value.Lifespan == span)
                {
                    toRemove.Add(res.Key);
                }
            }

            foreach (string key in toRemove)
            {
                _resources.Remove(key);
            }
        }

        public static T Load<T>(string path, ResourceLifespan span = ResourceLifespan.Global)
        {
            Resource<T> res;
            if (checkCache(path, out res)) return res.Data;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Could not load resource: File '" + path + "' not found", path);
            }

            if (!_handlers.ContainsKey(typeof(T)))
            {
                throw new ArgumentException("Could not load resource: No handler found for type " + typeof(T));
            }

            _handlers[typeof(T)].Load(path, span);

            return ((Resource<T>)_resources[path]).Data;
        }

        public static void RegisterResource(string path, GenericResource r)
        {
            _resources[path] = r;
        }

        public static void RegisterHandler(IResourceHandler handler)
        {
            _handlers[handler.GetResourceType()] = handler;
        }

        private static bool checkCache<T>(string path, out T data) where T : class
        {
            if (_resources.ContainsKey(path))
            {
                data = _resources[path] as T;
                return true;
            }
            data = null;
            return false;
        }
    }
}
