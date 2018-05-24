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
        private static readonly ResourceLifespans _lifespans;

        private static readonly string lifespansDataFileName = "resourcelifespans.json";

        static ResourceManager()
        {
            _resources = new Dictionary<string, GenericResource>();
            _handlers = new Dictionary<Type, IResourceHandler>();
            _lifespans = new ResourceLifespans();
        }

        public static void Initialize()
        {
            RegisterHandler(new SpriteHandler());
            RegisterHandler(new Texture2DHandler());

            loadLifespans();
        }

        public static void ClearLifespan(string span)
        {
            int spanID = _lifespans[span];

            List<string> toRemove = new List<string>();
            foreach (var res in _resources)
            {
                if (res.Value.Lifespan == spanID)
                {
                    toRemove.Add(res.Key);
                }
            }

            foreach (string key in toRemove)
            {
                switch (_resources[key].ResourceType)
                {
                    case ResourceType.Streamed:
                        _resources.Remove(key);
                        break;
                    case ResourceType.Unity:
                        Resources.UnloadAsset((UnityEngine.Object)_resources[key].GetData());
                        _resources.Remove(key);
                        break;
                }
                
            }
        }

        public static void ClearUnusedUnityAssets()
        {
            Resources.UnloadUnusedAssets();
        }

        public static T Load<T>(string path)
        {
            return Load<T>(path, _lifespans["global"]);
        }

        public static T Load<T>(string path, string span)
        {
            return Load<T>(path, _lifespans[span]);
        }

        public static T Load<T>(string path, int span)
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

        public static T UnityLoad<T>(string path) where T : UnityEngine.Object
        {
            return UnityLoad<T>(path, _lifespans["global"]);
        }

        public static T UnityLoad<T>(string path, string span) where T : UnityEngine.Object
        {
            return UnityLoad<T>(path, _lifespans[span]);
        }

        public static T UnityLoad<T>(string path, int span) where T : UnityEngine.Object
        {
            Resource<T> res;
            if (checkCache(path, out res)) return res.Data;

            // add it to the cache if it didn't already exist
            res = new Resource<T>(span, ResourceType.Unity)
            {
                Data = Resources.Load<T>(path)
            };
            string resourcePath = PathUtil.Combine("Resources", path);
            _resources[resourcePath] = res;

            return Resources.Load<T>(path);
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

        private static void loadLifespans()
        {
            string lifespanPath = PathUtil.Combine(Application.streamingAssetsPath,
                ToriiToolkit.StreamingAssetsDataDirectory, lifespansDataFileName);
            JSONArray lifespanArray = JSONUtil.ReadJSONFromDisk(lifespanPath).AsArray;

            foreach (JSONString lifespan in lifespanArray)
            {
                _lifespans.CreateLifespan(lifespan);
            }
        }
    }
}
