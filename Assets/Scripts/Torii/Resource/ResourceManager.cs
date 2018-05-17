using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using Torii.Exceptions;
using Torii.UI;
using Torii.Util;
using UnityEngine;

namespace Torii.Resource
{
    public enum ResourceLifespan
    {
        Scene,
        Global
    }

    public class GenericResource
    {
        public ResourceLifespan Lifespan;

        public GenericResource(ResourceLifespan lifespan)
        {
            Lifespan = lifespan;
        }
    }

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

    public static class ResourceManager
    {
        private static readonly Dictionary<string, GenericResource> _resources;

        static ResourceManager()
        {
            _resources = new Dictionary<string, GenericResource>();
        }

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

        public static bool CheckCache<T>(string path, out T data) where T : class
        {
            if (_resources.ContainsKey(path))
            {
                data = _resources[path] as T;
                return true;
            }
            data = null;
            return false;
        }

        public static T Load<T>(string path, ResourceLifespan span = ResourceLifespan.Global)
        {
            Resource<T> res;
            if (CheckCache(path, out res)) return res.Data;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Could not load resource: File '" + path + "' not found", path);
            }

            if (typeof(T) == typeof(Texture2D))
            {
                loadTexture2D(path, span);
            }
            else if (typeof(T) == typeof(Sprite))
            {
                loadSprite(path, span);
            }
            else
            {
                throw new ArgumentException("Could not load resource: No handler found for type " + typeof(T));
            }

            return ((Resource<T>)_resources[path]).Data;
        }

        private static void loadTexture2D(string path, ResourceLifespan span)
        {
            byte[] fileData = File.ReadAllBytes(path);

            // size of 2,2 is arbitrary, it gets resized next line anyway
            var tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            tex.LoadImage(fileData);

            Resource<Texture2D> resource = new Resource<Texture2D>(tex, span);
            _resources[path] = resource;
        }

        private static void loadSprite(string path, ResourceLifespan span)
        {
            JSONNode sprite = JSONUtil.ReadJSONFromDisk(path);
            string type = sprite["type"].Value;
            if (!sprite["type"].Value.Equals("sprite"))
            {
                throw new ResourceLoadException(
                    "Could not load resource: JSON type '" + sprite["type"] + "' did not match 'sprite'",
                    typeof(Sprite));
            }
            string textureValue = sprite["texture"];
            JSONObject rectValue = sprite["rect"].AsObject;
            JSONObject pivotValue = sprite["pivot"].AsObject;
            float ppuValue = sprite["ppu"];
            JSONObject borderValue = sprite["border"].AsObject;

            Texture2D texture = ResourceManager.Load<Texture2D>(TUI.UIDirectory + "/" + textureValue, span);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            if (rectValue.Count != 0)
            {
                rect = rectValue;
            }
            Vector2 pivot = new Vector2(texture.width / 2f, texture.height / 2f);
            if (pivotValue.Count != 0)
            {
                pivot = pivotValue;
            }
            Vector4 border = Vector4.zero;
            if (borderValue.Count != 0)
            {
                border = borderValue;
            }

            Sprite s = Sprite.Create(texture, rect, pivot, ppuValue, 0, SpriteMeshType.FullRect, border);
            Resource<Sprite> resource = new Resource<Sprite>(s, span);
            _resources[path] = resource;
        }
    }
}
